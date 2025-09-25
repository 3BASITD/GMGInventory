using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class GenericAuditVM
    {
        public int Id { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }

        [Display(Name = "User Modified")]
        public string? UserModified { get; set; }

        [Display(Name = "Date Modifed")]
        public string? DateModified => DateModifiedD == null ? null : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }
    }

    public class TimeSpanVM
    {
        private TimeSpan _timeSpan;

        public TimeSpanVM() : this(TimeSpan.Zero) { }

        public TimeSpanVM(TimeSpan ts) => TimeSpan = ts;
        public TimeSpanVM(long ticks) => TimeSpan = TimeSpan.FromTicks(ticks);

        public string String =>
            $"{(Days > 0 ? $"{Days} Day{(Days > 1 ? "s" : "")} " : "")}" +
            $"{(Hours > 0 ? $"{Hours} Hour{(Hours > 1 ? "s" : "")} " : "")}" +
            $"{(Minutes > 0 ? $"{Minutes} Minute{(Minutes > 1 ? "s" : "")}" : "")}".Trim();

        public TimeSpan TimeSpan
        {
            get => _timeSpan;
            set => _timeSpan = value;
        }

        public int Days
        {
            get => _timeSpan.Days;
            set => _timeSpan = new TimeSpan(value, Hours, Minutes, 0);
        }

        public int Hours
        {
            get => _timeSpan.Hours;
            set => _timeSpan = new TimeSpan(Days, value, Minutes, 0);
        }

        public int Minutes
        {
            get => _timeSpan.Minutes;
            set => _timeSpan = new TimeSpan(Days, Hours, value, 0);
        }

        public long Ticks
        {
            get => _timeSpan.Ticks;
            set => _timeSpan = TimeSpan.FromTicks(value);
        }
    }

}
