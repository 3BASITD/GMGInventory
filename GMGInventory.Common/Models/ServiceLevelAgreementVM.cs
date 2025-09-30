using GMGInventory.Common.Constants;

namespace GMGInventory.Common.Models
{
    public class ServiceLevelAgreementVM : GenericAuditVM
    {
        public string PriorAvailability => DateTime.Today.Add(PriorAvailabilityD).ToString("hh:mm tt");
        public string PriorAvailability3 => PriorAvailabilityD.ToString(@"hh\:mm");
        public TimeSpan PriorAvailabilityD { get; set; }
    }
}
