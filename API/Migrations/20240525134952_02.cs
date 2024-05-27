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
            migrationBuilder.DropForeignKey(
                name: "FK_questionCompletes_sentenceCompletes_sencomSentenceID",
                table: "questionCompletes");

            migrationBuilder.RenameColumn(
                name: "sencomSentenceID",
                table: "questionCompletes",
                newName: "SentenceId");

            migrationBuilder.RenameIndex(
                name: "IX_questionCompletes_sencomSentenceID",
                table: "questionCompletes",
                newName: "IX_questionCompletes_SentenceId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "79f2bf13-43d4-4184-89e4-a5cd15987245", "AQAAAAIAAYagAAAAEF2KRzo9EPxu97m7shv+DHzFS8QgYdXhDcwX4wvKdS8d/lwJw0TpJBn5/nuY2znuLg==" });

            migrationBuilder.AddForeignKey(
                name: "FK_questionCompletes_sentences_SentenceId",
                table: "questionCompletes",
                column: "SentenceId",
                principalTable: "sentences",
                principalColumn: "SentenceId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questionCompletes_sentences_SentenceId",
                table: "questionCompletes");

            migrationBuilder.RenameColumn(
                name: "SentenceId",
                table: "questionCompletes",
                newName: "sencomSentenceID");

            migrationBuilder.RenameIndex(
                name: "IX_questionCompletes_SentenceId",
                table: "questionCompletes",
                newName: "IX_questionCompletes_sencomSentenceID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "476a7139-1858-4c3e-94a8-d9ad788a2995", "AQAAAAIAAYagAAAAEEzMwsRZoLhI0C9gXrQTIeeg4owQrslxo1A5+7oiRjYIJSUWF3iVftpypks/IRbnMA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_questionCompletes_sentenceCompletes_sencomSentenceID",
                table: "questionCompletes",
                column: "sencomSentenceID",
                principalTable: "sentenceCompletes",
                principalColumn: "SentenceID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
