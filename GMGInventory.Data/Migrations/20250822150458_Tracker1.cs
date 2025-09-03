using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackerTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    AssignedToId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    OldStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    NewStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    OldPriority = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    NewPriority = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                values: new object[] { "83744ab2-b298-4240-84d0-43ab7b2e6f75", "AQAAAAIAAYagAAAAEOmAp5EI1n14CsBqMpuqQVEYrzbCFBuGBF8v0Ir+qmX7dD2S8eaLG2o5pjVkwZ/4CQ==", "87230791-145b-491b-8fef-6a0acd8f6e28" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "415a228b-358d-416c-a712-04214316a03e", "AQAAAAIAAYagAAAAEPRoXaQ2lqxd7EzMtV2dOxmLbLMzGnymyyR9MOTnCx8SiD9OQ2I30jGdb/nh12FcUw==", "c1c377a7-fb27-4cf5-ad90-761516bd5d81" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackerAttachments");

            migrationBuilder.DropTable(
                name: "TrackerHistories");

            migrationBuilder.DropTable(
                name: "TrackerComments");

            migrationBuilder.DropTable(
                name: "TrackerErrors");

            migrationBuilder.DropTable(
                name: "TrackerTasks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35f6f24e-65aa-41e2-be5d-a1d77675ee10", "AQAAAAIAAYagAAAAEHWgA13MRSBZz9HnZeUDd12dM/kehoI2q4GeZDBEe9/vCw5Hzv650ZvN5dIa0dXZfg==", "ac898afa-f066-416c-938a-542872ea04d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46920cd2-0d7d-49e0-ad0c-0e1d8c4e9ec7", "AQAAAAIAAYagAAAAEAAW4qtutKY/UYVT2+hsKacTdNyi0Glr7pg2FeRSLelpEcPDoK9WPaFnShGcQCtDsQ==", "2e617f7d-ce68-4047-83a4-b6c28ed261a1" });
        }
    }
}
