using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class _03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sentence",
                table: "questionCompletes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cdd31766-d8ae-4f0a-9bac-f14b1e8a0b2f", "AQAAAAIAAYagAAAAEFB6B7ITzRqJcM7aAYGhH6IoWBHfoszoNE5CgQmQMoRQ4+9DwRdt6VvEuDKVxGzDwQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sentence",
                table: "questionCompletes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "79d76083-92f3-4002-ad05-0f071f0c9eb8", "AQAAAAIAAYagAAAAEAY8pOD9kMSPzsX5xtHbO5VqcfpHJg+yfDmxWhCWdocpRBbOCV9Dq7pWTM36r5oPmw==" });
        }
    }
}
