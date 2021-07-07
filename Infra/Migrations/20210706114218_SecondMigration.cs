using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("02e34f4c-44dd-4fd5-bb51-ce16a275c015"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDate", "Email", "Name", "PersonalDocument", "Phone", "RemovedAt" },
                values: new object[] { new Guid("f7777df5-0c96-41c1-b0d3-e4a1d5ed8fce"), "01/01/2000", "usertest@gmail.com", "User Test", "24068108013", "47992345671", null });

            migrationBuilder.InsertData(
                table: "Adress",
                columns: new[] { "Id", "City", "District", "Line1", "Line2", "Number", "PostalCode", "Principal", "State", "UserId" },
                values: new object[] { new Guid("1c8fd5c0-c01f-4d4e-9136-888503c60942"), "City", "District", "line1", "line2", 100, "89055050", true, "SC", new Guid("f7777df5-0c96-41c1-b0d3-e4a1d5ed8fce") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Adress",
                keyColumn: "Id",
                keyValue: new Guid("1c8fd5c0-c01f-4d4e-9136-888503c60942"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("f7777df5-0c96-41c1-b0d3-e4a1d5ed8fce"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDate", "Email", "Name", "PersonalDocument", "Phone", "RemovedAt" },
                values: new object[] { new Guid("02e34f4c-44dd-4fd5-bb51-ce16a275c015"), "01/01/2000", "usertest@gmail.com", "User Test", "24068108013", "47992345671", null });
        }
    }
}
