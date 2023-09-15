using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_ComponentesCodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class migracionConDescripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Facturas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Facturas");
        }
    }
}
