using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Cheque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cheques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefNo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CheckDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Check = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CheckNo = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cheques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cheques_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ae9986a-4e67-45ad-8f9f-b409dd54a679", "AQAAAAIAAYagAAAAEGD4i4IGx8w8IDGfMeAvj0qJkYhZIYmrxv5lrRnGulooLcR4YTf8XHbWp2aaw4UZxA==", "27b0ce29-662d-4364-8526-480798265e8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac9595b1-c85c-44f1-b8c7-b339048dac67", "AQAAAAIAAYagAAAAEOg6SoDZfdP+MH6GoZN+5NqetwX3tfI5RCr4aLUFWVZxqr7x/0zZGE2pBpgzEu2F2g==", "76efe837-bc80-4c5b-a280-9b59d3aa688c" });

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_CustomerId",
                table: "Cheques",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cheques");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58f5d0c6-4d37-401c-9757-c4dc969d617e", "AQAAAAIAAYagAAAAEHyuZYB4zIvCeKDySuyDsWskT6qiy3sWSJifXH9ZxfCdnTom4RFh3u/dDZgGP1r6Cw==", "f18ac569-2c59-448e-8c24-0a04c49f29bf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1a99dda-a967-4fc1-a64a-13cbebc4377c", "AQAAAAIAAYagAAAAEAISUD0UQ78HHgM9LPTbedet7uCQp1qvsUKb7VznFtXS1mMa24hMDjlky5kJDfdX1g==", "f473e7a3-0ece-4f11-8489-6b03320c507e" });
        }
    }
}
