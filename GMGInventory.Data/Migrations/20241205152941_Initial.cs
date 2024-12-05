using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Nickname = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsDarkMode = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessStyles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessStyles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditDebitMemoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditDebitMemoTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DaysToPay = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccountStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccountStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forwarders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    FaxNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forwarders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    File = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    DisplayTime = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    HasView = table.Column<bool>(type: "bit", nullable: false),
                    HasAdd = table.Column<bool>(type: "bit", nullable: false),
                    HasEdit = table.Column<bool>(type: "bit", nullable: false),
                    HasDelete = table.Column<bool>(type: "bit", nullable: false),
                    HasApproveDisapprove = table.Column<bool>(type: "bit", nullable: false),
                    HasUploadDownload = table.Column<bool>(type: "bit", nullable: false),
                    HasPrint = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTypes", x => x.Id);
                });

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
                name: "ReasonOfReturns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonOfReturns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SOACRTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOACRTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTypes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PageId = table.Column<int>(type: "int", nullable: false),
                    CanView = table.Column<bool>(type: "bit", nullable: false),
                    CanAdd = table.Column<bool>(type: "bit", nullable: false),
                    CanEdit = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false),
                    CanApproveDisapprove = table.Column<bool>(type: "bit", nullable: true),
                    CanUploadDownload = table.Column<bool>(type: "bit", nullable: true),
                    CanPrint = table.Column<bool>(type: "bit", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accesses_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accesses_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestTypeId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderTypes_PurchaseRequestTypes_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "PurchaseRequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequestPurposes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequestPurposes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseRequestPurposes_PurchaseRequestTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PurchaseRequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TIN = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    OfficeTelNo = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    BusinessStyleId = table.Column<int>(type: "int", nullable: false),
                    Terms = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PriceList = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    VAT = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_BusinessStyles_BusinessStyleId",
                        column: x => x.BusinessStyleId,
                        principalTable: "BusinessStyles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suppliers_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suppliers_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferItems_Warehouses_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferItems_Warehouses_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ItemTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemTypes",
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
                    PurposeId = table.Column<int>(type: "int", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurposeDefinition = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    Approver1Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Approver2Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DisapproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DisapprovedReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResubmitId = table.Column<int>(type: "int", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequests", x => x.Id);
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
                        name: "FK_PurchaseRequests_AspNetUsers_DisapproverId",
                        column: x => x.DisapproverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_PurchaseRequestPurposes_PurposeId",
                        column: x => x.PurposeId,
                        principalTable: "PurchaseRequestPurposes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_PurchaseRequests_ResubmitId",
                        column: x => x.ResubmitId,
                        principalTable: "PurchaseRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_PurchaseRequestTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PurchaseRequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_PurchaseStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "PurchaseStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseRequests_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DateHired = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agents_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
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
                    TermId = table.Column<int>(type: "int", nullable: false),
                    DeliveryModeId = table.Column<int>(type: "int", nullable: false),
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
                    ApproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DisapproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DisapprovedReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResubmitId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_PurchaseOrderNews_AspNetUsers_DisapproverId",
                        column: x => x.DisapproverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_CreditTerms_TermId",
                        column: x => x.TermId,
                        principalTable: "CreditTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_PurchaseOrderDeliveryModes_DeliveryModeId",
                        column: x => x.DeliveryModeId,
                        principalTable: "PurchaseOrderDeliveryModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_PurchaseOrderNews_ResubmitId",
                        column: x => x.ResubmitId,
                        principalTable: "PurchaseOrderNews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNews_PurchaseOrderTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PurchaseOrderTypes",
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
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    PONumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    TotalPurchaseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReplenishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Warehouses_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    InterchangeNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ModelNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ItemTypeId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    AgentId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CommodityQuantity = table.Column<int>(type: "int", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditTermId = table.Column<int>(type: "int", nullable: false),
                    CustomerAccountStatusId = table.Column<int>(type: "int", nullable: false),
                    CustomerTypeId = table.Column<int>(type: "int", nullable: false),
                    FaxNumber = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    OutstandingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    RequiredDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TIN = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    VAT = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_CreditTerms_CreditTermId",
                        column: x => x.CreditTermId,
                        principalTable: "CreditTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerAccountStatuses_CustomerAccountStatusId",
                        column: x => x.CustomerAccountStatusId,
                        principalTable: "CustomerAccountStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerTypes_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReceivings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    ReceiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    DeliveryReceiptNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SalesInvoiceNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    ApproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DisapproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DisapprovedReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResubmitId = table.Column<int>(type: "int", nullable: true),
                    CreditTermId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_PurchaseReceivings_AspNetUsers_DisapproverId",
                        column: x => x.DisapproverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_CreditTerms_CreditTermId",
                        column: x => x.CreditTermId,
                        principalTable: "CreditTerms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_PurchaseOrderNews_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrderNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_PurchaseReceivings_ResubmitId",
                        column: x => x.ResubmitId,
                        principalTable: "PurchaseReceivings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_PurchaseStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "PurchaseStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReceivings_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReturns_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemConversions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SubItemId = table.Column<int>(type: "int", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    EquivalentQuantity = table.Column<int>(type: "int", nullable: true),
                    EquivalentUnitId = table.Column<int>(type: "int", nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemConversions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemConversions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemConversions_Items_SubItemId",
                        column: x => x.SubItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemConversions_Units_EquivalentUnitId",
                        column: x => x.EquivalentUnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemConversions_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemInventories_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemInventories_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    NetSellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetailSellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPrices_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalCountHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    QuantityFrom = table.Column<int>(type: "int", nullable: false),
                    QuantityTo = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalCountHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalCountHistories_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhysicalCountHistories_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLineItems_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    In = table.Column<int>(type: "int", nullable: true),
                    Out = table.Column<int>(type: "int", nullable: true),
                    Balance = table.Column<int>(type: "int", nullable: true),
                    InOutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockCards_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferItemDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferItemId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferItemDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferItemDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferItemDetails_TransferItems_TransferItemId",
                        column: x => x.TransferItemId,
                        principalTable: "TransferItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ForwarderId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: true),
                    DeclaredValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceTypeId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityPacked = table.Column<int>(type: "int", nullable: false),
                    QuantityInvoiced = table.Column<int>(type: "int", nullable: false),
                    QuantityOrdered = table.Column<int>(type: "int", nullable: false),
                    SalesOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisapprovedBy = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DisapprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    DisapprovedReason = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    ResubmittedSalesOrderId = table.Column<int>(type: "int", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Forwarders_ForwarderId",
                        column: x => x.ForwarderId,
                        principalTable: "Forwarders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_PriceTypes_PriceTypeId",
                        column: x => x.PriceTypeId,
                        principalTable: "PriceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrders_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesOrders_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesReturns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesReturnNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesReturns_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    FaxNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SOACRs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SOAFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SOATo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmountToPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SOACRTypeId = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOACRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SOACRs_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SOACRs_SOACRTypes_SOACRTypeId",
                        column: x => x.SOACRTypeId,
                        principalTable: "SOACRTypes",
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
                    PurchaseReceiveId = table.Column<int>(type: "int", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    ApproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DisapproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DisapprovedReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResubmitId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_PurchaseReturnNews_AspNetUsers_DisapproverId",
                        column: x => x.DisapproverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNews_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNews_PurchaseReceivings_PurchaseReceiveId",
                        column: x => x.PurchaseReceiveId,
                        principalTable: "PurchaseReceivings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNews_PurchaseReturnNews_ResubmitId",
                        column: x => x.ResubmitId,
                        principalTable: "PurchaseReturnNews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNews_PurchaseStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "PurchaseStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNews_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNews_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Packs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesOrderId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    QuantityInvoiced = table.Column<int>(type: "int", nullable: false),
                    PackDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisapprovedBy = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DisapprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    PackTypeId = table.Column<int>(type: "int", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packs_PackTypes_PackTypeId",
                        column: x => x.PackTypeId,
                        principalTable: "PackTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Packs_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Packs_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
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
                name: "SalesOrderDeliveryMonitorings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesOrderId = table.Column<int>(type: "int", nullable: false),
                    ForwarderId = table.Column<int>(type: "int", nullable: false),
                    Waybill = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    DatePickup = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeliver = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderDeliveryMonitorings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderDeliveryMonitorings_Forwarders_ForwarderId",
                        column: x => x.ForwarderId,
                        principalTable: "Forwarders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderDeliveryMonitorings_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesOrderId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ServedQuantity = table.Column<int>(type: "int", nullable: false),
                    UnservedQuantity = table.Column<int>(type: "int", nullable: false),
                    PackedQuantity = table.Column<int>(type: "int", nullable: false),
                    InvoicedQuantity = table.Column<int>(type: "int", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsServed = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLineItems_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CRNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    OverPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TKTSolicitation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OffSet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BackLoad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashAdvance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    SOACRId = table.Column<int>(type: "int", nullable: false),
                    TotalAmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalInvoiceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPayments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerPayments_SOACRs_SOACRId",
                        column: x => x.SOACRId,
                        principalTable: "SOACRs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesOrderId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    InvoiceTypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PackId = table.Column<int>(type: "int", nullable: false),
                    ControlNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsGenerated = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisapprovedBy = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DisapprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                        column: x => x.InvoiceTypeId,
                        principalTable: "InvoiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Packs_PackId",
                        column: x => x.PackId,
                        principalTable: "Packs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PackLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BoxNumber = table.Column<int>(type: "int", nullable: false),
                    PackTypeId = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PackLineItems_Packs_PackId",
                        column: x => x.PackId,
                        principalTable: "Packs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PackLineItems_PackTypes_PackTypeId",
                        column: x => x.PackTypeId,
                        principalTable: "PackTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PackReturns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackId = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackReturns_Packs_PackId",
                        column: x => x.PackId,
                        principalTable: "Packs",
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
                    RequestItemId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_PurchaseOrderNewLineItems_PurchaseRequestLineItems_RequestItemId",
                        column: x => x.RequestItemId,
                        principalTable: "PurchaseRequestLineItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SOACRChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerPaymentId = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BankBranch = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CheckNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CheckAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CheckDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOACRChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SOACRChecks_CustomerPayments_CustomerPaymentId",
                        column: x => x.CustomerPaymentId,
                        principalTable: "CustomerPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditDebitMemos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemoNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditDebitMemoTypeId = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditDebitMemos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditDebitMemos_CreditDebitMemoTypes_CreditDebitMemoTypeId",
                        column: x => x.CreditDebitMemoTypeId,
                        principalTable: "CreditDebitMemoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditDebitMemos_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPaymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerPaymentId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPaymentDetails_CustomerPayments_CustomerPaymentId",
                        column: x => x.CustomerPaymentId,
                        principalTable: "CustomerPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerPaymentDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SOACRLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SOACRId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    InvoiceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreightCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreightWaybill = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOACRLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SOACRLineItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SOACRLineItems_SOACRs_SOACRId",
                        column: x => x.SOACRId,
                        principalTable: "SOACRs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PackReturnLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackReturnId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    BoxNumber = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackReturnLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackReturnLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PackReturnLineItems_PackReturns_PackReturnId",
                        column: x => x.PackReturnId,
                        principalTable: "PackReturns",
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
                    OrderItemId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_PurchaseReceivingLineItems_PurchaseOrderNewLineItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "PurchaseOrderNewLineItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReceivingLineItems_PurchaseReceivings_PurchaseReceivingId",
                        column: x => x.PurchaseReceivingId,
                        principalTable: "PurchaseReceivings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesReturnLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesReturnId = table.Column<int>(type: "int", nullable: false),
                    InvoiceLineItemId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDefective = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturnLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesReturnLineItems_InvoiceLineItems_InvoiceLineItemId",
                        column: x => x.InvoiceLineItemId,
                        principalTable: "InvoiceLineItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesReturnLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReturnLineItems_SalesReturns_SalesReturnId",
                        column: x => x.SalesReturnId,
                        principalTable: "SalesReturns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturnLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderLineItemId = table.Column<int>(type: "int", nullable: false),
                    PurchaseReturnId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PurchaseReceivingLineItemId = table.Column<int>(type: "int", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturnLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnLineItems_PurchaseOrderLineItems_PurchaseOrderLineItemId",
                        column: x => x.PurchaseOrderLineItemId,
                        principalTable: "PurchaseOrderLineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReturnLineItems_PurchaseReceivingLineItems_PurchaseReceivingLineItemId",
                        column: x => x.PurchaseReceivingLineItemId,
                        principalTable: "PurchaseReceivingLineItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnLineItems_PurchaseReturns_PurchaseReturnId",
                        column: x => x.PurchaseReturnId,
                        principalTable: "PurchaseReturns",
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
                    ReceivingItemId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_PurchaseReturnNewLineItems_PurchaseReceivingLineItems_ReceivingItemId",
                        column: x => x.ReceivingItemId,
                        principalTable: "PurchaseReceivingLineItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnNewLineItems_PurchaseReturnNews_PurchaseReturnId",
                        column: x => x.PurchaseReturnId,
                        principalTable: "PurchaseReturnNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SOACRLineItemTBRs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesReturnLineItemId = table.Column<int>(type: "int", nullable: false),
                    SOACRLineItemId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOACRLineItemTBRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SOACRLineItemTBRs_SalesReturnLineItems_SalesReturnLineItemId",
                        column: x => x.SalesReturnLineItemId,
                        principalTable: "SalesReturnLineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SOACRLineItemTBRs_SOACRLineItems_SOACRLineItemId",
                        column: x => x.SOACRLineItemId,
                        principalTable: "SOACRLineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DateCreated", "DateModified", "Discriminator", "IsActive", "Name", "NormalizedName", "UserCreated", "UserModified" },
                values: new object[,]
                {
                    { "0BFA2C03-A5D4-43A6-A797-8CFF1A282080", null, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Role", true, "ACCOUNTING", "ACCOUNTING", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { "1E0DA252-5984-43F4-B23C-F755B0E739B5", null, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Role", true, "INVENTORY MANAGER", "INVENTORY MANAGER", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337", null, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Role", true, "INVOICING CLERK", "INVOICING CLERK", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", null, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Role", true, "SYSTEM ADMIN", "SYSTEM ADMIN", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { "826FC15C-21E5-4BC5-A580-5F52E054299B", null, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Role", true, "SALES DIRECTOR", "SALES DIRECTOR", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { "851E0BE3-8728-48EB-BD65-89DC38DB4E01", null, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Role", true, "TELEMARKETER", "TELEMARKETER", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "DateModified", "Discriminator", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDarkMode", "LastName", "LockoutEnabled", "LockoutEnd", "Nickname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserCreated", "UserModified", "UserName" },
                values: new object[,]
                {
                    { "66A86BDE-9D0F-48F8-9CCF-DDF33673862C", 0, "b3f5a4d9-fc0d-444c-a489-21b7f65b7c01", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User", null, false, "GMG", true, false, "Administrator", false, null, "GMG", null, "admin-gmg", "AQAAAAIAAYagAAAAEP4DtRE9fWdG+c11ZPmY8Zr58ajscxQmR0cq5XU7BGzqFQkd3urWbQ7OfQjrb//KIQ==", null, false, "33f35f42-c5d3-4595-846b-da4c5bd80691", false, "CDE19596-2C9E-4ECE-837C-85950E9E907D", null, "admin-gmg" },
                    { "CDE19596-2C9E-4ECE-837C-85950E9E907D", 0, "f6281f70-03b2-4fa8-b39f-2d8a704f7818", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User", null, false, "MISNet", true, false, "Administrator", false, null, "MISNet", null, "admin-misnet", "AQAAAAIAAYagAAAAEL+6MoF47JC+NepPTzxjTzuuBALBVGhVR6tRpgtzMvqqus6mQ03i5od494SfVRDSKA==", null, false, "75ddedc4-a53a-41f7-8c3c-58473d139147", false, "CDE19596-2C9E-4ECE-837C-85950E9E907D", null, "admin-misnet" }
                });

            migrationBuilder.InsertData(
                table: "CreditDebitMemoTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "CR", "Credit" },
                    { 2, "DB", "Debit" }
                });

            migrationBuilder.InsertData(
                table: "CustomerAccountStatuses",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "RED FLAG", "RED FLAG" },
                    { 2, "HOLD", "HOLD" },
                    { 3, "WATCH LIST", "WATCH LIST" },
                    { 4, "GOOD", "GOOD" }
                });

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "LuzMM", "Luzon - Metro Manila" },
                    { 2, "VisMin", "Visayas - Mindanao" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "SI", "Sales Invoice" },
                    { 2, "DR", "Delivery Receipt" }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "HasAdd", "HasApproveDisapprove", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload", "HasView", "Name" },
                values: new object[,]
                {
                    { 1, false, false, false, false, false, false, true, "Customer Purchase History" },
                    { 2, true, true, true, true, true, false, true, "Packing Listing" },
                    { 3, true, true, true, true, true, false, true, "Sales Invoice" },
                    { 4, true, true, true, true, true, false, true, "Sales Order" },
                    { 5, true, true, true, true, true, false, true, "Sales Order Delivery" },
                    { 6, true, false, true, true, false, false, true, "Sales Return" },
                    { 7, true, false, true, true, false, true, true, "Item" },
                    { 8, true, false, true, true, false, false, true, "Price List" },
                    { 9, true, false, true, true, false, false, true, "Price Update" },
                    { 10, true, false, true, true, false, false, true, "Purchase Return" },
                    { 11, true, false, true, true, false, false, true, "Replenish Inventory" },
                    { 12, true, false, true, true, false, false, true, "Warehouse" },
                    { 13, true, false, true, true, false, false, true, "Warehouse Transfer" },
                    { 14, true, false, true, true, false, false, true, "Unserved Item" },
                    { 15, true, true, true, true, true, false, true, "Purchase Request" },
                    { 16, true, true, true, true, true, false, true, "Purchase Order" },
                    { 17, true, true, true, true, true, false, true, "Purchase Receiving" },
                    { 18, true, true, true, true, true, false, true, "Purchase Return (New)" },
                    { 19, true, false, true, true, false, false, true, "Customer" },
                    { 20, true, false, true, true, false, false, true, "Payment" },
                    { 21, true, false, true, true, false, false, true, "SOA / Counter Receipt" },
                    { 22, true, false, true, true, false, false, true, "Check / Cheque" },
                    { 23, true, false, true, true, false, true, true, "Agents" },
                    { 24, true, false, true, true, false, false, true, "Accesses" }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "HasAdd", "HasApproveDisapprove", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload", "HasView", "Name" },
                values: new object[,]
                {
                    { 25, true, false, true, true, false, true, true, "Areas" },
                    { 26, true, false, true, true, false, true, true, "Brands" },
                    { 27, true, false, true, true, false, true, true, "Categories" },
                    { 28, true, false, true, true, false, true, true, "Forwarders" },
                    { 29, true, false, true, true, false, false, true, "Item Types" },
                    { 30, true, false, true, true, false, false, true, "Location Codes" },
                    { 31, true, false, true, true, false, false, true, "Reasons of Return" },
                    { 32, true, false, true, true, false, false, true, "Regions" },
                    { 33, true, false, true, true, false, false, true, "Suppliers" },
                    { 34, true, false, true, true, false, false, true, "Users" },
                    { 35, true, false, true, true, false, false, true, "Home Image" },
                    { 36, false, false, false, false, false, false, true, "Audit Logs" },
                    { 37, false, false, false, false, true, true, true, "Agent Sales per Item" },
                    { 38, false, false, false, false, true, true, true, "Brand Sales Report" },
                    { 39, false, false, false, false, true, true, true, "Best Selling Items" },
                    { 40, false, false, false, false, true, true, true, "Commision Report" },
                    { 41, false, false, false, false, true, true, true, "Customer Sales Ranking Report" },
                    { 42, false, false, false, false, true, true, true, "Customer Sales Report" },
                    { 43, false, false, false, false, true, true, true, "Daily Sales Report" },
                    { 44, false, false, false, false, true, true, true, "Sales Invoice Listing Report" },
                    { 45, false, false, false, false, true, true, true, "Sales by Agent/Area" },
                    { 46, false, false, false, false, true, true, true, "Sales Report by Basis" },
                    { 47, false, false, false, false, true, true, true, "Sales Return Listing Report" },
                    { 48, false, false, false, false, true, true, true, "Unserved Item Report" },
                    { 49, false, false, false, false, true, true, true, "Below Maintaining Items" },
                    { 50, false, false, false, false, true, true, true, "Receiving Listing" },
                    { 51, false, false, false, false, true, true, true, "Receiving Listing by Supplier" },
                    { 52, false, false, false, false, true, true, true, "Receiving Summary" },
                    { 53, false, false, false, false, true, true, true, "Stock Card" },
                    { 54, false, false, false, false, true, true, true, "Stock Status Report" },
                    { 55, false, false, false, false, true, true, true, "Supplier Purchase History" },
                    { 56, false, false, false, false, true, true, true, "Credit Memo Listing Report" },
                    { 57, false, false, false, false, true, true, true, "Debit Memo Listing Report" },
                    { 58, false, false, false, false, true, true, true, "Purchase Order Report" },
                    { 59, false, false, false, false, true, true, true, "Purchase Return Report" },
                    { 60, false, false, false, false, true, true, true, "Purchase Receiving Report" }
                });

            migrationBuilder.InsertData(
                table: "PriceTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "GROSS", "GROSS PAYMENT" },
                    { 2, "NET", "NET PAYMENT" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrderDeliveryModes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "PICK-UP", "PICK-UP" },
                    { 2, "DELIVERY", "DELIVERY" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseRequestTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AM", "AUTOMOTIVE" },
                    { 2, "NA", "NON-AUTOMOTIVE" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseRequestTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 3, "MR", "MAINTENANCE & REPAIR" });

            migrationBuilder.InsertData(
                table: "PurchaseStatuses",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "DRAFT", "DRAFT" },
                    { 2, "SUBMITTED", "SUBMITTED" },
                    { 3, "APPROVED", "APPROVED" },
                    { 4, "DISAPPROVED", "DISAPPROVED" },
                    { 5, "RESUBMITTED", "RESUBMITTED" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "DateCreated", "DateModified", "IsActive", "Name", "UserCreated", "UserModified" },
                values: new object[,]
                {
                    { 1, "LUZ", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "LUZON", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 2, "VIS", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "VISAYAS", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 3, "MIN", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "MINDANAO", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 4, "MM", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "METRO MANILA", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
                });

            migrationBuilder.InsertData(
                table: "SOACRTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "SOA", "STATEMENT OF ACCOUNT" },
                    { 2, "CR", "COUNTER RECEIPT" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "DRAFT", "SALES ORDER DRAFT" },
                    { 2, "SUBMITTED", "SALES ORDER SUBMITTED" },
                    { 3, "APPROVED", "SALES ORDER APPROVED" },
                    { 4, "DENIED", "SALES ORDER DENIED" },
                    { 5, "DRAFT", "PACKING DRAFT" },
                    { 6, "POSTED", "PACKING SUBMITTED" },
                    { 7, "DRAFT", "INVOICE DRAFT" },
                    { 8, "INVOICED", "INVOICED SUBMITTED" },
                    { 9, "PACKED", "FULLY PACKED" },
                    { 10, "INVOICED", "FULLY INVOICED" },
                    { 11, "PRINTED", "INVOICE PRINTED" },
                    { 12, "PAID", "INVOICE PAID" },
                    { 13, "NOT PAID", "INVOICE NOT PAID" },
                    { 14, "CLEARED", "CHECK CLEARED" },
                    { 15, "BOUNCED", "CHECK BOUNCED" },
                    { 16, "REPLACED", "CHECK REPLACED" },
                    { 17, "DEPOSIT", "DEPOSIT" },
                    { 18, "REDEPOSITED", "REDEPOSITED" },
                    { 19, "DEPOSITED", "DEPOSITED" },
                    { 20, "CANCELLED", "CANCELLED INVOICE" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "BOT", "BOTTLE" },
                    { 2, "BOX", "BOX" },
                    { 3, "FT", "FEET" },
                    { 4, "GAL", "GALLON" },
                    { 5, "JAR", "JAR" },
                    { 6, "L", "LITER" },
                    { 7, "M", "METER" },
                    { 8, "MM", "MILLILITER" },
                    { 9, "PACK", "PACK" },
                    { 10, "PAIL", "PAIL" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 11, "PC", "PIECE" },
                    { 12, "QT", "QUART" },
                    { 13, "ROLL", "ROLL" },
                    { 14, "SET", "SET" }
                });

            migrationBuilder.InsertData(
                table: "Accesses",
                columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
                values: new object[,]
                {
                    { 1, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 2, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 3, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 4, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 5, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 6, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 7, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 7, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 8, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 9, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 10, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 10, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 11, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 11, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 12, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 12, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 13, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 13, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 14, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 14, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 15, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 15, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 16, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 16, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 17, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 17, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 18, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 18, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 19, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 19, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 20, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 20, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 21, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 21, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 22, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 22, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 23, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 23, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 24, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 24, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 25, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 25, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 26, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 26, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 27, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 27, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 28, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 28, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 29, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 29, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 30, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 30, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 31, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 31, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 32, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 32, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 33, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 33, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 34, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 34, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 35, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 35, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 36, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 36, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 37, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 37, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 38, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 38, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 39, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 39, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 40, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 40, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 41, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 41, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 42, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 42, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
                });

            migrationBuilder.InsertData(
                table: "Accesses",
                columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
                values: new object[,]
                {
                    { 43, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 43, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 44, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 44, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 45, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 45, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 46, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 46, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 47, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 47, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 48, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 48, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 49, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 49, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 50, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 50, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 51, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 51, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 52, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 52, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 53, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 53, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 54, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 54, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 55, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 55, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 56, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 56, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 57, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 57, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 58, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 58, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 59, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 59, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 60, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 60, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "66A86BDE-9D0F-48F8-9CCF-DDF33673862C" },
                    { "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrderTypes",
                columns: new[] { "Id", "Code", "Name", "RequestTypeId" },
                values: new object[,]
                {
                    { 1, "NA", "NON-AUTOMOTIVE", 1 },
                    { 2, "LPO", "LOCAL PURCHASE ORDER", 2 },
                    { 3, "IPO", "INTERNATIONAL PURCHASE ORDER", 2 },
                    { 4, "MR", "MAINTENANCE & REPAIR", 3 }
                });

            migrationBuilder.InsertData(
                table: "PurchaseRequestPurposes",
                columns: new[] { "Id", "Code", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, "RUSH", "RUSH ORDER", 1 },
                    { 2, "STOCK", "STOCKING", 1 },
                    { 3, "OFFICE", "OFFICE SUPPLIES", 2 },
                    { 4, "ONE-TIME", "ONE-TIME PURCHASE", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_PageId",
                table: "Accesses",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_RoleId",
                table: "Accesses",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AreaId",
                table: "Agents",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_RegionId",
                table: "Areas",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ItemTypeId",
                table: "Brands",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditDebitMemos_CreditDebitMemoTypeId",
                table: "CreditDebitMemos",
                column: "CreditDebitMemoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditDebitMemos_InvoiceId",
                table: "CreditDebitMemos",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPaymentDetails_CustomerPaymentId",
                table: "CustomerPaymentDetails",
                column: "CustomerPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPaymentDetails_InvoiceId",
                table: "CustomerPaymentDetails",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPayments_CustomerId",
                table: "CustomerPayments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPayments_SOACRId",
                table: "CustomerPayments",
                column: "SOACRId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AgentId",
                table: "Customers",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AreaId",
                table: "Customers",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreditTermId",
                table: "Customers",
                column: "CreditTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerAccountStatusId",
                table: "Customers",
                column: "CustomerAccountStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_InvoiceId",
                table: "InvoiceLineItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_ItemId",
                table: "InvoiceLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceTypeId",
                table: "Invoices",
                column: "InvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PackId",
                table: "Invoices",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SalesOrderId",
                table: "Invoices",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_StatusId",
                table: "Invoices",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemConversions_EquivalentUnitId",
                table: "ItemConversions",
                column: "EquivalentUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemConversions_ItemId",
                table: "ItemConversions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemConversions_SubItemId",
                table: "ItemConversions",
                column: "SubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemConversions_UnitId",
                table: "ItemConversions",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventories_ItemId",
                table: "ItemInventories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventories_WarehouseId",
                table: "ItemInventories",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_ItemId",
                table: "ItemPrices",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BrandId",
                table: "Items",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnitId",
                table: "Items",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_CategoryId",
                table: "ItemTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PackLineItems_ItemId",
                table: "PackLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PackLineItems_PackId",
                table: "PackLineItems",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_PackLineItems_PackTypeId",
                table: "PackLineItems",
                column: "PackTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PackReturnLineItems_ItemId",
                table: "PackReturnLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PackReturnLineItems_PackReturnId",
                table: "PackReturnLineItems",
                column: "PackReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_PackReturns_PackId",
                table: "PackReturns",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_Packs_PackTypeId",
                table: "Packs",
                column: "PackTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Packs_SalesOrderId",
                table: "Packs",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Packs_StatusId",
                table: "Packs",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalCountHistories_ItemId",
                table: "PhysicalCountHistories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalCountHistories_WarehouseId",
                table: "PhysicalCountHistories",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLineItems_ItemId",
                table: "PurchaseOrderLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLineItems_PurchaseOrderId",
                table: "PurchaseOrderLineItems",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNewLineItems_ItemId",
                table: "PurchaseOrderNewLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNewLineItems_PurchaseOrderId",
                table: "PurchaseOrderNewLineItems",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNewLineItems_RequestItemId",
                table: "PurchaseOrderNewLineItems",
                column: "RequestItemId");

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
                name: "IX_PurchaseOrderNews_DeliveryModeId",
                table: "PurchaseOrderNews",
                column: "DeliveryModeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_DisapproverId",
                table: "PurchaseOrderNews",
                column: "DisapproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_ResubmitId",
                table: "PurchaseOrderNews",
                column: "ResubmitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_StatusId",
                table: "PurchaseOrderNews",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_SupplierId",
                table: "PurchaseOrderNews",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_TermId",
                table: "PurchaseOrderNews",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_TypeId",
                table: "PurchaseOrderNews",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CurrencyId",
                table: "PurchaseOrders",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_DestinationId",
                table: "PurchaseOrders",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_StatusId",
                table: "PurchaseOrders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderTypes_RequestTypeId",
                table: "PurchaseOrderTypes",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivingLineItems_ItemId",
                table: "PurchaseReceivingLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivingLineItems_OrderItemId",
                table: "PurchaseReceivingLineItems",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivingLineItems_PurchaseReceivingId",
                table: "PurchaseReceivingLineItems",
                column: "PurchaseReceivingId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_ApproverId",
                table: "PurchaseReceivings",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_CreditTermId",
                table: "PurchaseReceivings",
                column: "CreditTermId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_CurrencyId",
                table: "PurchaseReceivings",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_DisapproverId",
                table: "PurchaseReceivings",
                column: "DisapproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_PurchaseOrderId",
                table: "PurchaseReceivings",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_ResubmitId",
                table: "PurchaseReceivings",
                column: "ResubmitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_StatusId",
                table: "PurchaseReceivings",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_SupplierId",
                table: "PurchaseReceivings",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_WarehouseId",
                table: "PurchaseReceivings",
                column: "WarehouseId");

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
                name: "IX_PurchaseRequests_DisapproverId",
                table: "PurchaseRequests",
                column: "DisapproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_PurposeId",
                table: "PurchaseRequests",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_RegionId",
                table: "PurchaseRequests",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_ResubmitId",
                table: "PurchaseRequests",
                column: "ResubmitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_StatusId",
                table: "PurchaseRequests",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_TypeId",
                table: "PurchaseRequests",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnLineItems_PurchaseOrderLineItemId",
                table: "PurchaseReturnLineItems",
                column: "PurchaseOrderLineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnLineItems_PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems",
                column: "PurchaseReceivingLineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnLineItems_PurchaseReturnId",
                table: "PurchaseReturnLineItems",
                column: "PurchaseReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNewLineItems_ItemId",
                table: "PurchaseReturnNewLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNewLineItems_PurchaseReturnId",
                table: "PurchaseReturnNewLineItems",
                column: "PurchaseReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNewLineItems_ReceivingItemId",
                table: "PurchaseReturnNewLineItems",
                column: "ReceivingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_ApproverId",
                table: "PurchaseReturnNews",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_CurrencyId",
                table: "PurchaseReturnNews",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_DisapproverId",
                table: "PurchaseReturnNews",
                column: "DisapproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_PurchaseReceiveId",
                table: "PurchaseReturnNews",
                column: "PurchaseReceiveId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_ResubmitId",
                table: "PurchaseReturnNews",
                column: "ResubmitId");

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

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturns_PurchaseOrderId",
                table: "PurchaseReturns",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveryMonitorings_ForwarderId",
                table: "SalesOrderDeliveryMonitorings",
                column: "ForwarderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveryMonitorings_SalesOrderId",
                table: "SalesOrderDeliveryMonitorings",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLineItems_ItemId",
                table: "SalesOrderLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLineItems_SalesOrderId",
                table: "SalesOrderLineItems",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_AgentId",
                table: "SalesOrders",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_CustomerId",
                table: "SalesOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_ForwarderId",
                table: "SalesOrders",
                column: "ForwarderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_PriceTypeId",
                table: "SalesOrders",
                column: "PriceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_PurchaseOrderId",
                table: "SalesOrders",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_StatusId",
                table: "SalesOrders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLineItems_InvoiceLineItemId",
                table: "SalesReturnLineItems",
                column: "InvoiceLineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLineItems_ItemId",
                table: "SalesReturnLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLineItems_SalesReturnId",
                table: "SalesReturnLineItems",
                column: "SalesReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_CustomerId",
                table: "SalesReturns",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_CustomerId",
                table: "ShippingAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRChecks_CustomerPaymentId",
                table: "SOACRChecks",
                column: "CustomerPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRLineItems_InvoiceId",
                table: "SOACRLineItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRLineItems_SOACRId",
                table: "SOACRLineItems",
                column: "SOACRId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRLineItemTBRs_SalesReturnLineItemId",
                table: "SOACRLineItemTBRs",
                column: "SalesReturnLineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRLineItemTBRs_SOACRLineItemId",
                table: "SOACRLineItemTBRs",
                column: "SOACRLineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRs_CustomerId",
                table: "SOACRs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRs_SOACRTypeId",
                table: "SOACRs",
                column: "SOACRTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCards_ItemId",
                table: "StockCards",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_BusinessStyleId",
                table: "Suppliers",
                column: "BusinessStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CurrencyId",
                table: "Suppliers",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_RegionId",
                table: "Suppliers",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItemDetails_ItemId",
                table: "TransferItemDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItemDetails_TransferItemId",
                table: "TransferItemDetails",
                column: "TransferItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_DestinationId",
                table: "TransferItems",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_SourceId",
                table: "TransferItems",
                column: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accesses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "CreditDebitMemos");

            migrationBuilder.DropTable(
                name: "CustomerPaymentDetails");

            migrationBuilder.DropTable(
                name: "HomeImages");

            migrationBuilder.DropTable(
                name: "ItemConversions");

            migrationBuilder.DropTable(
                name: "ItemInventories");

            migrationBuilder.DropTable(
                name: "ItemPrices");

            migrationBuilder.DropTable(
                name: "LocationCodes");

            migrationBuilder.DropTable(
                name: "PackLineItems");

            migrationBuilder.DropTable(
                name: "PackReturnLineItems");

            migrationBuilder.DropTable(
                name: "PhysicalCountHistories");

            migrationBuilder.DropTable(
                name: "PurchaseReturnLineItems");

            migrationBuilder.DropTable(
                name: "PurchaseReturnNewLineItems");

            migrationBuilder.DropTable(
                name: "ReasonOfReturns");

            migrationBuilder.DropTable(
                name: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropTable(
                name: "SalesOrderLineItems");

            migrationBuilder.DropTable(
                name: "ShippingAddresses");

            migrationBuilder.DropTable(
                name: "SOACRChecks");

            migrationBuilder.DropTable(
                name: "SOACRLineItemTBRs");

            migrationBuilder.DropTable(
                name: "StockCards");

            migrationBuilder.DropTable(
                name: "TransferItemDetails");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CreditDebitMemoTypes");

            migrationBuilder.DropTable(
                name: "PackReturns");

            migrationBuilder.DropTable(
                name: "PurchaseOrderLineItems");

            migrationBuilder.DropTable(
                name: "PurchaseReturns");

            migrationBuilder.DropTable(
                name: "PurchaseReceivingLineItems");

            migrationBuilder.DropTable(
                name: "PurchaseReturnNews");

            migrationBuilder.DropTable(
                name: "CustomerPayments");

            migrationBuilder.DropTable(
                name: "SalesReturnLineItems");

            migrationBuilder.DropTable(
                name: "SOACRLineItems");

            migrationBuilder.DropTable(
                name: "TransferItems");

            migrationBuilder.DropTable(
                name: "PurchaseOrderNewLineItems");

            migrationBuilder.DropTable(
                name: "PurchaseReceivings");

            migrationBuilder.DropTable(
                name: "InvoiceLineItems");

            migrationBuilder.DropTable(
                name: "SalesReturns");

            migrationBuilder.DropTable(
                name: "SOACRs");

            migrationBuilder.DropTable(
                name: "PurchaseRequestLineItems");

            migrationBuilder.DropTable(
                name: "PurchaseOrderNews");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "SOACRTypes");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "PurchaseRequests");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDeliveryModes");

            migrationBuilder.DropTable(
                name: "PurchaseOrderTypes");

            migrationBuilder.DropTable(
                name: "InvoiceTypes");

            migrationBuilder.DropTable(
                name: "Packs");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PurchaseRequestPurposes");

            migrationBuilder.DropTable(
                name: "PurchaseStatuses");

            migrationBuilder.DropTable(
                name: "PackTypes");

            migrationBuilder.DropTable(
                name: "SalesOrders");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "PurchaseRequestTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Forwarders");

            migrationBuilder.DropTable(
                name: "PriceTypes");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "CreditTerms");

            migrationBuilder.DropTable(
                name: "CustomerAccountStatuses");

            migrationBuilder.DropTable(
                name: "CustomerTypes");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "BusinessStyles");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
