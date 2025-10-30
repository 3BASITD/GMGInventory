using GMGInventory.Common.Constants;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Common.Models
{
    public class APILogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    // unit
    public class APIGeneric
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    // customer, category, location code
    public class APIGenericActive : APIGeneric
    {
        public bool IsActive { get; set; }
    }

    public class APIAgent
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public int AreaId { get; set; }
        public bool IsActive { get; set; }
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
        public bool IsActive { get; set; }
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
        public bool IsActive { get; set; }
        public IList<int> LocationCodeIds { get; set; }
    }

    public class APIItemType : APIGenericActive
    {
        public int CategoryId { get; set; }
    }

    public class APIBrand : APIGenericActive
    {
        public int ItemTypeId { get; set; }
    }
}