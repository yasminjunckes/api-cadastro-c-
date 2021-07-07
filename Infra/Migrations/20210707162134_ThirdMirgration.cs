using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class ThirdMirgration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adress");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Line1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Line2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Number = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    District = table.Column<string>(type: "text", nullable: true),
                    Principal = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "District", "Line1", "Line2", "Number", "PostalCode", "Principal", "State", "UserId" },
                values: new object[] { new Guid("871b1fee-e901-44e8-a78d-783463c93a76"), "City", "District", "line1", "line2", 100, "89055050", true, "SC", new Guid("f7777df5-0c96-41c1-b0d3-e4a1d5ed8fce") });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.CreateTable(
                name: "Adress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
                    District = table.Column<string>(type: "text", nullable: true),
                    Line1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Line2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Number = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    Principal = table.Column<bool>(type: "boolean", nullable: false),
                    State = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adress_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Adress",
                columns: new[] { "Id", "City", "District", "Line1", "Line2", "Number", "PostalCode", "Principal", "State", "UserId" },
                values: new object[] { new Guid("1c8fd5c0-c01f-4d4e-9136-888503c60942"), "City", "District", "line1", "line2", 100, "89055050", true, "State", new Guid("f7777df5-0c96-41c1-b0d3-e4a1d5ed8fce") });

            migrationBuilder.CreateIndex(
                name: "IX_Adress_UserId",
                table: "Adress",
                column: "UserId");
        }
    }
}
