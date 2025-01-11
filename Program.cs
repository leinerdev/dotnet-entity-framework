using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;
using projectef.Models;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", ([FromServices] TareasContext dbContext) =>
{
  dbContext.Database.EnsureCreated();
  return Results.Ok("Conexión a la base de datos en memoria establecida: " + dbContext.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
  return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
  tarea.TareaId = Guid.NewGuid();
  tarea.FechaCreacion = DateTime.Now;
  await dbContext.AddAsync(tarea);
  // await dbContext.Tareas.AddAsync(tarea);
  await dbContext.SaveChangesAsync();
  return Results.Ok();
});

app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) =>
{
  var tareaActual = dbContext.Tareas.Find(id);
  if (tareaActual == null)
  {
    return Results.NotFound();
  }

  tareaActual.CategoriaId = tarea.CategoriaId;
  tareaActual.Titulo = tarea.Titulo;
  tareaActual.Descripcion = tarea.Descripcion;
  tareaActual.PrioridadTarea = tarea.PrioridadTarea;

  await dbContext.SaveChangesAsync();
  return Results.Ok();
});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, Guid id) =>
{
  var tarea = dbContext.Tareas.Find(id);
  if (tarea == null)
  {
    return Results.NotFound();
  }

  dbContext.Tareas.Remove(tarea);
  await dbContext.SaveChangesAsync();
  return Results.Ok();
});

await app.RunAsync();
