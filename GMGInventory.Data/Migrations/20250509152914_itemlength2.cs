using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class itemlength2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
                table: "Items",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModelNumber",
                table: "Items",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InterchangeNumber",
                table: "Items",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef86fb88-91c7-471a-8bf4-609c4cf221e3", "AQAAAAIAAYagAAAAEIo7Pr3RdJLjXYj3MALL7bqtQ0/Me7MFliStX4XzlNPc6Gth66VMyk7AiJUIv3BgcA==", "be0a1035-3b9c-4999-82fe-2b2a038e87b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "960ba283-ba3c-4243-b672-c453edbd6a59", "AQAAAAIAAYagAAAAEBfLTxd5k1sQiNbwAsfhsQMFjVyqs9Q7fpTbHi3MSWxjRP+8kd+29xT9++5IAitZFg==", "0daedfe5-1fa0-4b81-9a78-77a51e188bdf" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
                table: "Items",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModelNumber",
                table: "Items",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InterchangeNumber",
                table: "Items",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

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
    }
}
