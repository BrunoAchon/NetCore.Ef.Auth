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
                    Vencimento = table.Column<DateTime>(type: "Date", nullable: true, defaultValue: new DateTime(2025, 10, 27, 10, 4, 58, 750, DateTimeKind.Local).AddTicks(8341))
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetClientModule_ModuleId",
                table: "AspNetClientModule",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetClientModule");

            migrationBuilder.DropTable(
                name: "AspNetClient");

            migrationBuilder.DropTable(
                name: "AspNetModule");
        }
    }
}
