using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WS.Data.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetClient",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orgao = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Server = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Banco = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Vencimento = table.Column<DateTime>(type: "Date", nullable: true, defaultValue: new DateTime(2025, 11, 26, 11, 20, 53, 609, DateTimeKind.Local).AddTicks(816))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetClient", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetModule",
                columns: table => new
                {
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Module = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImgMenu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ordem = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetModule", x => x.ModuleId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetClientModule",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetClientModule", x => new { x.ClientId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_AspNetClientModule_AspNetClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetClient",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetClientModule_AspNetModule_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "AspNetModule",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetMenu",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    Menu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetMenu", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_AspNetMenu_AspNetModule_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "AspNetModule",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetClientMenu",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    Exibir = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Inserir = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Editar = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Excluir = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetClientMenu", x => new { x.ClientId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_AspNetClientMenu_AspNetClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetClient",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetClientMenu_AspNetMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "AspNetMenu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetClientMenu_MenuId",
                table: "AspNetClientMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetClientModule_ModuleId",
                table: "AspNetClientModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetMenu_ModuleId",
                table: "AspNetMenu",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetClientMenu");

            migrationBuilder.DropTable(
                name: "AspNetClientModule");

            migrationBuilder.DropTable(
                name: "AspNetMenu");

            migrationBuilder.DropTable(
                name: "AspNetClient");

            migrationBuilder.DropTable(
                name: "AspNetModule");
        }
    }
}
