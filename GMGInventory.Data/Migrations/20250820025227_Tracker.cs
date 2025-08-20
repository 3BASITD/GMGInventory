using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackerTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    OldStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldPriority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewPriority = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    ErrorId = table.Column<int>(type: "int", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "56e4333e-3bcc-48f3-b7f0-f50c00311c37", "AQAAAAIAAYagAAAAEOPwT8q0InIE7iM+imKt0CPV+6DkVC42zqcqiUVqVRDU+jD8CT2Fkeq2htFCSKolYQ==", "0c17d005-aac5-44b7-b73d-78deca5d0ea3" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "a730f06a-182a-4398-a5d2-d9fd58ade167", "AQAAAAIAAYagAAAAEH9ztP9S9KPiFz3zuZwueCUw1//mq2lh75KX8qS5SC6KCXVm1wKPAZKPjEm5QLt6nw==", "462146fd-2bfb-4efd-b5b9-beb3d4e51d52" });

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

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "2481e557-b961-4ac6-bed3-317ba554ea34", "AQAAAAIAAYagAAAAEGdeshGBZSVofhCEimW57+mNqMQqn7W96GyifeiRWnNXP2xSbFSf11sCUosS8OYlwg==", "b146d64d-8826-4c44-8d3d-9f778cb90e23" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d7fadc48-9be6-49a5-aee7-2f32f76997d4", "AQAAAAIAAYagAAAAEJr7GsD9O89nR6lzZS5Xj3fl0G9zTOy2zoEj9/K5BY43dVx4ZQXoLWlQF7OxVEXF6Q==", "ca785c0e-7f90-43d6-8eb5-f99cc828d32c" });
        }
    }
}
