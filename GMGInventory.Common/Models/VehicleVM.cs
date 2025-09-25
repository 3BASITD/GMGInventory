using GMGInventory.Common.Constants;

namespace GMGInventory.Common.Models
{
    public class VehicleVM : GenericAuditVM
    {
        public string FleetPlateNumber { get; set; }
        public string VehicleType { get; set; }
        public string? DateRegistered => DateRegisteredD == null
                                       ? null
                                       : DateRegisteredD.Value.ToString(SystemFormat.StringDate);
        public string? DateRegistered3 => DateRegisteredD == null
                                         ? null
                                         : DateRegisteredD.Value.ToString(SystemFormat.StringDate3);
        public DateTime? DateRegisteredD { get; set; }
        public string? DateExpiration => DateExpirationD == null
                                       ? null
                                       : DateExpirationD.Value.ToString(SystemFormat.StringDate);
        public string? DateExpiration3 => DateExpirationD == null
                                        ? null
                                        : DateExpirationD.Value.ToString(SystemFormat.StringDate3);
        public DateTime? DateExpirationD { get; set; }
    }
}
