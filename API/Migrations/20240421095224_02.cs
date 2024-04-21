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
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ad376a8f-9eab-4bb9-9fca-30b01540f445", "a18be9c0-aa65-4af8-bd17-00bd9344e575" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f-9eab-4bb9-9fca-30b01540f445");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575");

            migrationBuilder.DropColumn(
                name: "CorrectPercent",
                table: "practiceCompletes");

            migrationBuilder.RenameColumn(
                name: "CorrectCount",
                table: "practiceCompletes",
                newName: "PracticeID");

            migrationBuilder.CreateIndex(
                name: "IX_practiceCompletes_PracticeID",
                table: "practiceCompletes",
                column: "PracticeID");

            migrationBuilder.AddForeignKey(
                name: "FK_practiceCompletes_practices_PracticeID",
                table: "practiceCompletes",
                column: "PracticeID",
                principalTable: "practices",
                principalColumn: "PracticeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_practiceCompletes_practices_PracticeID",
                table: "practiceCompletes");

            migrationBuilder.DropIndex(
                name: "IX_practiceCompletes_PracticeID",
                table: "practiceCompletes");

            migrationBuilder.RenameColumn(
                name: "PracticeID",
                table: "practiceCompletes",
                newName: "CorrectCount");

            migrationBuilder.AddColumn<decimal>(
                name: "CorrectPercent",
                table: "practiceCompletes",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ad376a8f-9eab-4bb9-9fca-30b01540f445", null, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", 0, "f7967ca6-c04d-4c95-a4a0-9ab7e6142075", "admin@gmail.com", false, false, null, "admin@gmail.com", "admin", "AQAAAAIAAYagAAAAEMibKmrporQsmXPhlYe8wFiyeRBQUsftdfodPelHd4duppo2w7Vq1cZiSEiDS2ho8Q==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ad376a8f-9eab-4bb9-9fca-30b01540f445", "a18be9c0-aa65-4af8-bd17-00bd9344e575" });
        }
    }
}
