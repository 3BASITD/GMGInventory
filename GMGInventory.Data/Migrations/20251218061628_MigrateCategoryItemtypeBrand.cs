using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateCategoryItemtypeBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_ItemTypes_ItemTypeId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTypes_Categories_CategoryId",
                table: "ItemTypes");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ItemTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ItemTypeId",
                table: "Brands",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2389fc2-272b-4ea0-9f29-b4d50edf6136", "AQAAAAIAAYagAAAAEAsyue1r6286Z5H/KBjqcZUfnRkbOuD0c1gzUUQZcIMx+TraqoSIx8NhJFLTP3Rpyw==", "f28e29fa-ff5b-4089-aded-c00a52eda1c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e661ddd-9cf4-4cb6-9032-1af3554ebee5", "AQAAAAIAAYagAAAAEG+xHZ7p8AmHpFxXPfr1cxv7IGw9/Ef3mSU6T1/azzB2TEspM85gd4bKvjiigvSorA==", "77c45926-1ed9-4190-b9d1-6e2cb0923c78" });

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_ItemTypes_ItemTypeId",
                table: "Brands",
                column: "ItemTypeId",
                principalTable: "ItemTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTypes_Categories_CategoryId",
                table: "ItemTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_ItemTypes_ItemTypeId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTypes_Categories_CategoryId",
                table: "ItemTypes");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ItemTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemTypeId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b18ad63b-26a5-42bf-b3f3-eefc8a74ec86", "AQAAAAIAAYagAAAAEMVR+2Q1cIPIORkoWVyVwQOULBYAqRhW+Yt6BeqZuXqJaYq+dc8ncRklTmANOqnqLw==", "867110e5-cec9-4aa0-9b64-5bf7099451e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "421055b0-df3f-41b4-a022-efeaa563ffbd", "AQAAAAIAAYagAAAAEIR8DT0t81l1TghwJ0CNgsFH77RlNo2zp6c6IfgYSfIsVNIPCo7hjStMxFQvh2IlWA==", "1cc83959-3eb6-4266-83f5-289796704aaa" });

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_ItemTypes_ItemTypeId",
                table: "Brands",
                column: "ItemTypeId",
                principalTable: "ItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTypes_Categories_CategoryId",
                table: "ItemTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
