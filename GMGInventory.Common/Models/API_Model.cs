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
        public int AreaId { get; set; }
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
        public int UnitId { get; set; }
        public int CategoryId { get; set; }
        public int ItemTypeId { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<int> LocationCodeIds { get; set; }
    }

    public class APIItemType : APIGeneric { }

    public class APIBrand : APIGeneric { }

    public class APISalesOrder
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int CustomerId { get; set; }
        public string SalesOrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<APISalesOrderLineItem> LineItems { get; set; }
    }

    public class APISalesOrderLineItem
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class APIPack
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        public DateTime PackDate { get; set; }
        public IEnumerable<APIPackLineItem> LineItems { get; set; }
    }

    public class APIPackLineItem
    {
        public int Id { get; set; }
        public string PackType { get; set; }
        public int BoxNumber { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class APISalesReturn
    {
        public int Id { get; set; }
        public int? SalesOrderId { get; set; }
        public string SalesReturnNumber { get; set; }
        public DateTime ReturnDate { get; set; }
        public IEnumerable<APISalesReturnLineItem> LineItems { get; set; }
    }

    public class APISalesReturnLineItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int ReasonOfReturnId { get; set; }
    }

    public class APIPurchaseOrder
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public int SupplierId { get; set; }
        public int WarehouseId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DueDate { get; set; }
        public IEnumerable<APIPurchaseLineItem> LineItems { get; set; }
    }

    public class APIPurchaseReturn
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime ReturnDate { get; set; }
        public IEnumerable<APIPurchaseLineItem> LineItems { get; set; }
    }

    public class APIPurchaseLineItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}