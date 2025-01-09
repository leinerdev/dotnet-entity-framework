using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using projectef.Models;

namespace projectef;

public class TareasContext : DbContext
{
  public DbSet<Categoria> Categorias { get; set; }
  public DbSet<Tarea> Tareas { get; set; }

  public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    List<Categoria> categoriasInit = new List<Categoria>();
    categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("83c8247b-34bf-4e95-be12-a279b9ec7b9c"), Nombre = "Actividades pendientes", Peso = 20 });
    categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("f3c8247b-34bf-4e95-be12-a279b9ec7b02"), Nombre = "Actividades personales", Peso = 50 });

    modelBuilder.Entity<Categoria>(categoria =>
    {
      categoria.ToTable("Categoria");
      categoria.HasKey(p => p.CategoriaId);
      categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
      categoria.Property(p => p.Descripcion).IsRequired(false);
      categoria.Property(p => p.Peso);
      categoria.HasData(categoriasInit);
    });

    List<Tarea> tareasInit = new List<Tarea>();
    tareasInit.Add(new Tarea() { TareaId = Guid.Parse("0144e6ed-3453-4031-8e1a-32a71a3a4fa5"), CategoriaId = Guid.Parse("83c8247b-34bf-4e95-be12-a279b9ec7b9c"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de servicios públicos", FechaCreacion = DateTime.Now, Descripcion = "Pagar el recibo de la luz y el agua" });
    tareasInit.Add(new Tarea() { TareaId = Guid.Parse("0144e6ed-3453-4031-8e1a-32a71a3a4fa3"), CategoriaId = Guid.Parse("f3c8247b-34bf-4e95-be12-a279b9ec7b02"), PrioridadTarea = Prioridad.Baja, Titulo = "Terminar de ver pellícula en Netflix", FechaCreacion = DateTime.Now, Descripcion = "Pagar el recibo de la luz y el agua" });

    modelBuilder.Entity<Tarea>(tarea =>
    {
      tarea.ToTable("Tarea");
      tarea.HasKey(p => p.TareaId);
      tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
      tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
      tarea.Property(p => p.Descripcion);
      tarea.Property(p => p.PrioridadTarea);
      tarea.Property(p => p.FechaCreacion);
      tarea.Ignore(p => p.Resumen);
      tarea.HasData(tareasInit);
    });
  }
}