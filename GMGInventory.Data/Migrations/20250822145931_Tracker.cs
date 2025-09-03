using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "TrackerAttachments");

            //migrationBuilder.DropTable(
            //    name: "TrackerHistories");

            //migrationBuilder.DropTable(
            //    name: "TrackerComments");

            //migrationBuilder.DropTable(
            //    name: "TrackerErrors");

            //migrationBuilder.DropTable(
            //    name: "TrackerTasks");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "35f6f24e-65aa-41e2-be5d-a1d77675ee10", "AQAAAAIAAYagAAAAEHWgA13MRSBZz9HnZeUDd12dM/kehoI2q4GeZDBEe9/vCw5Hzv650ZvN5dIa0dXZfg==", "ac898afa-f066-416c-938a-542872ea04d8" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "46920cd2-0d7d-49e0-ad0c-0e1d8c4e9ec7", "AQAAAAIAAYagAAAAEAAW4qtutKY/UYVT2+hsKacTdNyi0Glr7pg2FeRSLelpEcPDoK9WPaFnShGcQCtDsQ==", "2e617f7d-ce68-4047-83a4-b6c28ed261a1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackerTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedToId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackerTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackerTasks_AspNetUsers_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackerComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackerComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackerComments_TrackerTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TrackerTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "TrackerHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NewPriority = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    NewStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    OldPriority = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    OldStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackerHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackerHistories_TrackerTasks_TaskId",
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
                values: new object[] { "0843c7a7-438f-450a-b503-1b7c2ed31716", "AQAAAAIAAYagAAAAEG9c4n4d12L3J5Sn7xG2FthvIvdRNSXrH67RqZRZOnUMScKX1PTEUShMgauJ7tartQ==", "c6a11a67-6c93-449e-bdce-a4d9536fad46" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a25620c-11a6-42e0-b86e-5c2c0ca04b6d", "AQAAAAIAAYagAAAAEDVco+A09DQmCGtSQhpSY44yeE/c/LBTkwKlZrR63JPYHRyr/GMc1VJb6BakJVQN8w==", "0baaf8c1-a59d-4e77-b824-a263985f2293" });

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
                name: "IX_TrackerComments_TaskId",
                table: "TrackerComments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackerErrors_TaskId",
                table: "TrackerErrors",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackerHistories_TaskId",
                table: "TrackerHistories",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackerTasks_AssignedToId",
                table: "TrackerTasks",
                column: "AssignedToId");
        }
    }
}
