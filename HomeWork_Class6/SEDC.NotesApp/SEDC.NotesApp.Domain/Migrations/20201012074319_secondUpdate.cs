using Microsoft.EntityFrameworkCore.Migrations;

namespace SEDC.NotesApp.Domain.Migrations
{
    public partial class secondUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "FirstName", "LastName", "Username" },
                values: new object[] { 1, "Adress1", "Mark", "Smith", "MSmith" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "FirstName", "LastName", "Username" },
                values: new object[] { 2, "Adress1", "Ema", "Smith", "ESmith" });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Color", "TagId", "Text", "UserId" },
                values: new object[] { 2, "Yellow", 2, "Be focused", 1 });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Color", "TagId", "Text", "UserId" },
                values: new object[] { 1, "Red", 1, "Stop drinking", 2 });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Color", "TagId", "Text", "UserId" },
                values: new object[] { 3, "Yellow", 1, "Drink lemon juice", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
