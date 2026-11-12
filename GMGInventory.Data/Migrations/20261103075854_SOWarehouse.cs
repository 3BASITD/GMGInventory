using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SOWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop FK if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.foreign_keys
                           WHERE name = 'FK_SalesOrders_Warehouses_WarehouseId'
                             AND parent_object_id = OBJECT_ID('dbo.SalesOrders'))
                    ALTER TABLE dbo.SalesOrders
                        DROP CONSTRAINT FK_SalesOrders_Warehouses_WarehouseId;");

            // 2. Add the column as nullable if missing, otherwise make it nullable
            migrationBuilder.Sql(@"
                IF COL_LENGTH('dbo.SalesOrders', 'WarehouseId') IS NULL
                    ALTER TABLE dbo.SalesOrders ADD WarehouseId int NULL;
                ELSE
                    ALTER TABLE dbo.SalesOrders ALTER COLUMN WarehouseId int NULL;");

            // 3. Null out values that point at no real warehouse (separate batch on purpose)
            migrationBuilder.Sql(@"
                UPDATE so
                SET so.WarehouseId = NULL
                FROM dbo.SalesOrders so
                WHERE so.WarehouseId IS NOT NULL
                  AND NOT EXISTS (SELECT 1 FROM dbo.Warehouses w WHERE w.Id = so.WarehouseId);");

            // 4. Index if missing
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.indexes
                               WHERE name = 'IX_SalesOrders_WarehouseId'
                                 AND object_id = OBJECT_ID('dbo.SalesOrders'))
                    CREATE INDEX IX_SalesOrders_WarehouseId ON dbo.SalesOrders (WarehouseId);");

            // 5. Add the FK
            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Warehouses_WarehouseId",
                table: "SalesOrders",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.foreign_keys
                           WHERE name = 'FK_SalesOrders_Warehouses_WarehouseId')
                    ALTER TABLE dbo.SalesOrders
                        DROP CONSTRAINT FK_SalesOrders_Warehouses_WarehouseId;");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SalesOrders_WarehouseId')
                    DROP INDEX IX_SalesOrders_WarehouseId ON dbo.SalesOrders;");
        }
    }
}
