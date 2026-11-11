IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [IsActive] bit NULL,
    [UserCreated] nvarchar(450) NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NULL,
    [DateModified] datetime2 NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(50) NULL,
    [LastName] nvarchar(50) NULL,
    [Nickname] nvarchar(50) NULL,
    [IsDarkMode] bit NULL,
    [IsActive] bit NULL,
    [UserCreated] nvarchar(450) NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NULL,
    [DateModified] datetime2 NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [BusinessStyles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_BusinessStyles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(10) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CreditDebitMemoTypes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_CreditDebitMemoTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CreditTerms] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [DaysToPay] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_CreditTerms] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Currencies] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(10) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Rate] decimal(18,5) NOT NULL,
    [EffectiveDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Currencies] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CustomerAccountStatuses] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_CustomerAccountStatuses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CustomerTypes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_CustomerTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Forwarders] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [Address] nvarchar(150) NOT NULL,
    [PhoneNumber] nvarchar(50) NOT NULL,
    [FaxNumber] nvarchar(50) NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Forwarders] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [HomeImages] (
    [Id] int NOT NULL IDENTITY,
    [File] nvarchar(MAX) NOT NULL,
    [Title] nvarchar(25) NULL,
    [Description] nvarchar(150) NULL,
    [DisplayOrder] int NOT NULL,
    [DisplayTime] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_HomeImages] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [InvoiceTypes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_InvoiceTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [LocationCodes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(10) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_LocationCodes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PackTypes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_PackTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pages] (
    [Id] int NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [HasView] bit NOT NULL,
    [HasAdd] bit NOT NULL,
    [HasEdit] bit NOT NULL,
    [HasDelete] bit NOT NULL,
    [HasApproveDisapprove] bit NOT NULL,
    [HasUploadDownload] bit NOT NULL,
    [HasPrint] bit NOT NULL,
    CONSTRAINT [PK_Pages] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PriceTypes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_PriceTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PurchaseOrderDeliveryModes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_PurchaseOrderDeliveryModes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PurchaseRequestTypes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_PurchaseRequestTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PurchaseStatuses] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_PurchaseStatuses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ReasonOfReturns] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(10) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_ReasonOfReturns] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Regions] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(10) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Regions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SOACRTypes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_SOACRTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Statuses] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Statuses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Units] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Units] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Warehouses] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(10) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [AddressLine1] nvarchar(250) NOT NULL,
    [AddressLine2] nvarchar(250) NULL,
    [ContactPerson] nvarchar(100) NOT NULL,
    [ContactNumber] nvarchar(50) NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Warehouses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AuditLogs] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [TableName] nvarchar(max) NOT NULL,
    [DateTime] datetime2 NOT NULL,
    [AffectedColumns] nvarchar(max) NOT NULL,
    [PrimaryKey] nvarchar(max) NOT NULL,
    [OldValues] nvarchar(max) NOT NULL,
    [NewValues] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_AuditLogs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AuditLogs_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ItemTypes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(10) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [CategoryId] int NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_ItemTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemTypes_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Accesses] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [PageId] int NOT NULL,
    [CanView] bit NOT NULL,
    [CanAdd] bit NOT NULL,
    [CanEdit] bit NOT NULL,
    [CanDelete] bit NOT NULL,
    [CanApproveDisapprove] bit NULL,
    [CanUploadDownload] bit NULL,
    [CanPrint] bit NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Accesses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Accesses_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Accesses_Pages_PageId] FOREIGN KEY ([PageId]) REFERENCES [Pages] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseOrderTypes] (
    [Id] int NOT NULL IDENTITY,
    [RequestTypeId] int NOT NULL,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_PurchaseOrderTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseOrderTypes_PurchaseRequestTypes_RequestTypeId] FOREIGN KEY ([RequestTypeId]) REFERENCES [PurchaseRequestTypes] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseRequestPurposes] (
    [Id] int NOT NULL IDENTITY,
    [TypeId] int NOT NULL,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_PurchaseRequestPurposes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseRequestPurposes_PurchaseRequestTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [PurchaseRequestTypes] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Areas] (
    [Id] int NOT NULL IDENTITY,
    [RegionId] int NOT NULL,
    [Code] nvarchar(10) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [CommissionRate] decimal(18,5) NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Areas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Areas_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Suppliers] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(50) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [TIN] nvarchar(12) NOT NULL,
    [AddressLine1] nvarchar(150) NOT NULL,
    [AddressLine2] nvarchar(150) NOT NULL,
    [OfficeTelNo] nvarchar(100) NOT NULL,
    [ContactPerson] nvarchar(100) NOT NULL,
    [RegionId] int NOT NULL,
    [BusinessStyleId] int NOT NULL,
    [Terms] nvarchar(100) NOT NULL,
    [PriceList] nvarchar(100) NOT NULL,
    [CurrencyId] int NOT NULL,
    [VAT] nvarchar(12) NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Suppliers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Suppliers_BusinessStyles_BusinessStyleId] FOREIGN KEY ([BusinessStyleId]) REFERENCES [BusinessStyles] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Suppliers_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currencies] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Suppliers_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [TransferItems] (
    [Id] int NOT NULL IDENTITY,
    [SourceId] int NOT NULL,
    [DestinationId] int NOT NULL,
    [Remarks] nvarchar(250) NOT NULL,
    [TransferDate] datetime2 NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_TransferItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransferItems_Warehouses_DestinationId] FOREIGN KEY ([DestinationId]) REFERENCES [Warehouses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransferItems_Warehouses_SourceId] FOREIGN KEY ([SourceId]) REFERENCES [Warehouses] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Brands] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(10) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [ItemTypeId] int NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Brands] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Brands_ItemTypes_ItemTypeId] FOREIGN KEY ([ItemTypeId]) REFERENCES [ItemTypes] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseRequests] (
    [Id] int NOT NULL IDENTITY,
    [DocumentNumber] nvarchar(max) NOT NULL,
    [TypeId] int NOT NULL,
    [PurposeId] int NULL,
    [RegionId] int NULL,
    [RequestDate] datetime2 NOT NULL,
    [PurposeDefinition] nvarchar(250) NULL,
    [Remarks] nvarchar(100) NOT NULL,
    [StatusId] int NULL,
    [Approver1Id] nvarchar(450) NULL,
    [Approver2Id] nvarchar(450) NULL,
    [DisapproverId] nvarchar(450) NULL,
    [DisapprovedReason] nvarchar(max) NULL,
    [ResubmitId] int NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseRequests] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseRequests_AspNetUsers_Approver1Id] FOREIGN KEY ([Approver1Id]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_PurchaseRequests_AspNetUsers_Approver2Id] FOREIGN KEY ([Approver2Id]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_PurchaseRequests_AspNetUsers_DisapproverId] FOREIGN KEY ([DisapproverId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_PurchaseRequests_PurchaseRequestPurposes_PurposeId] FOREIGN KEY ([PurposeId]) REFERENCES [PurchaseRequestPurposes] ([Id]),
    CONSTRAINT [FK_PurchaseRequests_PurchaseRequests_ResubmitId] FOREIGN KEY ([ResubmitId]) REFERENCES [PurchaseRequests] ([Id]),
    CONSTRAINT [FK_PurchaseRequests_PurchaseRequestTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [PurchaseRequestTypes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseRequests_PurchaseStatuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [PurchaseStatuses] ([Id]),
    CONSTRAINT [FK_PurchaseRequests_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id])
);
GO

CREATE TABLE [Agents] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(50) NOT NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [MiddleName] nvarchar(50) NULL,
    [LastName] nvarchar(50) NOT NULL,
    [DateHired] datetime2 NOT NULL,
    [CommissionRate] decimal(18,5) NOT NULL,
    [AreaId] int NOT NULL,
    [IsActive] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Agents] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Agents_Areas_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [Areas] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseOrderNews] (
    [Id] int NOT NULL IDENTITY,
    [DocumentNumber] nvarchar(max) NOT NULL,
    [TypeId] int NOT NULL,
    [SupplierId] int NOT NULL,
    [TermId] int NOT NULL,
    [DeliveryModeId] int NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [DeliveryAddressId] int NOT NULL,
    [CurrencyId] int NOT NULL,
    [CurrencyRate] decimal(18,2) NOT NULL,
    [CanvasSummaryReference] nvarchar(50) NOT NULL,
    [Remarks] nvarchar(250) NOT NULL,
    [OtherCharges] decimal(18,2) NOT NULL,
    [Discount1] decimal(18,2) NOT NULL,
    [Discount2] decimal(18,2) NOT NULL,
    [StatusId] int NULL,
    [ApproverId] nvarchar(450) NULL,
    [DisapproverId] nvarchar(450) NULL,
    [DisapprovedReason] nvarchar(max) NULL,
    [ResubmitId] int NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseOrderNews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseOrderNews_AspNetUsers_ApproverId] FOREIGN KEY ([ApproverId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_PurchaseOrderNews_AspNetUsers_DisapproverId] FOREIGN KEY ([DisapproverId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_PurchaseOrderNews_CreditTerms_TermId] FOREIGN KEY ([TermId]) REFERENCES [CreditTerms] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseOrderNews_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currencies] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseOrderNews_PurchaseOrderDeliveryModes_DeliveryModeId] FOREIGN KEY ([DeliveryModeId]) REFERENCES [PurchaseOrderDeliveryModes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseOrderNews_PurchaseOrderNews_ResubmitId] FOREIGN KEY ([ResubmitId]) REFERENCES [PurchaseOrderNews] ([Id]),
    CONSTRAINT [FK_PurchaseOrderNews_PurchaseOrderTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [PurchaseOrderTypes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseOrderNews_PurchaseStatuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [PurchaseStatuses] ([Id]),
    CONSTRAINT [FK_PurchaseOrderNews_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseOrderNews_Warehouses_DeliveryAddressId] FOREIGN KEY ([DeliveryAddressId]) REFERENCES [Warehouses] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseOrders] (
    [Id] int NOT NULL IDENTITY,
    [SupplierId] int NOT NULL,
    [PONumber] nvarchar(50) NOT NULL,
    [ApprovedBy] nvarchar(50) NULL,
    [DestinationId] int NOT NULL,
    [CurrencyId] int NOT NULL,
    [StatusId] int NOT NULL,
    [TotalPurchaseAmount] decimal(18,2) NULL,
    [ReferenceNumber] nvarchar(100) NULL,
    [CurrencyRate] decimal(18,2) NOT NULL,
    [ReplenishDate] datetime2 NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseOrders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseOrders_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currencies] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseOrders_Statuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Statuses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseOrders_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseOrders_Warehouses_DestinationId] FOREIGN KEY ([DestinationId]) REFERENCES [Warehouses] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Items] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(50) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [PartNumber] nvarchar(50) NULL,
    [InterchangeNumber] nvarchar(50) NULL,
    [ModelNumber] nvarchar(50) NULL,
    [Size] nvarchar(50) NOT NULL,
    [UnitId] int NOT NULL,
    [CategoryId] int NOT NULL,
    [ItemTypeId] int NOT NULL,
    [BrandId] int NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Items_Brands_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Brands] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Items_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Items_ItemTypes_ItemTypeId] FOREIGN KEY ([ItemTypeId]) REFERENCES [ItemTypes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Items_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(50) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [IsActive] bit NOT NULL,
    [Address] nvarchar(MAX) NOT NULL,
    [AgentId] int NOT NULL,
    [AreaId] int NOT NULL,
    [Email] nvarchar(100) NULL,
    [CommissionRate] decimal(18,2) NULL,
    [CommodityQuantity] int NOT NULL,
    [ContactNumber] nvarchar(100) NULL,
    [ContactPerson] nvarchar(150) NULL,
    [CreditLimit] decimal(18,2) NOT NULL,
    [CreditTermId] int NOT NULL,
    [CustomerAccountStatusId] int NOT NULL,
    [CustomerTypeId] int NOT NULL,
    [FaxNumber] nvarchar(100) NULL,
    [OutstandingBalance] decimal(18,2) NOT NULL,
    [Remarks] nvarchar(250) NULL,
    [RequiredDeliveryDate] datetime2 NOT NULL,
    [TIN] nvarchar(50) NULL,
    [VAT] nvarchar(50) NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Customers_Agents_AgentId] FOREIGN KEY ([AgentId]) REFERENCES [Agents] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Customers_Areas_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [Areas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Customers_CreditTerms_CreditTermId] FOREIGN KEY ([CreditTermId]) REFERENCES [CreditTerms] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Customers_CustomerAccountStatuses_CustomerAccountStatusId] FOREIGN KEY ([CustomerAccountStatusId]) REFERENCES [CustomerAccountStatuses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Customers_CustomerTypes_CustomerTypeId] FOREIGN KEY ([CustomerTypeId]) REFERENCES [CustomerTypes] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseReceivings] (
    [Id] int NOT NULL IDENTITY,
    [DocumentNumber] nvarchar(max) NOT NULL,
    [PurchaseOrderId] int NOT NULL,
    [ReceiveDate] datetime2 NOT NULL,
    [WarehouseId] int NOT NULL,
    [DeliveryReceiptNumber] nvarchar(100) NOT NULL,
    [SalesInvoiceNumber] nvarchar(100) NOT NULL,
    [Remarks] nvarchar(max) NOT NULL,
    [StatusId] int NULL,
    [ApproverId] nvarchar(450) NULL,
    [DisapproverId] nvarchar(450) NULL,
    [DisapprovedReason] nvarchar(max) NULL,
    [ResubmitId] int NULL,
    [CreditTermId] int NULL,
    [CurrencyId] int NULL,
    [SupplierId] int NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseReceivings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseReceivings_AspNetUsers_ApproverId] FOREIGN KEY ([ApproverId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_PurchaseReceivings_AspNetUsers_DisapproverId] FOREIGN KEY ([DisapproverId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_PurchaseReceivings_CreditTerms_CreditTermId] FOREIGN KEY ([CreditTermId]) REFERENCES [CreditTerms] ([Id]),
    CONSTRAINT [FK_PurchaseReceivings_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currencies] ([Id]),
    CONSTRAINT [FK_PurchaseReceivings_PurchaseOrderNews_PurchaseOrderId] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [PurchaseOrderNews] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseReceivings_PurchaseReceivings_ResubmitId] FOREIGN KEY ([ResubmitId]) REFERENCES [PurchaseReceivings] ([Id]),
    CONSTRAINT [FK_PurchaseReceivings_PurchaseStatuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [PurchaseStatuses] ([Id]),
    CONSTRAINT [FK_PurchaseReceivings_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]),
    CONSTRAINT [FK_PurchaseReceivings_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseReturns] (
    [Id] int NOT NULL IDENTITY,
    [PurchaseOrderId] int NOT NULL,
    [Remarks] nvarchar(250) NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseReturns] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseReturns_PurchaseOrders_PurchaseOrderId] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [PurchaseOrders] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ItemConversions] (
    [Id] int NOT NULL IDENTITY,
    [ItemId] int NOT NULL,
    [Quantity] int NOT NULL,
    [SubItemId] int NULL,
    [UnitId] int NULL,
    [EquivalentQuantity] int NULL,
    [EquivalentUnitId] int NULL,
    [OrderNo] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_ItemConversions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemConversions_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ItemConversions_Items_SubItemId] FOREIGN KEY ([SubItemId]) REFERENCES [Items] ([Id]),
    CONSTRAINT [FK_ItemConversions_Units_EquivalentUnitId] FOREIGN KEY ([EquivalentUnitId]) REFERENCES [Units] ([Id]),
    CONSTRAINT [FK_ItemConversions_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([Id])
);
GO

CREATE TABLE [ItemInventories] (
    [Id] int NOT NULL IDENTITY,
    [WarehouseId] int NOT NULL,
    [ItemId] int NOT NULL,
    [Quantity] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_ItemInventories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemInventories_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ItemInventories_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ItemPrices] (
    [Id] int NOT NULL IDENTITY,
    [ItemId] int NOT NULL,
    [NetSellingPrice] decimal(18,2) NOT NULL,
    [RetailSellingPrice] decimal(18,2) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_ItemPrices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemPrices_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PhysicalCountHistories] (
    [Id] int NOT NULL IDENTITY,
    [ItemId] int NOT NULL,
    [WarehouseId] int NOT NULL,
    [QuantityFrom] int NOT NULL,
    [QuantityTo] int NOT NULL,
    [Remarks] nvarchar(250) NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PhysicalCountHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PhysicalCountHistories_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PhysicalCountHistories_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseOrderLineItems] (
    [Id] int NOT NULL IDENTITY,
    [PurchaseOrderId] int NOT NULL,
    [ItemId] int NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [Discount1] decimal(18,2) NOT NULL,
    [Discount2] decimal(18,2) NOT NULL,
    [Discount3] decimal(18,2) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseOrderLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseOrderLineItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseOrderLineItems_PurchaseOrders_PurchaseOrderId] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [PurchaseOrders] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [StockCards] (
    [Id] int NOT NULL IDENTITY,
    [ItemId] int NOT NULL,
    [In] int NULL,
    [Out] int NULL,
    [Balance] int NULL,
    [InOutDate] datetime2 NULL,
    [Remarks] nvarchar(250) NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_StockCards] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StockCards_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [TransferItemDetails] (
    [Id] int NOT NULL IDENTITY,
    [TransferItemId] int NOT NULL,
    [ItemId] int NOT NULL,
    [Quantity] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_TransferItemDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransferItemDetails_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TransferItemDetails_TransferItems_TransferItemId] FOREIGN KEY ([TransferItemId]) REFERENCES [TransferItems] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Cheques] (
    [Id] int NOT NULL IDENTITY,
    [CollectDate] datetime2 NOT NULL,
    [RefNo] nvarchar(10) NOT NULL,
    [CustomerId] int NOT NULL,
    [CheckDate] datetime2 NULL,
    [Check] nvarchar(10) NOT NULL,
    [Branch] nvarchar(50) NULL,
    [CheckNo] nvarchar(10) NULL,
    [Amount] decimal(18,2) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Cheques] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cheques_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SalesOrders] (
    [Id] int NOT NULL IDENTITY,
    [AgentId] int NOT NULL,
    [CustomerId] int NOT NULL,
    [SalesOrderNumber] nvarchar(50) NOT NULL,
    [ForwarderId] int NOT NULL,
    [StatusId] int NOT NULL,
    [PurchaseOrderId] int NULL,
    [DeclaredValue] decimal(18,2) NULL,
    [PriceTypeId] int NOT NULL,
    [TotalAmount] decimal(18,2) NOT NULL,
    [QuantityPacked] int NOT NULL,
    [QuantityInvoiced] int NOT NULL,
    [QuantityOrdered] int NOT NULL,
    [SalesOrderDate] datetime2 NOT NULL,
    [ApprovedBy] nvarchar(50) NULL,
    [ApprovedDate] datetime2 NULL,
    [DisapprovedBy] nvarchar(50) NULL,
    [DisapprovedDate] datetime2 NULL,
    [Remarks] nvarchar(150) NULL,
    [DisapprovedReason] nvarchar(150) NULL,
    [ResubmittedSalesOrderId] int NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SalesOrders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SalesOrders_Agents_AgentId] FOREIGN KEY ([AgentId]) REFERENCES [Agents] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SalesOrders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SalesOrders_Forwarders_ForwarderId] FOREIGN KEY ([ForwarderId]) REFERENCES [Forwarders] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SalesOrders_PriceTypes_PriceTypeId] FOREIGN KEY ([PriceTypeId]) REFERENCES [PriceTypes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SalesOrders_PurchaseOrders_PurchaseOrderId] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [PurchaseOrders] ([Id]),
    CONSTRAINT [FK_SalesOrders_Statuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Statuses] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SalesReturns] (
    [Id] int NOT NULL IDENTITY,
    [SalesReturnNumber] nvarchar(50) NOT NULL,
    [CustomerId] int NOT NULL,
    [Remarks] nvarchar(250) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SalesReturns] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SalesReturns_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ShippingAddresses] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [Address] nvarchar(250) NOT NULL,
    [PhoneNumber] nvarchar(50) NOT NULL,
    [FaxNumber] nvarchar(50) NOT NULL,
    [ContactPerson] nvarchar(100) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_ShippingAddresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ShippingAddresses_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SOACRs] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [SOAFrom] datetime2 NULL,
    [SOATo] datetime2 NULL,
    [AmountToPay] decimal(18,2) NOT NULL,
    [SOACRTypeId] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SOACRs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SOACRs_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SOACRs_SOACRTypes_SOACRTypeId] FOREIGN KEY ([SOACRTypeId]) REFERENCES [SOACRTypes] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseReturnNews] (
    [Id] int NOT NULL IDENTITY,
    [DocumentNumber] nvarchar(max) NOT NULL,
    [PurchaseReceiveId] int NOT NULL,
    [ReturnDate] datetime2 NOT NULL,
    [Remarks] nvarchar(max) NOT NULL,
    [Discount1] decimal(18,2) NOT NULL,
    [Discount2] decimal(18,2) NOT NULL,
    [StatusId] int NULL,
    [ApproverId] nvarchar(450) NULL,
    [DisapproverId] nvarchar(450) NULL,
    [DisapprovedReason] nvarchar(max) NULL,
    [ResubmitId] int NULL,
    [CurrencyId] int NULL,
    [SupplierId] int NULL,
    [WarehouseId] int NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseReturnNews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseReturnNews_AspNetUsers_ApproverId] FOREIGN KEY ([ApproverId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_PurchaseReturnNews_AspNetUsers_DisapproverId] FOREIGN KEY ([DisapproverId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_PurchaseReturnNews_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currencies] ([Id]),
    CONSTRAINT [FK_PurchaseReturnNews_PurchaseReceivings_PurchaseReceiveId] FOREIGN KEY ([PurchaseReceiveId]) REFERENCES [PurchaseReceivings] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseReturnNews_PurchaseReturnNews_ResubmitId] FOREIGN KEY ([ResubmitId]) REFERENCES [PurchaseReturnNews] ([Id]),
    CONSTRAINT [FK_PurchaseReturnNews_PurchaseStatuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [PurchaseStatuses] ([Id]),
    CONSTRAINT [FK_PurchaseReturnNews_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]),
    CONSTRAINT [FK_PurchaseReturnNews_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id])
);
GO

CREATE TABLE [Packs] (
    [Id] int NOT NULL IDENTITY,
    [SalesOrderId] int NOT NULL,
    [StatusId] int NOT NULL,
    [TotalAmount] decimal(18,2) NULL,
    [TotalQuantity] int NOT NULL,
    [QuantityInvoiced] int NOT NULL,
    [PackDate] datetime2 NOT NULL,
    [ApprovedBy] nvarchar(50) NULL,
    [ApprovedDate] datetime2 NULL,
    [DisapprovedBy] nvarchar(50) NULL,
    [DisapprovedDate] datetime2 NULL,
    [Remarks] nvarchar(150) NULL,
    [PackTypeId] int NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Packs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Packs_PackTypes_PackTypeId] FOREIGN KEY ([PackTypeId]) REFERENCES [PackTypes] ([Id]),
    CONSTRAINT [FK_Packs_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Packs_Statuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Statuses] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseRequestLineItems] (
    [Id] int NOT NULL IDENTITY,
    [PurchaseRequestId] int NOT NULL,
    [LineNumber] int NOT NULL,
    [SalesOrderId] int NULL,
    [ItemId] int NULL,
    [ItemDescription] nvarchar(100) NOT NULL,
    [StockOnHand] int NOT NULL,
    [Quantity] int NOT NULL,
    [UnitId] int NOT NULL,
    [Purpose] nvarchar(100) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseRequestLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseRequestLineItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]),
    CONSTRAINT [FK_PurchaseRequestLineItems_PurchaseRequests_PurchaseRequestId] FOREIGN KEY ([PurchaseRequestId]) REFERENCES [PurchaseRequests] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseRequestLineItems_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]),
    CONSTRAINT [FK_PurchaseRequestLineItems_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SalesOrderDeliveryMonitorings] (
    [Id] int NOT NULL IDENTITY,
    [SalesOrderId] int NOT NULL,
    [ForwarderId] int NOT NULL,
    [Waybill] nvarchar(150) NOT NULL,
    [Remarks] nvarchar(250) NOT NULL,
    [DatePickup] datetime2 NULL,
    [DateDeliver] datetime2 NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SalesOrderDeliveryMonitorings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SalesOrderDeliveryMonitorings_Forwarders_ForwarderId] FOREIGN KEY ([ForwarderId]) REFERENCES [Forwarders] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SalesOrderDeliveryMonitorings_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SalesOrderLineItems] (
    [Id] int NOT NULL IDENTITY,
    [SalesOrderId] int NOT NULL,
    [ItemId] int NOT NULL,
    [Quantity] int NOT NULL,
    [ServedQuantity] int NOT NULL,
    [UnservedQuantity] int NOT NULL,
    [PackedQuantity] int NOT NULL,
    [InvoicedQuantity] int NOT NULL,
    [SellingPrice] decimal(18,2) NOT NULL,
    [Discount1] decimal(18,2) NOT NULL,
    [Discount2] decimal(18,2) NOT NULL,
    [IsServed] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SalesOrderLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SalesOrderLineItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SalesOrderLineItems_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [CustomerPayments] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [CRNumber] nvarchar(50) NOT NULL,
    [OverPayment] decimal(18,2) NOT NULL,
    [Tax] decimal(18,2) NOT NULL,
    [Discount] decimal(18,2) NOT NULL,
    [TKTSolicitation] decimal(18,2) NOT NULL,
    [OffSet] decimal(18,2) NOT NULL,
    [BackLoad] decimal(18,2) NOT NULL,
    [Cash] decimal(18,2) NOT NULL,
    [CashAdvance] decimal(18,2) NOT NULL,
    [Remarks] nvarchar(500) NOT NULL,
    [SOACRId] int NOT NULL,
    [TotalAmountPaid] decimal(18,2) NOT NULL,
    [TotalInvoiceAmount] decimal(18,2) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_CustomerPayments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerPayments_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerPayments_SOACRs_SOACRId] FOREIGN KEY ([SOACRId]) REFERENCES [SOACRs] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Invoices] (
    [Id] int NOT NULL IDENTITY,
    [SalesOrderId] int NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [InvoiceNumber] nvarchar(50) NOT NULL,
    [InvoiceTypeId] int NOT NULL,
    [StatusId] int NOT NULL,
    [PackId] int NOT NULL,
    [ControlNumber] nvarchar(50) NOT NULL,
    [PaidAmount] decimal(18,2) NOT NULL,
    [IsPaid] bit NOT NULL,
    [IsGenerated] bit NOT NULL,
    [Remarks] nvarchar(250) NOT NULL,
    [InvoiceDate] datetime2 NOT NULL,
    [ApprovedBy] nvarchar(50) NULL,
    [ApprovedDate] datetime2 NULL,
    [DisapprovedBy] nvarchar(50) NULL,
    [DisapprovedDate] datetime2 NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Invoices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Invoices_InvoiceTypes_InvoiceTypeId] FOREIGN KEY ([InvoiceTypeId]) REFERENCES [InvoiceTypes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Invoices_Packs_PackId] FOREIGN KEY ([PackId]) REFERENCES [Packs] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Invoices_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Invoices_Statuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Statuses] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PackLineItems] (
    [Id] int NOT NULL IDENTITY,
    [PackId] int NOT NULL,
    [ItemId] int NOT NULL,
    [Quantity] int NOT NULL,
    [BoxNumber] int NOT NULL,
    [PackTypeId] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PackLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PackLineItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PackLineItems_Packs_PackId] FOREIGN KEY ([PackId]) REFERENCES [Packs] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PackLineItems_PackTypes_PackTypeId] FOREIGN KEY ([PackTypeId]) REFERENCES [PackTypes] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PackReturns] (
    [Id] int NOT NULL IDENTITY,
    [PackId] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PackReturns] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PackReturns_Packs_PackId] FOREIGN KEY ([PackId]) REFERENCES [Packs] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseOrderNewLineItems] (
    [Id] int NOT NULL IDENTITY,
    [PurchaseOrderId] int NOT NULL,
    [LineNumber] int NOT NULL,
    [RequestItemId] int NULL,
    [Quantity] int NOT NULL,
    [UnitCost] decimal(18,2) NOT NULL,
    [ItemId] int NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseOrderNewLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseOrderNewLineItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]),
    CONSTRAINT [FK_PurchaseOrderNewLineItems_PurchaseOrderNews_PurchaseOrderId] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [PurchaseOrderNews] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseOrderNewLineItems_PurchaseRequestLineItems_RequestItemId] FOREIGN KEY ([RequestItemId]) REFERENCES [PurchaseRequestLineItems] ([Id])
);
GO

CREATE TABLE [SOACRChecks] (
    [Id] int NOT NULL IDENTITY,
    [CustomerPaymentId] int NOT NULL,
    [BankName] nvarchar(50) NOT NULL,
    [BankBranch] nvarchar(50) NOT NULL,
    [CheckNumber] nvarchar(50) NOT NULL,
    [CheckAmount] decimal(18,2) NOT NULL,
    [CheckDate] datetime2 NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SOACRChecks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SOACRChecks_CustomerPayments_CustomerPaymentId] FOREIGN KEY ([CustomerPaymentId]) REFERENCES [CustomerPayments] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [CreditDebitMemos] (
    [Id] int NOT NULL IDENTITY,
    [MemoNumber] nvarchar(50) NOT NULL,
    [InvoiceId] int NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [CreditDebitMemoTypeId] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_CreditDebitMemos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CreditDebitMemos_CreditDebitMemoTypes_CreditDebitMemoTypeId] FOREIGN KEY ([CreditDebitMemoTypeId]) REFERENCES [CreditDebitMemoTypes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CreditDebitMemos_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [CustomerPaymentDetails] (
    [Id] int NOT NULL IDENTITY,
    [CustomerPaymentId] int NOT NULL,
    [InvoiceId] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_CustomerPaymentDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerPaymentDetails_CustomerPayments_CustomerPaymentId] FOREIGN KEY ([CustomerPaymentId]) REFERENCES [CustomerPayments] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerPaymentDetails_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [InvoiceLineItems] (
    [Id] int NOT NULL IDENTITY,
    [InvoiceId] int NOT NULL,
    [ItemId] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [Quantity] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_InvoiceLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InvoiceLineItems_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_InvoiceLineItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SOACRLineItems] (
    [Id] int NOT NULL IDENTITY,
    [SOACRId] int NOT NULL,
    [InvoiceId] int NOT NULL,
    [InvoiceAmount] decimal(18,2) NOT NULL,
    [PaidAmount] decimal(18,2) NOT NULL,
    [CreditAmount] decimal(18,2) NOT NULL,
    [DebitAmount] decimal(18,2) NOT NULL,
    [FreightCost] decimal(18,2) NOT NULL,
    [FreightWaybill] nvarchar(50) NULL,
    [IsPaid] bit NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SOACRLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SOACRLineItems_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SOACRLineItems_SOACRs_SOACRId] FOREIGN KEY ([SOACRId]) REFERENCES [SOACRs] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PackReturnLineItems] (
    [Id] int NOT NULL IDENTITY,
    [PackReturnId] int NOT NULL,
    [Quantity] int NOT NULL,
    [ItemId] int NOT NULL,
    [BoxNumber] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PackReturnLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PackReturnLineItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PackReturnLineItems_PackReturns_PackReturnId] FOREIGN KEY ([PackReturnId]) REFERENCES [PackReturns] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseReceivingLineItems] (
    [Id] int NOT NULL IDENTITY,
    [PurchaseReceivingId] int NOT NULL,
    [OrderItemId] int NULL,
    [Quantity] int NOT NULL,
    [ItemId] int NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseReceivingLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseReceivingLineItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]),
    CONSTRAINT [FK_PurchaseReceivingLineItems_PurchaseOrderNewLineItems_OrderItemId] FOREIGN KEY ([OrderItemId]) REFERENCES [PurchaseOrderNewLineItems] ([Id]),
    CONSTRAINT [FK_PurchaseReceivingLineItems_PurchaseReceivings_PurchaseReceivingId] FOREIGN KEY ([PurchaseReceivingId]) REFERENCES [PurchaseReceivings] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SalesReturnLineItems] (
    [Id] int NOT NULL IDENTITY,
    [SalesReturnId] int NOT NULL,
    [InvoiceLineItemId] int NULL,
    [ItemId] int NOT NULL,
    [Quantity] int NOT NULL,
    [SellingPrice] decimal(18,2) NOT NULL,
    [Discount1] decimal(18,2) NOT NULL,
    [Discount2] decimal(18,2) NOT NULL,
    [IsDefective] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SalesReturnLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SalesReturnLineItems_InvoiceLineItems_InvoiceLineItemId] FOREIGN KEY ([InvoiceLineItemId]) REFERENCES [InvoiceLineItems] ([Id]),
    CONSTRAINT [FK_SalesReturnLineItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SalesReturnLineItems_SalesReturns_SalesReturnId] FOREIGN KEY ([SalesReturnId]) REFERENCES [SalesReturns] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseReturnLineItems] (
    [Id] int NOT NULL IDENTITY,
    [PurchaseOrderLineItemId] int NOT NULL,
    [PurchaseReturnId] int NOT NULL,
    [Quantity] int NOT NULL,
    [PurchaseReceivingLineItemId] int NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseReturnLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseReturnLineItems_PurchaseOrderLineItems_PurchaseOrderLineItemId] FOREIGN KEY ([PurchaseOrderLineItemId]) REFERENCES [PurchaseOrderLineItems] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PurchaseReturnLineItems_PurchaseReceivingLineItems_PurchaseReceivingLineItemId] FOREIGN KEY ([PurchaseReceivingLineItemId]) REFERENCES [PurchaseReceivingLineItems] ([Id]),
    CONSTRAINT [FK_PurchaseReturnLineItems_PurchaseReturns_PurchaseReturnId] FOREIGN KEY ([PurchaseReturnId]) REFERENCES [PurchaseReturns] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PurchaseReturnNewLineItems] (
    [Id] int NOT NULL IDENTITY,
    [PurchaseReturnId] int NOT NULL,
    [ReceivingItemId] int NULL,
    [Quantity] int NOT NULL,
    [ItemId] int NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_PurchaseReturnNewLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseReturnNewLineItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]),
    CONSTRAINT [FK_PurchaseReturnNewLineItems_PurchaseReceivingLineItems_ReceivingItemId] FOREIGN KEY ([ReceivingItemId]) REFERENCES [PurchaseReceivingLineItems] ([Id]),
    CONSTRAINT [FK_PurchaseReturnNewLineItems_PurchaseReturnNews_PurchaseReturnId] FOREIGN KEY ([PurchaseReturnId]) REFERENCES [PurchaseReturnNews] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SOACRLineItemTBRs] (
    [Id] int NOT NULL IDENTITY,
    [SalesReturnLineItemId] int NOT NULL,
    [SOACRLineItemId] int NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SOACRLineItemTBRs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SOACRLineItemTBRs_SalesReturnLineItems_SalesReturnLineItemId] FOREIGN KEY ([SalesReturnLineItemId]) REFERENCES [SalesReturnLineItems] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SOACRLineItemTBRs_SOACRLineItems_SOACRLineItemId] FOREIGN KEY ([SOACRLineItemId]) REFERENCES [SOACRLineItems] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'DateCreated', N'DateModified', N'Discriminator', N'IsActive', N'Name', N'NormalizedName', N'UserCreated', N'UserModified') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [DateCreated], [DateModified], [Discriminator], [IsActive], [Name], [NormalizedName], [UserCreated], [UserModified])
VALUES (N'0BFA2C03-A5D4-43A6-A797-8CFF1A282080', NULL, '2022-01-01T00:00:00.0000000', NULL, N'Role', CAST(1 AS bit), N'ACCOUNTING', N'ACCOUNTING', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(N'1E0DA252-5984-43F4-B23C-F755B0E739B5', NULL, '2022-01-01T00:00:00.0000000', NULL, N'Role', CAST(1 AS bit), N'INVENTORY MANAGER', N'INVENTORY MANAGER', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(N'3C76B2D8-55D9-4BF6-9A6E-E1F498E73337', NULL, '2022-01-01T00:00:00.0000000', NULL, N'Role', CAST(1 AS bit), N'INVOICING CLERK', N'INVOICING CLERK', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', NULL, '2022-01-01T00:00:00.0000000', NULL, N'Role', CAST(1 AS bit), N'SYSTEM ADMIN', N'SYSTEM ADMIN', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(N'826FC15C-21E5-4BC5-A580-5F52E054299B', NULL, '2022-01-01T00:00:00.0000000', NULL, N'Role', CAST(1 AS bit), N'SALES DIRECTOR', N'SALES DIRECTOR', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(N'851E0BE3-8728-48EB-BD65-89DC38DB4E01', NULL, '2022-01-01T00:00:00.0000000', NULL, N'Role', CAST(1 AS bit), N'TELEMARKETER', N'TELEMARKETER', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'DateCreated', N'DateModified', N'Discriminator', N'IsActive', N'Name', N'NormalizedName', N'UserCreated', N'UserModified') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'DateCreated', N'DateModified', N'Discriminator', N'Email', N'EmailConfirmed', N'FirstName', N'IsActive', N'IsDarkMode', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'Nickname', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserCreated', N'UserModified', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] ON;
INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [DateCreated], [DateModified], [Discriminator], [Email], [EmailConfirmed], [FirstName], [IsActive], [IsDarkMode], [LastName], [LockoutEnabled], [LockoutEnd], [Nickname], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserCreated], [UserModified], [UserName])
VALUES (N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C', 0, N'02c6ad64-5aa8-4110-8d05-9f859437e0b8', '2022-01-01T00:00:00.0000000', NULL, N'User', NULL, CAST(0 AS bit), N'GMG', CAST(1 AS bit), CAST(0 AS bit), N'Administrator', CAST(0 AS bit), NULL, N'GMG', NULL, N'admin-gmg', N'AQAAAAIAAYagAAAAEIxNudJwdxheutc4JN9KfMSpGSt8hhuEygRhiXxDVlq5q7HamL+O/iOYg/qDOUslqQ==', NULL, CAST(0 AS bit), N'25d1f9a3-a646-4b1c-a8f9-c17892d9dfb3', CAST(0 AS bit), N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL, N'admin-gmg'),
(N'CDE19596-2C9E-4ECE-837C-85950E9E907D', 0, N'a0168a97-f8b9-407a-9f4c-ce0de55f1732', '2022-01-01T00:00:00.0000000', NULL, N'User', NULL, CAST(0 AS bit), N'MISNet', CAST(1 AS bit), CAST(0 AS bit), N'Administrator', CAST(0 AS bit), NULL, N'MISNet', NULL, N'admin-misnet', N'AQAAAAIAAYagAAAAECK0GcOeThN2oM6TpnnU3+9xCJpbBMbp2NYO3wy6KMO8vwNm5PcqhXDYWPmXfBV71Q==', NULL, CAST(0 AS bit), N'177d6ff9-ba7d-45dd-ab36-bdd08fca958a', CAST(0 AS bit), N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL, N'admin-misnet');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'DateCreated', N'DateModified', N'Discriminator', N'Email', N'EmailConfirmed', N'FirstName', N'IsActive', N'IsDarkMode', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'Nickname', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserCreated', N'UserModified', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[CreditDebitMemoTypes]'))
    SET IDENTITY_INSERT [CreditDebitMemoTypes] ON;
INSERT INTO [CreditDebitMemoTypes] ([Id], [Code], [Name])
VALUES (1, N'CR', N'Credit'),
(2, N'DB', N'Debit');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[CreditDebitMemoTypes]'))
    SET IDENTITY_INSERT [CreditDebitMemoTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[CustomerAccountStatuses]'))
    SET IDENTITY_INSERT [CustomerAccountStatuses] ON;
INSERT INTO [CustomerAccountStatuses] ([Id], [Code], [Name])
VALUES (1, N'RED FLAG', N'RED FLAG'),
(2, N'HOLD', N'HOLD'),
(3, N'WATCH LIST', N'WATCH LIST'),
(4, N'GOOD', N'GOOD');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[CustomerAccountStatuses]'))
    SET IDENTITY_INSERT [CustomerAccountStatuses] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[CustomerTypes]'))
    SET IDENTITY_INSERT [CustomerTypes] ON;
INSERT INTO [CustomerTypes] ([Id], [Code], [Name])
VALUES (1, N'LuzMM', N'Luzon - Metro Manila'),
(2, N'VisMin', N'Visayas - Mindanao');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[CustomerTypes]'))
    SET IDENTITY_INSERT [CustomerTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[InvoiceTypes]'))
    SET IDENTITY_INSERT [InvoiceTypes] ON;
INSERT INTO [InvoiceTypes] ([Id], [Code], [Name])
VALUES (1, N'SI', N'Sales Invoice'),
(2, N'DR', N'Delivery Receipt');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[InvoiceTypes]'))
    SET IDENTITY_INSERT [InvoiceTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'HasAdd', N'HasApproveDisapprove', N'HasDelete', N'HasEdit', N'HasPrint', N'HasUploadDownload', N'HasView', N'Name') AND [object_id] = OBJECT_ID(N'[Pages]'))
    SET IDENTITY_INSERT [Pages] ON;
INSERT INTO [Pages] ([Id], [HasAdd], [HasApproveDisapprove], [HasDelete], [HasEdit], [HasPrint], [HasUploadDownload], [HasView], [Name])
VALUES (1, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Customer Purchase History'),
(2, CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Packing Listing'),
(3, CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Sales Invoice'),
(4, CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Sales Order'),
(5, CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Sales Order Delivery'),
(6, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Sales Return'),
(7, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Item'),
(8, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Price List'),
(9, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Price Update'),
(10, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Purchase Return'),
(11, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Replenish Inventory'),
(12, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Warehouse'),
(13, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Warehouse Transfer'),
(14, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Unserved Item'),
(15, CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Purchase Request'),
(16, CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Purchase Order'),
(17, CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Purchase Receiving'),
(18, CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Purchase Return (New)'),
(19, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Customer'),
(20, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Payment'),
(21, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'SOA / Counter Receipt'),
(22, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Check / Cheque'),
(23, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Agents'),
(24, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Accesses');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'HasAdd', N'HasApproveDisapprove', N'HasDelete', N'HasEdit', N'HasPrint', N'HasUploadDownload', N'HasView', N'Name') AND [object_id] = OBJECT_ID(N'[Pages]'))
    SET IDENTITY_INSERT [Pages] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'HasAdd', N'HasApproveDisapprove', N'HasDelete', N'HasEdit', N'HasPrint', N'HasUploadDownload', N'HasView', N'Name') AND [object_id] = OBJECT_ID(N'[Pages]'))
    SET IDENTITY_INSERT [Pages] ON;
INSERT INTO [Pages] ([Id], [HasAdd], [HasApproveDisapprove], [HasDelete], [HasEdit], [HasPrint], [HasUploadDownload], [HasView], [Name])
VALUES (25, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Areas'),
(26, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Brands'),
(27, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Categories'),
(28, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Forwarders'),
(29, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Item Types'),
(30, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Location Codes'),
(31, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Reasons of Return'),
(32, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Regions'),
(33, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Suppliers'),
(34, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Users'),
(35, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Home Image'),
(36, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), N'Audit Logs'),
(37, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Agent Sales per Item'),
(38, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Brand Sales Report'),
(39, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Best Selling Items'),
(40, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Commision Report'),
(41, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Customer Sales Ranking Report'),
(42, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Customer Sales Report'),
(43, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Daily Sales Report'),
(44, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Sales Invoice Listing Report'),
(45, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Sales by Agent/Area'),
(46, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Sales Report by Basis'),
(47, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Sales Return Listing Report'),
(48, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Unserved Item Report'),
(49, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Below Maintaining Items'),
(50, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Receiving Listing'),
(51, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Receiving Listing by Supplier'),
(52, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Receiving Summary'),
(53, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Stock Card'),
(54, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Stock Status Report'),
(55, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Supplier Purchase History'),
(56, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Credit Memo Listing Report'),
(57, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Debit Memo Listing Report'),
(58, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Purchase Order Report'),
(59, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Purchase Return Report'),
(60, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Purchase Receiving Report');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'HasAdd', N'HasApproveDisapprove', N'HasDelete', N'HasEdit', N'HasPrint', N'HasUploadDownload', N'HasView', N'Name') AND [object_id] = OBJECT_ID(N'[Pages]'))
    SET IDENTITY_INSERT [Pages] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PriceTypes]'))
    SET IDENTITY_INSERT [PriceTypes] ON;
INSERT INTO [PriceTypes] ([Id], [Code], [Name])
VALUES (1, N'GROSS', N'GROSS PAYMENT'),
(2, N'NET', N'NET PAYMENT');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PriceTypes]'))
    SET IDENTITY_INSERT [PriceTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PurchaseOrderDeliveryModes]'))
    SET IDENTITY_INSERT [PurchaseOrderDeliveryModes] ON;
INSERT INTO [PurchaseOrderDeliveryModes] ([Id], [Code], [Name])
VALUES (1, N'PICK-UP', N'PICK-UP'),
(2, N'DELIVERY', N'DELIVERY');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PurchaseOrderDeliveryModes]'))
    SET IDENTITY_INSERT [PurchaseOrderDeliveryModes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PurchaseRequestTypes]'))
    SET IDENTITY_INSERT [PurchaseRequestTypes] ON;
INSERT INTO [PurchaseRequestTypes] ([Id], [Code], [Name])
VALUES (1, N'AM', N'AUTOMOTIVE'),
(2, N'NA', N'NON-AUTOMOTIVE');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PurchaseRequestTypes]'))
    SET IDENTITY_INSERT [PurchaseRequestTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PurchaseRequestTypes]'))
    SET IDENTITY_INSERT [PurchaseRequestTypes] ON;
INSERT INTO [PurchaseRequestTypes] ([Id], [Code], [Name])
VALUES (3, N'MR', N'MAINTENANCE & REPAIR');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PurchaseRequestTypes]'))
    SET IDENTITY_INSERT [PurchaseRequestTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PurchaseStatuses]'))
    SET IDENTITY_INSERT [PurchaseStatuses] ON;
INSERT INTO [PurchaseStatuses] ([Id], [Code], [Name])
VALUES (1, N'DRAFT', N'DRAFT'),
(2, N'SUBMITTED', N'SUBMITTED'),
(3, N'APPROVED', N'APPROVED'),
(4, N'DISAPPROVED', N'DISAPPROVED'),
(5, N'RESUBMITTED', N'RESUBMITTED');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[PurchaseStatuses]'))
    SET IDENTITY_INSERT [PurchaseStatuses] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'DateCreated', N'DateModified', N'IsActive', N'Name', N'UserCreated', N'UserModified') AND [object_id] = OBJECT_ID(N'[Regions]'))
    SET IDENTITY_INSERT [Regions] ON;
INSERT INTO [Regions] ([Id], [Code], [DateCreated], [DateModified], [IsActive], [Name], [UserCreated], [UserModified])
VALUES (1, N'LUZ', '2022-01-01T00:00:00.0000000', NULL, CAST(0 AS bit), N'LUZON', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(2, N'VIS', '2022-01-01T00:00:00.0000000', NULL, CAST(0 AS bit), N'VISAYAS', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(3, N'MIN', '2022-01-01T00:00:00.0000000', NULL, CAST(0 AS bit), N'MINDANAO', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(4, N'MM', '2022-01-01T00:00:00.0000000', NULL, CAST(0 AS bit), N'METRO MANILA', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'DateCreated', N'DateModified', N'IsActive', N'Name', N'UserCreated', N'UserModified') AND [object_id] = OBJECT_ID(N'[Regions]'))
    SET IDENTITY_INSERT [Regions] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[SOACRTypes]'))
    SET IDENTITY_INSERT [SOACRTypes] ON;
INSERT INTO [SOACRTypes] ([Id], [Code], [Name])
VALUES (1, N'SOA', N'STATEMENT OF ACCOUNT'),
(2, N'CR', N'COUNTER RECEIPT');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[SOACRTypes]'))
    SET IDENTITY_INSERT [SOACRTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[Statuses]'))
    SET IDENTITY_INSERT [Statuses] ON;
INSERT INTO [Statuses] ([Id], [Code], [Name])
VALUES (1, N'DRAFT', N'SALES ORDER DRAFT'),
(2, N'SUBMITTED', N'SALES ORDER SUBMITTED'),
(3, N'APPROVED', N'SALES ORDER APPROVED'),
(4, N'DENIED', N'SALES ORDER DENIED'),
(5, N'DRAFT', N'PACKING DRAFT'),
(6, N'POSTED', N'PACKING SUBMITTED'),
(7, N'DRAFT', N'INVOICE DRAFT'),
(8, N'INVOICED', N'INVOICED SUBMITTED'),
(9, N'PACKED', N'FULLY PACKED'),
(10, N'INVOICED', N'FULLY INVOICED'),
(11, N'PRINTED', N'INVOICE PRINTED'),
(12, N'PAID', N'INVOICE PAID'),
(13, N'NOT PAID', N'INVOICE NOT PAID'),
(14, N'CLEARED', N'CHECK CLEARED'),
(15, N'BOUNCED', N'CHECK BOUNCED'),
(16, N'REPLACED', N'CHECK REPLACED'),
(17, N'DEPOSIT', N'DEPOSIT'),
(18, N'REDEPOSITED', N'REDEPOSITED'),
(19, N'DEPOSITED', N'DEPOSITED'),
(20, N'CANCELLED', N'CANCELLED INVOICE');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[Statuses]'))
    SET IDENTITY_INSERT [Statuses] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[Units]'))
    SET IDENTITY_INSERT [Units] ON;
INSERT INTO [Units] ([Id], [Code], [Name])
VALUES (1, N'BOT', N'BOTTLE'),
(2, N'BOX', N'BOX'),
(3, N'FT', N'FEET'),
(4, N'GAL', N'GALLON'),
(5, N'JAR', N'JAR'),
(6, N'L', N'LITER'),
(7, N'M', N'METER'),
(8, N'MM', N'MILLILITER'),
(9, N'PACK', N'PACK'),
(10, N'PAIL', N'PAIL');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[Units]'))
    SET IDENTITY_INSERT [Units] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[Units]'))
    SET IDENTITY_INSERT [Units] ON;
INSERT INTO [Units] ([Id], [Code], [Name])
VALUES (11, N'PC', N'PIECE'),
(12, N'QT', N'QUART'),
(13, N'ROLL', N'ROLL'),
(14, N'SET', N'SET');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[Units]'))
    SET IDENTITY_INSERT [Units] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CanAdd', N'CanApproveDisapprove', N'CanDelete', N'CanEdit', N'CanPrint', N'CanUploadDownload', N'CanView', N'DateCreated', N'DateModified', N'PageId', N'RoleId', N'UserCreated', N'UserModified') AND [object_id] = OBJECT_ID(N'[Accesses]'))
    SET IDENTITY_INSERT [Accesses] ON;
INSERT INTO [Accesses] ([Id], [CanAdd], [CanApproveDisapprove], [CanDelete], [CanEdit], [CanPrint], [CanUploadDownload], [CanView], [DateCreated], [DateModified], [PageId], [RoleId], [UserCreated], [UserModified])
VALUES (1, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 1, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(2, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 2, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(3, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 3, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(4, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 4, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(5, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 5, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(6, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 6, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(7, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 7, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(8, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 8, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(9, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 9, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(10, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 10, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(11, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 11, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(12, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 12, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(13, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 13, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(14, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 14, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(15, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 15, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(16, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 16, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(17, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 17, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(18, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 18, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(19, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 19, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(20, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 20, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(21, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 21, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(22, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 22, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(23, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 23, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(24, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 24, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(25, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 25, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(26, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 26, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(27, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 27, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(28, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 28, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(29, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 29, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(30, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 30, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(31, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 31, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(32, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 32, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(33, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 33, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(34, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 34, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(35, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 35, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(36, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 36, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(37, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 37, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(38, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 38, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(39, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 39, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(40, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 40, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(41, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 41, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(42, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 42, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CanAdd', N'CanApproveDisapprove', N'CanDelete', N'CanEdit', N'CanPrint', N'CanUploadDownload', N'CanView', N'DateCreated', N'DateModified', N'PageId', N'RoleId', N'UserCreated', N'UserModified') AND [object_id] = OBJECT_ID(N'[Accesses]'))
    SET IDENTITY_INSERT [Accesses] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CanAdd', N'CanApproveDisapprove', N'CanDelete', N'CanEdit', N'CanPrint', N'CanUploadDownload', N'CanView', N'DateCreated', N'DateModified', N'PageId', N'RoleId', N'UserCreated', N'UserModified') AND [object_id] = OBJECT_ID(N'[Accesses]'))
    SET IDENTITY_INSERT [Accesses] ON;
INSERT INTO [Accesses] ([Id], [CanAdd], [CanApproveDisapprove], [CanDelete], [CanEdit], [CanPrint], [CanUploadDownload], [CanView], [DateCreated], [DateModified], [PageId], [RoleId], [UserCreated], [UserModified])
VALUES (43, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 43, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(44, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 44, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(45, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 45, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(46, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 46, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(47, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 47, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(48, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 48, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(49, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 49, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(50, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 50, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(51, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 51, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(52, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 52, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(53, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 53, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(54, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 54, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(55, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 55, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(56, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 56, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(57, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 57, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(58, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 58, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(59, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 59, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL),
(60, CAST(0 AS bit), NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, CAST(1 AS bit), '2022-01-01T00:00:00.0000000', NULL, 60, N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CanAdd', N'CanApproveDisapprove', N'CanDelete', N'CanEdit', N'CanPrint', N'CanUploadDownload', N'CanView', N'DateCreated', N'DateModified', N'PageId', N'RoleId', N'UserCreated', N'UserModified') AND [object_id] = OBJECT_ID(N'[Accesses]'))
    SET IDENTITY_INSERT [Accesses] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] ON;
INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
VALUES (N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C'),
(N'4ADE7270-CC24-43A0-98A5-BD8095F93BD2', N'CDE19596-2C9E-4ECE-837C-85950E9E907D');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'RequestTypeId') AND [object_id] = OBJECT_ID(N'[PurchaseOrderTypes]'))
    SET IDENTITY_INSERT [PurchaseOrderTypes] ON;
INSERT INTO [PurchaseOrderTypes] ([Id], [Code], [Name], [RequestTypeId])
VALUES (1, N'NA', N'NON-AUTOMOTIVE', 1),
(2, N'LPO', N'LOCAL PURCHASE ORDER', 2),
(3, N'IPO', N'INTERNATIONAL PURCHASE ORDER', 2),
(4, N'MR', N'MAINTENANCE & REPAIR', 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'RequestTypeId') AND [object_id] = OBJECT_ID(N'[PurchaseOrderTypes]'))
    SET IDENTITY_INSERT [PurchaseOrderTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'TypeId') AND [object_id] = OBJECT_ID(N'[PurchaseRequestPurposes]'))
    SET IDENTITY_INSERT [PurchaseRequestPurposes] ON;
INSERT INTO [PurchaseRequestPurposes] ([Id], [Code], [Name], [TypeId])
VALUES (1, N'RUSH', N'RUSH ORDER', 1),
(2, N'STOCK', N'STOCKING', 1),
(3, N'OFFICE', N'OFFICE SUPPLIES', 2),
(4, N'ONE-TIME', N'ONE-TIME PURCHASE', 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'TypeId') AND [object_id] = OBJECT_ID(N'[PurchaseRequestPurposes]'))
    SET IDENTITY_INSERT [PurchaseRequestPurposes] OFF;
GO

CREATE INDEX [IX_Accesses_PageId] ON [Accesses] ([PageId]);
GO

CREATE INDEX [IX_Accesses_RoleId] ON [Accesses] ([RoleId]);
GO

CREATE INDEX [IX_Agents_AreaId] ON [Agents] ([AreaId]);
GO

CREATE INDEX [IX_Areas_RegionId] ON [Areas] ([RegionId]);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_AuditLogs_UserId] ON [AuditLogs] ([UserId]);
GO

CREATE INDEX [IX_Brands_ItemTypeId] ON [Brands] ([ItemTypeId]);
GO

CREATE INDEX [IX_Cheques_CustomerId] ON [Cheques] ([CustomerId]);
GO

CREATE INDEX [IX_CreditDebitMemos_CreditDebitMemoTypeId] ON [CreditDebitMemos] ([CreditDebitMemoTypeId]);
GO

CREATE INDEX [IX_CreditDebitMemos_InvoiceId] ON [CreditDebitMemos] ([InvoiceId]);
GO

CREATE INDEX [IX_CustomerPaymentDetails_CustomerPaymentId] ON [CustomerPaymentDetails] ([CustomerPaymentId]);
GO

CREATE INDEX [IX_CustomerPaymentDetails_InvoiceId] ON [CustomerPaymentDetails] ([InvoiceId]);
GO

CREATE INDEX [IX_CustomerPayments_CustomerId] ON [CustomerPayments] ([CustomerId]);
GO

CREATE INDEX [IX_CustomerPayments_SOACRId] ON [CustomerPayments] ([SOACRId]);
GO

CREATE INDEX [IX_Customers_AgentId] ON [Customers] ([AgentId]);
GO

CREATE INDEX [IX_Customers_AreaId] ON [Customers] ([AreaId]);
GO

CREATE INDEX [IX_Customers_CreditTermId] ON [Customers] ([CreditTermId]);
GO

CREATE INDEX [IX_Customers_CustomerAccountStatusId] ON [Customers] ([CustomerAccountStatusId]);
GO

CREATE INDEX [IX_Customers_CustomerTypeId] ON [Customers] ([CustomerTypeId]);
GO

CREATE INDEX [IX_InvoiceLineItems_InvoiceId] ON [InvoiceLineItems] ([InvoiceId]);
GO

CREATE INDEX [IX_InvoiceLineItems_ItemId] ON [InvoiceLineItems] ([ItemId]);
GO

CREATE INDEX [IX_Invoices_InvoiceTypeId] ON [Invoices] ([InvoiceTypeId]);
GO

CREATE INDEX [IX_Invoices_PackId] ON [Invoices] ([PackId]);
GO

CREATE INDEX [IX_Invoices_SalesOrderId] ON [Invoices] ([SalesOrderId]);
GO

CREATE INDEX [IX_Invoices_StatusId] ON [Invoices] ([StatusId]);
GO

CREATE INDEX [IX_ItemConversions_EquivalentUnitId] ON [ItemConversions] ([EquivalentUnitId]);
GO

CREATE INDEX [IX_ItemConversions_ItemId] ON [ItemConversions] ([ItemId]);
GO

CREATE INDEX [IX_ItemConversions_SubItemId] ON [ItemConversions] ([SubItemId]);
GO

CREATE INDEX [IX_ItemConversions_UnitId] ON [ItemConversions] ([UnitId]);
GO

CREATE INDEX [IX_ItemInventories_ItemId] ON [ItemInventories] ([ItemId]);
GO

CREATE INDEX [IX_ItemInventories_WarehouseId] ON [ItemInventories] ([WarehouseId]);
GO

CREATE INDEX [IX_ItemPrices_ItemId] ON [ItemPrices] ([ItemId]);
GO

CREATE INDEX [IX_Items_BrandId] ON [Items] ([BrandId]);
GO

CREATE INDEX [IX_Items_CategoryId] ON [Items] ([CategoryId]);
GO

CREATE INDEX [IX_Items_ItemTypeId] ON [Items] ([ItemTypeId]);
GO

CREATE INDEX [IX_Items_UnitId] ON [Items] ([UnitId]);
GO

CREATE INDEX [IX_ItemTypes_CategoryId] ON [ItemTypes] ([CategoryId]);
GO

CREATE INDEX [IX_PackLineItems_ItemId] ON [PackLineItems] ([ItemId]);
GO

CREATE INDEX [IX_PackLineItems_PackId] ON [PackLineItems] ([PackId]);
GO

CREATE INDEX [IX_PackLineItems_PackTypeId] ON [PackLineItems] ([PackTypeId]);
GO

CREATE INDEX [IX_PackReturnLineItems_ItemId] ON [PackReturnLineItems] ([ItemId]);
GO

CREATE INDEX [IX_PackReturnLineItems_PackReturnId] ON [PackReturnLineItems] ([PackReturnId]);
GO

CREATE INDEX [IX_PackReturns_PackId] ON [PackReturns] ([PackId]);
GO

CREATE INDEX [IX_Packs_PackTypeId] ON [Packs] ([PackTypeId]);
GO

CREATE INDEX [IX_Packs_SalesOrderId] ON [Packs] ([SalesOrderId]);
GO

CREATE INDEX [IX_Packs_StatusId] ON [Packs] ([StatusId]);
GO

CREATE INDEX [IX_PhysicalCountHistories_ItemId] ON [PhysicalCountHistories] ([ItemId]);
GO

CREATE INDEX [IX_PhysicalCountHistories_WarehouseId] ON [PhysicalCountHistories] ([WarehouseId]);
GO

CREATE INDEX [IX_PurchaseOrderLineItems_ItemId] ON [PurchaseOrderLineItems] ([ItemId]);
GO

CREATE INDEX [IX_PurchaseOrderLineItems_PurchaseOrderId] ON [PurchaseOrderLineItems] ([PurchaseOrderId]);
GO

CREATE INDEX [IX_PurchaseOrderNewLineItems_ItemId] ON [PurchaseOrderNewLineItems] ([ItemId]);
GO

CREATE INDEX [IX_PurchaseOrderNewLineItems_PurchaseOrderId] ON [PurchaseOrderNewLineItems] ([PurchaseOrderId]);
GO

CREATE INDEX [IX_PurchaseOrderNewLineItems_RequestItemId] ON [PurchaseOrderNewLineItems] ([RequestItemId]);
GO

CREATE INDEX [IX_PurchaseOrderNews_ApproverId] ON [PurchaseOrderNews] ([ApproverId]);
GO

CREATE INDEX [IX_PurchaseOrderNews_CurrencyId] ON [PurchaseOrderNews] ([CurrencyId]);
GO

CREATE INDEX [IX_PurchaseOrderNews_DeliveryAddressId] ON [PurchaseOrderNews] ([DeliveryAddressId]);
GO

CREATE INDEX [IX_PurchaseOrderNews_DeliveryModeId] ON [PurchaseOrderNews] ([DeliveryModeId]);
GO

CREATE INDEX [IX_PurchaseOrderNews_DisapproverId] ON [PurchaseOrderNews] ([DisapproverId]);
GO

CREATE INDEX [IX_PurchaseOrderNews_ResubmitId] ON [PurchaseOrderNews] ([ResubmitId]);
GO

CREATE INDEX [IX_PurchaseOrderNews_StatusId] ON [PurchaseOrderNews] ([StatusId]);
GO

CREATE INDEX [IX_PurchaseOrderNews_SupplierId] ON [PurchaseOrderNews] ([SupplierId]);
GO

CREATE INDEX [IX_PurchaseOrderNews_TermId] ON [PurchaseOrderNews] ([TermId]);
GO

CREATE INDEX [IX_PurchaseOrderNews_TypeId] ON [PurchaseOrderNews] ([TypeId]);
GO

CREATE INDEX [IX_PurchaseOrders_CurrencyId] ON [PurchaseOrders] ([CurrencyId]);
GO

CREATE INDEX [IX_PurchaseOrders_DestinationId] ON [PurchaseOrders] ([DestinationId]);
GO

CREATE INDEX [IX_PurchaseOrders_StatusId] ON [PurchaseOrders] ([StatusId]);
GO

CREATE INDEX [IX_PurchaseOrders_SupplierId] ON [PurchaseOrders] ([SupplierId]);
GO

CREATE INDEX [IX_PurchaseOrderTypes_RequestTypeId] ON [PurchaseOrderTypes] ([RequestTypeId]);
GO

CREATE INDEX [IX_PurchaseReceivingLineItems_ItemId] ON [PurchaseReceivingLineItems] ([ItemId]);
GO

CREATE INDEX [IX_PurchaseReceivingLineItems_OrderItemId] ON [PurchaseReceivingLineItems] ([OrderItemId]);
GO

CREATE INDEX [IX_PurchaseReceivingLineItems_PurchaseReceivingId] ON [PurchaseReceivingLineItems] ([PurchaseReceivingId]);
GO

CREATE INDEX [IX_PurchaseReceivings_ApproverId] ON [PurchaseReceivings] ([ApproverId]);
GO

CREATE INDEX [IX_PurchaseReceivings_CreditTermId] ON [PurchaseReceivings] ([CreditTermId]);
GO

CREATE INDEX [IX_PurchaseReceivings_CurrencyId] ON [PurchaseReceivings] ([CurrencyId]);
GO

CREATE INDEX [IX_PurchaseReceivings_DisapproverId] ON [PurchaseReceivings] ([DisapproverId]);
GO

CREATE INDEX [IX_PurchaseReceivings_PurchaseOrderId] ON [PurchaseReceivings] ([PurchaseOrderId]);
GO

CREATE INDEX [IX_PurchaseReceivings_ResubmitId] ON [PurchaseReceivings] ([ResubmitId]);
GO

CREATE INDEX [IX_PurchaseReceivings_StatusId] ON [PurchaseReceivings] ([StatusId]);
GO

CREATE INDEX [IX_PurchaseReceivings_SupplierId] ON [PurchaseReceivings] ([SupplierId]);
GO

CREATE INDEX [IX_PurchaseReceivings_WarehouseId] ON [PurchaseReceivings] ([WarehouseId]);
GO

CREATE INDEX [IX_PurchaseRequestLineItems_ItemId] ON [PurchaseRequestLineItems] ([ItemId]);
GO

CREATE INDEX [IX_PurchaseRequestLineItems_PurchaseRequestId] ON [PurchaseRequestLineItems] ([PurchaseRequestId]);
GO

CREATE INDEX [IX_PurchaseRequestLineItems_SalesOrderId] ON [PurchaseRequestLineItems] ([SalesOrderId]);
GO

CREATE INDEX [IX_PurchaseRequestLineItems_UnitId] ON [PurchaseRequestLineItems] ([UnitId]);
GO

CREATE INDEX [IX_PurchaseRequestPurposes_TypeId] ON [PurchaseRequestPurposes] ([TypeId]);
GO

CREATE INDEX [IX_PurchaseRequests_Approver1Id] ON [PurchaseRequests] ([Approver1Id]);
GO

CREATE INDEX [IX_PurchaseRequests_Approver2Id] ON [PurchaseRequests] ([Approver2Id]);
GO

CREATE INDEX [IX_PurchaseRequests_DisapproverId] ON [PurchaseRequests] ([DisapproverId]);
GO

CREATE INDEX [IX_PurchaseRequests_PurposeId] ON [PurchaseRequests] ([PurposeId]);
GO

CREATE INDEX [IX_PurchaseRequests_RegionId] ON [PurchaseRequests] ([RegionId]);
GO

CREATE INDEX [IX_PurchaseRequests_ResubmitId] ON [PurchaseRequests] ([ResubmitId]);
GO

CREATE INDEX [IX_PurchaseRequests_StatusId] ON [PurchaseRequests] ([StatusId]);
GO

CREATE INDEX [IX_PurchaseRequests_TypeId] ON [PurchaseRequests] ([TypeId]);
GO

CREATE INDEX [IX_PurchaseReturnLineItems_PurchaseOrderLineItemId] ON [PurchaseReturnLineItems] ([PurchaseOrderLineItemId]);
GO

CREATE INDEX [IX_PurchaseReturnLineItems_PurchaseReceivingLineItemId] ON [PurchaseReturnLineItems] ([PurchaseReceivingLineItemId]);
GO

CREATE INDEX [IX_PurchaseReturnLineItems_PurchaseReturnId] ON [PurchaseReturnLineItems] ([PurchaseReturnId]);
GO

CREATE INDEX [IX_PurchaseReturnNewLineItems_ItemId] ON [PurchaseReturnNewLineItems] ([ItemId]);
GO

CREATE INDEX [IX_PurchaseReturnNewLineItems_PurchaseReturnId] ON [PurchaseReturnNewLineItems] ([PurchaseReturnId]);
GO

CREATE INDEX [IX_PurchaseReturnNewLineItems_ReceivingItemId] ON [PurchaseReturnNewLineItems] ([ReceivingItemId]);
GO

CREATE INDEX [IX_PurchaseReturnNews_ApproverId] ON [PurchaseReturnNews] ([ApproverId]);
GO

CREATE INDEX [IX_PurchaseReturnNews_CurrencyId] ON [PurchaseReturnNews] ([CurrencyId]);
GO

CREATE INDEX [IX_PurchaseReturnNews_DisapproverId] ON [PurchaseReturnNews] ([DisapproverId]);
GO

CREATE INDEX [IX_PurchaseReturnNews_PurchaseReceiveId] ON [PurchaseReturnNews] ([PurchaseReceiveId]);
GO

CREATE INDEX [IX_PurchaseReturnNews_ResubmitId] ON [PurchaseReturnNews] ([ResubmitId]);
GO

CREATE INDEX [IX_PurchaseReturnNews_StatusId] ON [PurchaseReturnNews] ([StatusId]);
GO

CREATE INDEX [IX_PurchaseReturnNews_SupplierId] ON [PurchaseReturnNews] ([SupplierId]);
GO

CREATE INDEX [IX_PurchaseReturnNews_WarehouseId] ON [PurchaseReturnNews] ([WarehouseId]);
GO

CREATE INDEX [IX_PurchaseReturns_PurchaseOrderId] ON [PurchaseReturns] ([PurchaseOrderId]);
GO

CREATE INDEX [IX_SalesOrderDeliveryMonitorings_ForwarderId] ON [SalesOrderDeliveryMonitorings] ([ForwarderId]);
GO

CREATE INDEX [IX_SalesOrderDeliveryMonitorings_SalesOrderId] ON [SalesOrderDeliveryMonitorings] ([SalesOrderId]);
GO

CREATE INDEX [IX_SalesOrderLineItems_ItemId] ON [SalesOrderLineItems] ([ItemId]);
GO

CREATE INDEX [IX_SalesOrderLineItems_SalesOrderId] ON [SalesOrderLineItems] ([SalesOrderId]);
GO

CREATE INDEX [IX_SalesOrders_AgentId] ON [SalesOrders] ([AgentId]);
GO

CREATE INDEX [IX_SalesOrders_CustomerId] ON [SalesOrders] ([CustomerId]);
GO

CREATE INDEX [IX_SalesOrders_ForwarderId] ON [SalesOrders] ([ForwarderId]);
GO

CREATE INDEX [IX_SalesOrders_PriceTypeId] ON [SalesOrders] ([PriceTypeId]);
GO

CREATE INDEX [IX_SalesOrders_PurchaseOrderId] ON [SalesOrders] ([PurchaseOrderId]);
GO

CREATE INDEX [IX_SalesOrders_StatusId] ON [SalesOrders] ([StatusId]);
GO

CREATE INDEX [IX_SalesReturnLineItems_InvoiceLineItemId] ON [SalesReturnLineItems] ([InvoiceLineItemId]);
GO

CREATE INDEX [IX_SalesReturnLineItems_ItemId] ON [SalesReturnLineItems] ([ItemId]);
GO

CREATE INDEX [IX_SalesReturnLineItems_SalesReturnId] ON [SalesReturnLineItems] ([SalesReturnId]);
GO

CREATE INDEX [IX_SalesReturns_CustomerId] ON [SalesReturns] ([CustomerId]);
GO

CREATE INDEX [IX_ShippingAddresses_CustomerId] ON [ShippingAddresses] ([CustomerId]);
GO

CREATE INDEX [IX_SOACRChecks_CustomerPaymentId] ON [SOACRChecks] ([CustomerPaymentId]);
GO

CREATE INDEX [IX_SOACRLineItems_InvoiceId] ON [SOACRLineItems] ([InvoiceId]);
GO

CREATE INDEX [IX_SOACRLineItems_SOACRId] ON [SOACRLineItems] ([SOACRId]);
GO

CREATE INDEX [IX_SOACRLineItemTBRs_SalesReturnLineItemId] ON [SOACRLineItemTBRs] ([SalesReturnLineItemId]);
GO

CREATE INDEX [IX_SOACRLineItemTBRs_SOACRLineItemId] ON [SOACRLineItemTBRs] ([SOACRLineItemId]);
GO

CREATE INDEX [IX_SOACRs_CustomerId] ON [SOACRs] ([CustomerId]);
GO

CREATE INDEX [IX_SOACRs_SOACRTypeId] ON [SOACRs] ([SOACRTypeId]);
GO

CREATE INDEX [IX_StockCards_ItemId] ON [StockCards] ([ItemId]);
GO

CREATE INDEX [IX_Suppliers_BusinessStyleId] ON [Suppliers] ([BusinessStyleId]);
GO

CREATE INDEX [IX_Suppliers_CurrencyId] ON [Suppliers] ([CurrencyId]);
GO

CREATE INDEX [IX_Suppliers_RegionId] ON [Suppliers] ([RegionId]);
GO

CREATE INDEX [IX_TransferItemDetails_ItemId] ON [TransferItemDetails] ([ItemId]);
GO

CREATE INDEX [IX_TransferItemDetails_TransferItemId] ON [TransferItemDetails] ([TransferItemId]);
GO

CREATE INDEX [IX_TransferItems_DestinationId] ON [TransferItems] ([DestinationId]);
GO

CREATE INDEX [IX_TransferItems_SourceId] ON [TransferItems] ([SourceId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241206003820_Initial', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseOrderNewLineItems] DROP CONSTRAINT [FK_PurchaseOrderNewLineItems_Items_ItemId];
GO

ALTER TABLE [PurchaseReceivingLineItems] DROP CONSTRAINT [FK_PurchaseReceivingLineItems_Items_ItemId];
GO

ALTER TABLE [PurchaseReturnNewLineItems] DROP CONSTRAINT [FK_PurchaseReturnNewLineItems_Items_ItemId];
GO

DROP INDEX [IX_PurchaseReturnNewLineItems_ItemId] ON [PurchaseReturnNewLineItems];
GO

DROP INDEX [IX_PurchaseReceivingLineItems_ItemId] ON [PurchaseReceivingLineItems];
GO

DROP INDEX [IX_PurchaseOrderNewLineItems_ItemId] ON [PurchaseOrderNewLineItems];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReturnNewLineItems]') AND [c].[name] = N'ItemId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReturnNewLineItems] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [PurchaseReturnNewLineItems] DROP COLUMN [ItemId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReceivingLineItems]') AND [c].[name] = N'ItemId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReceivingLineItems] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [PurchaseReceivingLineItems] DROP COLUMN [ItemId];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNewLineItems]') AND [c].[name] = N'ItemId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNewLineItems] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [PurchaseOrderNewLineItems] DROP COLUMN [ItemId];
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'8e7f56b2-6f5f-4e01-a08d-403304ff078a', [PasswordHash] = N'AQAAAAIAAYagAAAAENdxSYsgHJzBRev8cmzS5Q9HcrHOWYZCH/eaXsjPhAeeBvpeXutns85yxhRa1Ldxpg==', [SecurityStamp] = N'153fabec-f818-4f45-ad6b-1686d60b1f12'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4a3a068f-f1e7-44cf-bf74-1f595e55ea07', [PasswordHash] = N'AQAAAAIAAYagAAAAENv1ZIvmVUofyr71wRQpnUthXg9iIE251GOMMr3Q3xTBYmvfTvJguUz5pxBEf47Vvw==', [SecurityStamp] = N'3c71fc65-84fc-45b8-83c5-9c18285d0eb0'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241209023708_ProcurementItem', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseReceivings] DROP CONSTRAINT [FK_PurchaseReceivings_CreditTerms_CreditTermId];
GO

ALTER TABLE [PurchaseReceivings] DROP CONSTRAINT [FK_PurchaseReceivings_Currencies_CurrencyId];
GO

ALTER TABLE [PurchaseReturnNews] DROP CONSTRAINT [FK_PurchaseReturnNews_Currencies_CurrencyId];
GO

DROP INDEX [IX_PurchaseReturnNews_CurrencyId] ON [PurchaseReturnNews];
GO

DROP INDEX [IX_PurchaseReceivings_CreditTermId] ON [PurchaseReceivings];
GO

DROP INDEX [IX_PurchaseReceivings_CurrencyId] ON [PurchaseReceivings];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'Terms');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Suppliers] DROP COLUMN [Terms];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReturnNews]') AND [c].[name] = N'CurrencyId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReturnNews] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [PurchaseReturnNews] DROP COLUMN [CurrencyId];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReceivings]') AND [c].[name] = N'CreditTermId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReceivings] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [PurchaseReceivings] DROP COLUMN [CreditTermId];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReceivings]') AND [c].[name] = N'CurrencyId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReceivings] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [PurchaseReceivings] DROP COLUMN [CurrencyId];
GO

ALTER TABLE [Suppliers] ADD [TermId] int NOT NULL DEFAULT 9;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'b1419de0-5cff-4b24-9475-5b4a72204c2e', [PasswordHash] = N'AQAAAAIAAYagAAAAEKX0WS1IlygTrCZZVw0C6G28+SNauuI2kgzghbnuT0OOc9AA3fkfxYnnyL8uXZ/6Zg==', [SecurityStamp] = N'35c895ab-8296-4588-b95f-5e62c13d3436'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'455de60d-2f0d-4df6-b2e9-1f1d0accefae', [PasswordHash] = N'AQAAAAIAAYagAAAAENqKH24Q0Q8/bRmP0dk8xj8c5UHR0rqe0a3R3KE0iOp7mWTQYzaA2BG8PzRo2zqkrg==', [SecurityStamp] = N'aaaa160d-2f7b-4ffc-bdd1-6860c4b34712'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_Suppliers_TermId] ON [Suppliers] ([TermId]);
GO

ALTER TABLE [Suppliers] ADD CONSTRAINT [FK_Suppliers_CreditTerms_TermId] FOREIGN KEY ([TermId]) REFERENCES [CreditTerms] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241209033226_mix', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseOrderNewLineItems] DROP CONSTRAINT [FK_PurchaseOrderNewLineItems_PurchaseRequestLineItems_RequestItemId];
GO

ALTER TABLE [PurchaseReceivingLineItems] DROP CONSTRAINT [FK_PurchaseReceivingLineItems_PurchaseOrderNewLineItems_OrderItemId];
GO

ALTER TABLE [PurchaseReturnNewLineItems] DROP CONSTRAINT [FK_PurchaseReturnNewLineItems_PurchaseReceivingLineItems_ReceivingItemId];
GO

DROP INDEX [IX_PurchaseReturnNewLineItems_ReceivingItemId] ON [PurchaseReturnNewLineItems];
DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReturnNewLineItems]') AND [c].[name] = N'ReceivingItemId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReturnNewLineItems] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [PurchaseReturnNewLineItems] ALTER COLUMN [ReceivingItemId] int NOT NULL;
ALTER TABLE [PurchaseReturnNewLineItems] ADD DEFAULT 0 FOR [ReceivingItemId];
CREATE INDEX [IX_PurchaseReturnNewLineItems_ReceivingItemId] ON [PurchaseReturnNewLineItems] ([ReceivingItemId]);
GO

DROP INDEX [IX_PurchaseReceivingLineItems_OrderItemId] ON [PurchaseReceivingLineItems];
DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReceivingLineItems]') AND [c].[name] = N'OrderItemId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReceivingLineItems] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [PurchaseReceivingLineItems] ALTER COLUMN [OrderItemId] int NOT NULL;
ALTER TABLE [PurchaseReceivingLineItems] ADD DEFAULT 0 FOR [OrderItemId];
CREATE INDEX [IX_PurchaseReceivingLineItems_OrderItemId] ON [PurchaseReceivingLineItems] ([OrderItemId]);
GO

DROP INDEX [IX_PurchaseOrderNewLineItems_RequestItemId] ON [PurchaseOrderNewLineItems];
DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNewLineItems]') AND [c].[name] = N'RequestItemId');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNewLineItems] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [PurchaseOrderNewLineItems] ALTER COLUMN [RequestItemId] int NOT NULL;
ALTER TABLE [PurchaseOrderNewLineItems] ADD DEFAULT 0 FOR [RequestItemId];
CREATE INDEX [IX_PurchaseOrderNewLineItems_RequestItemId] ON [PurchaseOrderNewLineItems] ([RequestItemId]);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'af683b08-9a31-44b6-9dc8-4b52ccf7f231', [PasswordHash] = N'AQAAAAIAAYagAAAAEMWrEsvNRUkWwR5+DFaNxUxYPo8jEiPpJVin3RW5664kRrpF0fZ8MmPf6iTRwPCMIQ==', [SecurityStamp] = N'3dc3e101-3169-4e6a-83cc-9c30d48c3be0'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'015a71ef-6d96-45f9-928e-38a6a69d592b', [PasswordHash] = N'AQAAAAIAAYagAAAAED1T6eQXAIZxSU/SVeyUbs9gdS1r5pobVin2t5qGEXhpk/V18uqUCl9WFFR2hQeLJA==', [SecurityStamp] = N'e2cad196-3359-4a3e-b913-5736d1286639'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [PurchaseOrderNewLineItems] ADD CONSTRAINT [FK_PurchaseOrderNewLineItems_PurchaseRequestLineItems_RequestItemId] FOREIGN KEY ([RequestItemId]) REFERENCES [PurchaseRequestLineItems] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [PurchaseReceivingLineItems] ADD CONSTRAINT [FK_PurchaseReceivingLineItems_PurchaseOrderNewLineItems_OrderItemId] FOREIGN KEY ([OrderItemId]) REFERENCES [PurchaseOrderNewLineItems] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [PurchaseReturnNewLineItems] ADD CONSTRAINT [FK_PurchaseReturnNewLineItems_PurchaseReceivingLineItems_ReceivingItemId] FOREIGN KEY ([ReceivingItemId]) REFERENCES [PurchaseReceivingLineItems] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241209060617_ProcurementNonNullableItem', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [Statuses] SET [Name] = N'INVOICE SUBMITTED'
WHERE [Id] = 8;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241217071251_status', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'PriceList');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Suppliers] DROP COLUMN [PriceList];
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'24923c75-3938-4ad7-87dc-28d07b590ece', [PasswordHash] = N'AQAAAAIAAYagAAAAEBiyb56lQeX4+2kaVPnAWUFXVd5OrMMQsE3RGwT6tIo0pwzb6cFmRHGu2bzcAZhyzQ==', [SecurityStamp] = N'7033e7dd-9b14-4752-afd1-28515d6bf0b0'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'684e8a94-875c-4f51-9584-fd11c7b1a594', [PasswordHash] = N'AQAAAAIAAYagAAAAEKVivor/TAMkWEkmBBANGGKwl5GKaxfArrDUhMAjVRj0qjYX5UvsWhgGLQin0csmZg==', [SecurityStamp] = N'707f173b-8885-456a-980d-7c7fdf21cb06'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241217091334_Supplier', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4a6846a4-53d4-4a96-9ad3-091e679a09aa', [PasswordHash] = N'AQAAAAIAAYagAAAAEDC5DpkG9ANh1MBL6cz5awm5DeyAoMPe8umPbXn/yY+vflGPoCb2IqXK/zdAfzpbNA==', [SecurityStamp] = N'01aa277b-85ee-4789-9329-fe83ae2dfed8'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'018f6647-1c20-4a90-bfbf-71f849ff628c', [PasswordHash] = N'AQAAAAIAAYagAAAAEKL/vziXXqB1pj7tG2mD9tiq0xKVCYYMNcH5YLtWvaTmQ410V8Bt/do3esKx1fgA2A==', [SecurityStamp] = N'13dbba94-a4e4-4271-8451-120761d4866a'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

UPDATE [Statuses] SET [Code] = N'DISAPPROVED', [Name] = N'SALES ORDER DISAPPROVED'
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241218083152_sodisapproved', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'CommodityQuantity');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Customers] DROP COLUMN [CommodityQuantity];
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'RequiredDeliveryDate');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Customers] DROP COLUMN [RequiredDeliveryDate];
GO

ALTER TABLE [SalesOrders] ADD [RequiredDeliveryDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'f9855e37-0127-42c0-80d2-419aceefa1c5', [PasswordHash] = N'AQAAAAIAAYagAAAAELoBjkFqqMuxPAV/+Ti+XRMOtfkNANL1tO7HPniXmTWAorvpLuZ15UwgC1NZHE/ryQ==', [SecurityStamp] = N'0932d31f-d8ea-4d53-9c04-ec7000f1a76a'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'2c5f31ee-1395-490b-b85d-8915ec1df62a', [PasswordHash] = N'AQAAAAIAAYagAAAAEPjZzXWu5UfR4GLf23Bq4c0II66hlv70CmDlyBMjip5ooREvkz111sb/UmYIf2+jTQ==', [SecurityStamp] = N'4ba7b882-536f-4357-a098-3db86940d8ac'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241219041708_customer', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrderLineItems] ADD [LineNumber] int NOT NULL DEFAULT 0;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4df78da9-cf34-480e-a718-696dcd78f259', [PasswordHash] = N'AQAAAAIAAYagAAAAEMigtvKzgmG2bNJDCn6u26enlIkddRtcYt9Z+mcvpOZT7lvOv+oBZs1T2idM11K7bg==', [SecurityStamp] = N'a80180df-f9f3-497f-b08e-91dd46f6676b'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'd88a7062-daaf-4536-82a2-2020012e0837', [PasswordHash] = N'AQAAAAIAAYagAAAAEEuuHQ3F33o7XoEVN+Z1uu34pGkMTj8JPteueOSH14z64NBTF5z7uSIMQVFSwRzlIQ==', [SecurityStamp] = N'035430d9-5273-40e7-9b72-a475f201028d'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241219153334_solinenumber', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'QuantityInvoiced');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [SalesOrders] DROP COLUMN [QuantityInvoiced];
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'QuantityOrdered');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [SalesOrders] DROP COLUMN [QuantityOrdered];
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'QuantityPacked');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [SalesOrders] DROP COLUMN [QuantityPacked];
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderLineItems]') AND [c].[name] = N'InvoicedQuantity');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderLineItems] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [SalesOrderLineItems] DROP COLUMN [InvoicedQuantity];
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderLineItems]') AND [c].[name] = N'PackedQuantity');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderLineItems] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [SalesOrderLineItems] DROP COLUMN [PackedQuantity];
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderLineItems]') AND [c].[name] = N'ServedQuantity');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderLineItems] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [SalesOrderLineItems] DROP COLUMN [ServedQuantity];
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderLineItems]') AND [c].[name] = N'UnservedQuantity');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderLineItems] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [SalesOrderLineItems] DROP COLUMN [UnservedQuantity];
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Packs]') AND [c].[name] = N'QuantityInvoiced');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Packs] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Packs] DROP COLUMN [QuantityInvoiced];
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Packs]') AND [c].[name] = N'TotalAmount');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Packs] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Packs] DROP COLUMN [TotalAmount];
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Packs]') AND [c].[name] = N'TotalQuantity');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Packs] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Packs] DROP COLUMN [TotalQuantity];
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'122c0fe1-492c-4f7a-b5af-9c5a2c649771', [PasswordHash] = N'AQAAAAIAAYagAAAAENjtcxacYzDRNxqyObWyJ5OFRrfeAErmcm/Ea94GCS8ptbQkGzEfDpORi+eaX8XsCw==', [SecurityStamp] = N'e82c84e2-e9db-4f4b-a44e-7aaf4cc6cb4d'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'f0e8a5ac-2734-4566-99e4-66a236c0891f', [PasswordHash] = N'AQAAAAIAAYagAAAAECDGPIMcdob1wc9z2PRyG261BAHdUu7YMhAtf4WdGHZqJx9m7nT8N3RejQk2ezlvZg==', [SecurityStamp] = N'4fc89ac7-62d0-48c1-902c-02bbf0b60406'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241220160243_SalesQuantityAdjustment', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseRequestLineItems] DROP CONSTRAINT [FK_PurchaseRequestLineItems_Units_UnitId];
GO

DROP INDEX [IX_PurchaseRequestLineItems_UnitId] ON [PurchaseRequestLineItems];
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseRequestLineItems]') AND [c].[name] = N'UnitId');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseRequestLineItems] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [PurchaseRequestLineItems] DROP COLUMN [UnitId];
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'a27eeb26-aef5-48cc-b279-7035faeebd3c', [PasswordHash] = N'AQAAAAIAAYagAAAAEL6jUp1sTmRb6EPeMuI2xQVaFlvsWLWuXENRXIb/lhSDLfpOPfo+3O2CeXnlRVsEjw==', [SecurityStamp] = N'4921b4f3-f730-43f0-aeb3-a2da1d374711'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'89aa0a1c-5102-40ef-bdc0-fb6b064d0597', [PasswordHash] = N'AQAAAAIAAYagAAAAEGz/ZT+I41tMmoobq+wGd5Ohhv26YEbTHpfjkTR4Ss4v//iczXGWp5O9nFqc0Ip60g==', [SecurityStamp] = N'398e3f4d-2b77-4e9c-9dc4-013d264809bc'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241229030849_requestunit', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrders] DROP CONSTRAINT [FK_SalesOrders_PurchaseOrders_PurchaseOrderId];
GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'DeclaredValue');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [SalesOrders] DROP COLUMN [DeclaredValue];
GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'TotalAmount');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var25 + '];');
ALTER TABLE [SalesOrders] DROP COLUMN [TotalAmount];
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'63ea1271-9333-45b9-9554-32ba13b2f3cb', [PasswordHash] = N'AQAAAAIAAYagAAAAEFmM4lKvU3ys9H0XqcBvRaceoTvcIi3K72Y6h1j4kYhMx8kcCUjc9nL36Pi0koPZAw==', [SecurityStamp] = N'5abd4a8e-640d-40c7-b68f-6261ca9f46a5'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'2f4baac9-11cf-4a8a-b678-c9eef5bc975b', [PasswordHash] = N'AQAAAAIAAYagAAAAEK82YrW47uh/G7ahNktnelKrio0EXo39UFz8+wrSSFNog5+gsUQ2n2G8sQp629FVSA==', [SecurityStamp] = N'4f583ed3-9ae0-4665-ba06-53f9e0f10913'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [SalesOrders] ADD CONSTRAINT [FK_SalesOrders_PurchaseOrderNews_PurchaseOrderId] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [PurchaseOrderNews] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241229035720_soadjustment', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseRequestLineItems] ADD [UnitId] int NULL;
GO

CREATE INDEX [IX_PurchaseRequestLineItems_UnitId] ON [PurchaseRequestLineItems] ([UnitId]);
GO

ALTER TABLE [PurchaseRequestLineItems] ADD CONSTRAINT [FK_PurchaseRequestLineItems_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250108161451_requestunit2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var26 sysname;
SELECT @var26 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseRequestLineItems]') AND [c].[name] = N'StockOnHand');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseRequestLineItems] DROP CONSTRAINT [' + @var26 + '];');
ALTER TABLE [PurchaseRequestLineItems] DROP COLUMN [StockOnHand];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250114130958_RequestStock', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseOrderNews] DROP CONSTRAINT [FK_PurchaseOrderNews_Currencies_CurrencyId];
GO

DROP TABLE [StockCards];
GO

DECLARE @var27 sysname;
SELECT @var27 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNews]') AND [c].[name] = N'CurrencyRate');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNews] DROP CONSTRAINT [' + @var27 + '];');
ALTER TABLE [PurchaseOrderNews] DROP COLUMN [CurrencyRate];
GO

DECLARE @var28 sysname;
SELECT @var28 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNews]') AND [c].[name] = N'CurrencyId');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNews] DROP CONSTRAINT [' + @var28 + '];');
ALTER TABLE [PurchaseOrderNews] ALTER COLUMN [CurrencyId] int NULL;
GO

ALTER TABLE [PurchaseOrderNews] ADD CONSTRAINT [FK_PurchaseOrderNews_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currencies] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250120120725_RemoveStockCard', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseOrderNews] DROP CONSTRAINT [FK_PurchaseOrderNews_Currencies_CurrencyId];
GO

DROP INDEX [IX_PurchaseOrderNews_CurrencyId] ON [PurchaseOrderNews];
GO

DECLARE @var29 sysname;
SELECT @var29 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNews]') AND [c].[name] = N'CurrencyId');
IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNews] DROP CONSTRAINT [' + @var29 + '];');
ALTER TABLE [PurchaseOrderNews] DROP COLUMN [CurrencyId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250120121554_RemoveStockCard2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var30 sysname;
SELECT @var30 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Agents]') AND [c].[name] = N'IsDeleted');
IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Agents] DROP CONSTRAINT [' + @var30 + '];');
ALTER TABLE [Agents] DROP COLUMN [IsDeleted];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250120165813_agent', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Pages] ADD [OrderNumber] int NOT NULL DEFAULT 1;
GO

ALTER TABLE [Pages] ADD [PageGroupId] int NOT NULL DEFAULT 1;
GO

CREATE TABLE [PageGroups] (
    [Id] int NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_PageGroups] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[PageGroups]'))
    SET IDENTITY_INSERT [PageGroups] ON;
INSERT INTO [PageGroups] ([Id], [Name])
VALUES (1, N'SALES'),
(2, N'INVENTORY'),
(3, N'PROCUREMENT'),
(4, N'CREDITS & COLLECTIONS'),
(5, N'ADMINISTRATION'),
(6, N'SALES REPORT'),
(7, N'INVENTORY REPORT'),
(8, N'ACCOUNTING REPORT'),
(9, N'PROCUREMENT REPORT');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[PageGroups]'))
    SET IDENTITY_INSERT [PageGroups] OFF;
GO

CREATE INDEX [IX_Pages_PageGroupId] ON [Pages] ([PageGroupId]);
GO

ALTER TABLE [Pages] ADD CONSTRAINT [FK_Pages_PageGroups_PageGroupId] FOREIGN KEY ([PageGroupId]) REFERENCES [PageGroups] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250121114320_pagegroup', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrders] DROP CONSTRAINT [FK_SalesOrders_PurchaseOrderNews_PurchaseOrderId];
GO

DROP INDEX [IX_SalesOrders_PurchaseOrderId] ON [SalesOrders];
GO

DECLARE @var31 sysname;
SELECT @var31 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'PurchaseOrderId');
IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var31 + '];');
ALTER TABLE [SalesOrders] DROP COLUMN [PurchaseOrderId];
GO

ALTER TABLE [SalesOrders] ADD [PurchaseOrder] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250121150517_salesorder', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Suppliers] ADD [MobileNumber] nvarchar(100) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250122015918_supplier2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesReturns] ADD [ReturnDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250122174046_salesreturn', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Items] ADD [LocationCodeId] int NOT NULL DEFAULT 2;
GO

CREATE INDEX [IX_Items_LocationCodeId] ON [Items] ([LocationCodeId]);
GO

ALTER TABLE [Items] ADD CONSTRAINT [FK_Items_LocationCodes_LocationCodeId] FOREIGN KEY ([LocationCodeId]) REFERENCES [LocationCodes] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250123111713_item', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Items] DROP CONSTRAINT [FK_Items_LocationCodes_LocationCodeId];
GO

DECLARE @var32 sysname;
SELECT @var32 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'LocationCodeId');
IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var32 + '];');
ALTER TABLE [Items] ALTER COLUMN [LocationCodeId] int NULL;
GO

ALTER TABLE [Items] ADD CONSTRAINT [FK_Items_LocationCodes_LocationCodeId] FOREIGN KEY ([LocationCodeId]) REFERENCES [LocationCodes] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250123140247_item2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Shippings] (
    [Id] int NOT NULL IDENTITY,
    [SalesOrderId] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Shippings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Shippings_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ShippingLineItems] (
    [Id] int NOT NULL IDENTITY,
    [ShippingId] int NOT NULL,
    [InvoiceLineItemId] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_ShippingLineItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ShippingLineItems_InvoiceLineItems_InvoiceLineItemId] FOREIGN KEY ([InvoiceLineItemId]) REFERENCES [InvoiceLineItems] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ShippingLineItems_Shippings_ShippingId] FOREIGN KEY ([ShippingId]) REFERENCES [Shippings] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ShippingLineItems_InvoiceLineItemId] ON [ShippingLineItems] ([InvoiceLineItemId]);
GO

CREATE INDEX [IX_ShippingLineItems_ShippingId] ON [ShippingLineItems] ([ShippingId]);
GO

CREATE INDEX [IX_Shippings_SalesOrderId] ON [Shippings] ([SalesOrderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250124020519_shipping', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ShippingLineItems] DROP CONSTRAINT [FK_ShippingLineItems_InvoiceLineItems_InvoiceLineItemId];
GO

EXEC sp_rename N'[ShippingLineItems].[InvoiceLineItemId]', N'SalesOrderLineItemId', N'COLUMN';
GO

EXEC sp_rename N'[ShippingLineItems].[IX_ShippingLineItems_InvoiceLineItemId]', N'IX_ShippingLineItems_SalesOrderLineItemId', N'INDEX';
GO

ALTER TABLE [ShippingLineItems] ADD CONSTRAINT [FK_ShippingLineItems_SalesOrderLineItems_SalesOrderLineItemId] FOREIGN KEY ([SalesOrderLineItemId]) REFERENCES [SalesOrderLineItems] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250124022148_shipping2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Invoices] ADD [ReasonOfReturnId] int NOT NULL DEFAULT 1;
GO

CREATE INDEX [IX_Invoices_ReasonOfReturnId] ON [Invoices] ([ReasonOfReturnId]);
GO

ALTER TABLE [Invoices] ADD CONSTRAINT [FK_Invoices_ReasonOfReturns_ReasonOfReturnId] FOREIGN KEY ([ReasonOfReturnId]) REFERENCES [ReasonOfReturns] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250124032316_salesreturn2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ShippingLineItems] DROP CONSTRAINT [FK_ShippingLineItems_SalesOrderLineItems_SalesOrderLineItemId];
GO

EXEC sp_rename N'[ShippingLineItems].[SalesOrderLineItemId]', N'InvoiceLineItemId', N'COLUMN';
GO

EXEC sp_rename N'[ShippingLineItems].[IX_ShippingLineItems_SalesOrderLineItemId]', N'IX_ShippingLineItems_InvoiceLineItemId', N'INDEX';
GO

ALTER TABLE [ShippingLineItems] ADD CONSTRAINT [FK_ShippingLineItems_InvoiceLineItems_InvoiceLineItemId] FOREIGN KEY ([InvoiceLineItemId]) REFERENCES [InvoiceLineItems] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250124033203_shipping3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Invoices] DROP CONSTRAINT [FK_Invoices_ReasonOfReturns_ReasonOfReturnId];
GO

DROP INDEX [IX_Invoices_ReasonOfReturnId] ON [Invoices];
GO

DECLARE @var33 sysname;
SELECT @var33 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invoices]') AND [c].[name] = N'ReasonOfReturnId');
IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [Invoices] DROP CONSTRAINT [' + @var33 + '];');
ALTER TABLE [Invoices] DROP COLUMN [ReasonOfReturnId];
GO

ALTER TABLE [SalesReturns] ADD [ReasonOfReturnId] int NOT NULL DEFAULT 1;
GO

CREATE INDEX [IX_SalesReturns_ReasonOfReturnId] ON [SalesReturns] ([ReasonOfReturnId]);
GO

ALTER TABLE [SalesReturns] ADD CONSTRAINT [FK_SalesReturns_ReasonOfReturns_ReasonOfReturnId] FOREIGN KEY ([ReasonOfReturnId]) REFERENCES [ReasonOfReturns] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250126160512_salesreturn3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesReturns] DROP CONSTRAINT [FK_SalesReturns_ReasonOfReturns_ReasonOfReturnId];
GO

DROP INDEX [IX_SalesReturns_ReasonOfReturnId] ON [SalesReturns];
GO

DECLARE @var34 sysname;
SELECT @var34 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesReturns]') AND [c].[name] = N'ReasonOfReturnId');
IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [SalesReturns] DROP CONSTRAINT [' + @var34 + '];');
ALTER TABLE [SalesReturns] DROP COLUMN [ReasonOfReturnId];
GO

DECLARE @var35 sysname;
SELECT @var35 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesReturnLineItems]') AND [c].[name] = N'IsDefective');
IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [SalesReturnLineItems] DROP CONSTRAINT [' + @var35 + '];');
ALTER TABLE [SalesReturnLineItems] DROP COLUMN [IsDefective];
GO

ALTER TABLE [SalesReturnLineItems] ADD [ReasonOfReturnId] int NOT NULL DEFAULT 1;
GO

CREATE INDEX [IX_SalesReturnLineItems_ReasonOfReturnId] ON [SalesReturnLineItems] ([ReasonOfReturnId]);
GO

ALTER TABLE [SalesReturnLineItems] ADD CONSTRAINT [FK_SalesReturnLineItems_ReasonOfReturns_ReasonOfReturnId] FOREIGN KEY ([ReasonOfReturnId]) REFERENCES [ReasonOfReturns] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250126162807_salesreturn4', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Cheques] ADD [StatusId] int NULL;
GO

CREATE TABLE [ChequeStatus] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_ChequeStatus] PRIMARY KEY ([Id])
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'2c32902d-515b-43f4-829e-e36f4518ef24', [PasswordHash] = N'AQAAAAIAAYagAAAAEPpCSpzO5Ee9pevPDS+B4eBUcZ72+X92eBh36LGfJnmIMj8Udw8BxtlyJYZ5Boq7lQ==', [SecurityStamp] = N'90a0285b-2928-443e-9004-3d29c0bf3e64'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'5402eb8c-b274-45df-af2b-7c11f13fe920', [PasswordHash] = N'AQAAAAIAAYagAAAAECCGz0kdv6lNpKwO/NbNAUOBM8H98u1U03/7lJbmg69YFb3lJqyEGB+YSLvqT4gOyA==', [SecurityStamp] = N'5b7fe2c4-e673-40db-b3b0-9b097d8a4fc2'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_Cheques_StatusId] ON [Cheques] ([StatusId]);
GO

ALTER TABLE [Cheques] ADD CONSTRAINT [FK_Cheques_ChequeStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [ChequeStatus] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250127075634_ChequeStatus', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [ShippingLineItems];
GO

DROP TABLE [Shippings];
GO

ALTER TABLE [ShippingAddresses] ADD [IsDefault] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [SalesReturns] ADD [SalesOrderId] int NOT NULL DEFAULT 297126;
GO

CREATE INDEX [IX_SalesReturns_SalesOrderId] ON [SalesReturns] ([SalesOrderId]);
GO

ALTER TABLE [SalesReturns] ADD CONSTRAINT [FK_SalesReturns_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250127172220_shipping4', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ReceptaclePrintHistory] (
    [Id] int NOT NULL IDENTITY,
    [PackId] int NOT NULL,
    [BoxNumber] int NOT NULL,
    [ShippingAddressId] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_ReceptaclePrintHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ReceptaclePrintHistory_Packs_PackId] FOREIGN KEY ([PackId]) REFERENCES [Packs] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ReceptaclePrintHistory_ShippingAddresses_ShippingAddressId] FOREIGN KEY ([ShippingAddressId]) REFERENCES [ShippingAddresses] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ReceptaclePrintHistory_PackId] ON [ReceptaclePrintHistory] ([PackId]);
GO

CREATE INDEX [IX_ReceptaclePrintHistory_ShippingAddressId] ON [ReceptaclePrintHistory] ([ShippingAddressId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250127173306_receptaclehistory', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Cheques] DROP CONSTRAINT [FK_Cheques_ChequeStatus_StatusId];
GO

ALTER TABLE [ChequeStatus] DROP CONSTRAINT [PK_ChequeStatus];
GO

EXEC sp_rename N'[ChequeStatus]', N'ChequeStatuses';
GO

ALTER TABLE [ChequeStatuses] ADD CONSTRAINT [PK_ChequeStatuses] PRIMARY KEY ([Id]);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'b6890f91-d9d9-47d9-a8d9-efc898f928da', [PasswordHash] = N'AQAAAAIAAYagAAAAEC+9x6UcWbo4FuvOE/h+vIXBqty6p+59iHL7PXg8MLAVdvt9xxiDMTYyYadMx/B0kA==', [SecurityStamp] = N'73cc0784-a023-4810-b504-ee49ddddf2b8'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'81c90c87-72de-43d5-9989-8b00af823114', [PasswordHash] = N'AQAAAAIAAYagAAAAEJGSbJ2VNXrkhsyU1Bzwn1QRYEfbRMmeZCfNRO+iP8eyadnwylGHqVg8K36waS2CtA==', [SecurityStamp] = N'798a2eed-fe49-46f3-9eaa-479fae18b151'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [Cheques] ADD CONSTRAINT [FK_Cheques_ChequeStatuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [ChequeStatuses] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250128025337_ChequeStatus_update', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var36 sysname;
SELECT @var36 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'Address');
IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var36 + '];');
ALTER TABLE [Customers] DROP COLUMN [Address];
GO

DECLARE @var37 sysname;
SELECT @var37 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'ContactNumber');
IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var37 + '];');
ALTER TABLE [Customers] DROP COLUMN [ContactNumber];
GO

DECLARE @var38 sysname;
SELECT @var38 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'ContactPerson');
IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var38 + '];');
ALTER TABLE [Customers] DROP COLUMN [ContactPerson];
GO

DECLARE @var39 sysname;
SELECT @var39 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'FaxNumber');
IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var39 + '];');
ALTER TABLE [Customers] DROP COLUMN [FaxNumber];
GO

DECLARE @var40 sysname;
SELECT @var40 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShippingAddresses]') AND [c].[name] = N'PhoneNumber');
IF @var40 IS NOT NULL EXEC(N'ALTER TABLE [ShippingAddresses] DROP CONSTRAINT [' + @var40 + '];');
ALTER TABLE [ShippingAddresses] ALTER COLUMN [PhoneNumber] nvarchar(50) NULL;
GO

DECLARE @var41 sysname;
SELECT @var41 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShippingAddresses]') AND [c].[name] = N'FaxNumber');
IF @var41 IS NOT NULL EXEC(N'ALTER TABLE [ShippingAddresses] DROP CONSTRAINT [' + @var41 + '];');
ALTER TABLE [ShippingAddresses] ALTER COLUMN [FaxNumber] nvarchar(50) NULL;
GO

ALTER TABLE [ShippingAddresses] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250128030105_shippingaddress', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var42 sysname;
SELECT @var42 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShippingAddresses]') AND [c].[name] = N'PhoneNumber');
IF @var42 IS NOT NULL EXEC(N'ALTER TABLE [ShippingAddresses] DROP CONSTRAINT [' + @var42 + '];');
ALTER TABLE [ShippingAddresses] ALTER COLUMN [PhoneNumber] nvarchar(100) NULL;
GO

DECLARE @var43 sysname;
SELECT @var43 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShippingAddresses]') AND [c].[name] = N'FaxNumber');
IF @var43 IS NOT NULL EXEC(N'ALTER TABLE [ShippingAddresses] DROP CONSTRAINT [' + @var43 + '];');
ALTER TABLE [ShippingAddresses] ALTER COLUMN [FaxNumber] nvarchar(100) NULL;
GO

DECLARE @var44 sysname;
SELECT @var44 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShippingAddresses]') AND [c].[name] = N'ContactPerson');
IF @var44 IS NOT NULL EXEC(N'ALTER TABLE [ShippingAddresses] DROP CONSTRAINT [' + @var44 + '];');
ALTER TABLE [ShippingAddresses] ALTER COLUMN [ContactPerson] nvarchar(150) NOT NULL;
GO

DECLARE @var45 sysname;
SELECT @var45 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShippingAddresses]') AND [c].[name] = N'Address');
IF @var45 IS NOT NULL EXEC(N'ALTER TABLE [ShippingAddresses] DROP CONSTRAINT [' + @var45 + '];');
ALTER TABLE [ShippingAddresses] ALTER COLUMN [Address] nvarchar(MAX) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250128035842_shippingaddress2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ReceptaclePrintHistory] DROP CONSTRAINT [FK_ReceptaclePrintHistory_Packs_PackId];
GO

ALTER TABLE [ReceptaclePrintHistory] DROP CONSTRAINT [FK_ReceptaclePrintHistory_ShippingAddresses_ShippingAddressId];
GO

ALTER TABLE [ReceptaclePrintHistory] DROP CONSTRAINT [PK_ReceptaclePrintHistory];
GO

EXEC sp_rename N'[ReceptaclePrintHistory]', N'ReceptaclePrintHistories';
GO

EXEC sp_rename N'[ReceptaclePrintHistories].[IX_ReceptaclePrintHistory_ShippingAddressId]', N'IX_ReceptaclePrintHistories_ShippingAddressId', N'INDEX';
GO

EXEC sp_rename N'[ReceptaclePrintHistories].[IX_ReceptaclePrintHistory_PackId]', N'IX_ReceptaclePrintHistories_PackId', N'INDEX';
GO

ALTER TABLE [ReceptaclePrintHistories] ADD CONSTRAINT [PK_ReceptaclePrintHistories] PRIMARY KEY ([Id]);
GO

ALTER TABLE [ReceptaclePrintHistories] ADD CONSTRAINT [FK_ReceptaclePrintHistories_Packs_PackId] FOREIGN KEY ([PackId]) REFERENCES [Packs] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [ReceptaclePrintHistories] ADD CONSTRAINT [FK_ReceptaclePrintHistories_ShippingAddresses_ShippingAddressId] FOREIGN KEY ([ShippingAddressId]) REFERENCES [ShippingAddresses] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250128073640_shippingaddress3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ReceptaclePrintHistories] DROP CONSTRAINT [FK_ReceptaclePrintHistories_ShippingAddresses_ShippingAddressId];
GO

DECLARE @var46 sysname;
SELECT @var46 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ReceptaclePrintHistories]') AND [c].[name] = N'ShippingAddressId');
IF @var46 IS NOT NULL EXEC(N'ALTER TABLE [ReceptaclePrintHistories] DROP CONSTRAINT [' + @var46 + '];');
ALTER TABLE [ReceptaclePrintHistories] ALTER COLUMN [ShippingAddressId] int NULL;
GO

ALTER TABLE [ReceptaclePrintHistories] ADD CONSTRAINT [FK_ReceptaclePrintHistories_ShippingAddresses_ShippingAddressId] FOREIGN KEY ([ShippingAddressId]) REFERENCES [ShippingAddresses] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250128110506_shippingaddress4', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ReceptaclePrintHistories] ADD [CompanyAs] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250128164730_shippingaddress5', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesReturns] DROP CONSTRAINT [FK_SalesReturns_Customers_CustomerId];
GO

DROP INDEX [IX_SalesReturns_CustomerId] ON [SalesReturns];
GO

DECLARE @var47 sysname;
SELECT @var47 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesReturns]') AND [c].[name] = N'CustomerId');
IF @var47 IS NOT NULL EXEC(N'ALTER TABLE [SalesReturns] DROP CONSTRAINT [' + @var47 + '];');
ALTER TABLE [SalesReturns] DROP COLUMN [CustomerId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250129183610_salesreturn5', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [CreditDebitMemos];
GO

DROP TABLE [CreditDebitMemoTypes];
GO

DECLARE @var48 sysname;
SELECT @var48 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'OutstandingBalance');
IF @var48 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var48 + '];');
ALTER TABLE [Customers] DROP COLUMN [OutstandingBalance];
GO

ALTER TABLE [SOACRLineItems] ADD [CreditReferenceNumber] nvarchar(50) NULL;
GO

ALTER TABLE [SOACRLineItems] ADD [DebitReferenceNumber] nvarchar(50) NULL;
GO

ALTER TABLE [SOACRLineItems] ADD [FreightReferenceNumber] nvarchar(50) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250131111400_mix2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var49 sysname;
SELECT @var49 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderLineItems]') AND [c].[name] = N'IsServed');
IF @var49 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderLineItems] DROP CONSTRAINT [' + @var49 + '];');
ALTER TABLE [SalesOrderLineItems] DROP COLUMN [IsServed];
GO

ALTER TABLE [SalesOrderLineItems] ADD [ServedQuantity] int NOT NULL DEFAULT 0;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'5da5d100-921c-4fd2-b732-0463df3d8642', [PasswordHash] = N'AQAAAAIAAYagAAAAEBp/Qt0/HFw9Do0vG03JfmZiLPXmyH6h22HNzZZMiTSQVkhHtOWTf4IdwFVHoDvWhA==', [SecurityStamp] = N'b2b155d3-8c67-46a2-ab91-e64dc45812b1'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'bd9df084-06a6-44e1-9673-74c4d36c4cc9', [PasswordHash] = N'AQAAAAIAAYagAAAAEFj2SzTmuee76EKKDCZL/YXDwaYPyL5Lym+KFZqi7/DvkKWy+uR07ee2fFRekLqNuQ==', [SecurityStamp] = N'36e8d96b-6432-4ec2-98e8-e8e115770adb'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250204062843_salesorder2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var50 sysname;
SELECT @var50 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderLineItems]') AND [c].[name] = N'ServedQuantity');
IF @var50 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderLineItems] DROP CONSTRAINT [' + @var50 + '];');
ALTER TABLE [SalesOrderLineItems] DROP COLUMN [ServedQuantity];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250204175413_salesorder3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrderDeliveryMonitorings] ADD [CustomerReceived] datetime2 NULL;
GO

ALTER TABLE [SalesOrderDeliveryMonitorings] ADD [Representative] nvarchar(150) NOT NULL DEFAULT N'';
GO

ALTER TABLE [SalesOrderDeliveryMonitorings] ADD [ServiceLevelAgreement] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250205011622_sodelivery', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[SalesOrderDeliveryMonitorings].[CustomerReceived]', N'DateReceive', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250205030018_sodelivery2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrders] DROP CONSTRAINT [FK_SalesOrders_Forwarders_ForwarderId];
GO

ALTER TABLE [SalesOrders] DROP CONSTRAINT [FK_SalesOrders_PriceTypes_PriceTypeId];
GO

DROP TABLE [PriceTypes];
GO

DROP INDEX [IX_SalesOrders_PriceTypeId] ON [SalesOrders];
GO

DECLARE @var51 sysname;
SELECT @var51 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'PriceTypeId');
IF @var51 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var51 + '];');
ALTER TABLE [SalesOrders] DROP COLUMN [PriceTypeId];
GO

DECLARE @var52 sysname;
SELECT @var52 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'ForwarderId');
IF @var52 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var52 + '];');
ALTER TABLE [SalesOrders] ALTER COLUMN [ForwarderId] int NULL;
GO

DECLARE @var53 sysname;
SELECT @var53 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderDeliveryMonitorings]') AND [c].[name] = N'Waybill');
IF @var53 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderDeliveryMonitorings] DROP CONSTRAINT [' + @var53 + '];');
ALTER TABLE [SalesOrderDeliveryMonitorings] ALTER COLUMN [Waybill] nvarchar(150) NULL;
GO

DECLARE @var54 sysname;
SELECT @var54 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderDeliveryMonitorings]') AND [c].[name] = N'Representative');
IF @var54 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderDeliveryMonitorings] DROP CONSTRAINT [' + @var54 + '];');
ALTER TABLE [SalesOrderDeliveryMonitorings] ALTER COLUMN [Representative] nvarchar(150) NULL;
GO

DECLARE @var55 sysname;
SELECT @var55 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderDeliveryMonitorings]') AND [c].[name] = N'Remarks');
IF @var55 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderDeliveryMonitorings] DROP CONSTRAINT [' + @var55 + '];');
ALTER TABLE [SalesOrderDeliveryMonitorings] ALTER COLUMN [Remarks] nvarchar(250) NULL;
GO

ALTER TABLE [SalesOrders] ADD CONSTRAINT [FK_SalesOrders_Forwarders_ForwarderId] FOREIGN KEY ([ForwarderId]) REFERENCES [Forwarders] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250205042910_salesorder4', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var56 sysname;
SELECT @var56 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SOACRs]') AND [c].[name] = N'AmountToPay');
IF @var56 IS NOT NULL EXEC(N'ALTER TABLE [SOACRs] DROP CONSTRAINT [' + @var56 + '];');
ALTER TABLE [SOACRs] DROP COLUMN [AmountToPay];
GO

DECLARE @var57 sysname;
SELECT @var57 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invoices]') AND [c].[name] = N'Amount');
IF @var57 IS NOT NULL EXEC(N'ALTER TABLE [Invoices] DROP CONSTRAINT [' + @var57 + '];');
ALTER TABLE [Invoices] DROP COLUMN [Amount];
GO

DECLARE @var58 sysname;
SELECT @var58 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invoices]') AND [c].[name] = N'IsGenerated');
IF @var58 IS NOT NULL EXEC(N'ALTER TABLE [Invoices] DROP CONSTRAINT [' + @var58 + '];');
ALTER TABLE [Invoices] DROP COLUMN [IsGenerated];
GO

DECLARE @var59 sysname;
SELECT @var59 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invoices]') AND [c].[name] = N'IsPaid');
IF @var59 IS NOT NULL EXEC(N'ALTER TABLE [Invoices] DROP CONSTRAINT [' + @var59 + '];');
ALTER TABLE [Invoices] DROP COLUMN [IsPaid];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250205100300_amount', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var60 sysname;
SELECT @var60 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerPayments]') AND [c].[name] = N'TotalInvoiceAmount');
IF @var60 IS NOT NULL EXEC(N'ALTER TABLE [CustomerPayments] DROP CONSTRAINT [' + @var60 + '];');
ALTER TABLE [CustomerPayments] DROP COLUMN [TotalInvoiceAmount];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250205152639_mix3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseOrderLineItems] ADD [PurchaseRequestLineItemId] int NULL;
GO

CREATE INDEX [IX_PurchaseOrderLineItems_PurchaseRequestLineItemId] ON [PurchaseOrderLineItems] ([PurchaseRequestLineItemId]);
GO

ALTER TABLE [PurchaseOrderLineItems] ADD CONSTRAINT [FK_PurchaseOrderLineItems_PurchaseRequestLineItems_PurchaseRequestLineItemId] FOREIGN KEY ([PurchaseRequestLineItemId]) REFERENCES [PurchaseRequestLineItems] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250212131343_purchaserequest', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseRequests] DROP CONSTRAINT [FK_PurchaseRequests_Regions_RegionId];
GO

DROP INDEX [IX_PurchaseRequests_RegionId] ON [PurchaseRequests];
GO

DECLARE @var61 sysname;
SELECT @var61 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseRequests]') AND [c].[name] = N'RegionId');
IF @var61 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseRequests] DROP CONSTRAINT [' + @var61 + '];');
ALTER TABLE [PurchaseRequests] DROP COLUMN [RegionId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250213101524_purchaserequest2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var62 sysname;
SELECT @var62 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SOACRLineItems]') AND [c].[name] = N'FreightWaybill');
IF @var62 IS NOT NULL EXEC(N'ALTER TABLE [SOACRLineItems] DROP CONSTRAINT [' + @var62 + '];');
ALTER TABLE [SOACRLineItems] DROP COLUMN [FreightWaybill];
GO

DECLARE @var63 sysname;
SELECT @var63 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SOACRLineItems]') AND [c].[name] = N'InvoiceAmount');
IF @var63 IS NOT NULL EXEC(N'ALTER TABLE [SOACRLineItems] DROP CONSTRAINT [' + @var63 + '];');
ALTER TABLE [SOACRLineItems] DROP COLUMN [InvoiceAmount];
GO

DECLARE @var64 sysname;
SELECT @var64 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SOACRLineItems]') AND [c].[name] = N'IsPaid');
IF @var64 IS NOT NULL EXEC(N'ALTER TABLE [SOACRLineItems] DROP CONSTRAINT [' + @var64 + '];');
ALTER TABLE [SOACRLineItems] DROP COLUMN [IsPaid];
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'f0ac0d19-ca81-4eb8-9a16-1a9c852b41e4', [PasswordHash] = N'AQAAAAIAAYagAAAAENNb0IHUKLVc/nyhBzWIVfcH1/CT0O/5R0uyHvJp/eqXykb2CdcLxk/6H9K9TaVN3Q==', [SecurityStamp] = N'7a0a7729-71d0-42b8-9eaf-6679d8803464'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'eabcdad5-1271-4917-8900-0091d875f01d', [PasswordHash] = N'AQAAAAIAAYagAAAAELzE6ctTH6rKAVpfhPdyD36EuGOumiorQ3+/CjQhHxScd3LAj812vf0qtn256JKcyw==', [SecurityStamp] = N'5713e072-5ade-417c-bc61-d01cec503322'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250215183227_mix4', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var65 sysname;
SELECT @var65 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SOACRLineItems]') AND [c].[name] = N'PaidAmount');
IF @var65 IS NOT NULL EXEC(N'ALTER TABLE [SOACRLineItems] DROP CONSTRAINT [' + @var65 + '];');
ALTER TABLE [SOACRLineItems] DROP COLUMN [PaidAmount];
GO

DECLARE @var66 sysname;
SELECT @var66 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invoices]') AND [c].[name] = N'PaidAmount');
IF @var66 IS NOT NULL EXEC(N'ALTER TABLE [Invoices] DROP CONSTRAINT [' + @var66 + '];');
ALTER TABLE [Invoices] DROP COLUMN [PaidAmount];
GO

ALTER TABLE [CustomerPaymentDetails] ADD [PaidAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250215193057_mix5', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var67 sysname;
SELECT @var67 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Agents]') AND [c].[name] = N'CommissionRate');
IF @var67 IS NOT NULL EXEC(N'ALTER TABLE [Agents] DROP CONSTRAINT [' + @var67 + '];');
ALTER TABLE [Agents] DROP COLUMN [CommissionRate];
GO

DECLARE @var68 sysname;
SELECT @var68 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Agents]') AND [c].[name] = N'DateHired');
IF @var68 IS NOT NULL EXEC(N'ALTER TABLE [Agents] DROP CONSTRAINT [' + @var68 + '];');
ALTER TABLE [Agents] DROP COLUMN [DateHired];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250215195500_agent2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var69 sysname;
SELECT @var69 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReceivings]') AND [c].[name] = N'Remarks');
IF @var69 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReceivings] DROP CONSTRAINT [' + @var69 + '];');
ALTER TABLE [PurchaseReceivings] ALTER COLUMN [Remarks] nvarchar(max) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'34ced6fc-ac9e-4592-bbd3-f2ea10117206', [PasswordHash] = N'AQAAAAIAAYagAAAAEHMHhi+4lj/l3nQRFhhGcMuevOY05sZVmj2/MkadnrexAVTCfxUWSceGsmWGQUI+mA==', [SecurityStamp] = N'05311b47-fb8d-470a-9968-dc55eb58bc9e'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'2c2da511-dc31-434e-889d-2021f0d3b5c5', [PasswordHash] = N'AQAAAAIAAYagAAAAECAwKq7o5cCmFS3vQ6J8u7p/O4OSGAOl8YBSpsAwbiq4BIIrosPbgZQAp48HSMKaJA==', [SecurityStamp] = N'925cee56-9de7-4efa-93f1-f9257360c875'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250221065753_updateRR', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var70 sysname;
SELECT @var70 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReturnNews]') AND [c].[name] = N'Remarks');
IF @var70 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReturnNews] DROP CONSTRAINT [' + @var70 + '];');
ALTER TABLE [PurchaseReturnNews] ALTER COLUMN [Remarks] nvarchar(max) NULL;
GO

DECLARE @var71 sysname;
SELECT @var71 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseRequests]') AND [c].[name] = N'Remarks');
IF @var71 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseRequests] DROP CONSTRAINT [' + @var71 + '];');
ALTER TABLE [PurchaseRequests] ALTER COLUMN [Remarks] nvarchar(100) NULL;
GO

DECLARE @var72 sysname;
SELECT @var72 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNews]') AND [c].[name] = N'Remarks');
IF @var72 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNews] DROP CONSTRAINT [' + @var72 + '];');
ALTER TABLE [PurchaseOrderNews] ALTER COLUMN [Remarks] nvarchar(250) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4bb71077-d07d-4ff7-8c7d-d9a07a41e590', [PasswordHash] = N'AQAAAAIAAYagAAAAEBXZ0+wQZzldkLaKDQSA8/UKsYU6MTUHKg4vaZ6p2dhJrYIa9nMxKfjyRH1NbzhJGw==', [SecurityStamp] = N'bdedfb5b-2d96-41c0-a1e4-ae9c5b176cfe'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'67769012-4360-420c-8f98-47d382ef9bd5', [PasswordHash] = N'AQAAAAIAAYagAAAAENpjb24sxcWQaOnDjMAwQjv5+/ITByXvWzuPGmCLoDkFSizW1LrRfA3Kf2SwoYzWuA==', [SecurityStamp] = N'317f988e-f928-414e-8bd5-5138c1b362bd'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250225002606_procurement_remarks', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var73 sysname;
SELECT @var73 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerPayments]') AND [c].[name] = N'TotalAmountPaid');
IF @var73 IS NOT NULL EXEC(N'ALTER TABLE [CustomerPayments] DROP CONSTRAINT [' + @var73 + '];');
ALTER TABLE [CustomerPayments] DROP COLUMN [TotalAmountPaid];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250228174733_customerpayment', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseReturnLineItems] DROP CONSTRAINT [FK_PurchaseReturnLineItems_PurchaseReceivingLineItems_PurchaseReceivingLineItemId];
GO

DROP INDEX [IX_PurchaseReturnLineItems_PurchaseReceivingLineItemId] ON [PurchaseReturnLineItems];
GO

DECLARE @var74 sysname;
SELECT @var74 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReturnLineItems]') AND [c].[name] = N'PurchaseReceivingLineItemId');
IF @var74 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReturnLineItems] DROP CONSTRAINT [' + @var74 + '];');
ALTER TABLE [PurchaseReturnLineItems] DROP COLUMN [PurchaseReceivingLineItemId];
GO

ALTER TABLE [PurchaseOrderNews] ADD [IsVATIncluded] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'edab2e6c-0aaf-419c-b89b-a5fec06be0c8', [PasswordHash] = N'AQAAAAIAAYagAAAAEFCA6zkifds6JxNRSDl/sWv/5HWvEeBr3tLvmAe50JFZILqj02RmzTcEK4cRVbms0Q==', [SecurityStamp] = N'5c1c2d86-d23e-4b3d-b0c7-630dedf8dc31'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'9dcf181b-49be-46ba-8e83-be2682ce92e1', [PasswordHash] = N'AQAAAAIAAYagAAAAEB1zA+8AyKhAZs+pgBz0yCRsoSIEZJtJ/4AlLEKyKOa+s4MDaWVi9wIKbDHG7czftQ==', [SecurityStamp] = N'114c3cda-bd64-4579-acb4-aee85587d009'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250314013607_po_vat', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseOrderNews] ADD [Discount3] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'cc7fbe14-24a8-4df6-bbcd-7181d802ad2a', [PasswordHash] = N'AQAAAAIAAYagAAAAELsxPyhTlVUz5fufbPnQXcEe9djgSbaJdkDXurJ9qDnFYUpVcyQ5NfP+ZbutDEgMGA==', [SecurityStamp] = N'e3c8c093-ea2b-465e-8bd7-19375ec6def6'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4da87cb4-e367-4ab6-b28d-371f96db0ced', [PasswordHash] = N'AQAAAAIAAYagAAAAENovJHX06TXYk2W0RFj7zt2nv1SqW7NmE+3EeknYduSmnD3iJZDcn7ojpm4i3lDq5w==', [SecurityStamp] = N'5d433ba1-4005-45a5-9f4d-75ad7e711ef8'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250317080922_po_discount3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'd5de4a14-4c3d-4443-8005-5ed7af6b9a63', [NormalizedUserName] = N'ADMIN-GMG', [PasswordHash] = N'AQAAAAIAAYagAAAAEPqBswlD17XtrTAlUcRo130+dzlqLN6UrjkG2c/oki2z2Pgrnbi5xGLhx7d1anQ0jQ==', [SecurityStamp] = N'73b587d1-e7e8-4b4e-bdd1-95e4fcb8b525'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'c722c7d0-8d33-4d95-941f-e5425fe4908d', [NormalizedUserName] = N'ADMIN-MISNET', [PasswordHash] = N'AQAAAAIAAYagAAAAEMFU5rA9vXN+WgcUJ2AMigOXdBUdBcxBmH84BHgCQNfKzNfSfCXf6BmWjApGlkF+Aw==', [SecurityStamp] = N'0998b386-ffef-40af-9d8d-1b8354a60a6b'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250319185019_remigrate', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var75 sysname;
SELECT @var75 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ItemTypes]') AND [c].[name] = N'Name');
IF @var75 IS NOT NULL EXEC(N'ALTER TABLE [ItemTypes] DROP CONSTRAINT [' + @var75 + '];');
ALTER TABLE [ItemTypes] ALTER COLUMN [Name] nvarchar(100) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250326030049_itemtype', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[SalesOrders].[ResubmittedSalesOrderId]', N'ResubmitId', N'COLUMN';
GO

CREATE INDEX [IX_SalesOrders_ResubmitId] ON [SalesOrders] ([ResubmitId]);
GO

ALTER TABLE [SalesOrders] ADD CONSTRAINT [FK_SalesOrders_SalesOrders_ResubmitId] FOREIGN KEY ([ResubmitId]) REFERENCES [SalesOrders] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250413153204_salesorder5', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrders] DROP CONSTRAINT [FK_SalesOrders_Forwarders_ForwarderId];
GO

DROP INDEX [IX_SalesOrders_ForwarderId] ON [SalesOrders];
GO

DECLARE @var76 sysname;
SELECT @var76 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'ForwarderId');
IF @var76 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var76 + '];');
ALTER TABLE [SalesOrders] DROP COLUMN [ForwarderId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250413154756_salesorder6', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Items] DROP CONSTRAINT [FK_Items_LocationCodes_LocationCodeId];
GO

DROP INDEX [IX_Items_LocationCodeId] ON [Items];
GO

DECLARE @var77 sysname;
SELECT @var77 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'LocationCodeId');
IF @var77 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var77 + '];');
ALTER TABLE [Items] DROP COLUMN [LocationCodeId];
GO

ALTER TABLE [Items] ADD [BeginningCost] decimal(18,2) NULL;
GO

CREATE TABLE [ItemLocation] (
    [Id] int NOT NULL IDENTITY,
    [ItemId] int NOT NULL,
    [LocationCodeId] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_ItemLocation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemLocation_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ItemLocation_LocationCodes_LocationCodeId] FOREIGN KEY ([LocationCodeId]) REFERENCES [LocationCodes] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'7315efd4-a059-4a03-a155-58c212085765', [PasswordHash] = N'AQAAAAIAAYagAAAAEAT6X/d4K7FUvg1Wowst4Jwlo2oj6kov9JX28ol0K/aho2rTyCix4tmhKWW8LvCPOw==', [SecurityStamp] = N'e57f4103-0f42-4a3b-b4cc-9402c0dce02a'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'd49a87a8-1e06-4a39-94b1-887746fce3b4', [PasswordHash] = N'AQAAAAIAAYagAAAAENT34ERv80J63qqwKAxGbTTVErCuzTm41fg1/oJOwwgd2ZzbduXpw64AvQQjlRwL0Q==', [SecurityStamp] = N'7dc661e3-247c-443d-beb7-1d821402f5fc'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_ItemLocation_ItemId] ON [ItemLocation] ([ItemId]);
GO

CREATE INDEX [IX_ItemLocation_LocationCodeId] ON [ItemLocation] ([LocationCodeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250507132157_LocationCode', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var78 sysname;
SELECT @var78 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'Name');
IF @var78 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var78 + '];');
ALTER TABLE [Items] ALTER COLUMN [Name] nvarchar(200) NOT NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'76c74b78-623d-4599-9228-105c145c02a3', [PasswordHash] = N'AQAAAAIAAYagAAAAENHaRqmvfdmjRJ8BQ4bI0zfaUK7vXHNMfE2F5m+w3U5A/FKSzxw5CfMTXAcSPhUoCQ==', [SecurityStamp] = N'7a4487fe-b6e3-4933-97eb-d40845fdee24'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'b873735e-922b-4bc2-82b7-ced59b4a9d3c', [PasswordHash] = N'AQAAAAIAAYagAAAAENRk8l/4mDBeGUKecPWbLSUMhECb76aYwHPi1kwct2AYPwY/YawHgKV6O53YLTrPQQ==', [SecurityStamp] = N'21bb43b6-482b-4021-a24e-12fb63650867'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250509040919_itemlength', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var79 sysname;
SELECT @var79 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'PartNumber');
IF @var79 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var79 + '];');
ALTER TABLE [Items] ALTER COLUMN [PartNumber] nvarchar(200) NULL;
GO

DECLARE @var80 sysname;
SELECT @var80 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'ModelNumber');
IF @var80 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var80 + '];');
ALTER TABLE [Items] ALTER COLUMN [ModelNumber] nvarchar(200) NULL;
GO

DECLARE @var81 sysname;
SELECT @var81 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'InterchangeNumber');
IF @var81 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var81 + '];');
ALTER TABLE [Items] ALTER COLUMN [InterchangeNumber] nvarchar(200) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'ef86fb88-91c7-471a-8bf4-609c4cf221e3', [PasswordHash] = N'AQAAAAIAAYagAAAAEIo7Pr3RdJLjXYj3MALL7bqtQ0/Me7MFliStX4XzlNPc6Gth66VMyk7AiJUIv3BgcA==', [SecurityStamp] = N'be0a1035-3b9c-4999-82fe-2b2a038e87b0'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'960ba283-ba3c-4243-b672-c453edbd6a59', [PasswordHash] = N'AQAAAAIAAYagAAAAEBfLTxd5k1sQiNbwAsfhsQMFjVyqs9Q7fpTbHi3MSWxjRP+8kd+29xT9++5IAitZFg==', [SecurityStamp] = N'0daedfe5-1fa0-4b81-9a78-77a51e188bdf'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250509152914_itemlength2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesReturns] DROP CONSTRAINT [FK_SalesReturns_SalesOrders_SalesOrderId];
GO

DECLARE @var82 sysname;
SELECT @var82 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesReturns]') AND [c].[name] = N'SalesOrderId');
IF @var82 IS NOT NULL EXEC(N'ALTER TABLE [SalesReturns] DROP CONSTRAINT [' + @var82 + '];');
ALTER TABLE [SalesReturns] ALTER COLUMN [SalesOrderId] int NULL;
GO

ALTER TABLE [SalesReturns] ADD CONSTRAINT [FK_SalesReturns_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250513055410_sreturnSO', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesReturns] ADD [CustomerId] int NULL;
GO

CREATE INDEX [IX_SalesReturns_CustomerId] ON [SalesReturns] ([CustomerId]);
GO

ALTER TABLE [SalesReturns] ADD CONSTRAINT [FK_SalesReturns_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250513105417_sreturnCustomer', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var83 sysname;
SELECT @var83 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invoices]') AND [c].[name] = N'Remarks');
IF @var83 IS NOT NULL EXEC(N'ALTER TABLE [Invoices] DROP CONSTRAINT [' + @var83 + '];');
ALTER TABLE [Invoices] ALTER COLUMN [Remarks] nvarchar(250) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'452588c7-48d1-46a6-9567-5821b8839497', [PasswordHash] = N'AQAAAAIAAYagAAAAELdBqdGtPlwmNRS/GbZLGMxmC//5mDMr4DkW1afmrfXHM14aklY3cJmzwk1AdRsnhw==', [SecurityStamp] = N'6eaf5c53-5a1d-4cfe-b681-be66d875177f'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'5c5596b9-0e70-4a05-a8b1-cf78bb4dccf1', [PasswordHash] = N'AQAAAAIAAYagAAAAEBRpDmg2ybE0iPCzQ66f9ijTN7XQBzf+nZsxj777p1tu+FxU+GyyMrPZvR49Q9oFOQ==', [SecurityStamp] = N'5376de64-f11a-43f2-911b-b398d80246a2'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250515030727_sinvoiceRemarks', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ItemLocation] DROP CONSTRAINT [FK_ItemLocation_Items_ItemId];
GO

ALTER TABLE [ItemLocation] DROP CONSTRAINT [FK_ItemLocation_LocationCodes_LocationCodeId];
GO

ALTER TABLE [ItemLocation] DROP CONSTRAINT [PK_ItemLocation];
GO

EXEC sp_rename N'[ItemLocation]', N'ItemLocations';
GO

EXEC sp_rename N'[ItemLocations].[IX_ItemLocation_LocationCodeId]', N'IX_ItemLocations_LocationCodeId', N'INDEX';
GO

EXEC sp_rename N'[ItemLocations].[IX_ItemLocation_ItemId]', N'IX_ItemLocations_ItemId', N'INDEX';
GO

ALTER TABLE [ItemLocations] ADD CONSTRAINT [PK_ItemLocations] PRIMARY KEY ([Id]);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4834bbba-81c6-4c14-9f4a-b608fa1d74b4', [PasswordHash] = N'AQAAAAIAAYagAAAAEEnA7AyzsQ/BgRZD8XDhwSe4pBe65n32Rpg1Zyug+f2mQjT1D50k8LNr84xuzOQtuw==', [SecurityStamp] = N'2264d448-a87e-440d-a4da-e7a7858f9692'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4067438a-e2b6-4b4c-b9d8-9b1866b71df0', [PasswordHash] = N'AQAAAAIAAYagAAAAEJ/VIpp4d2c4qDEGZ/Ch013GjC3jyikH+a97y+CKrybilOe7NmuV1LZK0+dBNSyjnw==', [SecurityStamp] = N'4cc5a528-49c7-4347-9dda-6886002aa0ee'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [ItemLocations] ADD CONSTRAINT [FK_ItemLocations_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [ItemLocations] ADD CONSTRAINT [FK_ItemLocations_LocationCodes_LocationCodeId] FOREIGN KEY ([LocationCodeId]) REFERENCES [LocationCodes] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250516112225_itemlocation', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TransferItemDetails] ADD [LocationCodeId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'fd266446-e7c6-47d5-865e-89512be376db', [PasswordHash] = N'AQAAAAIAAYagAAAAECZUNtRzba6KYEshDJtBhzTYzCQVfJA8VthLfIYSvXHOWaHmSmGy05P/+XZCJOYaqQ==', [SecurityStamp] = N'3899afaa-32e4-4691-b798-a74690b0c40f'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'7be083fc-0392-44bb-9ecb-6f0bfb403f8d', [PasswordHash] = N'AQAAAAIAAYagAAAAEFjW5yK+ZzQ8avYwbGTTZaqUrl1fE/an+yrJqpu8bCFnCprZbpXF4rY0tQIP49VV5A==', [SecurityStamp] = N'4801088c-be59-44f3-b50c-daaf4a05038c'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_TransferItemDetails_LocationCodeId] ON [TransferItemDetails] ([LocationCodeId]);
GO

ALTER TABLE [TransferItemDetails] ADD CONSTRAINT [FK_TransferItemDetails_LocationCodes_LocationCodeId] FOREIGN KEY ([LocationCodeId]) REFERENCES [LocationCodes] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250519062945_TransferItemDetail_locationcode', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var84 sysname;
SELECT @var84 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseRequestLineItems]') AND [c].[name] = N'ItemDescription');
IF @var84 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseRequestLineItems] DROP CONSTRAINT [' + @var84 + '];');
ALTER TABLE [PurchaseRequestLineItems] ALTER COLUMN [ItemDescription] nvarchar(250) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250527183320_pritemdesc', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'd3e79f54-e3f2-4d24-954d-b5c6c8f2e67c', [PasswordHash] = N'AQAAAAIAAYagAAAAEEx7y1c1yMNgRjr0nSodHxfluAc4I9MX5Lz/68Ov63tZryN7DzlUf8hi5DrCJUsaOA==', [SecurityStamp] = N'02ff0ac7-ba07-4049-814a-d03e045936c4'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'3d04e281-3249-4742-b846-c9ec50c5c11e', [PasswordHash] = N'AQAAAAIAAYagAAAAEHiFiXyWc5qtCow545QwTeYlT8uJV+dK6PecfTX/AjcUYRdMMuf42DK7eF5jfjARGQ==', [SecurityStamp] = N'16e91e32-dd7c-4311-86fa-05c8f5e4aa8b'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250531040331_pocurrency', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var85 sysname;
SELECT @var85 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TransferItems]') AND [c].[name] = N'Remarks');
IF @var85 IS NOT NULL EXEC(N'ALTER TABLE [TransferItems] DROP CONSTRAINT [' + @var85 + '];');
ALTER TABLE [TransferItems] ALTER COLUMN [Remarks] nvarchar(501) NOT NULL;
GO

DECLARE @var86 sysname;
SELECT @var86 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesReturns]') AND [c].[name] = N'Remarks');
IF @var86 IS NOT NULL EXEC(N'ALTER TABLE [SalesReturns] DROP CONSTRAINT [' + @var86 + '];');
ALTER TABLE [SalesReturns] ALTER COLUMN [Remarks] nvarchar(501) NOT NULL;
GO

DECLARE @var87 sysname;
SELECT @var87 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'Remarks');
IF @var87 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var87 + '];');
ALTER TABLE [SalesOrders] ALTER COLUMN [Remarks] nvarchar(501) NULL;
GO

DECLARE @var88 sysname;
SELECT @var88 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderDeliveryMonitorings]') AND [c].[name] = N'Remarks');
IF @var88 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderDeliveryMonitorings] DROP CONSTRAINT [' + @var88 + '];');
ALTER TABLE [SalesOrderDeliveryMonitorings] ALTER COLUMN [Remarks] nvarchar(501) NULL;
GO

DECLARE @var89 sysname;
SELECT @var89 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReturns]') AND [c].[name] = N'Remarks');
IF @var89 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReturns] DROP CONSTRAINT [' + @var89 + '];');
ALTER TABLE [PurchaseReturns] ALTER COLUMN [Remarks] nvarchar(501) NULL;
GO

DECLARE @var90 sysname;
SELECT @var90 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReturnNews]') AND [c].[name] = N'Remarks');
IF @var90 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReturnNews] DROP CONSTRAINT [' + @var90 + '];');
ALTER TABLE [PurchaseReturnNews] ALTER COLUMN [Remarks] nvarchar(501) NULL;
GO

DECLARE @var91 sysname;
SELECT @var91 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseRequests]') AND [c].[name] = N'Remarks');
IF @var91 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseRequests] DROP CONSTRAINT [' + @var91 + '];');
ALTER TABLE [PurchaseRequests] ALTER COLUMN [Remarks] nvarchar(501) NULL;
GO

DECLARE @var92 sysname;
SELECT @var92 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReceivings]') AND [c].[name] = N'Remarks');
IF @var92 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReceivings] DROP CONSTRAINT [' + @var92 + '];');
ALTER TABLE [PurchaseReceivings] ALTER COLUMN [Remarks] nvarchar(501) NULL;
GO

DECLARE @var93 sysname;
SELECT @var93 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNews]') AND [c].[name] = N'Remarks');
IF @var93 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNews] DROP CONSTRAINT [' + @var93 + '];');
ALTER TABLE [PurchaseOrderNews] ALTER COLUMN [Remarks] nvarchar(501) NULL;
GO

DECLARE @var94 sysname;
SELECT @var94 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PhysicalCountHistories]') AND [c].[name] = N'Remarks');
IF @var94 IS NOT NULL EXEC(N'ALTER TABLE [PhysicalCountHistories] DROP CONSTRAINT [' + @var94 + '];');
ALTER TABLE [PhysicalCountHistories] ALTER COLUMN [Remarks] nvarchar(501) NULL;
GO

DECLARE @var95 sysname;
SELECT @var95 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Packs]') AND [c].[name] = N'Remarks');
IF @var95 IS NOT NULL EXEC(N'ALTER TABLE [Packs] DROP CONSTRAINT [' + @var95 + '];');
ALTER TABLE [Packs] ALTER COLUMN [Remarks] nvarchar(501) NULL;
GO

DECLARE @var96 sysname;
SELECT @var96 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invoices]') AND [c].[name] = N'Remarks');
IF @var96 IS NOT NULL EXEC(N'ALTER TABLE [Invoices] DROP CONSTRAINT [' + @var96 + '];');
ALTER TABLE [Invoices] ALTER COLUMN [Remarks] nvarchar(501) NULL;
GO

DECLARE @var97 sysname;
SELECT @var97 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'Remarks');
IF @var97 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var97 + '];');
ALTER TABLE [Customers] ALTER COLUMN [Remarks] nvarchar(501) NULL;
GO

DECLARE @var98 sysname;
SELECT @var98 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerPayments]') AND [c].[name] = N'Remarks');
IF @var98 IS NOT NULL EXEC(N'ALTER TABLE [CustomerPayments] DROP CONSTRAINT [' + @var98 + '];');
ALTER TABLE [CustomerPayments] ALTER COLUMN [Remarks] nvarchar(501) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250531041212_remarks', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseOrderNews] ADD [CurrencyId] int NOT NULL DEFAULT 1;
GO

ALTER TABLE [PurchaseOrderNews] ADD [CurrencyRate] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

CREATE INDEX [IX_PurchaseOrderNews_CurrencyId] ON [PurchaseOrderNews] ([CurrencyId]);
GO

ALTER TABLE [PurchaseOrderNews] ADD CONSTRAINT [FK_PurchaseOrderNews_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currencies] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250531041648_prcurrency2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Items] ADD [OldSKU] nvarchar(50) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250531042358_itemoldsku', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var99 sysname;
SELECT @var99 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'OldSKU');
IF @var99 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var99 + '];');
ALTER TABLE [Items] ALTER COLUMN [OldSKU] nvarchar(50) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250531043803_itemoldsku2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Items].[OldSKU]', N'OldCode', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250531064355_itemoldsku3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'8d09d8f1-a09d-4391-a33f-af109d635c29', [PasswordHash] = N'AQAAAAIAAYagAAAAEM0VouG6kL4d9Q+pqdkO0VhOp2nd0CSrNn9/2sGsJDm2nJ5zZl3B8fkUmsCi3rfKlw==', [SecurityStamp] = N'fa4afef6-903e-47e0-b9ae-785b53cd30ca'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'b7bf046c-8d7e-4f5e-9d71-17c703c743ac', [PasswordHash] = N'AQAAAAIAAYagAAAAEBNwm39EhsFfD+1Oi8qau+5q8D4kPNf3AXRqSl1yudIT2ue/CYpmgF9ctmFWrI0b9g==', [SecurityStamp] = N'2a9646c9-5bc5-4bb1-a79a-34041be1ae84'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250531064619_itemoldsku4', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var100 sysname;
SELECT @var100 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNews]') AND [c].[name] = N'CurrencyRate');
IF @var100 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNews] DROP CONSTRAINT [' + @var100 + '];');
ALTER TABLE [PurchaseOrderNews] ALTER COLUMN [CurrencyRate] decimal(18,5) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250531111652_prcurrency3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [PurchaseOrderNewLineItemNewCosts] (
    [Id] int NOT NULL IDENTITY,
    [OrderItemId] int NOT NULL,
    [PreviousCost] decimal(18,2) NOT NULL,
    [NewCost] decimal(18,2) NOT NULL,
    [Remarks] nvarchar(500) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_PurchaseOrderNewLineItemNewCosts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseOrderNewLineItemNewCosts_PurchaseOrderNewLineItems_OrderItemId] FOREIGN KEY ([OrderItemId]) REFERENCES [PurchaseOrderNewLineItems] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_PurchaseOrderNewLineItemNewCosts_OrderItemId] ON [PurchaseOrderNewLineItemNewCosts] ([OrderItemId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250602141423_ponewcost', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [PurchaseOrderNewLineItemNewQuantities] (
    [Id] int NOT NULL IDENTITY,
    [OrderItemId] int NOT NULL,
    [PreviousQuantity] decimal(18,2) NOT NULL,
    [NewQuantity] decimal(18,2) NOT NULL,
    [Remarks] nvarchar(500) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_PurchaseOrderNewLineItemNewQuantities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PurchaseOrderNewLineItemNewQuantities_PurchaseOrderNewLineItems_OrderItemId] FOREIGN KEY ([OrderItemId]) REFERENCES [PurchaseOrderNewLineItems] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'8d070506-4719-4ad5-aeee-0b03cddff36e', [PasswordHash] = N'AQAAAAIAAYagAAAAEJp7JFhmeOPr4Og+1a15jHioudfThq6dRDeoZJdv/uUcWuO8wYWxQvQbHHuZy6i2lg==', [SecurityStamp] = N'91d9df42-02c9-4f5f-b897-cd39b2bbe07d'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'3f587165-8826-4196-97f6-7f2f1efdb128', [PasswordHash] = N'AQAAAAIAAYagAAAAEAXeqkKQ+f463dJ3xQKQvEUB2GSBDcb7xJ5vmHRAhccMADglba1J+1iZFboqnEuMWA==', [SecurityStamp] = N'b6a6c3d3-4ff8-4a93-871c-bc0f551bd0ed'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_PurchaseOrderNewLineItemNewQuantities_OrderItemId] ON [PurchaseOrderNewLineItemNewQuantities] ([OrderItemId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250614013704_ponewquantity', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var101 sysname;
SELECT @var101 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNewLineItemNewQuantities]') AND [c].[name] = N'PreviousQuantity');
IF @var101 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNewLineItemNewQuantities] DROP CONSTRAINT [' + @var101 + '];');
ALTER TABLE [PurchaseOrderNewLineItemNewQuantities] ALTER COLUMN [PreviousQuantity] int NOT NULL;
GO

DECLARE @var102 sysname;
SELECT @var102 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNewLineItemNewQuantities]') AND [c].[name] = N'NewQuantity');
IF @var102 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNewLineItemNewQuantities] DROP CONSTRAINT [' + @var102 + '];');
ALTER TABLE [PurchaseOrderNewLineItemNewQuantities] ALTER COLUMN [NewQuantity] int NOT NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'833c6ab4-e05d-4d7d-a5ac-a1cf4855fbe6', [PasswordHash] = N'AQAAAAIAAYagAAAAEHxPUEYIyeNbAw/9YplAkzElLroYvAg+U1/opwmjPmFt7Rq8lMFGxTXjzaLfa1eQ2g==', [SecurityStamp] = N'77544a7a-08b0-4f6c-9345-c2fa0fc595ac'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'92c112f7-07a6-466a-8cbf-85d00fd64c08', [PasswordHash] = N'AQAAAAIAAYagAAAAEIIkimxddMqSba8rNd65YKUa6tWg8dCMEJ8One8ceL0GgD+wDkiLwVdUdoXI/PuydA==', [SecurityStamp] = N'bd20d0ba-fbf2-4d76-83b4-86da0d244413'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250614020118_ponewquantity2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Warehouses] ADD [IsMain] bit NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250707183651_warehousemain', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Packs] ADD [WarehouseId] int NULL;
GO

CREATE INDEX [IX_Packs_WarehouseId] ON [Packs] ([WarehouseId]);
GO

ALTER TABLE [Packs] ADD CONSTRAINT [FK_Packs_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250707185815_warehousemain2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var103 sysname;
SELECT @var103 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Warehouses]') AND [c].[name] = N'AddressLine1');
IF @var103 IS NOT NULL EXEC(N'ALTER TABLE [Warehouses] DROP CONSTRAINT [' + @var103 + '];');
ALTER TABLE [Warehouses] DROP COLUMN [AddressLine1];
GO

EXEC sp_rename N'[Warehouses].[AddressLine2]', N'Address', N'COLUMN';
GO

DECLARE @var104 sysname;
SELECT @var104 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Warehouses]') AND [c].[name] = N'ContactPerson');
IF @var104 IS NOT NULL EXEC(N'ALTER TABLE [Warehouses] DROP CONSTRAINT [' + @var104 + '];');
ALTER TABLE [Warehouses] ALTER COLUMN [ContactPerson] nvarchar(100) NULL;
GO

DECLARE @var105 sysname;
SELECT @var105 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Warehouses]') AND [c].[name] = N'ContactNumber');
IF @var105 IS NOT NULL EXEC(N'ALTER TABLE [Warehouses] DROP CONSTRAINT [' + @var105 + '];');
ALTER TABLE [Warehouses] ALTER COLUMN [ContactNumber] nvarchar(50) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250708155824_warehousemain3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var106 sysname;
SELECT @var106 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'TIN');
IF @var106 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var106 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [TIN] nvarchar(12) NULL;
GO

DECLARE @var107 sysname;
SELECT @var107 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'OfficeTelNo');
IF @var107 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var107 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [OfficeTelNo] nvarchar(100) NULL;
GO

DECLARE @var108 sysname;
SELECT @var108 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'MobileNumber');
IF @var108 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var108 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [MobileNumber] nvarchar(100) NULL;
GO

DECLARE @var109 sysname;
SELECT @var109 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'ContactPerson');
IF @var109 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var109 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [ContactPerson] nvarchar(100) NULL;
GO

DECLARE @var110 sysname;
SELECT @var110 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'AddressLine2');
IF @var110 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var110 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [AddressLine2] nvarchar(150) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250712171456_supplier3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var111 sysname;
SELECT @var111 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'VAT');
IF @var111 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var111 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [VAT] nvarchar(12) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250712174851_supplier4', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250822145931_Tracker', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TrackerTasks] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Status] nvarchar(20) NOT NULL,
    [Priority] nvarchar(20) NOT NULL,
    [AssignedToId] nvarchar(450) NOT NULL,
    [DueDate] datetime2 NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_TrackerTasks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TrackerTasks_AspNetUsers_AssignedToId] FOREIGN KEY ([AssignedToId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TrackerComments] (
    [Id] uniqueidentifier NOT NULL,
    [TaskId] uniqueidentifier NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_TrackerComments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TrackerComments_TrackerTasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [TrackerTasks] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TrackerErrors] (
    [Id] uniqueidentifier NOT NULL,
    [TaskId] uniqueidentifier NOT NULL,
    [Summary] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Severity] nvarchar(20) NOT NULL,
    [Status] nvarchar(20) NOT NULL,
    [Resolution] nvarchar(max) NULL,
    [FixedAt] datetime2 NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_TrackerErrors] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TrackerErrors_TrackerTasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [TrackerTasks] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TrackerHistories] (
    [Id] uniqueidentifier NOT NULL,
    [TaskId] uniqueidentifier NOT NULL,
    [OldStatus] nvarchar(20) NOT NULL,
    [NewStatus] nvarchar(20) NOT NULL,
    [OldPriority] nvarchar(20) NOT NULL,
    [NewPriority] nvarchar(20) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_TrackerHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TrackerHistories_TrackerTasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [TrackerTasks] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TrackerAttachments] (
    [Id] uniqueidentifier NOT NULL,
    [TaskId] uniqueidentifier NULL,
    [CommentId] uniqueidentifier NULL,
    [ErrorId] uniqueidentifier NULL,
    [FilePath] nvarchar(255) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_TrackerAttachments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TrackerAttachments_TrackerComments_CommentId] FOREIGN KEY ([CommentId]) REFERENCES [TrackerComments] ([Id]),
    CONSTRAINT [FK_TrackerAttachments_TrackerErrors_ErrorId] FOREIGN KEY ([ErrorId]) REFERENCES [TrackerErrors] ([Id]),
    CONSTRAINT [FK_TrackerAttachments_TrackerTasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [TrackerTasks] ([Id])
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'83744ab2-b298-4240-84d0-43ab7b2e6f75', [PasswordHash] = N'AQAAAAIAAYagAAAAEOmAp5EI1n14CsBqMpuqQVEYrzbCFBuGBF8v0Ir+qmX7dD2S8eaLG2o5pjVkwZ/4CQ==', [SecurityStamp] = N'87230791-145b-491b-8fef-6a0acd8f6e28'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'415a228b-358d-416c-a712-04214316a03e', [PasswordHash] = N'AQAAAAIAAYagAAAAEPRoXaQ2lqxd7EzMtV2dOxmLbLMzGnymyyR9MOTnCx8SiD9OQ2I30jGdb/nh12FcUw==', [SecurityStamp] = N'c1c377a7-fb27-4cf5-ad90-761516bd5d81'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_TrackerAttachments_CommentId] ON [TrackerAttachments] ([CommentId]);
GO

CREATE INDEX [IX_TrackerAttachments_ErrorId] ON [TrackerAttachments] ([ErrorId]);
GO

CREATE INDEX [IX_TrackerAttachments_TaskId] ON [TrackerAttachments] ([TaskId]);
GO

CREATE INDEX [IX_TrackerComments_TaskId] ON [TrackerComments] ([TaskId]);
GO

CREATE INDEX [IX_TrackerErrors_TaskId] ON [TrackerErrors] ([TaskId]);
GO

CREATE INDEX [IX_TrackerHistories_TaskId] ON [TrackerHistories] ([TaskId]);
GO

CREATE INDEX [IX_TrackerTasks_AssignedToId] ON [TrackerTasks] ([AssignedToId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250822150458_Tracker1', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[TrackerTasks].[Title]', N'Summary', N'COLUMN';
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'3c9a8a71-0606-49fd-87d0-2d4926730665', [PasswordHash] = N'AQAAAAIAAYagAAAAECtF5D8IDmAphhhzVj1fQ/ijrVsJZFwLE3pyzm7n4IDMTz4+1SmXgJKFUFVbir8ZkQ==', [SecurityStamp] = N'70ac81b9-c836-47f0-8595-5d3b59d34fb4'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'1829a9cf-0f94-457a-a982-3877a3abe55e', [PasswordHash] = N'AQAAAAIAAYagAAAAEB1pN7hUWYPun/hrvpGZ/PAT0tBYpjA2ws6VZPw++qi9H7+9ARikOeCXzA+6WQ5C4g==', [SecurityStamp] = N'dfbb23d9-a937-429a-865d-360041e2ab97'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250822150931_Tracker2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var112 sysname;
SELECT @var112 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TrackerHistories]') AND [c].[name] = N'NewPriority');
IF @var112 IS NOT NULL EXEC(N'ALTER TABLE [TrackerHistories] DROP CONSTRAINT [' + @var112 + '];');
ALTER TABLE [TrackerHistories] DROP COLUMN [NewPriority];
GO

DECLARE @var113 sysname;
SELECT @var113 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TrackerHistories]') AND [c].[name] = N'NewStatus');
IF @var113 IS NOT NULL EXEC(N'ALTER TABLE [TrackerHistories] DROP CONSTRAINT [' + @var113 + '];');
ALTER TABLE [TrackerHistories] DROP COLUMN [NewStatus];
GO

EXEC sp_rename N'[TrackerHistories].[OldStatus]', N'EntityName', N'COLUMN';
GO

EXEC sp_rename N'[TrackerHistories].[OldPriority]', N'ActionType', N'COLUMN';
GO

ALTER TABLE [TrackerHistories] ADD [Description] nvarchar(max) NOT NULL DEFAULT N'';
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'cf7c36d5-9aa7-46da-9624-dcd44c7e1f28', [PasswordHash] = N'AQAAAAIAAYagAAAAEPFWbtq8LEhmJoOgZBdA42w1hrqOgarDWaLCpTwhavCFhZ9hYB/Db93Ji+LKhIP9dw==', [SecurityStamp] = N'3afcc782-5224-4819-a4d6-35d6b1d46673'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'1e9fba1f-58eb-4772-b38d-afb61e661d14', [PasswordHash] = N'AQAAAAIAAYagAAAAEC7wedfLS7lr85ZVuWFYrX+K9OOHF2h/5FR/mbE7JsjVVrXJ1+kDvnshPjJ9vi8sEg==', [SecurityStamp] = N'9c4fc9ba-d699-4c96-9712-1daa6628acda'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250822154341_Tracker3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TrackerTasks] DROP CONSTRAINT [FK_TrackerTasks_AspNetUsers_AssignedToId];
GO

DROP TABLE [TrackerAttachments];
GO

DROP TABLE [TrackerErrors];
GO

DECLARE @var114 sysname;
SELECT @var114 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TrackerHistories]') AND [c].[name] = N'DateModified');
IF @var114 IS NOT NULL EXEC(N'ALTER TABLE [TrackerHistories] DROP CONSTRAINT [' + @var114 + '];');
ALTER TABLE [TrackerHistories] DROP COLUMN [DateModified];
GO

DECLARE @var115 sysname;
SELECT @var115 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TrackerHistories]') AND [c].[name] = N'UserModified');
IF @var115 IS NOT NULL EXEC(N'ALTER TABLE [TrackerHistories] DROP CONSTRAINT [' + @var115 + '];');
ALTER TABLE [TrackerHistories] DROP COLUMN [UserModified];
GO

EXEC sp_rename N'[TrackerTasks].[AssignedToId]', N'AssigneeId', N'COLUMN';
GO

EXEC sp_rename N'[TrackerTasks].[IX_TrackerTasks_AssignedToId]', N'IX_TrackerTasks_AssigneeId', N'INDEX';
GO

CREATE TABLE [TrackerIssues] (
    [Id] uniqueidentifier NOT NULL,
    [TaskId] uniqueidentifier NOT NULL,
    [Summary] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Severity] nvarchar(20) NOT NULL,
    [Status] nvarchar(20) NOT NULL,
    [Resolution] nvarchar(max) NULL,
    [FixedAt] datetime2 NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_TrackerIssues] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TrackerIssues_TrackerTasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [TrackerTasks] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'8115eaa3-c428-448a-ad6a-480c238d6dec', [PasswordHash] = N'AQAAAAIAAYagAAAAEHP5KIaGsXoZACvBV+eHGbCiLqAW7Z6nDtzB9OpzIc6FXoUvqcL2af6nARpYOA91Ug==', [SecurityStamp] = N'6dc5b8e2-e62b-4a7c-9eb7-f575f6302fc6'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'93a622a5-2803-4b7a-8136-c6d8facdd236', [PasswordHash] = N'AQAAAAIAAYagAAAAEL8yBlc4yrpvb0n1AIM3WsPBFR0yGAyxBCzLpBEQtJid1PQ22SorG3r8gM1fT4W8QQ==', [SecurityStamp] = N'c66c1097-f008-4baa-8b1e-84612a1f70b1'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_TrackerIssues_TaskId] ON [TrackerIssues] ([TaskId]);
GO

ALTER TABLE [TrackerTasks] ADD CONSTRAINT [FK_TrackerTasks_AspNetUsers_AssigneeId] FOREIGN KEY ([AssigneeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250823152724_Tracker4', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TrackerIssues] ADD [Replication] nvarchar(max) NOT NULL DEFAULT N'';
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'43f53cdf-5fc5-4a1a-92cc-a9c35fd05235', [PasswordHash] = N'AQAAAAIAAYagAAAAEJoT+ZwiWDalbSoz5VmyVA8svZXfBvq6UoJzF5lbr/8yNgObYPUi6Ifm2ZkAVXNJ1w==', [SecurityStamp] = N'b2b49d83-d875-414f-81db-dd90eeefd302'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'f5a50d0d-1bf4-4ea3-94b6-13faa5a36a65', [PasswordHash] = N'AQAAAAIAAYagAAAAEHYpVbjCoWDluDF/T3+wlYoGqTxPnn9oCmL1dIwxMp63HQeusO5OMq5jSbPMaxgi+w==', [SecurityStamp] = N'2363ae02-819f-4a14-b12d-54418b6d26ae'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250826075308_Tracker5', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TrackerTasks] ADD [ApprovedById] nvarchar(450) NULL;
GO

ALTER TABLE [TrackerTasks] ADD [DateApproved] datetime2 NULL;
GO

ALTER TABLE [TrackerTasks] ADD [DateDisapproved] datetime2 NULL;
GO

ALTER TABLE [TrackerTasks] ADD [DisapprovedById] nvarchar(450) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'0cf166b2-239b-4d30-ac84-018884b65b1b', [PasswordHash] = N'AQAAAAIAAYagAAAAENq69/MR/0l5bxBrGGtciICHTCjJltDdUZdNJZkozDngjk4/ARMMp6qAGOUSmbtd0Q==', [SecurityStamp] = N'c70fec30-e1c4-45d7-9a87-a9820e5d60cf'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'bf9ed71e-badc-427c-b47c-e589a67031c0', [PasswordHash] = N'AQAAAAIAAYagAAAAEDyyV2HyWSVPsShCqYg6MD4MDYIc8VmY0tEsbf767/xRH6JTpu1ejUX7+kszAI/sJw==', [SecurityStamp] = N'430082f8-2f7a-4b46-bc85-89b9b7914d7f'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_TrackerTasks_ApprovedById] ON [TrackerTasks] ([ApprovedById]);
GO

CREATE INDEX [IX_TrackerTasks_DisapprovedById] ON [TrackerTasks] ([DisapprovedById]);
GO

ALTER TABLE [TrackerTasks] ADD CONSTRAINT [FK_TrackerTasks_AspNetUsers_ApprovedById] FOREIGN KEY ([ApprovedById]) REFERENCES [AspNetUsers] ([Id]);
GO

ALTER TABLE [TrackerTasks] ADD CONSTRAINT [FK_TrackerTasks_AspNetUsers_DisapprovedById] FOREIGN KEY ([DisapprovedById]) REFERENCES [AspNetUsers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250828152744_Tracker6', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[TrackerIssues].[FixedAt]', N'DateFixed', N'COLUMN';
GO

ALTER TABLE [TrackerIssues] ADD [FixedBy] nvarchar(450) NULL;
GO

ALTER TABLE [TrackerTasks] ADD [DisapproveReason] nvarchar(max) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4fa95ad1-b4a2-444a-b22d-7e517c04729d', [PasswordHash] = N'AQAAAAIAAYagAAAAEIrZJbxjQokGUf2F/SD5A6LMqa5GwtTrXkesQFao10xPyQPxcKjsYF2OnUcTU5pjxw==', [SecurityStamp] = N'669fcd3f-374e-4aef-86a9-04406be648e2'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'7f895f7d-02b3-451c-b915-6795632619e0', [PasswordHash] = N'AQAAAAIAAYagAAAAEBVI/bpWA/OEzrEfcv/ddmTK3SLq+Rr7RuY/qSHS4QcEHRlfglCnwd8LVolP9gi69A==', [SecurityStamp] = N'e334cc36-6d4f-43de-87ee-14e573be3f08'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250829022245_Tracker7', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TrackerTasks] ADD [Module] nvarchar(100) NOT NULL DEFAULT N'';
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'09773a82-f637-498b-aa88-c99cc62ba527', [PasswordHash] = N'AQAAAAIAAYagAAAAEMMeYcJQAiPRIRTVcHBk+A8qfoeXUFNmg2d3dPvm4gNnMalyuXTz/eNMDQvgdcEyXQ==', [SecurityStamp] = N'd4c5da4e-1be7-434c-8537-8176c26843c9'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'7abce7f8-f61d-4722-9011-e26817de705b', [PasswordHash] = N'AQAAAAIAAYagAAAAEN6e6heYuyOsaXWIehelzzsC9TO3idANha3heFAIZO2mNPNMxJuncUoLqT5TNZtfag==', [SecurityStamp] = N'77f3e2e6-a550-4072-8fd8-a1ec239dfd5d'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250901045555_Tracker8', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[TrackerTasks].[Description]', N'Function', N'COLUMN';
GO

ALTER TABLE [TrackerTasks] ADD [AccessControl] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [TrackerTasks] ADD [Behavior] nvarchar(max) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'9e2b9673-8b9a-4ba5-aa14-992999433694', [PasswordHash] = N'AQAAAAIAAYagAAAAEDquIkgCw0N0oP4NGF6gi7DghAzEF+4HEYOQrW/0L9ZBboPn9WIjysoDd5w4A92oHA==', [SecurityStamp] = N'16b7dd94-1b3a-49bb-880a-cd0c87aefacc'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'333413ba-107b-4624-ae53-ad27c5a249c4', [PasswordHash] = N'AQAAAAIAAYagAAAAEFfU7eqcHp50pXR2/VJZBbwJwUIW33unLWBmk1rThC2+VmW+Uleg9SdobMAxB9OtEQ==', [SecurityStamp] = N'9bdafa7f-9092-4f87-abad-46447fbd779f'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250901060604_Tracker9', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TrackerTasks] ADD [Number] int NOT NULL DEFAULT 0;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4123f594-9b85-420f-b9db-be82afbc5485', [PasswordHash] = N'AQAAAAIAAYagAAAAEGDK5PCgtGLHUK0cm3Ow9RJ9kX9NnFj1WwP/1iyp5R6p3JtLpicRy2fpduSRyZsddA==', [SecurityStamp] = N'ed9f1afa-af39-459f-8cda-32c95f364fe6'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'01c0689e-2f74-43f0-ab5e-26bcba39a9d0', [PasswordHash] = N'AQAAAAIAAYagAAAAEEi+NC/Wh65I/Ju6QhDfMKudzse7uyDOaV0KkDU3iKwlNt8bE4x4WVLQ9pAj5e9E7w==', [SecurityStamp] = N'3845dd84-ce76-41d6-8748-3deda4667c7a'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250903035855_Tracker10', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseReceivings] ADD [HashMicroMoveById] nvarchar(450) NULL;
GO

ALTER TABLE [PurchaseReceivings] ADD [HashMicroMoveDate] datetime2 NULL;
GO

ALTER TABLE [PurchaseReceivings] ADD [HashMicroMoveId] int NULL;
GO

ALTER TABLE [PurchaseReceivings] ADD [HashMicroMoveName] nvarchar(max) NULL;
GO

ALTER TABLE [PurchaseReceivings] ADD [HashMicroMoveStatus] nvarchar(max) NULL;
GO

ALTER TABLE [Invoices] ADD [HashMicroMoveById] nvarchar(450) NULL;
GO

ALTER TABLE [Invoices] ADD [HashMicroMoveDate] datetime2 NULL;
GO

ALTER TABLE [Invoices] ADD [HashMicroMoveId] int NULL;
GO

ALTER TABLE [Invoices] ADD [HashMicroMoveName] nvarchar(max) NULL;
GO

ALTER TABLE [Invoices] ADD [HashMicroMoveStatus] nvarchar(max) NULL;
GO

CREATE INDEX [IX_PurchaseReceivings_HashMicroMoveById] ON [PurchaseReceivings] ([HashMicroMoveById]);
GO

CREATE INDEX [IX_Invoices_HashMicroMoveById] ON [Invoices] ([HashMicroMoveById]);
GO

ALTER TABLE [Invoices] ADD CONSTRAINT [FK_Invoices_AspNetUsers_HashMicroMoveById] FOREIGN KEY ([HashMicroMoveById]) REFERENCES [AspNetUsers] ([Id]);
GO

ALTER TABLE [PurchaseReceivings] ADD CONSTRAINT [FK_PurchaseReceivings_AspNetUsers_HashMicroMoveById] FOREIGN KEY ([HashMicroMoveById]) REFERENCES [AspNetUsers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250903150010_hashmicro', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Drivers] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Nickname] nvarchar(100) NOT NULL,
    [LicenseNumber] nvarchar(100) NULL,
    [Restriction] nvarchar(100) NULL,
    [LicenseDateExpiration] datetime2 NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Drivers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Vehicles] (
    [Id] int NOT NULL IDENTITY,
    [FleetPlateNumber] nvarchar(100) NOT NULL,
    [VehicleType] nvarchar(100) NOT NULL,
    [DateRegistered] datetime2 NULL,
    [DateExpiration] datetime2 NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY ([Id])
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'0a753941-8a0c-48e9-91cb-94f7f621a34d', [PasswordHash] = N'AQAAAAIAAYagAAAAEGrexQjPcOrdG7bAbK9pUxC/TSxUf1eXAty4XP+XveQ7TxwbjW44cPgjKcnGxttqgw==', [SecurityStamp] = N'ee871380-bf2a-4b9d-9f41-caa1ab7b9e25'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'bd9e71c4-e9fe-4ac9-af6a-feb85a99f041', [PasswordHash] = N'AQAAAAIAAYagAAAAEDBHQACPnKXbu1hnmUpJuwM4PP8YlxWYyulyEgSbwF6djf7vIpKa9yXuywU738NlDA==', [SecurityStamp] = N'084fa244-3448-4610-8ee0-58b13f6fe2f5'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250905121023_DriverVehicle', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Vehicles] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Drivers] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'd6ea3d5c-9779-4076-858f-0daaa45dc9bb', [PasswordHash] = N'AQAAAAIAAYagAAAAEI5ToLTuQL7ca/q/hPptHroDr9597DvVGRoGxk61L5gsDg4FXkpYaM8TMpOddmQZHg==', [SecurityStamp] = N'f6cf5315-a0a7-44a0-a5ee-c407c2c1a366'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4d7d34ff-7c08-4a83-981e-2b2552ffdbbf', [PasswordHash] = N'AQAAAAIAAYagAAAAEBFI23wnr5TaCzT52hCZmOrrZusVdLaWPQN5huAjZ+40GFDKzYpBQsbrIq24VlOxuQ==', [SecurityStamp] = N'5921c2e3-4a8c-4358-b8d1-8e9102f39189'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250905153848_DriverVehicle2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [IsNavIconPlain] bit NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'49430f47-3a63-4a86-b025-19ad47a258fc', [PasswordHash] = N'AQAAAAIAAYagAAAAEFxGs4l3yfFfH1GoIkGDJvXNi3dP+w7E+M6r8F3rbviboN1OSFh5M+f0BwMkClSuBw==', [SecurityStamp] = N'a781111f-4136-423b-baa9-5f6d122a4850'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'da28b11d-4c88-4bb1-8403-d2098aba9e60', [PasswordHash] = N'AQAAAAIAAYagAAAAEFMeu9pFN+EulY0GtvU2JohmTvel8nlJFGR8FbigMwSj862ad7rgrpKaY/+Agh9wwA==', [SecurityStamp] = N'9d6eb69f-ed28-46a8-be2c-1fab124f4830'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250905155320_UserMigration1', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var116 sysname;
SELECT @var116 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Currencies]') AND [c].[name] = N'Code');
IF @var116 IS NOT NULL EXEC(N'ALTER TABLE [Currencies] DROP CONSTRAINT [' + @var116 + '];');
ALTER TABLE [Currencies] DROP COLUMN [Code];
GO

DECLARE @var117 sysname;
SELECT @var117 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Currencies]') AND [c].[name] = N'EffectiveDate');
IF @var117 IS NOT NULL EXEC(N'ALTER TABLE [Currencies] DROP CONSTRAINT [' + @var117 + '];');
ALTER TABLE [Currencies] DROP COLUMN [EffectiveDate];
GO

ALTER TABLE [Currencies] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'224182d4-cff6-4ca3-bbf4-f7deb0703903', [PasswordHash] = N'AQAAAAIAAYagAAAAENHxZRt8Z+gnfzboltKzll3JISsm/m7KfJ8j2MbA61wELHTvA1Xv7OCGwYpCRl9adA==', [SecurityStamp] = N'd5e797b1-c972-4025-a41e-09422189da04'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'9b02084c-8f04-4509-b8cf-9e5e2e87267d', [PasswordHash] = N'AQAAAAIAAYagAAAAEOGBkq3NG6kRegHPN5nh71a5ej5EclP33HZqp/nsnfYhOTEcM5w98/LmPIzEFJ8faA==', [SecurityStamp] = N'29520c40-c4cb-42ec-b5fd-5017e5635cd3'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250910171031_currency', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'bd02dc8b-40bc-4e66-9e6a-b84afe9460e7', [PasswordHash] = N'AQAAAAIAAYagAAAAEI0OXgDEtmthYSQUsOxPuz1/ZnFoByXXocfL5fNOISTEc/3SsaSltmBf9ObQWT0v0w==', [SecurityStamp] = N'a4a92854-ade6-45d8-bb27-dd527be5ff54'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'52d43833-a9da-49f5-ae4e-157eb5b257ec', [PasswordHash] = N'AQAAAAIAAYagAAAAED2UeWIfxaiZFwOMPjU7IHo1t33cwRR+T2EMtajV3A99+kqL+Yw4vwaND4scV2nmCQ==', [SecurityStamp] = N'9bd828f8-38a2-449d-a49e-139916b95186'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250911040844_Currency2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var118 sysname;
SELECT @var118 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Currencies]') AND [c].[name] = N'Rate');
IF @var118 IS NOT NULL EXEC(N'ALTER TABLE [Currencies] DROP CONSTRAINT [' + @var118 + '];');
ALTER TABLE [Currencies] DROP COLUMN [Rate];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250911142407_Currency3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ServiceLevelAgreements] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [PriorAvailability] bigint NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_ServiceLevelAgreements] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250912155641_sla', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseOrderNews] ADD [Country] nvarchar(max) NULL;
GO

ALTER TABLE [PurchaseOrderNews] ADD [DueDate] datetime2 NULL;
GO

ALTER TABLE [Invoices] ADD [DueDate] datetime2 NULL;
GO

CREATE TABLE [CustomerBrandDiscount] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [BrandId] int NULL,
    [Discount] decimal(18,2) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_CustomerBrandDiscount] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerBrandDiscount_Brands_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Brands] ([Id]),
    CONSTRAINT [FK_CustomerBrandDiscount_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'3393f162-22d4-49ac-aa47-6775aeda0ba4', [PasswordHash] = N'AQAAAAIAAYagAAAAEJCfvX4j/Kc0hZuWAdvG8BOrhD8g52+mplP76SXg5MABB/KoYkdXWBfmKa/wXL+4Sg==', [SecurityStamp] = N'd25cb937-7672-4556-ba51-7b958b7fafa3'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'aa717c0d-46eb-45fc-bcfb-3651dd149bf6', [PasswordHash] = N'AQAAAAIAAYagAAAAEGIdo+ILfQ0Em/1gz5kOl6YuEhs9tOIs6xLC6e6KAFXeTNQ5/r+eESNzUW/Pvw1LVA==', [SecurityStamp] = N'fc8da272-36bf-407d-9371-8bd56560c266'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_CustomerBrandDiscount_BrandId] ON [CustomerBrandDiscount] ([BrandId]);
GO

CREATE INDEX [IX_CustomerBrandDiscount_CustomerId] ON [CustomerBrandDiscount] ([CustomerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250918165143_duedatecolumns', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var119 sysname;
SELECT @var119 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNews]') AND [c].[name] = N'Country');
IF @var119 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNews] DROP CONSTRAINT [' + @var119 + '];');
ALTER TABLE [PurchaseOrderNews] DROP COLUMN [Country];
GO

ALTER TABLE [Suppliers] ADD [Country] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Regions] ADD [CountryId] int NOT NULL DEFAULT 608;
GO

CREATE TABLE [Countries] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Alpha2] nvarchar(max) NOT NULL,
    [Alpha3] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] ON;
INSERT INTO [Countries] ([Id], [Alpha2], [Alpha3], [Name])
VALUES (4, N'AF', N'AFG', N'Afghanistan'),
(8, N'AL', N'ALB', N'Albania'),
(10, N'AQ', N'ATA', N'Antarctica'),
(12, N'DZ', N'DZA', N'Algeria'),
(16, N'AS', N'ASM', N'American Samoa'),
(20, N'AD', N'AND', N'Andorra'),
(24, N'AO', N'AGO', N'Angola'),
(28, N'AG', N'ATG', N'Antigua and Barbuda'),
(31, N'AZ', N'AZE', N'Azerbaijan'),
(32, N'AR', N'ARG', N'Argentina'),
(36, N'AU', N'AUS', N'Australia'),
(40, N'AT', N'AUT', N'Austria'),
(44, N'BS', N'BHS', N'Bahamas'),
(48, N'BH', N'BHR', N'Bahrain'),
(50, N'BD', N'BGD', N'Bangladesh'),
(51, N'AM', N'ARM', N'Armenia'),
(52, N'BB', N'BRB', N'Barbados'),
(56, N'BE', N'BEL', N'Belgium'),
(60, N'BM', N'BMU', N'Bermuda'),
(64, N'BT', N'BTN', N'Bhutan'),
(68, N'BO', N'BOL', N'Bolivia (Plurinational State of)'),
(70, N'BA', N'BIH', N'Bosnia and Herzegovina'),
(72, N'BW', N'BWA', N'Botswana'),
(74, N'BV', N'BVT', N'Bouvet Island'),
(76, N'BR', N'BRA', N'Brazil'),
(84, N'BZ', N'BLZ', N'Belize'),
(86, N'IO', N'IOT', N'British Indian Ocean Territory'),
(90, N'SB', N'SLB', N'Solomon Islands'),
(96, N'BN', N'BRN', N'Brunei Darussalam'),
(100, N'BG', N'BGR', N'Bulgaria'),
(104, N'MM', N'MMR', N'Myanmar'),
(108, N'BI', N'BDI', N'Burundi'),
(112, N'BY', N'BLR', N'Belarus'),
(116, N'KH', N'KHM', N'Cambodia'),
(120, N'CM', N'CMR', N'Cameroon'),
(124, N'CA', N'CAN', N'Canada'),
(132, N'CV', N'CPV', N'Cabo Verde'),
(136, N'KY', N'CYM', N'Cayman Islands'),
(140, N'CF', N'CAF', N'Central African Republic'),
(144, N'LK', N'LKA', N'Sri Lanka');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] ON;
INSERT INTO [Countries] ([Id], [Alpha2], [Alpha3], [Name])
VALUES (148, N'TD', N'TCD', N'Chad'),
(152, N'CL', N'CHL', N'Chile'),
(156, N'CN', N'CHN', N'China'),
(158, N'TW', N'TWN', N'Taiwan (Province of China)'),
(162, N'CX', N'CXR', N'Christmas Island'),
(166, N'CC', N'CCK', N'Cocos (Keeling) Islands'),
(170, N'CO', N'COL', N'Colombia'),
(174, N'KM', N'COM', N'Comoros'),
(175, N'YT', N'MYT', N'Mayotte'),
(178, N'CG', N'COG', N'Congo (Republic of the)'),
(180, N'CD', N'COD', N'Congo (Democratic Republic of the)'),
(184, N'CK', N'COK', N'Cook Islands'),
(188, N'CR', N'CRI', N'Costa Rica'),
(191, N'HR', N'HRV', N'Croatia'),
(192, N'CU', N'CUB', N'Cuba'),
(196, N'CY', N'CYP', N'Cyprus'),
(203, N'CZ', N'CZE', N'Czechia'),
(204, N'BJ', N'BEN', N'Benin'),
(208, N'DK', N'DNK', N'Denmark'),
(212, N'DM', N'DMA', N'Dominica'),
(214, N'DO', N'DOM', N'Dominican Republic'),
(218, N'EC', N'ECU', N'Ecuador'),
(222, N'SV', N'SLV', N'El Salvador'),
(226, N'GQ', N'GNQ', N'Equatorial Guinea'),
(231, N'ET', N'ETH', N'Ethiopia'),
(232, N'ER', N'ERI', N'Eritrea'),
(233, N'EE', N'EST', N'Estonia'),
(234, N'FO', N'FRO', N'Faroe Islands'),
(238, N'FK', N'FLK', N'Falkland Islands (Malvinas)'),
(239, N'GS', N'SGS', N'South Georgia and the South Sandwich Islands'),
(242, N'FJ', N'FJI', N'Fiji'),
(246, N'FI', N'FIN', N'Finland'),
(248, N'AX', N'ALA', N'Åland Islands'),
(250, N'FR', N'FRA', N'France'),
(254, N'GF', N'GUF', N'French Guiana'),
(258, N'PF', N'PYF', N'French Polynesia'),
(260, N'TF', N'ATF', N'French Southern Territories'),
(262, N'DJ', N'DJI', N'Djibouti'),
(266, N'GA', N'GAB', N'Gabon'),
(268, N'GE', N'GEO', N'Georgia'),
(270, N'GM', N'GMB', N'Gambia'),
(275, N'PS', N'PSE', N'Palestine, State of');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] ON;
INSERT INTO [Countries] ([Id], [Alpha2], [Alpha3], [Name])
VALUES (276, N'DE', N'DEU', N'Germany'),
(288, N'GH', N'GHA', N'Ghana'),
(292, N'GI', N'GIB', N'Gibraltar'),
(296, N'KI', N'KIR', N'Kiribati'),
(300, N'GR', N'GRC', N'Greece'),
(304, N'GL', N'GRL', N'Greenland'),
(308, N'GD', N'GRD', N'Grenada'),
(312, N'GP', N'GLP', N'Guadeloupe'),
(316, N'GU', N'GUM', N'Guam'),
(320, N'GT', N'GTM', N'Guatemala'),
(324, N'GN', N'GIN', N'Guinea'),
(328, N'GY', N'GUY', N'Guyana'),
(332, N'HT', N'HTI', N'Haiti'),
(334, N'HM', N'HMD', N'Heard Island and McDonald Islands'),
(336, N'VA', N'VAT', N'Holy See'),
(340, N'HN', N'HND', N'Honduras'),
(344, N'HK', N'HKG', N'Hong Kong'),
(348, N'HU', N'HUN', N'Hungary'),
(352, N'IS', N'ISL', N'Iceland'),
(356, N'IN', N'IND', N'India'),
(360, N'ID', N'IDN', N'Indonesia'),
(364, N'IR', N'IRN', N'Iran (Islamic Republic of)'),
(368, N'IQ', N'IRQ', N'Iraq'),
(372, N'IE', N'IRL', N'Ireland'),
(380, N'IT', N'ITA', N'Italy'),
(384, N'CI', N'CIV', N'Côte d''Ivoire'),
(388, N'JM', N'JAM', N'Jamaica'),
(392, N'JP', N'JPN', N'Japan'),
(398, N'KZ', N'KAZ', N'Kazakhstan'),
(400, N'JO', N'JOR', N'Jordan'),
(404, N'KE', N'KEN', N'Kenya'),
(408, N'KP', N'PRK', N'Korea (Democratic People’s Republic of)'),
(410, N'KR', N'KOR', N'Korea (Republic of)'),
(414, N'KW', N'KWT', N'Kuwait'),
(417, N'KG', N'KGZ', N'Kyrgyzstan'),
(418, N'LA', N'LAO', N'Lao People’s Democratic Republic'),
(422, N'LB', N'LBN', N'Lebanon'),
(426, N'LS', N'LSO', N'Lesotho'),
(428, N'LV', N'LVA', N'Latvia'),
(430, N'LR', N'LBR', N'Liberia'),
(434, N'LY', N'LB Y', N'Libya'),
(438, N'LI', N'LIE', N'Liechtenstein');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] ON;
INSERT INTO [Countries] ([Id], [Alpha2], [Alpha3], [Name])
VALUES (440, N'LT', N'LTU', N'Lithuania'),
(442, N'LU', N'LUX', N'Luxembourg'),
(446, N'MO', N'MAC', N'Macao'),
(450, N'MG', N'MDG', N'Madagascar'),
(454, N'MW', N'MWI', N'Malawi'),
(458, N'MY', N'MYS', N'Malaysia'),
(462, N'MV', N'MDV', N'Maldives'),
(466, N'ML', N'MLI', N'Mali'),
(470, N'MT', N'MLT', N'Malta'),
(474, N'MQ', N'MTQ', N'Martinique'),
(478, N'MR', N'MRT', N'Mauritania'),
(480, N'MU', N'MUS', N'Mauritius'),
(484, N'MX', N'MEX', N'Mexico'),
(492, N'MC', N'MCO', N'Monaco'),
(496, N'MN', N'MNG', N'Mongolia'),
(498, N'MD', N'MDA', N'Moldova (Republic of)'),
(499, N'ME', N'MNE', N'Montenegro'),
(500, N'MS', N'MSR', N'Montserrat'),
(504, N'MA', N'MAR', N'Morocco'),
(508, N'MZ', N'MOZ', N'Mozambique'),
(512, N'OM', N'OMN', N'Oman'),
(516, N'NA', N'NAM', N'Namibia'),
(520, N'NR', N'NRU', N'Nauru'),
(524, N'NP', N'NPL', N'Nepal'),
(528, N'NL', N'NLD', N'Netherlands'),
(531, N'CW', N'CUW', N'Curaçao'),
(533, N'AW', N'ABW', N'Aruba'),
(534, N'SX', N'SXM', N'Sint Maarten (Dutch part)'),
(535, N'BQ', N'BES', N'Bonaire, Sint Eustatius and Saba'),
(540, N'NC', N'NCL', N'New Caledonia'),
(554, N'NZ', N'NZL', N'New Zealand'),
(558, N'NI', N'NIC', N'Nicaragua'),
(562, N'NE', N'NER', N'Niger'),
(566, N'NG', N'NGA', N'Nigeria'),
(570, N'NU', N'NIU', N'Niue'),
(574, N'NF', N'NFK', N'Norfolk Island'),
(578, N'NO', N'NOR', N'Norway'),
(580, N'MP', N'MNP', N'Northern Mariana Islands'),
(583, N'FM', N'FSM', N'Micronesia (Federated States of)'),
(584, N'MH', N'MHL', N'Marshall Islands'),
(585, N'PW', N'PLW', N'Palau'),
(586, N'PK', N'PAK', N'Pakistan');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] ON;
INSERT INTO [Countries] ([Id], [Alpha2], [Alpha3], [Name])
VALUES (591, N'PA', N'PAN', N'Panama'),
(598, N'PG', N'PNG', N'Papua New Guinea'),
(600, N'PY', N'PRY', N'Paraguay'),
(604, N'PE', N'PER', N'Peru'),
(608, N'PH', N'PHL', N'Philippines'),
(612, N'PN', N'PCN', N'Pitcairn'),
(616, N'PL', N'POL', N'Poland'),
(620, N'PT', N'PRT', N'Portugal'),
(624, N'GW', N'GNB', N'Guinea-Bissau'),
(630, N'PR', N'PRI', N'Puerto Rico'),
(634, N'QA', N'QAT', N'Qatar'),
(638, N'RE', N'REU', N'Réunion'),
(642, N'RO', N'ROU', N'Romania'),
(643, N'RU', N'RUS', N'Russian Federation'),
(646, N'RW', N'RWA', N'Rwanda'),
(652, N'BL', N'BLM', N'Saint Barthélemy'),
(654, N'SH', N'SHN', N'Saint Helena, Ascension and Tristan da Cunha'),
(659, N'KN', N'KNA', N'Saint Kitts and Nevis'),
(660, N'AI', N'AIA', N'Anguilla'),
(662, N'LC', N'LCA', N'Saint Lucia'),
(663, N'MF', N'MAF', N'Saint Martin (French part)'),
(666, N'PM', N'SPM', N'Saint Pierre and Miquelon'),
(670, N'VC', N'VCT', N'Saint Vincent and the Grenadines'),
(674, N'SM', N'SMR', N'San Marino'),
(678, N'ST', N'STP', N'Sao Tome and Principe'),
(682, N'SA', N'SAU', N'Saudi Arabia'),
(686, N'SN', N'SEN', N'Senegal'),
(688, N'RS', N'SRB', N'Serbia'),
(690, N'SC', N'SYC', N'Seychelles'),
(694, N'SL', N'SLE', N'Sierra Leone'),
(702, N'SG', N'SGP', N'Singapore'),
(703, N'SK', N'SVK', N'Slovakia'),
(704, N'VN', N'VNM', N'Viet Nam'),
(705, N'SI', N'SVN', N'Slovenia'),
(706, N'SO', N'SOM', N'Somalia'),
(710, N'ZA', N'ZAF', N'South Africa'),
(716, N'ZW', N'ZWE', N'Zimbabwe'),
(724, N'ES', N'ESP', N'Spain'),
(728, N'SS', N'SSD', N'South Sudan'),
(729, N'SD', N'SDN', N'Sudan'),
(740, N'SR', N'SUR', N'Suriname'),
(744, N'SJ', N'SJM', N'Svalbard and Jan Mayen');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] ON;
INSERT INTO [Countries] ([Id], [Alpha2], [Alpha3], [Name])
VALUES (748, N'SZ', N'SWZ', N'Eswatini'),
(752, N'SE', N'SWE', N'Sweden'),
(756, N'CH', N'CHE', N'Switzerland'),
(760, N'SY', N'SYR', N'Syrian Arab Republic'),
(762, N'TJ', N'TJK', N'Tajikistan'),
(764, N'TH', N'THA', N'Thailand'),
(768, N'TG', N'TGO', N'Togo'),
(780, N'TT', N'TTO', N'Trinidad and Tobago'),
(784, N'AE', N'ARE', N'United Arab Emirates'),
(788, N'TN', N'TUN', N'Tunisia'),
(792, N'TR', N'TUR', N'Turkey'),
(800, N'UG', N'UGA', N'Uganda'),
(804, N'UA', N'UKR', N'Ukraine'),
(818, N'EG', N'EGY', N'Egypt'),
(826, N'GB', N'GBR', N'United Kingdom of Great Britain and Northern Ireland'),
(831, N'GG', N'GGY', N'Guernsey'),
(832, N'JE', N'JEY', N'Jersey'),
(834, N'TZ', N'TZA', N'Tanzania, United Republic of'),
(840, N'US', N'USA', N'United States of America'),
(850, N'VI', N'VIR', N'Virgin Islands (U.S.)'),
(854, N'BF', N'BFA', N'Burkina Faso'),
(858, N'UY', N'URY', N'Uruguay'),
(862, N'VE', N'VEN', N'Venezuela (Bolivarian Republic of)'),
(882, N'WS', N'WSM', N'Samoa'),
(891, N'YE', N'YEM', N'Yemen'),
(894, N'ZM', N'ZMB', N'Zambia');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Alpha2', N'Alpha3', N'Name') AND [object_id] = OBJECT_ID(N'[Countries]'))
    SET IDENTITY_INSERT [Countries] OFF;
GO

CREATE INDEX [IX_Regions_CountryId] ON [Regions] ([CountryId]);
GO

ALTER TABLE [Regions] ADD CONSTRAINT [FK_Regions_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250919154445_Country', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var120 sysname;
SELECT @var120 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'Country');
IF @var120 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var120 + '];');
ALTER TABLE [Suppliers] DROP COLUMN [Country];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250919155415_Country2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Regions] ADD [IsLocal] bit NOT NULL DEFAULT CAST(1 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250920122045_regionmigrate', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Regions] DROP CONSTRAINT [FK_Regions_Countries_CountryId];
GO

ALTER TABLE [Suppliers] DROP CONSTRAINT [FK_Suppliers_Regions_RegionId];
GO

DECLARE @var121 sysname;
SELECT @var121 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Regions]') AND [c].[name] = N'IsLocal');
IF @var121 IS NOT NULL EXEC(N'ALTER TABLE [Regions] DROP CONSTRAINT [' + @var121 + '];');
ALTER TABLE [Regions] DROP COLUMN [IsLocal];
GO

DECLARE @var122 sysname;
SELECT @var122 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'RegionId');
IF @var122 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var122 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [RegionId] int NULL;
GO

ALTER TABLE [Suppliers] ADD [CountryId] int NOT NULL DEFAULT 608;
GO

DECLARE @var123 sysname;
SELECT @var123 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Regions]') AND [c].[name] = N'CountryId');
IF @var123 IS NOT NULL EXEC(N'ALTER TABLE [Regions] DROP CONSTRAINT [' + @var123 + '];');
ALTER TABLE [Regions] ALTER COLUMN [CountryId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'152f433d-7879-4777-a877-2208c467fdd6', [PasswordHash] = N'AQAAAAIAAYagAAAAEAHWDeCVV+gtwSRJFDkhbm/s3ZUtJ9biHRd9ZKGO8OS/I8//M+Do8LPR7DLUZVs1eA==', [SecurityStamp] = N'7c4e2b91-1d30-40a6-9da8-468e90b51f43'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'2b8bf345-271f-4389-9758-856e73f6d53b', [PasswordHash] = N'AQAAAAIAAYagAAAAEBwP9tQ8Iqt4357L/SMoMwjfA31J/1Lx+KNfU9RuPAkuonjpc+tNjs5LaVPkYiL2CQ==', [SecurityStamp] = N'a116ee39-8980-4626-b32c-5444ca943f5f'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

UPDATE [Regions] SET [CountryId] = NULL
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Regions] SET [CountryId] = NULL
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Regions] SET [CountryId] = NULL
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [Regions] SET [CountryId] = NULL
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_Suppliers_CountryId] ON [Suppliers] ([CountryId]);
GO

ALTER TABLE [Regions] ADD CONSTRAINT [FK_Regions_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id]);
GO

ALTER TABLE [Suppliers] ADD CONSTRAINT [FK_Suppliers_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Suppliers] ADD CONSTRAINT [FK_Suppliers_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250920175457_suppliermigrate', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Regions] DROP CONSTRAINT [FK_Regions_Countries_CountryId];
GO

DROP INDEX [IX_Regions_CountryId] ON [Regions];
GO

DECLARE @var124 sysname;
SELECT @var124 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Regions]') AND [c].[name] = N'CountryId');
IF @var124 IS NOT NULL EXEC(N'ALTER TABLE [Regions] DROP CONSTRAINT [' + @var124 + '];');
ALTER TABLE [Regions] DROP COLUMN [CountryId];
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'd881619e-1cc6-4de9-8a29-dc475e58aeef', [PasswordHash] = N'AQAAAAIAAYagAAAAEBlru3pJOJFZL7Pe1iUsT+9b8gjG9Gk8siHgO5EJRyqCabK2hQiBvd4dkndKpc8Luw==', [SecurityStamp] = N'56885b0e-026b-4a5d-a56c-f5abb95dacaf'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'15dd48f6-b83d-4441-8d25-1d57e9c9b773', [PasswordHash] = N'AQAAAAIAAYagAAAAEGRVWan8O47d8iH4T2z8jakT6oAQD2CL1uilKC8i5DgDP/J/F8wxNxefl+K+NmHy8A==', [SecurityStamp] = N'ec4d9787-4e08-4722-bd5f-52a60c8a82fc'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250920180145_suppliermigrate2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'173dc481-0e6b-4a6c-a712-e4ba5d47083b', [PasswordHash] = N'AQAAAAIAAYagAAAAEJI9YQhDXIHiOSK7tzXT+TWjwhQkHfDdO3PutIId0W2rwd0t3lYxWvFWLYlApoe9qQ==', [SecurityStamp] = N'44854276-d9f1-40db-8beb-2dc4c2eb154b'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'53585fff-7d5f-4edb-a86e-9892e492d19d', [PasswordHash] = N'AQAAAAIAAYagAAAAEPhZSh+/NlnBRybLuYuoLbn1bOz6SXmGDGhgoxqvaCbF6u1Wkl8pMn3QRgO3OhYvjg==', [SecurityStamp] = N'accaa79e-84d6-4c45-baba-2bc70f8dcc12'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250921131958_suppliermigrate3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Suppliers] DROP CONSTRAINT [FK_Suppliers_Regions_RegionId];
GO

DROP INDEX [IX_Suppliers_RegionId] ON [Suppliers];
DECLARE @var125 sysname;
SELECT @var125 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'RegionId');
IF @var125 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var125 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [RegionId] int NOT NULL;
ALTER TABLE [Suppliers] ADD DEFAULT 0 FOR [RegionId];
CREATE INDEX [IX_Suppliers_RegionId] ON [Suppliers] ([RegionId]);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'2ea47d4b-7ad7-4f7d-abe9-4d0833501fa3', [PasswordHash] = N'AQAAAAIAAYagAAAAEDvALM+Td2cpwJwa21fBXr4nIvFV+mXQSVfeL92qOTHj0ovMEB7rlWHzNwzByODymA==', [SecurityStamp] = N'a45482b3-ce96-4f1e-9710-90805cd23d01'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'ef8c4d6f-cb4d-4be5-8e31-7bc402ee8648', [PasswordHash] = N'AQAAAAIAAYagAAAAEK4sTfIiOW82eLjm6CAAnPrdYT6HCm+WPZLHXi1Qr6cL/LmXHK/yEeWP79nRFsutaw==', [SecurityStamp] = N'bdb2894c-23a8-416b-9835-6599975fb3b4'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [Suppliers] ADD CONSTRAINT [FK_Suppliers_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250921133815_suppliermigrate4', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Suppliers] DROP CONSTRAINT [FK_Suppliers_Regions_RegionId];
GO

DECLARE @var126 sysname;
SELECT @var126 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'RegionId');
IF @var126 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var126 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [RegionId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'59d5d044-1e9b-4580-b481-8bd130dc7c35', [PasswordHash] = N'AQAAAAIAAYagAAAAEB8IycWOqoQh5XOqkTwEAl4IS4Mi2nYWvxxcPx9j/V6oz1jE4ZZjCWCigDZlXNpQpw==', [SecurityStamp] = N'ea429ba7-9e76-4920-b8d7-de015945f74f'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'7c520851-ed36-4431-949f-180e9d612166', [PasswordHash] = N'AQAAAAIAAYagAAAAEMiowsa3G2zgOYsGlL80yBvRKclnF4kBPmzSxXAY/5BS2emweGQo2Zwe0ydq8H7mag==', [SecurityStamp] = N'd42a3eac-ad8e-4f15-8c1e-2ccea0b0419a'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [Suppliers] ADD CONSTRAINT [FK_Suppliers_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250921134259_suppliermigrate5', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'153100e1-a6ca-461b-af0c-f9ba41ddb9a3', [PasswordHash] = N'AQAAAAIAAYagAAAAEFCSaXqN7QSk2q7Mpkuhv+HKhRR0IYCpbowoImXXzgLjxMVvgPonDzs5rQ0XCEixLg==', [SecurityStamp] = N'b2e2b541-814d-450e-82e0-5299f2f2bb36'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'd3a39dce-ad82-4184-8864-8d83feb36397', [PasswordHash] = N'AQAAAAIAAYagAAAAEIjyk8qur2FJr34JSXBtahGtwWbTPVEb1/+WqNbo+pa15E911NgUa6Yswx+yMWFBFA==', [SecurityStamp] = N'54f14f18-13b6-4296-aa59-1122fd62c5dc'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250930160420_accessmigrate', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250930161227_accessmigrate2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Items] ADD [Image] nvarchar(MAX) NOT NULL DEFAULT N'';
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'dc25ea9a-7786-43f7-8894-0e6f6a09010c', [PasswordHash] = N'AQAAAAIAAYagAAAAEAHBYCu7VFgCZVA1U0CNsJe90fvHfWAteAZmrJKOvcSS58WcQCnnZ5o+m4i/rCzcGQ==', [SecurityStamp] = N'400795ed-f33e-4e39-81cd-f6611c03f9d0'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'49427c1d-692b-46d7-bcce-fb5ccb8cf14e', [PasswordHash] = N'AQAAAAIAAYagAAAAEJBHY+CWgRuJDuBDs/2Dkn6wt0KBrhDC6zQ4G3vYy4yPGk5/co9OKu5uOzGedwe4Qw==', [SecurityStamp] = N'4ec2e712-e7a9-4dfb-98ad-3f4882406688'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251001030057_itemmigrate', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var127 sysname;
SELECT @var127 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'Image');
IF @var127 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var127 + '];');
ALTER TABLE [Items] ALTER COLUMN [Image] nvarchar(MAX) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'cba5ee59-9791-4e23-a54a-7f73835716fe', [PasswordHash] = N'AQAAAAIAAYagAAAAEAf9f1N77eKiILMSGoJAkST0l0gqOICL2BDqp9HxKiswtF58S+mVDy6vce+Uz5cUdw==', [SecurityStamp] = N'ff8967b3-d357-4c98-8ac2-4e4ac497218b'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'6f4532e1-0051-41b8-94b5-1fb108409ea5', [PasswordHash] = N'AQAAAAIAAYagAAAAEFqCW7N/atDmNysLKpCuXNs4+91E+VxbybSxMtdtOdX6FmdUfm2/uk+EBrKGCi0UoQ==', [SecurityStamp] = N'8e36361f-3e29-4f4f-bd61-e77fc72732f4'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251001030803_itemmigrate2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customers] ADD [Branch] nvarchar(250) NULL;
GO

ALTER TABLE [Customers] ADD [Priorities] nvarchar(250) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'6a5e7a32-e79d-4eef-bb1d-7269e350a4c2', [PasswordHash] = N'AQAAAAIAAYagAAAAED8mM0uTB8nA0hq2rt+/5xqUoOLJGF+ruvZHI9QhuDTdJklmCR6V6yt+pLmHP8Er5g==', [SecurityStamp] = N'13e5c1ca-5f0d-43db-93eb-83b35e54756b'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'92c06215-9099-4f15-aaf7-0c7b10629ebb', [PasswordHash] = N'AQAAAAIAAYagAAAAEFA5gct1Rkn92v4wx7Lb7P6/l46VRZYi2tNAiqCUp5tQ80JlKrY1XmihE/JKT+w5uA==', [SecurityStamp] = N'ad4274fd-a544-4aee-bdcb-468d8b91da0a'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251003085826_20251003_Customer', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var128 sysname;
SELECT @var128 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceLevelAgreements]') AND [c].[name] = N'PriorAvailability');
IF @var128 IS NOT NULL EXEC(N'ALTER TABLE [ServiceLevelAgreements] DROP CONSTRAINT [' + @var128 + '];');
ALTER TABLE [ServiceLevelAgreements] DROP COLUMN [PriorAvailability];
GO

ALTER TABLE [ServiceLevelAgreements] ADD [PriorAvailability] time NOT NULL DEFAULT '00:00:00';
GO

ALTER TABLE [SalesOrders] ADD [OverallDiscount] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [SalesOrders] ADD [ServiceLevelAgreementId] int NULL;
GO

ALTER TABLE [SalesOrders] ADD [SpecialInstruction] nvarchar(250) NULL;
GO

ALTER TABLE [SalesOrders] ADD [WithLP] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [SalesOrders] ADD [WithREYCOJITLPDeliveryId] int NOT NULL DEFAULT 1;
GO

ALTER TABLE [SalesOrders] ADD [WithREYCONonDefective] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

CREATE TABLE [DeliveryTimes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(15) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_DeliveryTimes] PRIMARY KEY ([Id])
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'00588df6-1687-47ab-9fed-36ec550419ee', [PasswordHash] = N'AQAAAAIAAYagAAAAEJy7G6F7PDY2v6JKPZ7mIP2iv/vc97BfdGgM4CE+admBpHAzH9Z6pGgO0q/rkOEHbQ==', [SecurityStamp] = N'9a93a997-ad4a-414e-9e93-978b8a5631ab'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'fc9a1a35-6594-415f-aeab-5c18985e0819', [PasswordHash] = N'AQAAAAIAAYagAAAAEJ02EdbDZzOroXrCHDcu3wFrk58sGmjpujCfJLxVuEYe5CVvGN5O/ckEwyPJiX+rgg==', [SecurityStamp] = N'55ef3283-03f3-494d-b7bc-5e29618a5116'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[DeliveryTimes]'))
    SET IDENTITY_INSERT [DeliveryTimes] ON;
INSERT INTO [DeliveryTimes] ([Id], [Code], [Name])
VALUES (1, N'AM', N'AM'),
(2, N'PM', N'PM'),
(3, N'AM NEXT DAY', N'AM NEXT DAY');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[DeliveryTimes]'))
    SET IDENTITY_INSERT [DeliveryTimes] OFF;
GO

CREATE INDEX [IX_SalesOrders_ServiceLevelAgreementId] ON [SalesOrders] ([ServiceLevelAgreementId]);
GO

CREATE INDEX [IX_SalesOrders_WithREYCOJITLPDeliveryId] ON [SalesOrders] ([WithREYCOJITLPDeliveryId]);
GO

ALTER TABLE [SalesOrders] ADD CONSTRAINT [FK_SalesOrders_DeliveryTimes_WithREYCOJITLPDeliveryId] FOREIGN KEY ([WithREYCOJITLPDeliveryId]) REFERENCES [DeliveryTimes] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [SalesOrders] ADD CONSTRAINT [FK_SalesOrders_ServiceLevelAgreements_ServiceLevelAgreementId] FOREIGN KEY ([ServiceLevelAgreementId]) REFERENCES [ServiceLevelAgreements] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251008171334_salesordermigrate', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CustomerBrandDiscount] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'212b3a05-4a89-435e-a57c-82a781b42df1', [PasswordHash] = N'AQAAAAIAAYagAAAAEFcd/16R0C6Yi3G+UlPI3e4wmWejCQLvq8ewe7ClRXYsUeO5P5Sx9m5yeXqI7MJ+kg==', [SecurityStamp] = N'3ce7bfeb-4b49-4f49-af13-b08bd9235c9d'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'0f945866-1e29-455b-8a98-84df00b73086', [PasswordHash] = N'AQAAAAIAAYagAAAAELIkeKfH9Q8/fzhUxo7U4uQXvnv8vVHhHFIPmzUE+KOir/R0hInDscMgABGtDPJY6A==', [SecurityStamp] = N'45b905e8-71a3-4014-b45f-1d83fe9eb7a5'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251009060957_AddIsActiveToCustomerBrandDiscount', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrders] DROP CONSTRAINT [FK_SalesOrders_DeliveryTimes_WithREYCOJITLPDeliveryId];
GO

DECLARE @var129 sysname;
SELECT @var129 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'WithREYCOJITLPDeliveryId');
IF @var129 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var129 + '];');
ALTER TABLE [SalesOrders] ALTER COLUMN [WithREYCOJITLPDeliveryId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'6e3b3d1a-6acc-4937-9921-b3c01f3710dd', [PasswordHash] = N'AQAAAAIAAYagAAAAENf8WcwqD2niSlhgq76o8dZPEt86CJmNiJKP/pIcvkVRYph5315goAv10cd34DJVjg==', [SecurityStamp] = N'dcb1bf8e-0486-4021-9ed7-60640dc84d44'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'24d0f3e3-6ff1-41e0-a6a8-59b886934560', [PasswordHash] = N'AQAAAAIAAYagAAAAEOqzyH5dH8J9S0uNdtyZgbArHnt93fkRjvYlxHCSI2aAPqzZmwNKdRU763kNu9v5rg==', [SecurityStamp] = N'81d7ff52-558e-48d5-b08e-3feff478e813'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [SalesOrders] ADD CONSTRAINT [FK_SalesOrders_DeliveryTimes_WithREYCOJITLPDeliveryId] FOREIGN KEY ([WithREYCOJITLPDeliveryId]) REFERENCES [DeliveryTimes] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251009122312_salesordermigrate2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrderLineItems] ADD [CustomerBrandDiscountId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'b4c85237-0250-4d24-9abd-f17787c227b2', [PasswordHash] = N'AQAAAAIAAYagAAAAEDTwSr85gdNI2OUJDVTTSnmS3zjdeu5TqOS0tl5Ju883tkb9T0sybN+fBNSf8EMm0A==', [SecurityStamp] = N'35ade42d-1a7a-4453-a40e-0c4e442a6094'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'c8b587e7-ccf9-49bb-bfc1-94a0a50d0c0a', [PasswordHash] = N'AQAAAAIAAYagAAAAEK+EuDRPuzyoAvQ8Z3TUKo2xfR8+PJ1RDYOzjUC5a2uYJlFJhb8zI004wuvs/xNM4Q==', [SecurityStamp] = N'c159d929-96f9-400d-b774-d55c735eca66'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [SalesOrderLineItems] ADD CONSTRAINT [FK_SalesOrderLineItems_CustomerBrandDiscount_CustomerBrandDiscountId] FOREIGN KEY ([CustomerBrandDiscountId]) REFERENCES [CustomerBrandDiscount] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251010030147_AddBrandDiscountToSalesLineItem', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrderLineItems] ADD [Discount3] decimal(18,2) NULL;
GO

ALTER TABLE [SalesOrderLineItems] ADD [Discount4] decimal(18,2) NULL;
GO

ALTER TABLE [SalesOrderLineItems] ADD [Discount5] decimal(18,2) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'1364c13f-b82a-4c29-84b3-710cde843a5e', [PasswordHash] = N'AQAAAAIAAYagAAAAENSaIWG1DG0Dq1VHTIonRxdSNqGRscRAan4waOVLMzzzZTBwWEjGNEND7DwGTi2zmg==', [SecurityStamp] = N'b99a49bc-ece8-4db9-a0ba-1b51ff57bf13'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'b51286c2-dd2e-48cd-95b9-2400dbee59b3', [PasswordHash] = N'AQAAAAIAAYagAAAAEIyZhPK8vOmfQr/HA8Pk4CwY22FP+J00+2SaNNIML9J+plfNlx1l7l24dsqWuJIqAg==', [SecurityStamp] = N'fb4c54aa-9057-4222-851d-74586567466f'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251014060723_salesorderlineitemdiscount', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var130 sysname;
SELECT @var130 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TrackerTasks]') AND [c].[name] = N'Behavior');
IF @var130 IS NOT NULL EXEC(N'ALTER TABLE [TrackerTasks] DROP CONSTRAINT [' + @var130 + '];');
ALTER TABLE [TrackerTasks] DROP COLUMN [Behavior];
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'903e1976-0bbc-4c87-8382-58159e27ce43', [PasswordHash] = N'AQAAAAIAAYagAAAAEImk/Luii7Qj5cyqbv+iLd4bofQp1Z54fyVOHO5C/w3SIK7WU9ovA8kqVvjNRbi5LA==', [SecurityStamp] = N'763b6961-e19a-4eae-b22a-6c0be77351ba'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'1ef331ea-5e6f-4a21-81be-306e404b39ec', [PasswordHash] = N'AQAAAAIAAYagAAAAEPz4RTF5gfp6dGa7CMHgvKoxO2f66RB9TTDLfoDvx+Sfi+7BhwJfFl9rKUnWFxP6fA==', [SecurityStamp] = N'c5899b94-42cc-49fd-b875-a8b7190ed6b2'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251018154226_Tracker11', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ItemPromo] (
    [Id] int NOT NULL IDENTITY,
    [ItemId] int NOT NULL,
    [Name] nvarchar(300) NOT NULL,
    [PromoPrice] decimal(18,2) NOT NULL,
    [EffectiveDate] datetime2 NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_ItemPromo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemPromo_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'137f2b6f-1b49-4e06-985f-31aba814396b', [PasswordHash] = N'AQAAAAIAAYagAAAAECGOSbPTnzFaai67RvYH3HoAE6/qAUjkfdm85ZKmGlR4mSv2q/+aKgR4fepURLDn0w==', [SecurityStamp] = N'632fe89a-236b-42a7-b989-6dfa92b0feba'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'723604c3-04cf-4d39-8ca8-a8798f994916', [PasswordHash] = N'AQAAAAIAAYagAAAAEPyz3M7LF5CAdf3SUNMmkbIq4s/bkXAjdvO+PY7PHs43rC0HF9EYH+ogMZQXNYWo5Q==', [SecurityStamp] = N'd7bee0f9-1b1a-4c40-988a-d1e3c593836a'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_ItemPromo_ItemId] ON [ItemPromo] ([ItemId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251022034220_ItemPromo', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'6c7dcac7-edcf-4b34-8ab6-7d21c5c46f19', [PasswordHash] = N'AQAAAAIAAYagAAAAEN+D2Tnzsme81SPg2OwgsDRJZzGVxMNAMIc1c0jT/K9DTxmr7krUvhVbx3aPeNQP3A==', [SecurityStamp] = N'c698ce22-befe-4110-8abf-8bb4cbde2c1c'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'8051a8bb-b904-41d2-bdab-72a5dea375bf', [PasswordHash] = N'AQAAAAIAAYagAAAAELKC/OGMDlGhLxyj9BNolWZ4+p+Orfw5KiyRBzSXLfHYSEd1yWzWjapm1S+hlLhicg==', [SecurityStamp] = N'f42fc28a-62da-429d-9d16-67b163eca71c'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE UNIQUE INDEX [IX_SalesOrders_SalesOrderNumber] ON [SalesOrders] ([SalesOrderNumber]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251022171558_migratesounique', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var131 sysname;
SELECT @var131 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ItemPromo]') AND [c].[name] = N'Name');
IF @var131 IS NOT NULL EXEC(N'ALTER TABLE [ItemPromo] DROP CONSTRAINT [' + @var131 + '];');
ALTER TABLE [ItemPromo] ALTER COLUMN [Name] nvarchar(300) NOT NULL;
GO

ALTER TABLE [Customers] ADD [PrioritiesTemp] int NOT NULL DEFAULT 3;
GO


                UPDATE Customers
                SET PrioritiesTemp = CASE
                    WHEN Priorities = 'Urgent'   THEN 5
                    WHEN Priorities = 'High'     THEN 4
                    WHEN Priorities = 'Normal'   THEN 3
                    WHEN Priorities = 'Low'      THEN 2
                    WHEN Priorities = 'Deferred' THEN 1
                    -- fallback if unexpected value or NULL
                    ELSE 3
                END
            
GO

DECLARE @var132 sysname;
SELECT @var132 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'Priorities');
IF @var132 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var132 + '];');
ALTER TABLE [Customers] DROP COLUMN [Priorities];
GO

EXEC sp_rename N'[Customers].[PrioritiesTemp]', N'Priorities', N'COLUMN';
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'638a672a-a27d-4e72-85de-639679d510fe', [PasswordHash] = N'AQAAAAIAAYagAAAAEOS6oeSyrOlnRzG3qLSqRQtAGLU8Znv3zf9eeEVyFpUH62Pz54d9xluakFZeYkuF4Q==', [SecurityStamp] = N'45e02889-57a9-4742-b3c0-f0ce19585f56'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'40c98947-aaa7-447c-b5fe-4a52ce772f40', [PasswordHash] = N'AQAAAAIAAYagAAAAEJxIqrFJp1SgGPWv2Ula24RXxe0Npd8jCn46XxvBJZHq6BJfFywOeKVHI7msCLP3cQ==', [SecurityStamp] = N'2138f3ce-3e4c-4745-8835-150025c98f5a'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251103055251_migratecustomer', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Customers].[Priorities]', N'Priority', N'COLUMN';
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'61c213ad-68b1-4844-89cc-a639cf6ed85f', [PasswordHash] = N'AQAAAAIAAYagAAAAEMhA1B9IE4GTwMrKBTqvGKD3+LkS/21eSPO6fMiTjS/cIE92I+VBS9WxXWIjEp0vLQ==', [SecurityStamp] = N'8e82214e-74f5-492e-87e1-7bb262a5deaa'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'8172f322-fed8-45b5-8131-a58d9ad9bee0', [PasswordHash] = N'AQAAAAIAAYagAAAAEGJenOZ2JZ8vpfDTVUguSkl+jnJgMyGaUgm2fhZhkXbYySnbGgHmeZb7beHZl+fkYQ==', [SecurityStamp] = N'80afbc2e-b9b6-41d9-8f42-7ce45d5a7202'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251103061233_migratecustomer2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrderLineItems] ADD [ItemPromoId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'73f3a803-9714-4bc4-9253-b8bb00ac1d4d', [PasswordHash] = N'AQAAAAIAAYagAAAAEPrqileUE4zoeq4cO8T8czN1EJTPxKBWOO0WKrbJNVs1hBE7Me6JxViRWEtEoN0IBg==', [SecurityStamp] = N'd42ea1a7-ded8-4ff2-9472-24889fac6609'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4882b4b2-b102-48a9-b835-9227c9fc453c', [PasswordHash] = N'AQAAAAIAAYagAAAAEKXanTeBzJlXk+NwXDg5A8Zl5/KgbKIyyaR5tX70YIbyzozJxNVe8yIqvBAfGMp5CQ==', [SecurityStamp] = N'58d6e9a5-0a82-4bdd-bbae-012dc6aa8541'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_SalesOrderLineItems_ItemPromoId] ON [SalesOrderLineItems] ([ItemPromoId]);
GO

ALTER TABLE [SalesOrderLineItems] ADD CONSTRAINT [FK_SalesOrderLineItems_ItemPromo_ItemPromoId] FOREIGN KEY ([ItemPromoId]) REFERENCES [ItemPromo] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251106091914_salesorderlineitempromo', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [SalesOrderSources] (
    [Id] int NOT NULL IDENTITY,
    [SourceOrderNumber] nvarchar(50) NOT NULL,
    [SalesOrderId] int NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SalesOrderSources] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SalesOrderSources_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]) ON DELETE NO ACTION
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'2c2ed681-0862-4129-a8fe-d4a1d2aedc16', [PasswordHash] = N'AQAAAAIAAYagAAAAEPpHnbZSKsa6nzeKUe5di3QmrwMrXe+vCOgUFyRLjrHmEg5CGjMJtvj/u9fBTt+0cA==', [SecurityStamp] = N'716023d3-dd94-4f75-975d-bf9194ea09c6'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'60730573-f4c8-4bb3-b1ae-4734e6de78cc', [PasswordHash] = N'AQAAAAIAAYagAAAAEKXt//UVcidr0C1obeC+89oI22RiODF/mH+Bu5b5T6btSPIJZfoyh37wi66DJjtIuA==', [SecurityStamp] = N'cbc83a2b-48cf-4ac8-9637-da3552b5b50c'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_SalesOrderSources_SalesOrderId] ON [SalesOrderSources] ([SalesOrderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251112012905_ordersourcemigrate', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'35ccf05e-6fd7-43e1-8ce2-1f0c03c5873a', [PasswordHash] = N'AQAAAAIAAYagAAAAEGfDB4CCZs5Qkq6L2RHW/bJr36Q5dzMU0U0AMrV4fy0B4gaBT+uqfA9tZdnWiQUDwg==', [SecurityStamp] = N'bcb6969c-0653-4f0f-a788-1348ba8320d1'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'2e722c78-3e43-44f0-9e15-11fdb8e9e3a8', [PasswordHash] = N'AQAAAAIAAYagAAAAENwZ8pfQ3EnxVAK1CHxTfITLaZgsqI8cCTN2/sc1J5kzkllSKar4CARO2ZyzdzaktQ==', [SecurityStamp] = N'0a6e4b42-f752-4870-adb5-4f132106cf9d'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251113061841_temp', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrderSources] DROP CONSTRAINT [FK_SalesOrderSources_SalesOrders_SalesOrderId];
GO

DECLARE @var133 sysname;
SELECT @var133 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderSources]') AND [c].[name] = N'SalesOrderId');
IF @var133 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderSources] DROP CONSTRAINT [' + @var133 + '];');
ALTER TABLE [SalesOrderSources] ALTER COLUMN [SalesOrderId] int NULL;
GO

ALTER TABLE [SalesOrderSources] ADD [Status] nvarchar(max) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'3429696b-14dd-4679-a25b-450bc5cd1ce0', [PasswordHash] = N'AQAAAAIAAYagAAAAEKDVeoeqIo9gZ/p9jc+Q00a3gfrro+XIAq9NHtZAT/2Hn5/5X3vp6iC9f90A6gwpog==', [SecurityStamp] = N'958ff039-f647-4108-953d-540c97e17478'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'7dde357f-4300-4af4-9e78-44d2ea3f5c4f', [PasswordHash] = N'AQAAAAIAAYagAAAAEM/HQf9bFvpj+MPJvT/tm3KO0oIrizJYNfaLfHHhqsNs0PgrOUoc92B4u8TK3vM1QQ==', [SecurityStamp] = N'bea88979-72de-4219-b7e6-d3f60c8e2230'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [SalesOrderSources] ADD CONSTRAINT [FK_SalesOrderSources_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251119021647_orderqueuemigrate', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseRequestLineItems] ADD [HaveStock] bit NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'd8cd9ea8-4623-4d00-8615-51ee4fd8e6c0', [PasswordHash] = N'AQAAAAIAAYagAAAAEA1eIXXd5sUbmv2FShtB0zO+Ud3ylr4Jw/g7QrfY/9KFvPbIHPZ1KiQVApxMWpu8dg==', [SecurityStamp] = N'21c786de-be44-4bea-897f-ae583a72b2ca'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'5c12f168-1772-4f93-a8c5-9894aa6c2ccc', [PasswordHash] = N'AQAAAAIAAYagAAAAEP9198De9tdYoBOCfQUh7nALeRA9JQpSLGnsIDeAf6nEm8DxM/l6roZD/apNWtUmfQ==', [SecurityStamp] = N'53897702-0036-4972-b201-ff283d13f4a8'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251119151807_PRHaveStockMigrate', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrderDeliveryMonitorings] ADD [DriverId] int NULL;
GO

ALTER TABLE [SalesOrderDeliveryMonitorings] ADD [VehicleId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'ee9e311e-463d-45d0-b21e-0c93eb39284e', [PasswordHash] = N'AQAAAAIAAYagAAAAEG/ic/UFzxRhPMfM+6VV47faPzFtAp1E+c7h0F/Llh6r4VplDs9g2VXIEVhi7suIhQ==', [SecurityStamp] = N'a07517e0-ce37-45db-a1a8-f64638d5fb3c'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'933438cc-d3fe-4c6f-b40b-09417f83246e', [PasswordHash] = N'AQAAAAIAAYagAAAAEGOLvCKQDZp8PU53fGSd66y5yYBr01hXyIBIA/BoZT+dWhtTcAngo5Eu76GidLUfKw==', [SecurityStamp] = N'157cfc54-154c-49ad-97cf-80737fa34608'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_SalesOrderDeliveryMonitorings_DriverId] ON [SalesOrderDeliveryMonitorings] ([DriverId]);
GO

CREATE INDEX [IX_SalesOrderDeliveryMonitorings_VehicleId] ON [SalesOrderDeliveryMonitorings] ([VehicleId]);
GO

ALTER TABLE [SalesOrderDeliveryMonitorings] ADD CONSTRAINT [FK_SalesOrderDeliveryMonitorings_Drivers_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [Drivers] ([Id]);
GO

ALTER TABLE [SalesOrderDeliveryMonitorings] ADD CONSTRAINT [FK_SalesOrderDeliveryMonitorings_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251120083217_salesorderdelivery', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'76423434-dd07-4141-9a4b-243719fc33a2', [PasswordHash] = N'AQAAAAIAAYagAAAAEJk7MleAoMCA2s+gzqjpUKt1dZo9P3ZZO5T2EVLR4ujSWmPReDLFEMCU/+L2s9GLuw==', [SecurityStamp] = N'07b3aa85-f5c9-40ab-9f44-25e81b364a1f'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'd6412ad7-75c7-4079-9eb3-88c47e83914f', [PasswordHash] = N'AQAAAAIAAYagAAAAEFCovQ/K2pjIRgj6M27wlxh+dhzcDL9ZGscqU69IZnRRdZzzQEhe/Bp4ne85ZkEWJg==', [SecurityStamp] = N'313c4001-5190-4c43-8114-f350ca73e72f'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251203165329_migrateprequestapprover2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseOrderLineItems] DROP CONSTRAINT [FK_PurchaseOrderLineItems_PurchaseRequestLineItems_PurchaseRequestLineItemId];
GO

DROP INDEX [IX_PurchaseOrderLineItems_PurchaseRequestLineItemId] ON [PurchaseOrderLineItems];
GO

DECLARE @var134 sysname;
SELECT @var134 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderLineItems]') AND [c].[name] = N'PurchaseRequestLineItemId');
IF @var134 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderLineItems] DROP CONSTRAINT [' + @var134 + '];');
ALTER TABLE [PurchaseOrderLineItems] DROP COLUMN [PurchaseRequestLineItemId];
GO

ALTER TABLE [SalesOrderLineItems] ADD [SalesOrderSourceId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'a5d0eaa1-35e8-4ca4-8c17-87f6760bc268', [PasswordHash] = N'AQAAAAIAAYagAAAAEB8VlxlBk26AmjpFKZwKCnY6rQAXgWy9lXgJHG64QQ4Uegy8+5ZDbVUKq4hGQon0kQ==', [SecurityStamp] = N'75b6a71c-7640-49f7-b904-c1bcf5c8e455'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'28ca393c-334f-477c-8ca1-ecf75cce58cf', [PasswordHash] = N'AQAAAAIAAYagAAAAECekghKPAWVFOrmoRBKh9ssgX1Wv982peQJxAXhlehNxT9nDUf+CibmxXohaSvf7Qw==', [SecurityStamp] = N'fd91b2aa-67eb-48e8-86c7-d361604fd632'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_SalesOrderLineItems_SalesOrderSourceId] ON [SalesOrderLineItems] ([SalesOrderSourceId]);
GO

ALTER TABLE [SalesOrderLineItems] ADD CONSTRAINT [FK_SalesOrderLineItems_SalesOrderSources_SalesOrderSourceId] FOREIGN KEY ([SalesOrderSourceId]) REFERENCES [SalesOrderSources] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251204174506_migratequeuetosalesorder', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TransferItemDetails] ADD [BarcotechTransferId] int NULL;
GO

ALTER TABLE [SalesReturnLineItems] ADD [BarcotechReturnId] int NULL;
GO

ALTER TABLE [SalesOrderLineItems] ADD [BarcotechOutboundId] int NULL;
GO

ALTER TABLE [PurchaseReceivingLineItems] ADD [BarcotechReceivingId] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251211052406_MigrateBarcotechReference', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var135 sysname;
SELECT @var135 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TransferItemDetails]') AND [c].[name] = N'BarcotechTransferId');
IF @var135 IS NOT NULL EXEC(N'ALTER TABLE [TransferItemDetails] DROP CONSTRAINT [' + @var135 + '];');
ALTER TABLE [TransferItemDetails] DROP COLUMN [BarcotechTransferId];
GO

ALTER TABLE [TransferItemDetails] ADD [BarcotechTransferCode] nvarchar(max) NULL;
GO

ALTER TABLE [SalesReturns] ADD [BarcotechRequestDate] datetime2 NULL;
GO

ALTER TABLE [PurchaseReturnNews] ADD [BarcotechRequestDate] datetime2 NULL;
GO

ALTER TABLE [PurchaseOrderNews] ADD [BarcotechRequestDate] datetime2 NULL;
GO

ALTER TABLE [Packs] ADD [BarcotechRequestDate] datetime2 NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'6a8a0167-fb0e-4471-8809-58413a8d3b11', [PasswordHash] = N'AQAAAAIAAYagAAAAEEjmBubKJcYyNBr9PoobN3+T9coblaWtIGk0y99wRthtMrtmSuahjn8eXabs5+qREg==', [SecurityStamp] = N'a6ad6735-6b59-43f8-923d-dc2df8d39a7a'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'da39f335-dccf-4e08-b28d-9b4867a0a7c3', [PasswordHash] = N'AQAAAAIAAYagAAAAEDQwHWIF1/ZPaY9f6tUdsNFMnFMl0TP0SO+Nd+56wXnaJxmof9TEIylp40+9LETDmA==', [SecurityStamp] = N'01ab234a-93b3-43c4-9558-b44766fe2d36'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251215154326_MigrateBarcotechRequestDate', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [BarcotechSyncs] (
    [Id] int NOT NULL IDENTITY,
    [ModuleName] nvarchar(max) NOT NULL,
    [PrimaryKey] nvarchar(max) NOT NULL,
    [SyncStatus] nvarchar(max) NOT NULL,
    [Remarks] nvarchar(max) NULL,
    [DatePerformed] datetime2 NOT NULL,
    CONSTRAINT [PK_BarcotechSyncs] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251218032957_BarcotechSyncStatus', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Brands] DROP CONSTRAINT [FK_Brands_ItemTypes_ItemTypeId];
GO

ALTER TABLE [ItemTypes] DROP CONSTRAINT [FK_ItemTypes_Categories_CategoryId];
GO

DECLARE @var136 sysname;
SELECT @var136 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ItemTypes]') AND [c].[name] = N'CategoryId');
IF @var136 IS NOT NULL EXEC(N'ALTER TABLE [ItemTypes] DROP CONSTRAINT [' + @var136 + '];');
ALTER TABLE [ItemTypes] ALTER COLUMN [CategoryId] int NULL;
GO

DECLARE @var137 sysname;
SELECT @var137 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Brands]') AND [c].[name] = N'ItemTypeId');
IF @var137 IS NOT NULL EXEC(N'ALTER TABLE [Brands] DROP CONSTRAINT [' + @var137 + '];');
ALTER TABLE [Brands] ALTER COLUMN [ItemTypeId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'c2389fc2-272b-4ea0-9f29-b4d50edf6136', [PasswordHash] = N'AQAAAAIAAYagAAAAEAsyue1r6286Z5H/KBjqcZUfnRkbOuD0c1gzUUQZcIMx+TraqoSIx8NhJFLTP3Rpyw==', [SecurityStamp] = N'f28e29fa-ff5b-4089-aded-c00a52eda1c6'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'0e661ddd-9cf4-4cb6-9032-1af3554ebee5', [PasswordHash] = N'AQAAAAIAAYagAAAAEG+xHZ7p8AmHpFxXPfr1cxv7IGw9/Ef3mSU6T1/azzB2TEspM85gd4bKvjiigvSorA==', [SecurityStamp] = N'77c45926-1ed9-4190-b9d1-6e2cb0923c78'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [Brands] ADD CONSTRAINT [FK_Brands_ItemTypes_ItemTypeId] FOREIGN KEY ([ItemTypeId]) REFERENCES [ItemTypes] ([Id]);
GO

ALTER TABLE [ItemTypes] ADD CONSTRAINT [FK_ItemTypes_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251218061628_MigrateCategoryItemtypeBrand', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var138 sysname;
SELECT @var138 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesReturns]') AND [c].[name] = N'BarcotechRequestDate');
IF @var138 IS NOT NULL EXEC(N'ALTER TABLE [SalesReturns] DROP CONSTRAINT [' + @var138 + '];');
ALTER TABLE [SalesReturns] DROP COLUMN [BarcotechRequestDate];
GO

EXEC sp_rename N'[TransferItemDetails].[BarcotechTransferCode]', N'BarcotechTransferId', N'COLUMN';
GO

ALTER TABLE [TransferItems] ADD [BarcotechTransferCode] nvarchar(max) NULL;
GO

ALTER TABLE [SalesReturns] ADD [BarcotechReturnCode] nvarchar(max) NULL;
GO

DECLARE @var139 sysname;
SELECT @var139 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesReturnLineItems]') AND [c].[name] = N'BarcotechReturnId');
IF @var139 IS NOT NULL EXEC(N'ALTER TABLE [SalesReturnLineItems] DROP CONSTRAINT [' + @var139 + '];');
ALTER TABLE [SalesReturnLineItems] ALTER COLUMN [BarcotechReturnId] nvarchar(max) NULL;
GO

ALTER TABLE [SalesReturnLineItems] ADD [CustomerBrandDiscountId] int NULL;
GO

ALTER TABLE [SalesReturnLineItems] ADD [ItemPromoId] int NULL;
GO

ALTER TABLE [SalesOrders] ADD [BarcotechOutboundCode] nvarchar(max) NULL;
GO

DECLARE @var140 sysname;
SELECT @var140 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderLineItems]') AND [c].[name] = N'BarcotechOutboundId');
IF @var140 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderLineItems] DROP CONSTRAINT [' + @var140 + '];');
ALTER TABLE [SalesOrderLineItems] ALTER COLUMN [BarcotechOutboundId] nvarchar(max) NULL;
GO

ALTER TABLE [PurchaseReceivings] ADD [BarcotechReceivingCode] nvarchar(max) NULL;
GO

DECLARE @var141 sysname;
SELECT @var141 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReceivingLineItems]') AND [c].[name] = N'BarcotechReceivingId');
IF @var141 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReceivingLineItems] DROP CONSTRAINT [' + @var141 + '];');
ALTER TABLE [PurchaseReceivingLineItems] ALTER COLUMN [BarcotechReceivingId] nvarchar(max) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'2812ce34-1e80-41b5-86f8-b8838401d44b', [PasswordHash] = N'AQAAAAIAAYagAAAAEOv7LnIv0k/Ga9hOlCsDEX0pAtfBPbvtPbCzxrSbAIQwIw6D0x2E5LMoJWbjMjh7Tg==', [SecurityStamp] = N'462e6548-a7a4-48b1-846c-f382017c1239'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'8425104e-ee1c-47f4-a697-e2839073cb36', [PasswordHash] = N'AQAAAAIAAYagAAAAEM9lae2XolCEk5D4ilOYIKNrZ1HqGAtc6r4s93KYgXyPCOZS0QmLHE9jEP5nrrwtpQ==', [SecurityStamp] = N'0cdf4158-9236-4f6b-a69c-bdb52af0023a'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_SalesReturnLineItems_CustomerBrandDiscountId] ON [SalesReturnLineItems] ([CustomerBrandDiscountId]);
GO

CREATE INDEX [IX_SalesReturnLineItems_ItemPromoId] ON [SalesReturnLineItems] ([ItemPromoId]);
GO

ALTER TABLE [SalesReturnLineItems] ADD CONSTRAINT [FK_SalesReturnLineItems_CustomerBrandDiscount_CustomerBrandDiscountId] FOREIGN KEY ([CustomerBrandDiscountId]) REFERENCES [CustomerBrandDiscount] ([Id]);
GO

ALTER TABLE [SalesReturnLineItems] ADD CONSTRAINT [FK_SalesReturnLineItems_ItemPromo_ItemPromoId] FOREIGN KEY ([ItemPromoId]) REFERENCES [ItemPromo] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251221160944_MigrateBarcotech', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseReturnNews] DROP CONSTRAINT [FK_PurchaseReturnNews_Warehouses_WarehouseId];
GO

DROP INDEX [IX_PurchaseReturnNews_WarehouseId] ON [PurchaseReturnNews];
DECLARE @var142 sysname;
SELECT @var142 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReturnNews]') AND [c].[name] = N'WarehouseId');
IF @var142 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReturnNews] DROP CONSTRAINT [' + @var142 + '];');
ALTER TABLE [PurchaseReturnNews] ALTER COLUMN [WarehouseId] int NOT NULL;
ALTER TABLE [PurchaseReturnNews] ADD DEFAULT 2 FOR [WarehouseId];
CREATE INDEX [IX_PurchaseReturnNews_WarehouseId] ON [PurchaseReturnNews] ([WarehouseId]);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'705a35ba-a992-4f3d-a6cd-3356d696ed2e', [PasswordHash] = N'AQAAAAIAAYagAAAAEL3pm8GzBxo92jQ1Ncdvk28xhevUWz1QzUPviunTRJbL3g7S2qd2tbfvZ31GdTLoXw==', [SecurityStamp] = N'06abd314-267b-4ac8-92ab-f0cc8d727071'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'168f2c81-2f42-4a2f-8ab3-70e7304d6ba4', [PasswordHash] = N'AQAAAAIAAYagAAAAEMTAz2D5KRFccYk5z1PFlLMeis5BZLObwRlh2G9SixJ1cHvbFnVfOdfgMd8mzKwtRA==', [SecurityStamp] = N'454faad8-68f7-465d-8f68-8e1c172c2294'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [PurchaseReturnNews] ADD CONSTRAINT [FK_PurchaseReturnNews_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260119014150_MigrateProcurementWarehouse', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var143 sysname;
SELECT @var143 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReturnNews]') AND [c].[name] = N'DocumentNumber');
IF @var143 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReturnNews] DROP CONSTRAINT [' + @var143 + '];');
ALTER TABLE [PurchaseReturnNews] ALTER COLUMN [DocumentNumber] nvarchar(450) NOT NULL;
GO

DECLARE @var144 sysname;
SELECT @var144 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseRequests]') AND [c].[name] = N'DocumentNumber');
IF @var144 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseRequests] DROP CONSTRAINT [' + @var144 + '];');
ALTER TABLE [PurchaseRequests] ALTER COLUMN [DocumentNumber] nvarchar(450) NOT NULL;
GO

DECLARE @var145 sysname;
SELECT @var145 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseReceivings]') AND [c].[name] = N'DocumentNumber');
IF @var145 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseReceivings] DROP CONSTRAINT [' + @var145 + '];');
ALTER TABLE [PurchaseReceivings] ALTER COLUMN [DocumentNumber] nvarchar(450) NOT NULL;
GO

DECLARE @var146 sysname;
SELECT @var146 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderNews]') AND [c].[name] = N'DocumentNumber');
IF @var146 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderNews] DROP CONSTRAINT [' + @var146 + '];');
ALTER TABLE [PurchaseOrderNews] ALTER COLUMN [DocumentNumber] nvarchar(450) NOT NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'dfd99b44-713e-4cb1-a9ed-ce48071b79eb', [PasswordHash] = N'AQAAAAIAAYagAAAAEO07/Ukp0nRmulC65AYm4sl5cW3MFP8pjqYBufTmC22wXXB2p5Uf1Ou8NOfISUwcyg==', [SecurityStamp] = N'7f156b5c-5a13-42f5-9e0e-279baca1be4d'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'431d5359-681b-4aba-9c2c-8c18f5c715aa', [PasswordHash] = N'AQAAAAIAAYagAAAAEPZaVWJHtPh8/9KoszSFO0H1UtCwXZJtzyqgBuqFjDv6NAQYpqhP5D4agZxqHqGEXQ==', [SecurityStamp] = N'a55cd238-9d06-44d0-a26a-a7eb669e8b6a'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE UNIQUE INDEX [IX_PurchaseReturnNews_DocumentNumber] ON [PurchaseReturnNews] ([DocumentNumber]);
GO

CREATE UNIQUE INDEX [IX_PurchaseRequests_DocumentNumber] ON [PurchaseRequests] ([DocumentNumber]);
GO

CREATE UNIQUE INDEX [IX_PurchaseReceivings_DocumentNumber] ON [PurchaseReceivings] ([DocumentNumber]);
GO

CREATE UNIQUE INDEX [IX_PurchaseOrderNews_DocumentNumber] ON [PurchaseOrderNews] ([DocumentNumber]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260128071658_MigrateUniqueDocumentNumber', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesReturns] ADD [BarcotechRequestDate] datetime2 NULL;
GO

ALTER TABLE [SalesOrders] ADD [BarcotechRequestDate] datetime2 NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'de8f2f63-067d-40e7-9892-45ff8f383755', [PasswordHash] = N'AQAAAAIAAYagAAAAEKW4Pl5dHL1sp60SNuOUx+fqF6uVu8Hl+wdoRygAM+zom/qWBxqJH7TsWslGTYjSsA==', [SecurityStamp] = N'4200a8cd-59c8-4371-943a-7a21436e9b4b'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'5bdf9412-27f6-43ac-8679-ec6a5acff4be', [PasswordHash] = N'AQAAAAIAAYagAAAAEO8ZAqe7a1izeCy1xAHOnNQFtEp0Vw3PRUedgjprptsqxW7T8zWVYVCHejLyW2ntig==', [SecurityStamp] = N'09ba319a-d091-4086-9a66-a6c6ce39bec0'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260213031201_MigrateSOBCDate', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var147 sysname;
SELECT @var147 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrders]') AND [c].[name] = N'BarcotechOutboundCode');
IF @var147 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrders] DROP CONSTRAINT [' + @var147 + '];');
ALTER TABLE [SalesOrders] DROP COLUMN [BarcotechOutboundCode];
GO

DECLARE @var148 sysname;
SELECT @var148 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderLineItems]') AND [c].[name] = N'BarcotechOutboundId');
IF @var148 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderLineItems] DROP CONSTRAINT [' + @var148 + '];');
ALTER TABLE [SalesOrderLineItems] DROP COLUMN [BarcotechOutboundId];
GO

ALTER TABLE [Packs] ADD [BarcotechOutboundCode] nvarchar(max) NULL;
GO

ALTER TABLE [PackLineItems] ADD [BarcotechOutboundId] nvarchar(max) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'7ad212f7-c398-46a4-b429-cdb89a144e21', [PasswordHash] = N'AQAAAAIAAYagAAAAEPTHCMwIGbuVP2KSeqd2QUaZGKh2d0c8xlNlJKuzofZtTEW/HTIXdIcVbRBTtxDp4w==', [SecurityStamp] = N'fbce40c0-f293-4632-8fdb-5af873c33379'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'e772ba5d-2fce-45f9-9acc-5cda2a208e9d', [PasswordHash] = N'AQAAAAIAAYagAAAAEPDYx7gkUYKqHT9Jy+MV5Cr4fTnfeGFNjB/9cKZwdDVo184BMqK5clwL2zOlxsLuTQ==', [SecurityStamp] = N'243c34c0-9f11-4e25-845e-9e290b9900ed'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260303082516_MigrateBarcotech2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'8494b7d4-1204-486b-8885-fc4981dcdeed', [PasswordHash] = N'AQAAAAIAAYagAAAAEP207oFipoamOmpkX2YrBG6nnTCXD3RF3ZOiTdcWi5+37mjP9WrYqTN4kRsC6RVolg==', [SecurityStamp] = N'7f92d0dd-0272-46e2-9d23-8425ed65cec6'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'40eff676-893f-41df-a961-d240fa2ad9e1', [PasswordHash] = N'AQAAAAIAAYagAAAAEGCwqh47rWmg7z2dqaxM/JOkWN1cuj9VlQlQ0XdgSvWQ9OR1RPjcit93M2T9HfnrUw==', [SecurityStamp] = N'9e67c721-2b2e-498f-a368-cd8d70563020'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260305184704_MigrateCheck', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var149 sysname;
SELECT @var149 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LocationCodes]') AND [c].[name] = N'Code');
IF @var149 IS NOT NULL EXEC(N'ALTER TABLE [LocationCodes] DROP CONSTRAINT [' + @var149 + '];');
ALTER TABLE [LocationCodes] ALTER COLUMN [Code] nvarchar(15) NOT NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'c59558a7-136f-46e9-adbb-7583209d5b5b', [PasswordHash] = N'AQAAAAIAAYagAAAAEKVZaMSnfCcQkj0nTjcsaRZbLAI1r2GtoEwlT902g8MTe2kxDUFRjU/2nSXWFIf1ZA==', [SecurityStamp] = N'44d03c41-42ca-48a4-8506-4cefca20031c'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'3baf23ed-b6da-4d51-8642-72deb7a71f47', [PasswordHash] = N'AQAAAAIAAYagAAAAEGRFyScDWsvHYgAhSnWLN5qZp77tqrNCdjr528ZAmzOofy2lanByjafwck5orUmiwQ==', [SecurityStamp] = N'b57c3f22-0714-4c21-aa37-ac3fb9ab9be6'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260324102005_MigrateLocationCode', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var150 sysname;
SELECT @var150 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Warehouses]') AND [c].[name] = N'Code');
IF @var150 IS NOT NULL EXEC(N'ALTER TABLE [Warehouses] DROP CONSTRAINT [' + @var150 + '];');
ALTER TABLE [Warehouses] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var151 sysname;
SELECT @var151 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Units]') AND [c].[name] = N'Code');
IF @var151 IS NOT NULL EXEC(N'ALTER TABLE [Units] DROP CONSTRAINT [' + @var151 + '];');
ALTER TABLE [Units] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var152 sysname;
SELECT @var152 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Statuses]') AND [c].[name] = N'Code');
IF @var152 IS NOT NULL EXEC(N'ALTER TABLE [Statuses] DROP CONSTRAINT [' + @var152 + '];');
ALTER TABLE [Statuses] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var153 sysname;
SELECT @var153 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SOACRTypes]') AND [c].[name] = N'Code');
IF @var153 IS NOT NULL EXEC(N'ALTER TABLE [SOACRTypes] DROP CONSTRAINT [' + @var153 + '];');
ALTER TABLE [SOACRTypes] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var154 sysname;
SELECT @var154 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Regions]') AND [c].[name] = N'Code');
IF @var154 IS NOT NULL EXEC(N'ALTER TABLE [Regions] DROP CONSTRAINT [' + @var154 + '];');
ALTER TABLE [Regions] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var155 sysname;
SELECT @var155 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ReasonOfReturns]') AND [c].[name] = N'Code');
IF @var155 IS NOT NULL EXEC(N'ALTER TABLE [ReasonOfReturns] DROP CONSTRAINT [' + @var155 + '];');
ALTER TABLE [ReasonOfReturns] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var156 sysname;
SELECT @var156 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseStatuses]') AND [c].[name] = N'Code');
IF @var156 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseStatuses] DROP CONSTRAINT [' + @var156 + '];');
ALTER TABLE [PurchaseStatuses] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var157 sysname;
SELECT @var157 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseRequestTypes]') AND [c].[name] = N'Code');
IF @var157 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseRequestTypes] DROP CONSTRAINT [' + @var157 + '];');
ALTER TABLE [PurchaseRequestTypes] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var158 sysname;
SELECT @var158 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseRequestPurposes]') AND [c].[name] = N'Code');
IF @var158 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseRequestPurposes] DROP CONSTRAINT [' + @var158 + '];');
ALTER TABLE [PurchaseRequestPurposes] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var159 sysname;
SELECT @var159 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderTypes]') AND [c].[name] = N'Code');
IF @var159 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderTypes] DROP CONSTRAINT [' + @var159 + '];');
ALTER TABLE [PurchaseOrderTypes] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var160 sysname;
SELECT @var160 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PurchaseOrderDeliveryModes]') AND [c].[name] = N'Code');
IF @var160 IS NOT NULL EXEC(N'ALTER TABLE [PurchaseOrderDeliveryModes] DROP CONSTRAINT [' + @var160 + '];');
ALTER TABLE [PurchaseOrderDeliveryModes] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var161 sysname;
SELECT @var161 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PackTypes]') AND [c].[name] = N'Code');
IF @var161 IS NOT NULL EXEC(N'ALTER TABLE [PackTypes] DROP CONSTRAINT [' + @var161 + '];');
ALTER TABLE [PackTypes] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var162 sysname;
SELECT @var162 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LocationCodes]') AND [c].[name] = N'Code');
IF @var162 IS NOT NULL EXEC(N'ALTER TABLE [LocationCodes] DROP CONSTRAINT [' + @var162 + '];');
ALTER TABLE [LocationCodes] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var163 sysname;
SELECT @var163 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ItemTypes]') AND [c].[name] = N'Code');
IF @var163 IS NOT NULL EXEC(N'ALTER TABLE [ItemTypes] DROP CONSTRAINT [' + @var163 + '];');
ALTER TABLE [ItemTypes] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var164 sysname;
SELECT @var164 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InvoiceTypes]') AND [c].[name] = N'Code');
IF @var164 IS NOT NULL EXEC(N'ALTER TABLE [InvoiceTypes] DROP CONSTRAINT [' + @var164 + '];');
ALTER TABLE [InvoiceTypes] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var165 sysname;
SELECT @var165 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DeliveryTimes]') AND [c].[name] = N'Code');
IF @var165 IS NOT NULL EXEC(N'ALTER TABLE [DeliveryTimes] DROP CONSTRAINT [' + @var165 + '];');
ALTER TABLE [DeliveryTimes] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var166 sysname;
SELECT @var166 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerTypes]') AND [c].[name] = N'Code');
IF @var166 IS NOT NULL EXEC(N'ALTER TABLE [CustomerTypes] DROP CONSTRAINT [' + @var166 + '];');
ALTER TABLE [CustomerTypes] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var167 sysname;
SELECT @var167 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerAccountStatuses]') AND [c].[name] = N'Code');
IF @var167 IS NOT NULL EXEC(N'ALTER TABLE [CustomerAccountStatuses] DROP CONSTRAINT [' + @var167 + '];');
ALTER TABLE [CustomerAccountStatuses] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var168 sysname;
SELECT @var168 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ChequeStatuses]') AND [c].[name] = N'Code');
IF @var168 IS NOT NULL EXEC(N'ALTER TABLE [ChequeStatuses] DROP CONSTRAINT [' + @var168 + '];');
ALTER TABLE [ChequeStatuses] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var169 sysname;
SELECT @var169 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Categories]') AND [c].[name] = N'Code');
IF @var169 IS NOT NULL EXEC(N'ALTER TABLE [Categories] DROP CONSTRAINT [' + @var169 + '];');
ALTER TABLE [Categories] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

DECLARE @var170 sysname;
SELECT @var170 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Brands]') AND [c].[name] = N'Code');
IF @var170 IS NOT NULL EXEC(N'ALTER TABLE [Brands] DROP CONSTRAINT [' + @var170 + '];');
ALTER TABLE [Brands] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

ALTER TABLE [BarcotechSyncs] ADD [Trace] nvarchar(max) NULL;
GO

DECLARE @var171 sysname;
SELECT @var171 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Areas]') AND [c].[name] = N'Code');
IF @var171 IS NOT NULL EXEC(N'ALTER TABLE [Areas] DROP CONSTRAINT [' + @var171 + '];');
ALTER TABLE [Areas] ALTER COLUMN [Code] nvarchar(50) NOT NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'28428cb0-7a31-4b76-8d8f-5ef492c775be', [PasswordHash] = N'AQAAAAIAAYagAAAAEOcXulgBKm4FcYag25pxhu/yFp+kaZvbi/PFhpMKbthLOFORzihY1OBC/s3WNqglAQ==', [SecurityStamp] = N'8fa06036-8f61-4964-a7fd-82256c6d0fb0'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4e9fd29c-ee48-4279-bae0-841701c0ca08', [PasswordHash] = N'AQAAAAIAAYagAAAAEHXWck43Ug+iF5JVr6NiVMS3K/b6oUwAoj1DkE70G2SkMBU7VmecZmqjhQCVLMhoXg==', [SecurityStamp] = N'3d844957-8b2e-497f-a1dc-c1036a85d0f3'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260331005503_MigrateSyncTraceException', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PurchaseOrderNews] ADD [PurposeId] int NULL;
GO

ALTER TABLE [PurchaseOrderNews] ADD [RegionId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'7f6eaaf5-ec45-4789-8777-4d7b2368ec35', [PasswordHash] = N'AQAAAAIAAYagAAAAEOnI+7d/d9nmqauJ66wAuajzYO9IhEOkoioP627kNx20GIs65Irz9T98WVtJpApbXQ==', [SecurityStamp] = N'f8a69686-c044-4f94-bc50-e011ffbcba68'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'53497f40-8f13-45f2-9c95-c1d5a19c15f2', [PasswordHash] = N'AQAAAAIAAYagAAAAEPvxggC+EaTZucphMmoqvKE6eOrGskc7TpGqGUwgMLzVJNZ/gvLdD4VNQLUN6vlZUQ==', [SecurityStamp] = N'a382e553-66e9-4e52-8ec3-f90396d52072'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_PurchaseOrderNews_PurposeId] ON [PurchaseOrderNews] ([PurposeId]);
GO

CREATE INDEX [IX_PurchaseOrderNews_RegionId] ON [PurchaseOrderNews] ([RegionId]);
GO

ALTER TABLE [PurchaseOrderNews] ADD CONSTRAINT [FK_PurchaseOrderNews_PurchaseRequestPurposes_PurposeId] FOREIGN KEY ([PurposeId]) REFERENCES [PurchaseRequestPurposes] ([Id]);
GO

ALTER TABLE [PurchaseOrderNews] ADD CONSTRAINT [FK_PurchaseOrderNews_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260419181952_MigratePurchaseOrderPurposeRegion', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Configurations] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Configurations] PRIMARY KEY ([Id])
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'de18ebf0-eeab-4793-9551-9a0b6648806b', [PasswordHash] = N'AQAAAAIAAYagAAAAEP+JGeCWnNnIVEW3OYclKbv9jGRyoIBN3j024x7MPpInrf/6/BphW+xZvTLJIkqA0w==', [SecurityStamp] = N'abda63c5-f9e8-448b-a2ca-4da498b33c6d'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'890ff2b3-d704-4c9e-925f-6b0e80b56635', [PasswordHash] = N'AQAAAAIAAYagAAAAEBVRn3gUxUDnzxrrnniuWUAcyTu6o9nD+tGMHwlP68sqJUsZm8ZdTcG9u7KeGKgEjg==', [SecurityStamp] = N'c8bfdc49-77e5-4dc6-a25c-d8a5234e772b'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260426160002_MigrateConfiguration', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Items] ADD [ItemClassId] int NULL;
GO

CREATE TABLE [ItemClasses] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(50) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [IsActive] bit NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_ItemClasses] PRIMARY KEY ([Id])
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'2d18695b-be6d-45cc-9758-0096087e96b9', [PasswordHash] = N'AQAAAAIAAYagAAAAEOeQ0fiQqq3s+Demed7MV3070RV68R6ljDBPSqx5lmAHesMIxQQCpXdMgN7njrv27Q==', [SecurityStamp] = N'4a2c419c-82a5-4c52-b095-e3ebd26a5ef9'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4cd5e72d-6165-4e47-a330-06fd372c0ce2', [PasswordHash] = N'AQAAAAIAAYagAAAAENm5IFhP44tDkRlOb9XHvVVYY9vCIEQa0c0oWRXVGfBXLQGDKwhlhxENG/HwzgVwwA==', [SecurityStamp] = N'6455e60c-e18d-44a6-90d4-ac36edace26d'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_Items_ItemClassId] ON [Items] ([ItemClassId]);
GO

ALTER TABLE [Items] ADD CONSTRAINT [FK_Items_ItemClasses_ItemClassId] FOREIGN KEY ([ItemClassId]) REFERENCES [ItemClasses] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260506142541_MigrateItemClass', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [SalesOrderItemSources] (
    [Id] int NOT NULL IDENTITY,
    [SourceOrderNumber] nvarchar(50) NOT NULL,
    [SalesOrderId] int NULL,
    [ItemId] int NULL,
    [Status] nvarchar(max) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SalesOrderItemSources] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SalesOrderItemSources_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]),
    CONSTRAINT [FK_SalesOrderItemSources_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id])
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'd0fcbe40-0582-4675-afbc-fc8760409e09', [PasswordHash] = N'AQAAAAIAAYagAAAAEImTrEnDXlBNxOrukdttP5HYTDybEZbTBQFO24XxCcZcRB9fwTh8vexKaXTItHmaxA==', [SecurityStamp] = N'b3c83572-94cc-4db4-b367-d6c40d0fa879'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'cf63aa7a-a885-4ba4-824a-5032a15eff63', [PasswordHash] = N'AQAAAAIAAYagAAAAEGqMjKqRcJLFs46l5fyytwliNU//I3sYoX9kuWZyqUwK7mfpp+/pM6fdkaB5630zWg==', [SecurityStamp] = N'e2ff93dc-88f9-488a-9527-1bbfc33ec4d7'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_SalesOrderItemSources_ItemId] ON [SalesOrderItemSources] ([ItemId]);
GO

CREATE INDEX [IX_SalesOrderItemSources_SalesOrderId] ON [SalesOrderItemSources] ([SalesOrderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260507164932_MigrateSalesOrderItemSource', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrderLineItems] DROP CONSTRAINT [FK_SalesOrderLineItems_SalesOrderSources_SalesOrderSourceId];
GO

DROP TABLE [Configurations];
GO

DROP INDEX [IX_SalesOrderLineItems_SalesOrderSourceId] ON [SalesOrderLineItems];
GO

DECLARE @var172 sysname;
SELECT @var172 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderLineItems]') AND [c].[name] = N'SalesOrderSourceId');
IF @var172 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderLineItems] DROP CONSTRAINT [' + @var172 + '];');
ALTER TABLE [SalesOrderLineItems] DROP COLUMN [SalesOrderSourceId];
GO

CREATE TABLE [SystemSettings] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SystemSettings] PRIMARY KEY ([Id])
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'b9d501ea-b9fd-4d2a-a4df-2a602aa3f69f', [PasswordHash] = N'AQAAAAIAAYagAAAAECAFiXsY8mzrYYnKg8vl0w3i/TVlJaIWo0CvgGeDSyvdqjvbCquKQ0YjEmyBegE4kQ==', [SecurityStamp] = N'9630531b-a162-4351-812b-e2f891f5ec16'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'10382d95-06f3-4ba9-9066-20c52a10b648', [PasswordHash] = N'AQAAAAIAAYagAAAAEDpRTcDjDNofkV/tnP5rIt9Kdo4jzSLmGtnV/3iB9YiLTJ3Wrg36TQcyXlG5MLIg2g==', [SecurityStamp] = N'bb3ae762-d4c9-4761-912c-06eb0f188ce5'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260512160901_MigrateSystemSettings', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [ItemLocations];
GO

ALTER TABLE [ItemInventories] ADD [LocationCodeId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'ce5387bf-496e-459a-8bd7-5607465327ac', [PasswordHash] = N'AQAAAAIAAYagAAAAEOKBn9msVj+yHjxYJQnHzHQdPzi3q8eAqkJ3mcDD0ucaV4nsEurdaDiow9BqR3qWgQ==', [SecurityStamp] = N'e9e14374-be97-49fe-b027-805458e264af'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'8bcff24c-1efb-45ff-a788-1fbd22df8170', [PasswordHash] = N'AQAAAAIAAYagAAAAENfFGoAJGgjxTHLmZ2PHRU7HmL5w500d7ywuEV09d+p2ELL+QlLMAXDXlkEIDzexHg==', [SecurityStamp] = N'e6906757-c1a9-4f53-9cd3-16458e0cd4a4'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_ItemInventories_LocationCodeId] ON [ItemInventories] ([LocationCodeId]);
GO

ALTER TABLE [ItemInventories] ADD CONSTRAINT [FK_ItemInventories_LocationCodes_LocationCodeId] FOREIGN KEY ([LocationCodeId]) REFERENCES [LocationCodes] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260518140118_MigrateItemLocation', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ItemInventories] ADD [AllocatedQuantity] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260523131947_MigrateStocks', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [SalesOrderDeliveryMethods] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(50) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_SalesOrderDeliveryMethods] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SalesOrderDeliveryStatuses] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(50) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_SalesOrderDeliveryStatuses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SalesOrderDeliveries] (
    [Id] int NOT NULL IDENTITY,
    [SalesOrderId] int NOT NULL,
    [PickupDate] datetime2 NULL,
    [DeliveryDate] datetime2 NULL,
    [ReceiveDate] datetime2 NULL,
    [SalesOrderDeliveryMethodId] int NOT NULL,
    [ForwarderId] int NOT NULL,
    [Waybill] nvarchar(150) NULL,
    [DriverId] int NULL,
    [VehicleId] int NULL,
    [ReceivedBy] nvarchar(501) NULL,
    [ReceiverContact] nvarchar(501) NULL,
    [Remarks] nvarchar(501) NULL,
    [SalesOrderDeliveryStatusId] int NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_SalesOrderDeliveries] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SalesOrderDeliveries_Drivers_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [Drivers] ([Id]),
    CONSTRAINT [FK_SalesOrderDeliveries_Forwarders_ForwarderId] FOREIGN KEY ([ForwarderId]) REFERENCES [Forwarders] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SalesOrderDeliveries_SalesOrderDeliveryMethods_SalesOrderDeliveryMethodId] FOREIGN KEY ([SalesOrderDeliveryMethodId]) REFERENCES [SalesOrderDeliveryMethods] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SalesOrderDeliveries_SalesOrderDeliveryStatuses_SalesOrderDeliveryStatusId] FOREIGN KEY ([SalesOrderDeliveryStatusId]) REFERENCES [SalesOrderDeliveryStatuses] ([Id]),
    CONSTRAINT [FK_SalesOrderDeliveries_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SalesOrderDeliveries_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[SalesOrderDeliveryMethods]'))
    SET IDENTITY_INSERT [SalesOrderDeliveryMethods] ON;
INSERT INTO [SalesOrderDeliveryMethods] ([Id], [Code], [Name])
VALUES (1, N'COMPANY TRUCK', N'COMPANY TRUCK'),
(2, N'FORWARDER', N'FORWARDER'),
(3, N'COURIER', N'COURIER'),
(4, N'CUSTOMER PICKUP', N'CUSTOMER PICKUP');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[SalesOrderDeliveryMethods]'))
    SET IDENTITY_INSERT [SalesOrderDeliveryMethods] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[SalesOrderDeliveryStatuses]'))
    SET IDENTITY_INSERT [SalesOrderDeliveryStatuses] ON;
INSERT INTO [SalesOrderDeliveryStatuses] ([Id], [Code], [Name])
VALUES (1, N'DRAFT', N'DRAFT'),
(2, N'PICKED UP', N'PICKED UP'),
(3, N'DELIVERED', N'DELIVERED');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[SalesOrderDeliveryStatuses]'))
    SET IDENTITY_INSERT [SalesOrderDeliveryStatuses] OFF;
GO

CREATE INDEX [IX_SalesOrderDeliveries_DriverId] ON [SalesOrderDeliveries] ([DriverId]);
GO

CREATE INDEX [IX_SalesOrderDeliveries_ForwarderId] ON [SalesOrderDeliveries] ([ForwarderId]);
GO

CREATE INDEX [IX_SalesOrderDeliveries_SalesOrderDeliveryMethodId] ON [SalesOrderDeliveries] ([SalesOrderDeliveryMethodId]);
GO

CREATE INDEX [IX_SalesOrderDeliveries_SalesOrderDeliveryStatusId] ON [SalesOrderDeliveries] ([SalesOrderDeliveryStatusId]);
GO

CREATE INDEX [IX_SalesOrderDeliveries_SalesOrderId] ON [SalesOrderDeliveries] ([SalesOrderId]);
GO

CREATE INDEX [IX_SalesOrderDeliveries_VehicleId] ON [SalesOrderDeliveries] ([VehicleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260525181713_MigrateSalesOrderDelivery', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrderDeliveries] DROP CONSTRAINT [FK_SalesOrderDeliveries_SalesOrderDeliveryStatuses_SalesOrderDeliveryStatusId];
GO

DROP INDEX [IX_SalesOrderDeliveries_SalesOrderDeliveryStatusId] ON [SalesOrderDeliveries];
DECLARE @var173 sysname;
SELECT @var173 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderDeliveries]') AND [c].[name] = N'SalesOrderDeliveryStatusId');
IF @var173 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderDeliveries] DROP CONSTRAINT [' + @var173 + '];');
ALTER TABLE [SalesOrderDeliveries] ALTER COLUMN [SalesOrderDeliveryStatusId] int NOT NULL;
ALTER TABLE [SalesOrderDeliveries] ADD DEFAULT 1 FOR [SalesOrderDeliveryStatusId];
CREATE INDEX [IX_SalesOrderDeliveries_SalesOrderDeliveryStatusId] ON [SalesOrderDeliveries] ([SalesOrderDeliveryStatusId]);
GO

ALTER TABLE [SalesOrderDeliveries] ADD [PackId] int NOT NULL DEFAULT 1;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'146159fc-88d5-4e8d-8a16-39c8e4c59820', [PasswordHash] = N'AQAAAAIAAYagAAAAELmlFSKb8pVgQy448WF61sRFHrDswplKrdQhoAs8GgbwdutjTRKjbWt2crdlrOtNBw==', [SecurityStamp] = N'98a833e6-5755-43de-90e8-7f973d41fbf0'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4ff94771-f362-43e1-82d4-a300922ce54a', [PasswordHash] = N'AQAAAAIAAYagAAAAEAtIzUlgZa5J1lpi/BITwW889V+H39+zzUlw/9ACt9ge3dPFHaetD+BuCAzppa2siA==', [SecurityStamp] = N'c3a5e16e-4af5-4344-b7cc-b5edeef4eb48'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_SalesOrderDeliveries_PackId] ON [SalesOrderDeliveries] ([PackId]);
GO

ALTER TABLE [SalesOrderDeliveries] ADD CONSTRAINT [FK_SalesOrderDeliveries_Packs_PackId] FOREIGN KEY ([PackId]) REFERENCES [Packs] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [SalesOrderDeliveries] ADD CONSTRAINT [FK_SalesOrderDeliveries_SalesOrderDeliveryStatuses_SalesOrderDeliveryStatusId] FOREIGN KEY ([SalesOrderDeliveryStatusId]) REFERENCES [SalesOrderDeliveryStatuses] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260526011905_MigrateSalesOrderDelivery2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrderDeliveries] DROP CONSTRAINT [FK_SalesOrderDeliveries_Forwarders_ForwarderId];
GO

DECLARE @var174 sysname;
SELECT @var174 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalesOrderDeliveries]') AND [c].[name] = N'ForwarderId');
IF @var174 IS NOT NULL EXEC(N'ALTER TABLE [SalesOrderDeliveries] DROP CONSTRAINT [' + @var174 + '];');
ALTER TABLE [SalesOrderDeliveries] ALTER COLUMN [ForwarderId] int NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'bb98464b-505f-41a9-a7e3-965448314b60', [PasswordHash] = N'AQAAAAIAAYagAAAAECYPI24LTi7bYnJCpgWXbZ99owiSzTwOl+LrVcYrSqKoP7QWdNvB5T/aMsvL7/Go0g==', [SecurityStamp] = N'477b404e-311d-4427-93c7-7b9e77027056'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'679646fb-ae1e-4bbc-b1fd-e2cca7b8b8a1', [PasswordHash] = N'AQAAAAIAAYagAAAAEKT+vAYcUyfoEEtUEneExdWoYIl/KPUTvZKBDO0iTfBPgYTPiAZ1XdtmIwsBuoEN3Q==', [SecurityStamp] = N'b0b17a10-0dd0-4a36-a346-061266e69a93'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [SalesOrderDeliveries] ADD CONSTRAINT [FK_SalesOrderDeliveries_Forwarders_ForwarderId] FOREIGN KEY ([ForwarderId]) REFERENCES [Forwarders] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260526105258_MigrateSalesOrderDelivery3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Pick] (
    [Id] int NOT NULL IDENTITY,
    [SalesOrderId] int NOT NULL,
    [Status] nvarchar(50) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_Pick] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pick_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Pick_SalesOrderId] ON [Pick] ([SalesOrderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260528163651_MigratePick', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Pick] DROP CONSTRAINT [FK_Pick_SalesOrders_SalesOrderId];
GO

ALTER TABLE [Pick] DROP CONSTRAINT [PK_Pick];
GO

EXEC sp_rename N'[Pick]', N'Picks';
GO

EXEC sp_rename N'[Picks].[IX_Pick_SalesOrderId]', N'IX_Picks_SalesOrderId', N'INDEX';
GO

ALTER TABLE [Picks] ADD [PickDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Picks] ADD CONSTRAINT [PK_Picks] PRIMARY KEY ([Id]);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'7042d1f1-2d58-4b61-8913-de1e4eb96ef4', [PasswordHash] = N'AQAAAAIAAYagAAAAEFHmK9iJto3aLTd1LaXgzAVladlPAYw+txch8ZvlEKzUfC537WltPyk8rGasLaZCAw==', [SecurityStamp] = N'b3822a8a-f134-45fe-87bc-8e5aa8c93cc4'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'38fcd515-2ef0-4de9-a3f2-1ba4db1ccb91', [PasswordHash] = N'AQAAAAIAAYagAAAAEF/X4iAjgaeTK2BjtIXFa8inGAtLULh/l0+tFe3OUpj0on4mMpCFADBSGnRMGxvljQ==', [SecurityStamp] = N'e27f5fdb-c493-4609-9ae8-96dd7101ed9f'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [Picks] ADD CONSTRAINT [FK_Picks_SalesOrders_SalesOrderId] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrders] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260528174545_MigratePick2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrders] ADD [ForwarderId] int NULL;
GO

CREATE INDEX [IX_SalesOrders_ForwarderId] ON [SalesOrders] ([ForwarderId]);
GO

ALTER TABLE [SalesOrders] ADD CONSTRAINT [FK_SalesOrders_Forwarders_ForwarderId] FOREIGN KEY ([ForwarderId]) REFERENCES [Forwarders] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260531051239_MigrateForwarder', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Brands] DROP CONSTRAINT [FK_Brands_ItemTypes_ItemTypeId];
GO

DROP INDEX [IX_Brands_ItemTypeId] ON [Brands];
GO

DECLARE @var175 sysname;
SELECT @var175 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Brands]') AND [c].[name] = N'ItemTypeId');
IF @var175 IS NOT NULL EXEC(N'ALTER TABLE [Brands] DROP CONSTRAINT [' + @var175 + '];');
ALTER TABLE [Brands] DROP COLUMN [ItemTypeId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260601112549_MigrateItemType', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrders] ADD [RegionId] int NOT NULL DEFAULT 1;
GO

CREATE INDEX [IX_SalesOrders_RegionId] ON [SalesOrders] ([RegionId]);
GO

ALTER TABLE [SalesOrders] ADD CONSTRAINT [FK_SalesOrders_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260621083955_MigrateSORegion', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [KYCStatus] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(50) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_KYCStatus] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [KYC] (
    [Id] int NOT NULL IDENTITY,
    [CompanyName] nvarchar(100) NOT NULL,
    [RegisteredAddress] nvarchar(300) NOT NULL,
    [OperatingAddress] nvarchar(300) NOT NULL,
    [WarehouseAddress] nvarchar(300) NOT NULL,
    [OperatingHours] nvarchar(50) NOT NULL,
    [Website] nvarchar(250) NULL,
    [Email] nvarchar(250) NULL,
    [ContactNumber] nvarchar(50) NULL,
    [IsSoleProprietorship] bit NULL,
    [SecDtiNumber] nvarchar(50) NULL,
    [TinNumber] nvarchar(50) NULL,
    [IsVATRegistered] bit NOT NULL,
    [BusinessNature] nvarchar(100) NULL,
    [ProductServiceOffered] nvarchar(100) NULL,
    [CreditLineAmount] decimal(18,2) NULL,
    [CreditTerm] int NULL,
    [SubmittedBIRForm2303] datetime2 NULL,
    [SubmittedMayorBusinessPermit] datetime2 NULL,
    [SubmittedSECDTICertificate] datetime2 NULL,
    [SubmittedLatestAuditedFinancialStatement] datetime2 NULL,
    [SubmittedIncorporationArticleByLaws] datetime2 NULL,
    [SubmittedSecretaryCertificate] datetime2 NULL,
    [SubmittedCompanyPortfolio] datetime2 NULL,
    [SubmittedSignatoriesIdentificationCard] datetime2 NULL,
    [SubmittedSampleSalesInvoiceOfficialReceipt] datetime2 NULL,
    [SubmittedPrivacyConsent] datetime2 NULL,
    [DateConsent] datetime2 NOT NULL,
    [CustomerId] int NOT NULL,
    [StatusId] int NOT NULL,
    [SubmittedId] nvarchar(max) NULL,
    [SubmitterId] nvarchar(450) NOT NULL,
    [DateSubmitted] datetime2 NULL,
    [ApproverId] nvarchar(450) NULL,
    [DateApproved] datetime2 NULL,
    [DisapproverId] nvarchar(450) NULL,
    [DateDisapproved] datetime2 NULL,
    [DisapprovedReason] nvarchar(50) NULL,
    [Remarks] nvarchar(50) NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_KYC] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_KYC_AspNetUsers_ApproverId] FOREIGN KEY ([ApproverId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_KYC_AspNetUsers_DisapproverId] FOREIGN KEY ([DisapproverId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_KYC_AspNetUsers_SubmitterId] FOREIGN KEY ([SubmitterId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_KYC_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_KYC_KYCStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [KYCStatus] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [KYCBank] (
    [Id] int NOT NULL IDENTITY,
    [KYCId] int NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Branch] nvarchar(50) NOT NULL,
    [AccountNumber] nvarchar(50) NOT NULL,
    [ContactNumber] nvarchar(50) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_KYCBank] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_KYCBank_KYC_KYCId] FOREIGN KEY ([KYCId]) REFERENCES [KYC] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [KYCClient] (
    [Id] int NOT NULL IDENTITY,
    [KYCId] int NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [ContactPerson] nvarchar(50) NOT NULL,
    [ContactNumber] nvarchar(50) NOT NULL,
    [SalesVolume] decimal(18,2) NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_KYCClient] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_KYCClient_KYC_KYCId] FOREIGN KEY ([KYCId]) REFERENCES [KYC] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [KYCSignatory] (
    [Id] int NOT NULL IDENTITY,
    [KYCId] int NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Designation] nvarchar(50) NOT NULL,
    [Email] nvarchar(50) NULL,
    [ContactNumber] nvarchar(50) NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_KYCSignatory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_KYCSignatory_KYC_KYCId] FOREIGN KEY ([KYCId]) REFERENCES [KYC] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'2314cce6-8d9b-4ebc-a202-55ef847adac1', [PasswordHash] = N'AQAAAAIAAYagAAAAEAC2lq2HY2Ef/5FWX/VKKRJvJs5kgc4w2IJrHwATxJlnpGAxrlR9UyTDznNIgYTfXw==', [SecurityStamp] = N'96cb3103-2fa8-4fdc-94a1-4407443899d6'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'055351fe-1036-49fd-99db-289bcaa02278', [PasswordHash] = N'AQAAAAIAAYagAAAAEPzne5jsuaa/wEbs96eVSBu700rIv+ozrX6VnjLyr3utpynVIsaxo/zi9FJGn0OWBA==', [SecurityStamp] = N'76684403-98b6-4406-84fe-03bc0c0929e3'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_KYC_ApproverId] ON [KYC] ([ApproverId]);
GO

CREATE INDEX [IX_KYC_CustomerId] ON [KYC] ([CustomerId]);
GO

CREATE INDEX [IX_KYC_DisapproverId] ON [KYC] ([DisapproverId]);
GO

CREATE INDEX [IX_KYC_StatusId] ON [KYC] ([StatusId]);
GO

CREATE INDEX [IX_KYC_SubmitterId] ON [KYC] ([SubmitterId]);
GO

CREATE INDEX [IX_KYCBank_KYCId] ON [KYCBank] ([KYCId]);
GO

CREATE INDEX [IX_KYCClient_KYCId] ON [KYCClient] ([KYCId]);
GO

CREATE INDEX [IX_KYCSignatory_KYCId] ON [KYCSignatory] ([KYCId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260630163531_MigrateKYC', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [KYC] DROP CONSTRAINT [FK_KYC_AspNetUsers_SubmitterId];
GO

ALTER TABLE [KYC] DROP CONSTRAINT [FK_KYC_KYCStatus_StatusId];
GO

DROP TABLE [KYCStatus];
GO

DROP INDEX [IX_KYC_StatusId] ON [KYC];
GO

DECLARE @var176 sysname;
SELECT @var176 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYC]') AND [c].[name] = N'StatusId');
IF @var176 IS NOT NULL EXEC(N'ALTER TABLE [KYC] DROP CONSTRAINT [' + @var176 + '];');
ALTER TABLE [KYC] DROP COLUMN [StatusId];
GO

DECLARE @var177 sysname;
SELECT @var177 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYC]') AND [c].[name] = N'SubmittedId');
IF @var177 IS NOT NULL EXEC(N'ALTER TABLE [KYC] DROP CONSTRAINT [' + @var177 + '];');
ALTER TABLE [KYC] DROP COLUMN [SubmittedId];
GO

DECLARE @var178 sysname;
SELECT @var178 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYC]') AND [c].[name] = N'SubmitterId');
IF @var178 IS NOT NULL EXEC(N'ALTER TABLE [KYC] DROP CONSTRAINT [' + @var178 + '];');
ALTER TABLE [KYC] ALTER COLUMN [SubmitterId] nvarchar(450) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'41953cbc-c096-45a4-b2ec-f0d314bd817b', [PasswordHash] = N'AQAAAAIAAYagAAAAEGbwS71ooY2nap9CTgig+eKUu5KB/BqG6wyoIUOiRdGcXwCp5/0fei4u1D7wVadSvg==', [SecurityStamp] = N'b2d0bda4-8e11-44b5-821a-014cfd38a87f'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'9accb308-13a0-416d-8da3-58f6077c790c', [PasswordHash] = N'AQAAAAIAAYagAAAAEIlKbOKTsd/RdouKgBe/GcJ5FipZ0EaR4BbzvRyz12EM3TgBOCf8qKiWnhCiHwSbKw==', [SecurityStamp] = N'1b902806-4595-42da-8bdd-f7de08816f2a'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [KYC] ADD CONSTRAINT [FK_KYC_AspNetUsers_SubmitterId] FOREIGN KEY ([SubmitterId]) REFERENCES [AspNetUsers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260630164233_MigrateKYC2', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [KYC] DROP CONSTRAINT [FK_KYC_AspNetUsers_ApproverId];
GO

ALTER TABLE [KYC] DROP CONSTRAINT [FK_KYC_AspNetUsers_DisapproverId];
GO

ALTER TABLE [KYC] DROP CONSTRAINT [FK_KYC_AspNetUsers_SubmitterId];
GO

ALTER TABLE [KYC] DROP CONSTRAINT [FK_KYC_Customers_CustomerId];
GO

ALTER TABLE [KYCBank] DROP CONSTRAINT [FK_KYCBank_KYC_KYCId];
GO

ALTER TABLE [KYCClient] DROP CONSTRAINT [FK_KYCClient_KYC_KYCId];
GO

ALTER TABLE [KYCSignatory] DROP CONSTRAINT [FK_KYCSignatory_KYC_KYCId];
GO

ALTER TABLE [KYCSignatory] DROP CONSTRAINT [PK_KYCSignatory];
GO

ALTER TABLE [KYCClient] DROP CONSTRAINT [PK_KYCClient];
GO

ALTER TABLE [KYCBank] DROP CONSTRAINT [PK_KYCBank];
GO

ALTER TABLE [KYC] DROP CONSTRAINT [PK_KYC];
GO

DECLARE @var179 sysname;
SELECT @var179 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYC]') AND [c].[name] = N'IsSoleProprietorship');
IF @var179 IS NOT NULL EXEC(N'ALTER TABLE [KYC] DROP CONSTRAINT [' + @var179 + '];');
ALTER TABLE [KYC] DROP COLUMN [IsSoleProprietorship];
GO

DECLARE @var180 sysname;
SELECT @var180 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYC]') AND [c].[name] = N'SubmittedBIRForm2303');
IF @var180 IS NOT NULL EXEC(N'ALTER TABLE [KYC] DROP CONSTRAINT [' + @var180 + '];');
ALTER TABLE [KYC] DROP COLUMN [SubmittedBIRForm2303];
GO

EXEC sp_rename N'[KYCSignatory]', N'KYCSignatories';
GO

EXEC sp_rename N'[KYCClient]', N'KYCClients';
GO

EXEC sp_rename N'[KYCBank]', N'KYCBanks';
GO

EXEC sp_rename N'[KYC]', N'KYCs';
GO

EXEC sp_rename N'[KYCSignatories].[IX_KYCSignatory_KYCId]', N'IX_KYCSignatories_KYCId', N'INDEX';
GO

EXEC sp_rename N'[KYCClients].[IX_KYCClient_KYCId]', N'IX_KYCClients_KYCId', N'INDEX';
GO

EXEC sp_rename N'[KYCBanks].[IX_KYCBank_KYCId]', N'IX_KYCBanks_KYCId', N'INDEX';
GO

EXEC sp_rename N'[KYCs].[TinNumber]', N'BirTinNumber', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[SubmittedSignatoriesIdentificationCard]', N'ExpirationSignatoriesIdentificationCard', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[SubmittedSecretaryCertificate]', N'ExpirationSecretaryCertificate', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[SubmittedSampleSalesInvoiceOfficialReceipt]', N'ExpirationSampleSalesInvoiceOfficialReceipt', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[SubmittedSECDTICertificate]', N'ExpirationSECDTICertificate', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[SubmittedPrivacyConsent]', N'ExpirationMayorBusinessPermit', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[SubmittedMayorBusinessPermit]', N'ExpirationLatestAuditedFinancialStatement', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[SubmittedLatestAuditedFinancialStatement]', N'ExpirationIncorporationArticleByLaws', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[SubmittedIncorporationArticleByLaws]', N'ExpirationCompanyPortfolio', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[SubmittedCompanyPortfolio]', N'ExpirationBIRForm2303', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[IsVATRegistered]', N'VatStatus', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[DateConsent]', N'PrivacyConsent', N'COLUMN';
GO

EXEC sp_rename N'[KYCs].[IX_KYC_SubmitterId]', N'IX_KYCs_SubmitterId', N'INDEX';
GO

EXEC sp_rename N'[KYCs].[IX_KYC_DisapproverId]', N'IX_KYCs_DisapproverId', N'INDEX';
GO

EXEC sp_rename N'[KYCs].[IX_KYC_CustomerId]', N'IX_KYCs_CustomerId', N'INDEX';
GO

EXEC sp_rename N'[KYCs].[IX_KYC_ApproverId]', N'IX_KYCs_ApproverId', N'INDEX';
GO

ALTER TABLE [KYCs] ADD [OrganizationType] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [KYCSignatories] ADD CONSTRAINT [PK_KYCSignatories] PRIMARY KEY ([Id]);
GO

ALTER TABLE [KYCClients] ADD CONSTRAINT [PK_KYCClients] PRIMARY KEY ([Id]);
GO

ALTER TABLE [KYCBanks] ADD CONSTRAINT [PK_KYCBanks] PRIMARY KEY ([Id]);
GO

ALTER TABLE [KYCs] ADD CONSTRAINT [PK_KYCs] PRIMARY KEY ([Id]);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'716c82ac-696a-4070-9848-f419f82c81ed', [PasswordHash] = N'AQAAAAIAAYagAAAAEJuiCNTFgrsYd/wnNmM+/q/IImx+4aI4TnvceIaXuOl9Th2+4a3kWezeiFeDnr8gyQ==', [SecurityStamp] = N'b86e8811-0919-4c45-80ec-cc939bc4be8b'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'c65e8e16-5446-474a-9f70-0054328215c8', [PasswordHash] = N'AQAAAAIAAYagAAAAEOMQVgnGbYny2weRXJPptuwYktct4CeMEu5pxnAfnLikaweOeHiJMANCD3PhLBgK3Q==', [SecurityStamp] = N'252c2325-650d-4c24-a0af-3a0f46567ff3'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [KYCBanks] ADD CONSTRAINT [FK_KYCBanks_KYCs_KYCId] FOREIGN KEY ([KYCId]) REFERENCES [KYCs] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [KYCClients] ADD CONSTRAINT [FK_KYCClients_KYCs_KYCId] FOREIGN KEY ([KYCId]) REFERENCES [KYCs] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [KYCs] ADD CONSTRAINT [FK_KYCs_AspNetUsers_ApproverId] FOREIGN KEY ([ApproverId]) REFERENCES [AspNetUsers] ([Id]);
GO

ALTER TABLE [KYCs] ADD CONSTRAINT [FK_KYCs_AspNetUsers_DisapproverId] FOREIGN KEY ([DisapproverId]) REFERENCES [AspNetUsers] ([Id]);
GO

ALTER TABLE [KYCs] ADD CONSTRAINT [FK_KYCs_AspNetUsers_SubmitterId] FOREIGN KEY ([SubmitterId]) REFERENCES [AspNetUsers] ([Id]);
GO

ALTER TABLE [KYCs] ADD CONSTRAINT [FK_KYCs_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [KYCSignatories] ADD CONSTRAINT [FK_KYCSignatories_KYCs_KYCId] FOREIGN KEY ([KYCId]) REFERENCES [KYCs] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260701171803_MigrateKYC3', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var181 sysname;
SELECT @var181 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'CreditTerm');
IF @var181 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var181 + '];');
ALTER TABLE [KYCs] DROP COLUMN [CreditTerm];
GO

DECLARE @var182 sysname;
SELECT @var182 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'PrivacyConsent');
IF @var182 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var182 + '];');
ALTER TABLE [KYCs] ALTER COLUMN [PrivacyConsent] datetime2 NULL;
GO

ALTER TABLE [KYCs] ADD [CreditTermId] int NOT NULL DEFAULT 0;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'bfb2ea09-e1fa-4138-83d2-94a0a34793bd', [PasswordHash] = N'AQAAAAIAAYagAAAAENkDir6yffdQk3/5BkxnPpNKxSpZWWQBrOJejToJyzmISCNK71vLqLxIBv7HKaLuGg==', [SecurityStamp] = N'ffd6f4ef-32f3-4a71-8c6a-ec438dc51373'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'589c653d-ee30-4527-ace9-7a079112224d', [PasswordHash] = N'AQAAAAIAAYagAAAAEE3xdOsgkWOG5eHWRZC7OTRfOxT9T8CQvRJcA2MmaZNBnQ3TskzCBmI+cA68K8O5rA==', [SecurityStamp] = N'2befe272-995a-40b1-bafe-9880e72d799f'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_KYCs_CreditTermId] ON [KYCs] ([CreditTermId]);
GO

ALTER TABLE [KYCs] ADD CONSTRAINT [FK_KYCs_CreditTerms_CreditTermId] FOREIGN KEY ([CreditTermId]) REFERENCES [CreditTerms] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260704171540_MigrateKYC4', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [KYCs] ADD [Status] nvarchar(max) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'9028ce89-c8c0-4bbb-93ae-dc31e3594a93', [PasswordHash] = N'AQAAAAIAAYagAAAAENDFKRyfDkw3+QlNjvNLPr3Eiuf065cOefOigTfC91gO8NlSEzhbFygOiK/YRfRtMQ==', [SecurityStamp] = N'f6616f75-5362-449c-9537-02ac42b83713'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'47491ef3-0986-412b-844c-ecfdc5e29945', [PasswordHash] = N'AQAAAAIAAYagAAAAEHicjadQI6jxwxha9/mD+z7M+/lpJV4iG6xRHZ96dOLLYsM87iorZ0X57w/c4dpfPg==', [SecurityStamp] = N'e956f922-3e30-435a-b917-a44e30d34848'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260706152751_MigrateKYC5', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var183 sysname;
SELECT @var183 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'OperatingHours');
IF @var183 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var183 + '];');
ALTER TABLE [KYCs] ALTER COLUMN [OperatingHours] nvarchar(50) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'bad9ecca-0730-4f8a-a552-1ea1e993e0a8', [PasswordHash] = N'AQAAAAIAAYagAAAAEIVInj/L4Q/Et5lL8aw026TdJi0ExE6Uu2hrKuuO8Ub29Bkz8hXflbTGDOu/sWgKuw==', [SecurityStamp] = N'36de042a-1aeb-44aa-a9ed-6a0ec442a759'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'3ded5871-e4f2-4c10-977d-3bf81190dd1c', [PasswordHash] = N'AQAAAAIAAYagAAAAELnHoSUOD0q5cT4cNTC9GPcSQzr+bPkRh1hEsbcTvwk8y+PPqfDgWDXOHjYbN46rFA==', [SecurityStamp] = N'59d5beeb-94f7-409a-aa9a-74624e08a790'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260706213001_MigrateKYC6', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var184 sysname;
SELECT @var184 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'WarehouseAddress');
IF @var184 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var184 + '];');
ALTER TABLE [KYCs] ALTER COLUMN [WarehouseAddress] nvarchar(300) NULL;
GO

DECLARE @var185 sysname;
SELECT @var185 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'SecDtiNumber');
IF @var185 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var185 + '];');
ALTER TABLE [KYCs] ALTER COLUMN [SecDtiNumber] nvarchar(50) NOT NULL;
ALTER TABLE [KYCs] ADD DEFAULT N'' FOR [SecDtiNumber];
GO

DECLARE @var186 sysname;
SELECT @var186 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'ProductServiceOffered');
IF @var186 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var186 + '];');
ALTER TABLE [KYCs] ALTER COLUMN [ProductServiceOffered] nvarchar(100) NOT NULL;
ALTER TABLE [KYCs] ADD DEFAULT N'' FOR [ProductServiceOffered];
GO

DECLARE @var187 sysname;
SELECT @var187 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'Email');
IF @var187 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var187 + '];');
ALTER TABLE [KYCs] ALTER COLUMN [Email] nvarchar(250) NOT NULL;
ALTER TABLE [KYCs] ADD DEFAULT N'' FOR [Email];
GO

DECLARE @var188 sysname;
SELECT @var188 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'CreditLineAmount');
IF @var188 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var188 + '];');
ALTER TABLE [KYCs] ALTER COLUMN [CreditLineAmount] decimal(18,2) NOT NULL;
ALTER TABLE [KYCs] ADD DEFAULT 0.0 FOR [CreditLineAmount];
GO

DECLARE @var189 sysname;
SELECT @var189 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'ContactNumber');
IF @var189 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var189 + '];');
ALTER TABLE [KYCs] ALTER COLUMN [ContactNumber] nvarchar(50) NOT NULL;
ALTER TABLE [KYCs] ADD DEFAULT N'' FOR [ContactNumber];
GO

DECLARE @var190 sysname;
SELECT @var190 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'BusinessNature');
IF @var190 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var190 + '];');
ALTER TABLE [KYCs] ALTER COLUMN [BusinessNature] nvarchar(100) NOT NULL;
ALTER TABLE [KYCs] ADD DEFAULT N'' FOR [BusinessNature];
GO

DECLARE @var191 sysname;
SELECT @var191 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'BirTinNumber');
IF @var191 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var191 + '];');
ALTER TABLE [KYCs] ALTER COLUMN [BirTinNumber] nvarchar(50) NOT NULL;
ALTER TABLE [KYCs] ADD DEFAULT N'' FOR [BirTinNumber];
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'79255145-99be-4ac5-b25b-c7d735aefa5c', [PasswordHash] = N'AQAAAAIAAYagAAAAEENiM4IoE6wK2BJhIt32WnOd2Xj6HTN6qUfPTle/AU8A9k4AxJP0cPQKYwr55/O3ug==', [SecurityStamp] = N'fe9ce668-fdb8-4fd5-a563-c8f68a5425e6'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'80144245-19c7-4000-a87a-90eabe9fbe2a', [PasswordHash] = N'AQAAAAIAAYagAAAAEJ3sY8+x9sMekbIMqRksl0Lr6KleEX2rFQaDiJ7idZHFIotKlEjYLi5CRrh/OZx4cA==', [SecurityStamp] = N'065a01ca-ae41-4360-b6e7-2804d8e350c1'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260706214709_MigrateKYC7', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var192 sysname;
SELECT @var192 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCClients]') AND [c].[name] = N'SalesVolume');
IF @var192 IS NOT NULL EXEC(N'ALTER TABLE [KYCClients] DROP CONSTRAINT [' + @var192 + '];');
ALTER TABLE [KYCClients] ALTER COLUMN [SalesVolume] decimal(18,2) NULL;
GO

DECLARE @var193 sysname;
SELECT @var193 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCClients]') AND [c].[name] = N'ContactPerson');
IF @var193 IS NOT NULL EXEC(N'ALTER TABLE [KYCClients] DROP CONSTRAINT [' + @var193 + '];');
ALTER TABLE [KYCClients] ALTER COLUMN [ContactPerson] nvarchar(50) NULL;
GO

DECLARE @var194 sysname;
SELECT @var194 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCClients]') AND [c].[name] = N'ContactNumber');
IF @var194 IS NOT NULL EXEC(N'ALTER TABLE [KYCClients] DROP CONSTRAINT [' + @var194 + '];');
ALTER TABLE [KYCClients] ALTER COLUMN [ContactNumber] nvarchar(50) NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'0584ecaf-8187-47c7-9567-dc75dfb0b3d3', [PasswordHash] = N'AQAAAAIAAYagAAAAECunjoqezESfMnfEu0uaTL5G5h5rIZLF++wFO1zKj/0TIe7lJXvoU5/dbo6VNwGFPw==', [SecurityStamp] = N'55bd7ad3-9c02-4773-9c91-a2fc367241fa'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'18f63f0c-7aba-4125-b850-1c97f26d7a10', [PasswordHash] = N'AQAAAAIAAYagAAAAEDZKcK/onMLH4eWPWv8z1HOlxMU8vE4skUN9KHmhg+whf+BM0AbEsa/T3tKDtADeKw==', [SecurityStamp] = N'8323ec91-cec1-4839-a113-59985c021295'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260712165357_MigrateKYC8', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrders] ADD [ResubmittedBy] nvarchar(50) NULL;
GO

ALTER TABLE [SalesOrders] ADD [ResubmittedDate] datetime2 NULL;
GO

ALTER TABLE [SalesOrders] ADD [SubmittedBy] nvarchar(50) NULL;
GO

ALTER TABLE [SalesOrders] ADD [SubmittedDate] datetime2 NULL;
GO

UPDATE [Pages] SET [HasUploadDownload] = CAST(1 AS bit)
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260728055850_MigrationSalesOrder', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [PackLineItems] ADD [GrossPriceD] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260801071359_MigrationPack', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var195 sysname;
SELECT @var195 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'ExpirationBIRForm2303');
IF @var195 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var195 + '];');
ALTER TABLE [KYCs] DROP COLUMN [ExpirationBIRForm2303];
GO

DECLARE @var196 sysname;
SELECT @var196 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'ExpirationCompanyPortfolio');
IF @var196 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var196 + '];');
ALTER TABLE [KYCs] DROP COLUMN [ExpirationCompanyPortfolio];
GO

DECLARE @var197 sysname;
SELECT @var197 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'ExpirationIncorporationArticleByLaws');
IF @var197 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var197 + '];');
ALTER TABLE [KYCs] DROP COLUMN [ExpirationIncorporationArticleByLaws];
GO

DECLARE @var198 sysname;
SELECT @var198 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'ExpirationLatestAuditedFinancialStatement');
IF @var198 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var198 + '];');
ALTER TABLE [KYCs] DROP COLUMN [ExpirationLatestAuditedFinancialStatement];
GO

DECLARE @var199 sysname;
SELECT @var199 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'ExpirationMayorBusinessPermit');
IF @var199 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var199 + '];');
ALTER TABLE [KYCs] DROP COLUMN [ExpirationMayorBusinessPermit];
GO

DECLARE @var200 sysname;
SELECT @var200 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'ExpirationSECDTICertificate');
IF @var200 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var200 + '];');
ALTER TABLE [KYCs] DROP COLUMN [ExpirationSECDTICertificate];
GO

DECLARE @var201 sysname;
SELECT @var201 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'ExpirationSampleSalesInvoiceOfficialReceipt');
IF @var201 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var201 + '];');
ALTER TABLE [KYCs] DROP COLUMN [ExpirationSampleSalesInvoiceOfficialReceipt];
GO

DECLARE @var202 sysname;
SELECT @var202 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'ExpirationSecretaryCertificate');
IF @var202 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var202 + '];');
ALTER TABLE [KYCs] DROP COLUMN [ExpirationSecretaryCertificate];
GO

DECLARE @var203 sysname;
SELECT @var203 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[KYCs]') AND [c].[name] = N'ExpirationSignatoriesIdentificationCard');
IF @var203 IS NOT NULL EXEC(N'ALTER TABLE [KYCs] DROP CONSTRAINT [' + @var203 + '];');
ALTER TABLE [KYCs] DROP COLUMN [ExpirationSignatoriesIdentificationCard];
GO

CREATE TABLE [KYCDocumentTypes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_KYCDocumentTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [KYCDocuments] (
    [Id] int NOT NULL IDENTITY,
    [KYCId] int NOT NULL,
    [KYCDocumentTypeId] int NOT NULL,
    [FileName] nvarchar(255) NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [UserCreated] nvarchar(450) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_KYCDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_KYCDocuments_KYCDocumentTypes_KYCDocumentTypeId] FOREIGN KEY ([KYCDocumentTypeId]) REFERENCES [KYCDocumentTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_KYCDocuments_KYCs_KYCId] FOREIGN KEY ([KYCId]) REFERENCES [KYCs] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'9d7778d3-f19f-4ead-b038-9df5b774d6e8', [PasswordHash] = N'AQAAAAIAAYagAAAAEPF0B0C3wVXxhBLNQ8ZyptxQ8zvmJor7aaRlLZPEbayuw9DJk0j65kFIN5DjzuGlDQ==', [SecurityStamp] = N'78e2deab-9edd-4868-9ba8-ea08a291227e'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'b648935f-6481-4b2f-9330-b90c6bd9b468', [PasswordHash] = N'AQAAAAIAAYagAAAAEKK0g3EMfLvHjzDK/n4Po91oDRO3Ai8cc9q0jeCQja0klHowd/bF9VYBGKo6b4biaQ==', [SecurityStamp] = N'e1bd5401-4e22-4700-9081-eb92e4c0c341'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[KYCDocumentTypes]'))
    SET IDENTITY_INSERT [KYCDocumentTypes] ON;
INSERT INTO [KYCDocumentTypes] ([Id], [Name])
VALUES (1, N'BIR Form 2303 (Certificate of Registration)'),
(2, N'Valid Mayor''s / Business Permit (Current Year)'),
(3, N'SEC / DTI Certificate of Registration'),
(4, N'Latest Audited Financial Statement (Last 2 Years)'),
(5, N'Articles of Incorporation / By-Laws (For Corporations)'),
(6, N'Secretary''s Certificate (Authorizing the person above the sign)'),
(7, N'Company Profile / Portfolio'),
(8, N'Valid Identification Card of the Signatories'),
(9, N'Sample Sales Invoice / Official Receipt (From Major Clients)');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[KYCDocumentTypes]'))
    SET IDENTITY_INSERT [KYCDocumentTypes] OFF;
GO

CREATE INDEX [IX_KYCDocuments_KYCDocumentTypeId] ON [KYCDocuments] ([KYCDocumentTypeId]);
GO

CREATE INDEX [IX_KYCDocuments_KYCId] ON [KYCDocuments] ([KYCId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260824153636_MigrateKYCDocument', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalesOrderLineItems] ADD [IsPromo] bit NULL;
GO

ALTER TABLE [PackLineItems] ADD [BinLocation] nvarchar(50) NULL;
GO

ALTER TABLE [PackLineItems] ADD [GrossPriceD] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [PackLineItems] ADD [IsPromo] bit NULL;
GO

ALTER TABLE [ItemInventories] ADD [BinLocation] nvarchar(max) NULL;
GO

ALTER TABLE [InvoiceLineItems] ADD [IsPromo] bit NULL;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'37d2d80e-6ab4-449b-9371-415707005dae', [PasswordHash] = N'AQAAAAIAAYagAAAAEPXW0sEbv+nlyWFJVuTTZ1qQhxH/9TMNhgDCkCPZqN4gpRvqNLM+LvXYN/a1AKlIWQ==', [SecurityStamp] = N'a9d480ec-bd3a-4e0d-a3bf-3b819cefa5f8'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'6b8952d5-22b5-4492-962b-3399b85eb315', [PasswordHash] = N'AQAAAAIAAYagAAAAEL6VGe2I8yDNqgrioTxmaP5kdCjkiZr7LVPt5X8HOWKMUJSU7cmN/It3EFld3pSpdA==', [SecurityStamp] = N'4c3a8184-bcab-439c-9d9f-5f2f9f7c48bb'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260929065855_mix_verify', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'fe81ac7a-0d88-453b-bf4a-7d8906d9b836', [PasswordHash] = N'AQAAAAIAAYagAAAAECyi3mxhOHk9PhjuySOWBv5VG1giXXYltVzekVcBeg/T1DmpnMCMT3KLBmNZMEpTyQ==', [SecurityStamp] = N'96fb4ec0-bfa3-45aa-bba4-2206691c47ce'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'157dcbb0-5e81-43cb-90e6-37c831378353', [PasswordHash] = N'AQAAAAIAAYagAAAAENeQDDkH12O5OXRw4MhD9GnqiaIZk1tDPXn+syoAqAZV+XFuYk8d4iKl0xQxzLxb7A==', [SecurityStamp] = N'1eef81af-3f9f-4edf-b3a7-c91f9b588c9b'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261003090820_SLA_Update', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [KPIs] (
    [Id] int NOT NULL IDENTITY,
    [SalesOrderId] int NOT NULL,
    [UserCreated] nvarchar(max) NOT NULL,
    [KpiStart] datetime2 NOT NULL,
    [KpiEnd] datetime2 NOT NULL,
    [Department] nvarchar(max) NOT NULL,
    [UserModified] nvarchar(450) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateModified] datetime2 NULL,
    CONSTRAINT [PK_KPIs] PRIMARY KEY ([Id])
);
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'e7215e88-3bb8-42dc-b134-fd9a7a36cb2e', [PasswordHash] = N'AQAAAAIAAYagAAAAEJGj3xPmxnHhMnxAdK0snraw5Tnqg36y8ymeBp54gzEV0uTsvnME2UECBwYUndlr5g==', [SecurityStamp] = N'7ed7707f-51d2-4eb1-91d7-02fdfee0bd6d'
WHERE [Id] = N'66A86BDE-9D0F-48F8-9CCF-DDF33673862C';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'cd0df4e2-c1f5-4394-a3d9-f348e74e7e57', [PasswordHash] = N'AQAAAAIAAYagAAAAELyCFJfK6S2rNpZWk6AII88LxMlLWmtzRruXad8J4E6xTQ5b9kGluWTpz/ZK5Th1vw==', [SecurityStamp] = N'ec929db9-d58a-457f-90e2-645ac631cb55'
WHERE [Id] = N'CDE19596-2C9E-4ECE-837C-85950E9E907D';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261013094242_Changes', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Pages] ADD [HasForceClose] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Accesses] ADD [CanForceClose] bit NULL;
GO

UPDATE [Pages] SET [HasForceClose] = CAST(1 AS bit)
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261023031013_ForceClose', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[PageGroups]'))
    SET IDENTITY_INSERT [PageGroups] ON;
INSERT INTO [PageGroups] ([Id], [Name])
VALUES (10, N'AGENT ITIRENARY REPORT');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[PageGroups]'))
    SET IDENTITY_INSERT [PageGroups] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'HasAdd', N'HasApproveDisapprove', N'HasDelete', N'HasEdit', N'HasForceClose', N'HasPrint', N'HasUploadDownload', N'HasView', N'Name', N'OrderNumber', N'PageGroupId') AND [object_id] = OBJECT_ID(N'[Pages]'))
    SET IDENTITY_INSERT [Pages] ON;
INSERT INTO [Pages] ([Id], [HasAdd], [HasApproveDisapprove], [HasDelete], [HasEdit], [HasForceClose], [HasPrint], [HasUploadDownload], [HasView], [Name], [OrderNumber], [PageGroupId])
VALUES (90, CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'Agent Itirenary', 1, 10);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'HasAdd', N'HasApproveDisapprove', N'HasDelete', N'HasEdit', N'HasForceClose', N'HasPrint', N'HasUploadDownload', N'HasView', N'Name', N'OrderNumber', N'PageGroupId') AND [object_id] = OBJECT_ID(N'[Pages]'))
    SET IDENTITY_INSERT [Pages] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261109064142_AgentItinerary', N'6.0.7');
GO

COMMIT;
GO

