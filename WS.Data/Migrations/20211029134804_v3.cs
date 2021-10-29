using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WS.Data.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Vencimento",
                table: "AspNetClient",
                type: "Date",
                nullable: true,
                defaultValue: new DateTime(2025, 10, 29, 10, 48, 4, 426, DateTimeKind.Local).AddTicks(2043),
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 10, 27, 11, 18, 47, 685, DateTimeKind.Local).AddTicks(3099));

            migrationBuilder.CreateTable(
                name: "AspNetClientMenu",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    View = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    New = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Edit = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Delete = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetClientMenu");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Vencimento",
                table: "AspNetClient",
                type: "Date",
                nullable: true,
                defaultValue: new DateTime(2025, 10, 27, 11, 18, 47, 685, DateTimeKind.Local).AddTicks(3099),
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 10, 29, 10, 48, 4, 426, DateTimeKind.Local).AddTicks(2043));
        }
    }
}
