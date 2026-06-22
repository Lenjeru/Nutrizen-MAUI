using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaCursos.API.Migrations
{
    /// <inheritdoc />
    public partial class PedidoTemporal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidoTemporal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlatilloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoTemporal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoTemporal_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PedidoTemporal_Platillos_PlatilloId",
                        column: x => x.PlatilloId,
                        principalTable: "Platillos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTemporal_PlatilloId",
                table: "PedidoTemporal",
                column: "PlatilloId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoTemporal_UsuarioId",
                table: "PedidoTemporal",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoTemporal");
        }
    }
}
