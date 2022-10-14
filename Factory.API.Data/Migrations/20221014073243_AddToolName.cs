using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factory.API.Data.Migrations
{
    public partial class AddToolName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Klucz płaski 13 (Stal nierdzewna 9%)");

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Klucz płaski 13 (Stal nierdzewna 8%)");

            migrationBuilder.UpdateData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Klucz płaski 13 (Stal nierdzewna wióry)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tools");
        }
    }
}
