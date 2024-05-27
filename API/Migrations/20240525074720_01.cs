using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class _01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "QuestionSerial",
                table: "questionCompletes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "476a7139-1858-4c3e-94a8-d9ad788a2995", "AQAAAAIAAYagAAAAEEzMwsRZoLhI0C9gXrQTIeeg4owQrslxo1A5+7oiRjYIJSUWF3iVftpypks/IRbnMA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "QuestionSerial",
                table: "questionCompletes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2916b412-259b-4948-869d-7478051ae7f4", "AQAAAAIAAYagAAAAED9zi7my9KBYAOJ9qVNo/6imLw6r8eXRGs0/jUfG7lWAESujRs8jB2+OJ5plbVe/cA==" });
        }
    }
}
