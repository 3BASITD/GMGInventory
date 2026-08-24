using GMGInventory.Common.Constants;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Common.Models
{
    public class APIGeneric
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class APIAgent
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? AreaCode { get; set; }
    }

    public class APICustomer : APIGeneric
    {
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FaxNumber { get; set; }
        public string? ContactPerson { get; set; }
    }

    public class APIDriver
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Restriction { get; set; }
        public DateTime? LicenseDateExpiration { get; set; }
    }

    public class APIItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string? OldCode { get; set; }
        public string Name { get; set; }
        public string? PartNumber { get; set; }
        public string? InterchangeNumber { get; set; }
        public string Size { get; set; }
        public string? UnitCode { get; set; }
        public string? ItemClassCode { get; set; }
        public string? CategoryCode { get; set; }
        public string? ItemTypeCode { get; set; }
        public string? BrandCode { get; set; }
        public IEnumerable<string> LocationCodes { get; set; }
    }

    public class APIItemType : APIGeneric 
    {
        public string Category { get; set; }
    }

    public class APIBrand : APIGeneric { }

    public class APISalesOrder
    {
        public int Id { get; set; }
        public string SalesOrderNumber { get; set; }
        public int AgentId { get; set; }
        public string AgentCode { get; set; }
        public int CustomerId { get; set; }
        public string CustomeName { get; set; }
        public int? ServiceLevelAgreementId { get; set; }
        public string? ServiceLevelAgreement { get; set; }
        public string CustomerCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? LastUserModified { get; set; }
        public DateTime? LastDateModified { get; set; }
        public IEnumerable<APISalesOrderLineItem> LineItems { get; set; }
    }

    public class APISalesOrderLineItem
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
    }

    public class APIPack
    {
        public int Id { get; set; }
        public string SalesOrderNumber { get; set; }
        public DateTime PackDate { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? LastUserModified { get; set; }
        public DateTime? LastDateModified { get; set; }
        public IEnumerable<APIPackLineItem> LineItems { get; set; }
    }

    public class APIPackLineItem
    {
        public int Id { get; set; }
        public string PackType { get; set; }
        public int BoxNumber { get; set; }
        public int? ItemId { get; set; }
        public string? ItemCode { get; set; }
        public int Quantity { get; set; }
    }

    public class APISalesReturn
    {
        public int Id { get; set; }
        public int? SalesOrderId { get; set; }
        public string? SalesOrderNumber { get; set; }
        public string SalesReturnNumber { get; set; }
        public DateTime ReturnDate { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? LastUserModified { get; set; }
        public DateTime? LastDateModified { get; set; }
        public IEnumerable<APISalesReturnLineItem> LineItems { get; set; }
    }

    public class APISalesReturnLineItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
        public int ReasonOfReturnId { get; set; }
    }

    public class APIPurchaseOrder
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public int? SupplierId { get; set; }
        public string? SupplierCode { get; set; }
        public int? WarehouseId { get; set; }
        public string? WarehouseCode { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? LastUserModified { get; set; }
        public DateTime? LastDateModified { get; set; }
        public IEnumerable<APIPurchaseLineItem> LineItems { get; set; }
    }

    public class APIPurchaseReturn
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime ReturnDate { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? LastUserModified { get; set; }
        public DateTime? LastDateModified { get; set; }
        public IEnumerable<APIPurchaseLineItem> LineItems { get; set; }
    }

    public class APIPurchaseLineItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
    }

    public class APISupplier: APIGeneric
    {
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? OfficeTelNo { get; set; }
        public string? ContactPerson { get; set; }
        public string Region { get; set; }
    }

    public class APIWarehouse : APIGeneric
    {
        public bool IsActive { get; set; }
        public bool IsMain { get; set; }
    }
}