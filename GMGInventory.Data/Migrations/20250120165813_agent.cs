using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class agent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Agents");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "be2b8ad8-6178-44cd-be18-d949c49c07c4", "AQAAAAIAAYagAAAAEB3YuswT6PaO4e4274MOn5FFhm6+pv2YGiE6OPzx8JCg3ZNP15fHwo/yyUcKQWq//Q==", "484f86b5-eba5-4cd0-a492-7eead447d028" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0aa83673-85c5-4346-94e0-00510fc77804", "AQAAAAIAAYagAAAAEII3cbt+7m0D6nCdW3Vd2tcfIrqFr7vK8LatxckrFquJZmnPCzyhPz5kEnnyqXQoWw==", "adbb1fe3-1794-4159-ad7e-e3c7944e7e35" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Agents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0ea07f01-44e6-4f4b-a920-3ab74251fd4d", "AQAAAAIAAYagAAAAEKNpxrRgbfSoD5pPe5E60ybikfOvLWMqoCikjw5kdMyx63C0Ds5QwFSQBsDdv53qPA==", "4c42a9f6-c86e-468a-a55a-b949b4f93fc8" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d44c9b06-d74e-473d-81b4-9ce53e302c66", "AQAAAAIAAYagAAAAEIo1z4VuO7XAcvkUnnXvJCQ1m8Vz/nF1v7VyCvG9jqg78xmjO0DK5+15ibgJMHFazg==", "b21919c6-2c57-4686-bb72-84f84f8a31f8" });
        }
    }
}
