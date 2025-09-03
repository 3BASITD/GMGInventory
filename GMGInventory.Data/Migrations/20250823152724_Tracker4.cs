using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackerTasks_AspNetUsers_AssignedToId",
                table: "TrackerTasks");

            migrationBuilder.DropTable(
                name: "TrackerAttachments");

            migrationBuilder.DropTable(
                name: "TrackerErrors");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "TrackerHistories");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "TrackerHistories");

            migrationBuilder.RenameColumn(
                name: "AssignedToId",
                table: "TrackerTasks",
                newName: "AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_TrackerTasks_AssignedToId",
                table: "TrackerTasks",
                newName: "IX_TrackerTasks_AssigneeId");

            migrationBuilder.CreateTable(
                name: "TrackerIssues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackerIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackerIssues_TrackerTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TrackerTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8115eaa3-c428-448a-ad6a-480c238d6dec", "AQAAAAIAAYagAAAAEHP5KIaGsXoZACvBV+eHGbCiLqAW7Z6nDtzB9OpzIc6FXoUvqcL2af6nARpYOA91Ug==", "6dc5b8e2-e62b-4a7c-9eb7-f575f6302fc6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93a622a5-2803-4b7a-8136-c6d8facdd236", "AQAAAAIAAYagAAAAEL8yBlc4yrpvb0n1AIM3WsPBFR0yGAyxBCzLpBEQtJid1PQ22SorG3r8gM1fT4W8QQ==", "c66c1097-f008-4baa-8b1e-84612a1f70b1" });

            migrationBuilder.CreateIndex(
                name: "IX_TrackerIssues_TaskId",
                table: "TrackerIssues",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackerTasks_AspNetUsers_AssigneeId",
                table: "TrackerTasks",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackerTasks_AspNetUsers_AssigneeId",
                table: "TrackerTasks");

            migrationBuilder.DropTable(
                name: "TrackerIssues");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "TrackerTasks",
                newName: "AssignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_TrackerTasks_AssigneeId",
                table: "TrackerTasks",
                newName: "IX_TrackerTasks_AssignedToId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "TrackerHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "TrackerHistories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrackerErrors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FixedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Severity = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackerErrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackerErrors_TrackerTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TrackerTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackerAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackerAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackerAttachments_TrackerComments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "TrackerComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrackerAttachments_TrackerErrors_ErrorId",
                        column: x => x.ErrorId,
                        principalTable: "TrackerErrors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrackerAttachments_TrackerTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TrackerTasks",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf7c36d5-9aa7-46da-9624-dcd44c7e1f28", "AQAAAAIAAYagAAAAEPFWbtq8LEhmJoOgZBdA42w1hrqOgarDWaLCpTwhavCFhZ9hYB/Db93Ji+LKhIP9dw==", "3afcc782-5224-4819-a4d6-35d6b1d46673" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e9fba1f-58eb-4772-b38d-afb61e661d14", "AQAAAAIAAYagAAAAEC7wedfLS7lr85ZVuWFYrX+K9OOHF2h/5FR/mbE7JsjVVrXJ1+kDvnshPjJ9vi8sEg==", "9c4fc9ba-d699-4c96-9712-1daa6628acda" });

            migrationBuilder.CreateIndex(
                name: "IX_TrackerAttachments_CommentId",
                table: "TrackerAttachments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackerAttachments_ErrorId",
                table: "TrackerAttachments",
                column: "ErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackerAttachments_TaskId",
                table: "TrackerAttachments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackerErrors_TaskId",
                table: "TrackerErrors",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackerTasks_AspNetUsers_AssignedToId",
                table: "TrackerTasks",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
