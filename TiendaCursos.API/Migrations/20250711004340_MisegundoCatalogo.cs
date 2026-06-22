using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaCursos.API.Migrations
{
    /// <inheritdoc />
    public partial class MisegundoCatalogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unidad",
                table: "Platillos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unidad",
                table: "Platillos");
        }
    }
}
