using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WS.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Vencimento",
                table: "AspNetClient",
                type: "Date",
                nullable: true,
                defaultValue: new DateTime(2025, 10, 27, 11, 18, 47, 685, DateTimeKind.Local).AddTicks(3099),
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 10, 27, 10, 4, 58, 750, DateTimeKind.Local).AddTicks(8341));

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetMenu_ModuleId",
                table: "AspNetMenu",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetMenu");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Vencimento",
                table: "AspNetClient",
                type: "Date",
                nullable: true,
                defaultValue: new DateTime(2025, 10, 27, 10, 4, 58, 750, DateTimeKind.Local).AddTicks(8341),
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 10, 27, 11, 18, 47, 685, DateTimeKind.Local).AddTicks(3099));
        }
    }
}
