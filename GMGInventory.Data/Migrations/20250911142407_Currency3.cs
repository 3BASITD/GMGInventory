using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Currency3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Currencies");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f4a9d960-975d-4d67-8a98-6d83aba39e94", "AQAAAAIAAYagAAAAEHBajhsXd9twNV6fM8LiYaXJJAqboD26Qfu3Lu54homyA7mTX19qR1avZ7CPLjXiZA==", "b49d8c3a-04b4-4adc-b41c-d6d09466ad97" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "6dbb31d4-470a-4d90-9fd9-4e0795ace2f5", "AQAAAAIAAYagAAAAECnuIoeTNjW9439pCU+HW1cjz3RQzjtfBcYAhOx3/KjnldG28ImsIzeQ/FvM9N3wSg==", "909564ee-dff4-45df-9519-7e3bbee02b63" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Currencies",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd02dc8b-40bc-4e66-9e6a-b84afe9460e7", "AQAAAAIAAYagAAAAEI0OXgDEtmthYSQUsOxPuz1/ZnFoByXXocfL5fNOISTEc/3SsaSltmBf9ObQWT0v0w==", "a4a92854-ade6-45d8-bb27-dd527be5ff54" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52d43833-a9da-49f5-ae4e-157eb5b257ec", "AQAAAAIAAYagAAAAED2UeWIfxaiZFwOMPjU7IHo1t33cwRR+T2EMtajV3A99+kqL+Yw4vwaND4scV2nmCQ==", "9bd828f8-38a2-449d-a49e-139916b95186" });
        }
    }
}
