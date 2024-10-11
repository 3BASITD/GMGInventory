using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Procurement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseOrderDeliveryModes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDeliveryModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchasePaymentTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaysToPay = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasePaymentTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequestPurposes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderTypeId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequestPurposes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseRequestPurposes_PurchaseOrderTypes_PurchaseOrderTypeId",
                        column: x => x.PurchaseOrderTypeId,
                        principalTable: "PurchaseOrderTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequestPurposes_PurchaseRequestTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PurchaseRequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderNews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderTermId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderDeliveryModeId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryAddressId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CanvasSummaryReference = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    OtherCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    PreparedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CheckedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ApproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderNews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_AspNetUsers_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_PurchaseOrderDeliveryModes_PurchaseOrderDeliveryModeId",
                        column: x => x.PurchaseOrderDeliveryModeId,
                        principalTable: "PurchaseOrderDeliveryModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_PurchaseOrderTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PurchaseOrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_PurchasePaymentTerms_PurchaseOrderTermId",
                        column: x => x.PurchaseOrderTermId,
                        principalTable: "PurchasePaymentTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_PurchaseStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "PurchaseStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_Warehouses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    PurchaseRequestTypeId = table.Column<int>(type: "int", nullable: false),
                    PurposeId = table.Column<int>(type: "int", nullable: false),
                    PurchaseRequestPurposeId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    PreparedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CheckedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Approver1Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Approver2Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseOrderTypeId = table.Column<int>(type: "int", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_AspNetUsers_Approver1Id",
                        column: x => x.Approver1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_AspNetUsers_Approver2Id",
                        column: x => x.Approver2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_PurchaseOrderTypes_PurchaseOrderTypeId",
                        column: x => x.PurchaseOrderTypeId,
                        principalTable: "PurchaseOrderTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_PurchaseRequestPurposes_PurchaseRequestPurposeId",
                        column: x => x.PurchaseRequestPurposeId,
                        principalTable: "PurchaseRequestPurposes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_PurchaseRequestTypes_PurchaseRequestTypeId",
                        column: x => x.PurchaseRequestTypeId,
                        principalTable: "PurchaseRequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_PurchaseStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "PurchaseStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReceivings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    PurchasePaymentTermId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    ReceiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryAddressId = table.Column<int>(type: "int", nullable: false),
                    DeliveryReceiptNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SalesInvoiceNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    PreparedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReceivings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_AspNetUsers_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_PurchaseOrderNews_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrderNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_PurchasePaymentTerms_PurchasePaymentTermId",
                        column: x => x.PurchasePaymentTermId,
                        principalTable: "PurchasePaymentTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_PurchaseStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "PurchaseStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_Warehouses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturnNews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    ReceiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    DeliveryReceiptNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SalesInvoiceNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    PreparedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturnNews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNews_AspNetUsers_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNews_PurchaseOrderNews_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrderNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNews_PurchaseStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "PurchaseStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNews_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNews_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderNewLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    PurchaseRequestId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    ItemDescription = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderNewLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNewLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNewLineItems_PurchaseOrderNews_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrderNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNewLineItems_PurchaseRequests_PurchaseRequestId",
                        column: x => x.PurchaseRequestId,
                        principalTable: "PurchaseRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNewLineItems_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequestLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseRequestId = table.Column<int>(type: "int", nullable: false),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    SalesOrderId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    ItemDescription = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StockOnHand = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequestLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseRequestLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequestLineItems_PurchaseRequests_PurchaseRequestId",
                        column: x => x.PurchaseRequestId,
                        principalTable: "PurchaseRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseRequestLineItems_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequestLineItems_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReceivingLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseReceivingId = table.Column<int>(type: "int", nullable: false),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    ItemDescription = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReceivingLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReceivingLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReceivingLineItems_PurchaseReceivings_PurchaseReceivingId",
                        column: x => x.PurchaseReceivingId,
                        principalTable: "PurchaseReceivings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReceivingLineItems_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturnNewLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseReturnId = table.Column<int>(type: "int", nullable: false),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    ItemDescription = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturnNewLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNewLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNewLineItems_PurchaseReturnNews_PurchaseReturnId",
                        column: x => x.PurchaseReturnId,
                        principalTable: "PurchaseReturnNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNewLineItems_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(620));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(632));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(633));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(634));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(635));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(637));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(642));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(643));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(644));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(651));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(657));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(658));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(659));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(660));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(661));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(661));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(662));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(664));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(665));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(666));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(667));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(668));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(668));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(669));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(670));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(671));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(672));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(672));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(673));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(682));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(683));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(684));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(685));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(686));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(687));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(688));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(689));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(689));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(690));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(691));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(692));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(693));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(693));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(694));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(695));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(696));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(697));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(698));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(699));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(699));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(700));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(701));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(702));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(703));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(703));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(704));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(746));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(747));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(748));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7305));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7323));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7328));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7349));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7333));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7354));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b692a51-fce6-457a-af96-066bfc5a6c0d", new DateTime(2024, 10, 11, 9, 37, 58, 973, DateTimeKind.Local).AddTicks(9382), "AQAAAAIAAYagAAAAEJCUWB6ubLtrxjTxbPCV3T1oQBUjS0Z+KzCjHq4N97TJ37UtAGcCLiEjf58uQ0abPQ==", "9bc3be56-a0e4-4aff-8131-109c5aa1744d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f16da6b9-e002-474d-b49e-b8eaaab5120c", new DateTime(2024, 10, 11, 9, 37, 58, 893, DateTimeKind.Local).AddTicks(5046), "AQAAAAIAAYagAAAAEGNEi+nHtqrHZsTCCjcgdfd+fyiJeewDRJvSyj7kgxg2gsxiKFmcpgkYyxCVdQa/EA==", "5a1d15cb-ce79-439b-8f6e-7d97a7159765" });

            migrationBuilder.InsertData(
                table: "PurchaseOrderDeliveryModes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "PICK-UP", "Pick-up" },
                    { 2, "DELIVERY", "Delivery" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrderTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "NA", "Not applicable" },
                    { 2, "LPO", "Local Purchase Order" },
                    { 3, "IPO", "International Purchase Order" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseRequestTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AM", "Automotive" },
                    { 2, "NA", "Non-Automotive" },
                    { 3, "MR", "Maintenance & Repair" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseStatuses",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "Draft", "Draft" },
                    { 2, "Posted", "Posted" },
                    { 3, "Approved", "Approved" }
                });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 812, DateTimeKind.Local).AddTicks(560));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 812, DateTimeKind.Local).AddTicks(574));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 812, DateTimeKind.Local).AddTicks(577));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 812, DateTimeKind.Local).AddTicks(578));

            migrationBuilder.InsertData(
                table: "PurchaseRequestPurposes",
                columns: new[] { "Id", "Code", "Name", "PurchaseOrderTypeId", "TypeId" },
                values: new object[,]
                {
                    { 1, "Rush", "Rush Order", null, 1 },
                    { 2, "Stock", "Stocking", null, 1 },
                    { 3, "Office", "Office Supplies", null, 2 },
                    { 4, "One-Time", "One-Time Purchase", null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNewLineItems_ItemId",
                table: "PurchaseOrderNewLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNewLineItems_PurchaseOrderId",
                table: "PurchaseOrderNewLineItems",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNewLineItems_PurchaseRequestId",
                table: "PurchaseOrderNewLineItems",
                column: "PurchaseRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNewLineItems_UnitId",
                table: "PurchaseOrderNewLineItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_ApproverId",
                table: "PurchaseOrderNews",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_CurrencyId",
                table: "PurchaseOrderNews",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_DeliveryAddressId",
                table: "PurchaseOrderNews",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_PurchaseOrderDeliveryModeId",
                table: "PurchaseOrderNews",
                column: "PurchaseOrderDeliveryModeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_PurchaseOrderTermId",
                table: "PurchaseOrderNews",
                column: "PurchaseOrderTermId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_StatusId",
                table: "PurchaseOrderNews",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_SupplierId",
                table: "PurchaseOrderNews",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_TypeId",
                table: "PurchaseOrderNews",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivingLineItems_ItemId",
                table: "PurchaseReceivingLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivingLineItems_PurchaseReceivingId",
                table: "PurchaseReceivingLineItems",
                column: "PurchaseReceivingId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivingLineItems_UnitId",
                table: "PurchaseReceivingLineItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_ApproverId",
                table: "PurchaseReceivings",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_DeliveryAddressId",
                table: "PurchaseReceivings",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_PurchaseOrderId",
                table: "PurchaseReceivings",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_PurchasePaymentTermId",
                table: "PurchaseReceivings",
                column: "PurchasePaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_StatusId",
                table: "PurchaseReceivings",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_SupplierId",
                table: "PurchaseReceivings",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestLineItems_ItemId",
                table: "PurchaseRequestLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestLineItems_PurchaseRequestId",
                table: "PurchaseRequestLineItems",
                column: "PurchaseRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestLineItems_SalesOrderId",
                table: "PurchaseRequestLineItems",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestLineItems_UnitId",
                table: "PurchaseRequestLineItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestPurposes_PurchaseOrderTypeId",
                table: "PurchaseRequestPurposes",
                column: "PurchaseOrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestPurposes_TypeId",
                table: "PurchaseRequestPurposes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_Approver1Id",
                table: "PurchaseRequests",
                column: "Approver1Id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_Approver2Id",
                table: "PurchaseRequests",
                column: "Approver2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_AreaId",
                table: "PurchaseRequests",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_PurchaseOrderTypeId",
                table: "PurchaseRequests",
                column: "PurchaseOrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_PurchaseRequestPurposeId",
                table: "PurchaseRequests",
                column: "PurchaseRequestPurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_PurchaseRequestTypeId",
                table: "PurchaseRequests",
                column: "PurchaseRequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_StatusId",
                table: "PurchaseRequests",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNewLineItems_ItemId",
                table: "PurchaseReturnNewLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNewLineItems_PurchaseReturnId",
                table: "PurchaseReturnNewLineItems",
                column: "PurchaseReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNewLineItems_UnitId",
                table: "PurchaseReturnNewLineItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_ApproverId",
                table: "PurchaseReturnNews",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_PurchaseOrderId",
                table: "PurchaseReturnNews",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_StatusId",
                table: "PurchaseReturnNews",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_SupplierId",
                table: "PurchaseReturnNews",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_WarehouseId",
                table: "PurchaseReturnNews",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrderNewLineItems");

            migrationBuilder.DropTable(
                name: "PurchaseReceivingLineItems");

            migrationBuilder.DropTable(
                name: "PurchaseRequestLineItems");

            migrationBuilder.DropTable(
                name: "PurchaseReturnNewLineItems");

            migrationBuilder.DropTable(
                name: "PurchaseReceivings");

            migrationBuilder.DropTable(
                name: "PurchaseRequests");

            migrationBuilder.DropTable(
                name: "PurchaseReturnNews");

            migrationBuilder.DropTable(
                name: "PurchaseRequestPurposes");

            migrationBuilder.DropTable(
                name: "PurchaseOrderNews");

            migrationBuilder.DropTable(
                name: "PurchaseRequestTypes");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDeliveryModes");

            migrationBuilder.DropTable(
                name: "PurchaseOrderTypes");

            migrationBuilder.DropTable(
                name: "PurchasePaymentTerms");

            migrationBuilder.DropTable(
                name: "PurchaseStatuses");

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9013));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9024));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9025));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9026));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9027));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9029));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9030));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9031));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9032));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9033));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9034));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9035));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9036));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9078));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9079));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9080));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9080));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9098));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9115));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9116));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9117));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9118));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9118));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9119));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9120));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9121));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9122));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9123));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9123));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9133));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9134));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9134));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9135));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9137));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9137));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9138));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9139));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9140));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9142));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9143));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9144));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9146));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9147));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9148));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9149));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9149));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9151));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9152));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9154));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9155));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9156));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9157));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9290));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9311));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9317));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9339));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9334));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9344));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "716208ca-2be4-4ac3-8a5a-69fb038e41d2", new DateTime(2024, 10, 8, 8, 19, 41, 985, DateTimeKind.Local).AddTicks(1084), "AQAAAAIAAYagAAAAEI0O7zTm2ENf3DZ9mqhhlVU6IyEaVT/iZ3e+htRRs//yDoyjuYfxnVggrAyWKu1+Hw==", "4ded77ff-6a5b-4286-8b5c-075488aed677" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e88878db-fb1e-437f-8007-67399c09785c", new DateTime(2024, 10, 8, 8, 19, 41, 898, DateTimeKind.Local).AddTicks(5217), "AQAAAAIAAYagAAAAEGKr1NRmIL93lucCilGqilZCaIwmFxBuukz6FAp6d7jVe7hOsibuWU/K1DOAAOvFYg==", "b0595e8a-d5e6-4615-89f4-c894aa7c15b2" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 814, DateTimeKind.Local).AddTicks(376));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 814, DateTimeKind.Local).AddTicks(392));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 814, DateTimeKind.Local).AddTicks(394));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 814, DateTimeKind.Local).AddTicks(396));
        }
    }
}
