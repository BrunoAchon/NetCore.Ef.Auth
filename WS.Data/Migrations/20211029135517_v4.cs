using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WS.Data.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "View",
                table: "AspNetClientMenu",
                newName: "Inserir");

            migrationBuilder.RenameColumn(
                name: "New",
                table: "AspNetClientMenu",
                newName: "Exibir");

            migrationBuilder.RenameColumn(
                name: "Edit",
                table: "AspNetClientMenu",
                newName: "Excluir");

            migrationBuilder.RenameColumn(
                name: "Delete",
                table: "AspNetClientMenu",
                newName: "Editar");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Vencimento",
                table: "AspNetClient",
                type: "Date",
                nullable: true,
                defaultValue: new DateTime(2025, 10, 29, 10, 55, 17, 266, DateTimeKind.Local).AddTicks(206),
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 10, 29, 10, 48, 4, 426, DateTimeKind.Local).AddTicks(2043));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Inserir",
                table: "AspNetClientMenu",
                newName: "View");

            migrationBuilder.RenameColumn(
                name: "Exibir",
                table: "AspNetClientMenu",
                newName: "New");

            migrationBuilder.RenameColumn(
                name: "Excluir",
                table: "AspNetClientMenu",
                newName: "Edit");

            migrationBuilder.RenameColumn(
                name: "Editar",
                table: "AspNetClientMenu",
                newName: "Delete");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Vencimento",
                table: "AspNetClient",
                type: "Date",
                nullable: true,
                defaultValue: new DateTime(2025, 10, 29, 10, 48, 4, 426, DateTimeKind.Local).AddTicks(2043),
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 10, 29, 10, 55, 17, 266, DateTimeKind.Local).AddTicks(206));
        }
    }
}
