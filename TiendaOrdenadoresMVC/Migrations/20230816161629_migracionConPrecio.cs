using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ComponentesCodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class migracionConPrecio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Ordenadores",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Facturas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Ordenadores");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Facturas");
        }
    }
}
