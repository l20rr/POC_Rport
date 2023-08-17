using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportApp.API.Migrations
{
    /// <inheritdoc />
    public partial class report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    BugReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.BugReportId);
                    table.ForeignKey(
                        name: "FK_Bugs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ranking = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: true),
                    Question1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Question2 = table.Column<int>(type: "int", nullable: false),
                    Question3 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BugReportId = table.Column<int>(type: "int", nullable: false),
                    FeedbackId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_Attachment_Bugs_BugReportId",
                        column: x => x.BugReportId,
                        principalTable: "Bugs",
                        principalColumn: "BugReportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "FeedbackId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "UserName" },
                values: new object[] { 1, "teste@gadsakm.com", "Lucas" });

            migrationBuilder.InsertData(
                table: "Bugs",
                columns: new[] { "BugReportId", "AttachmentId", "Description", "Timestamp", "UserId" },
                values: new object[] { 1, 1, "Application crashes when clicking on button X", new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "FeedbackId", "AttachmentId", "Comments", "Question1", "Question2", "Question3", "Ranking", "Timestamp", "UserId" },
                values: new object[] { 1, 1, "Great job!", "Friend's recommendation", 4, true, 2, new DateTime(2023, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Attachment",
                columns: new[] { "AttachmentId", "BugReportId", "FeedbackId", "FileName", "FilePath" },
                values: new object[] { 1, 1, 1, "File1", "http://api.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_BugReportId",
                table: "Attachment",
                column: "BugReportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_FeedbackId",
                table: "Attachment",
                column: "FeedbackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_UserId",
                table: "Bugs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
