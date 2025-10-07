using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class itemmigrate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Items",
                type: "nvarchar(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cba5ee59-9791-4e23-a54a-7f73835716fe", "AQAAAAIAAYagAAAAEAf9f1N77eKiILMSGoJAkST0l0gqOICL2BDqp9HxKiswtF58S+mVDy6vce+Uz5cUdw==", "ff8967b3-d357-4c98-8ac2-4e4ac497218b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f4532e1-0051-41b8-94b5-1fb108409ea5", "AQAAAAIAAYagAAAAEFqCW7N/atDmNysLKpCuXNs4+91E+VxbybSxMtdtOdX6FmdUfm2/uk+EBrKGCi0UoQ==", "8e36361f-3e29-4f4f-bd61-e77fc72732f4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Items",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc25ea9a-7786-43f7-8894-0e6f6a09010c", "AQAAAAIAAYagAAAAEAHBYCu7VFgCZVA1U0CNsJe90fvHfWAteAZmrJKOvcSS58WcQCnnZ5o+m4i/rCzcGQ==", "400795ed-f33e-4e39-81cd-f6611c03f9d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49427c1d-692b-46d7-bcce-fb5ccb8cf14e", "AQAAAAIAAYagAAAAEJBHY+CWgRuJDuBDs/2Dkn6wt0KBrhDC6zQ4G3vYy4yPGk5/co9OKu5uOzGedwe4Qw==", "4ec2e712-e7a9-4dfb-98ad-3f4882406688" });
        }
    }
}
