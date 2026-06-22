using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaCursos.API.Migrations
{
    /// <inheritdoc />
    public partial class MiprimerCatalogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Programa",
                table: "Cursos",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Platillos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Ingredientes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Calorias = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platillos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Platillos_Id",
                table: "Platillos",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Platillos");

            migrationBuilder.AlterColumn<string>(
                name: "Programa",
                table: "Cursos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);
        }
    }
}
