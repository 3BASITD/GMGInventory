using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class HomeImageVM
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string File { get; set; }

        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Display Time")]
        public string DisplayTime
        {
            get
            {
                TimeSpan time = TimeSpan.FromMilliseconds(DisplayTimeD);
                return string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
            }
        }
        public int DisplayTimeD { get; set; }

        [Display(Name = "Created By")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }

        [Display(Name = "Modified By")]
        public string? UserModified { get; set; }

        [Display(Name = "Date Modified")]
        public string DateModified => DateModifiedD == null ? "-" : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }
    }

    public class HomeImageFormVM
    {
        public int? Id { get; set; }
        public IFormFile File { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int DisplayTime { get; set; }
    }
}
