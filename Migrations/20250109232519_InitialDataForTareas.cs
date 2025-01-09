using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace projectef.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataForTareas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("83c8247b-34bf-4e95-be12-a279b9ec7b9c"), null, "Actividades pendientes", 20 },
                    { new Guid("f3c8247b-34bf-4e95-be12-a279b9ec7b02"), null, "Actividades personales", 50 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("0144e6ed-3453-4031-8e1a-32a71a3a4fa3"), new Guid("f3c8247b-34bf-4e95-be12-a279b9ec7b02"), "Pagar el recibo de la luz y el agua", new DateTime(2025, 1, 9, 18, 25, 19, 379, DateTimeKind.Local).AddTicks(4268), 0, "Terminar de ver pellícula en Netflix" },
                    { new Guid("0144e6ed-3453-4031-8e1a-32a71a3a4fa5"), new Guid("83c8247b-34bf-4e95-be12-a279b9ec7b9c"), "Pagar el recibo de la luz y el agua", new DateTime(2025, 1, 9, 18, 25, 19, 378, DateTimeKind.Local).AddTicks(3174), 1, "Pago de servicios públicos" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("0144e6ed-3453-4031-8e1a-32a71a3a4fa3"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("0144e6ed-3453-4031-8e1a-32a71a3a4fa5"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("83c8247b-34bf-4e95-be12-a279b9ec7b9c"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("f3c8247b-34bf-4e95-be12-a279b9ec7b02"));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
