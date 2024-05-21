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
            migrationBuilder.AlterColumn<int>(
                name: "SentenceSerial",
                table: "sentences",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d7fcc16f-c5a5-44e7-93e2-25baad996fd9", "AQAAAAIAAYagAAAAEABu9Pvjt0npuv0pyAy5VFs3zVvaMqXBOR233CtELb1LsAc5STdaCm2cKJJyqQ89Rw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SentenceSerial",
                table: "sentences",
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
                values: new object[] { "58822dd7-7eb3-4e3a-9ee5-1bc0f67d6d8c", "AQAAAAIAAYagAAAAEDHeymj+swVbXatPu+riymb/fyn9qcAAp3hdcI0OJtvzX+DBHbQvpp+J9OQqd70ohg==" });
        }
    }
}
