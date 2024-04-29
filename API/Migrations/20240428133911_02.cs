using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class _02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectDescription",
                table: "questionCompletes");

            migrationBuilder.AddColumn<string>(
                name: "CorrectDescription",
                table: "questions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "79d76083-92f3-4002-ad05-0f071f0c9eb8", "AQAAAAIAAYagAAAAEAY8pOD9kMSPzsX5xtHbO5VqcfpHJg+yfDmxWhCWdocpRBbOCV9Dq7pWTM36r5oPmw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectDescription",
                table: "questions");

            migrationBuilder.AddColumn<string>(
                name: "CorrectDescription",
                table: "questionCompletes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e038a6fa-3a6c-452c-b1f6-0d09f142078e", "AQAAAAIAAYagAAAAEKGCxL/FAFC05Y15NbVJ0ULUN5zupWeua1W4WasxY0h4zHoEbX09UoYRop2retIvKQ==" });
        }
    }
}
