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
            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "files",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "files");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d7fcc16f-c5a5-44e7-93e2-25baad996fd9", "AQAAAAIAAYagAAAAEABu9Pvjt0npuv0pyAy5VFs3zVvaMqXBOR233CtELb1LsAc5STdaCm2cKJJyqQ89Rw==" });
        }
    }
}
