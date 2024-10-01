using GMGInventory.Common.Constants;

namespace GMGInventory.Common.Models
{
    public class ViewsAccessVM
    {
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanApproveDisapprove { get; set; }
        public bool CanUploadDownload { get; set; }
        public bool CanPrint { get; set; }
    }

    public class AccessVM
    {
        public int AccessId { get; set; }
        public int PageId { get; set; }
        public string Name { get; set; }
        public bool HasView { get; set; }
        public bool CanView { get; set; }
        public bool HasAdd { get; set; }
        public bool CanAdd { get; set; }
        public bool HasEdit { get; set; }
        public bool CanEdit { get; set; }
        public bool HasDelete { get; set; }
        public bool CanDelete { get; set; }
        public bool HasApproveDisapprove { get; set; }
        public bool CanApproveDisapprove { get; set; }
        public bool HasUploadDownload { get; set; }
        public bool CanUploadDownload { get; set; }
        public bool HasPrint { get; set; }
        public bool CanPrint { get; set; }
        public string UserCreated { get; set; }
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }
        public string? UserModified { get; set; }
        public string DateModified => DateModifiedD == null ? "-" : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }
    }

    public class RoleVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string UserCreated { get; set; }
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }
        public string? UserModified { get; set; }
        public string DateModified => DateModifiedD == null ? "-" : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }
    }
}
