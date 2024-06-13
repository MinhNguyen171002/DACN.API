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
            migrationBuilder.CreateTable(
                name: "topic",
                columns: table => new
                {
                    TopicId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopicName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserID = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topic", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_topic_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vocabularies",
                columns: table => new
                {
                    VocabularyId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vocabulary = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopicId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserID = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vocabularies", x => x.VocabularyId);
                    table.ForeignKey(
                        name: "FK_vocabularies_topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_vocabularies_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "669408c1-293d-48c4-8410-8d54605d6f86", "AQAAAAIAAYagAAAAELD9OqfD5sa8wDBUrt9uR7yiPJ5qWkIVIMpnmI9hRjr4EDxKp2YmpQJ+tNXSqXiChA==" });

            migrationBuilder.CreateIndex(
                name: "IX_topic_UserID",
                table: "topic",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_vocabularies_TopicId",
                table: "vocabularies",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_vocabularies_UserID",
                table: "vocabularies",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vocabularies");

            migrationBuilder.DropTable(
                name: "topic");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ae2e45fb-3cb7-4fba-8b17-6d3841d877eb", "AQAAAAIAAYagAAAAEMWrYvevK4Aup1dsXqrSnCRKgFtWFbcrPIjj2LjsuFdaJRyZtqZK7x8MKngwLmnU4Q==" });
        }
    }
}
