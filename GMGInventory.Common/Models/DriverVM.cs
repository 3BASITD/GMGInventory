using GMGInventory.Common.Constants;

namespace GMGInventory.Common.Models
{
    public class DriverVM : GenericAuditVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Restriction { get; set; }
        public string? LicenseDateExpiration => LicenseDateExpirationD == null 
                                              ? null
                                              : LicenseDateExpirationD.Value.ToString(SystemFormat.StringDate);
        public string? LicenseDateExpiration3 => LicenseDateExpirationD == null 
                                               ? null
                                               : LicenseDateExpirationD.Value.ToString(SystemFormat.StringDate3);
        public DateTime? LicenseDateExpirationD { get; set; }
    }
}
