using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ejercicio_ordenadores.Migrations
{
    /// <inheritdoc />
    public partial class nuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Ordenadores",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Ordenadores", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Componentes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TipoComponente = table.Column<int>(type: "int", nullable: false),
            //        Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Serie = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Calor = table.Column<int>(type: "int", nullable: false),
            //        Megas = table.Column<long>(type: "bigint", nullable: false),
            //        Cores = table.Column<int>(type: "int", nullable: false),
            //        Coste = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        OrdenadorId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Componentes", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Componentes_Ordenadores_OrdenadorId",
            //            column: x => x.OrdenadorId,
            //            principalTable: "Ordenadores",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Componentes_OrdenadorId",
            //    table: "Componentes",
            //    column: "OrdenadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Componentes");

            //migrationBuilder.DropTable(
            //    name: "Ordenadores");
        }
    }
}
