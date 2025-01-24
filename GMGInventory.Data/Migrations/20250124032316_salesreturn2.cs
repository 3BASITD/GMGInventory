using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesreturn2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReasonOfReturnId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 1);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "9efe5f61-3f50-4126-b1ad-c898e2232e5d", "AQAAAAIAAYagAAAAEHCqkgaJGDwtSnhfklYCPd2AByj5u6tGZsAv2ZkF1mxitr9gjgU1W1t1C/2dJwLF9A==", "f8047f9d-361f-4c64-ba22-8e9e5e9cdc48" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0b1fb962-264b-4260-89b8-b4fa35c02bba", "AQAAAAIAAYagAAAAEC9PaKFTKCbb/b4W+t2itxD6Q4POPsBLgQ1WzzA5Kcm9n+UNll2io3Lx8DgRgDZthA==", "c6501abd-63c4-4db1-bfa3-9ea7056759d2" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReasonOfReturnId",
                table: "Invoices",
                column: "ReasonOfReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_ReasonOfReturns_ReasonOfReturnId",
                table: "Invoices",
                column: "ReasonOfReturnId",
                principalTable: "ReasonOfReturns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_ReasonOfReturns_ReasonOfReturnId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ReasonOfReturnId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ReasonOfReturnId",
                table: "Invoices");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "99585efe-88c6-46ea-8832-48fe03f0e485", "AQAAAAIAAYagAAAAEHDw65nvOf+L/lhm9f+3SvCIAlSPxmtRdhl2w9t/EQqSOiRvYKpvbciALQQRben2ZA==", "4e363e20-d247-406e-9bac-a5912d62f414" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "4e8b7743-2aa5-4c66-87c5-5ef6f8ba375f", "AQAAAAIAAYagAAAAEMr+NwiZI3MrCCibWzl0NB2vQkqHi5BUnTxHoWh+QDvLwyPID6tbhoOl5QO0XOejEw==", "233463c9-a354-418f-9d98-923b52c40b5a" });
        }
    }
}
