using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class _04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "sentenceCompletes");

            migrationBuilder.DropColumn(
                name: "CorrectDescription",
                table: "questions");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionSerial",
                table: "questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ef20ced9-d296-4998-bdb9-ae6625f2943e", "AQAAAAIAAYagAAAAEGe3RYYPwZMzBQ59JbgU08Rg81IANhCgDwHj1cXW3jCszNcGSRNUVAS+v/pqYuEATA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Result",
                table: "sentenceCompletes",
                type: "double",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionSerial",
                table: "questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                values: new object[] { "c3a88e15-0773-40b2-82b4-8ec650a47351", "AQAAAAIAAYagAAAAECHIkGQzr7Hvz0l5swgmFG0dmq3VdoCe9f1qUU0sSzAhBPgmcrRuagZb3UtUV0r9OQ==" });
        }
    }
}
