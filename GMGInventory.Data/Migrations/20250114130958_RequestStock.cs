using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class RequestStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockOnHand",
                table: "PurchaseRequestLineItems");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "6bb6edea-4612-4f9a-afa9-1ceda03a5de6", "AQAAAAIAAYagAAAAEKe3h8+UjGMa2KFuIMxGMYen4QsGcym3ymCYojmrW9DMbdtbiwKp3ImcfpiIP1InaQ==", "52fff6c1-1f24-4e0e-a028-858863c429ae" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "ed9e1c45-7f8d-4c61-aefa-3655c398fa99", "AQAAAAIAAYagAAAAEL03JK4Zy+p3A/izQDYD4gKljKXFnd/0GK2O5vA/xOs2eSnwfK0mK2gjXMq+y6HmQA==", "b14076c6-1bea-46e0-ba2d-62acd5c4fc56" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockOnHand",
                table: "PurchaseRequestLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "dbcb3ae9-9f6a-4862-98c6-6a750d3d05c1", "AQAAAAIAAYagAAAAEA13f/+O7V3JMSCZjIBv11oPxKLI5zgFzEVYHIkRSmNPnonZgQJscsTe97lFrujxLA==", "55bd5860-d11a-4fa4-bcc6-1f33786d1713" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f166c4ea-3b7f-4fae-a72b-d77ae04ac60e", "AQAAAAIAAYagAAAAEFaKFSza+pFz5GNOp4X/ubEVWk98kIyMoZZU4iGLBl4Sv+16VlWqFIPRrWp7mu3oMg==", "229cca85-a20d-4cfe-ab99-66fb0021acb6" });
        }
    }
}
