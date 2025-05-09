using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class itemlength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Items",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76c74b78-623d-4599-9228-105c145c02a3", "AQAAAAIAAYagAAAAENHaRqmvfdmjRJ8BQ4bI0zfaUK7vXHNMfE2F5m+w3U5A/FKSzxw5CfMTXAcSPhUoCQ==", "7a4487fe-b6e3-4933-97eb-d40845fdee24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b873735e-922b-4bc2-82b7-ced59b4a9d3c", "AQAAAAIAAYagAAAAENRk8l/4mDBeGUKecPWbLSUMhECb76aYwHPi1kwct2AYPwY/YawHgKV6O53YLTrPQQ==", "21bb43b6-482b-4021-a24e-12fb63650867" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Items",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7315efd4-a059-4a03-a155-58c212085765", "AQAAAAIAAYagAAAAEAT6X/d4K7FUvg1Wowst4Jwlo2oj6kov9JX28ol0K/aho2rTyCix4tmhKWW8LvCPOw==", "e57f4103-0f42-4a3b-b4cc-9402c0dce02a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d49a87a8-1e06-4a39-94b1-887746fce3b4", "AQAAAAIAAYagAAAAENT34ERv80J63qqwKAxGbTTVErCuzTm41fg1/oJOwwgd2ZzbduXpw64AvQQjlRwL0Q==", "7dc661e3-247c-443d-beb7-1d821402f5fc" });
        }
    }
}
