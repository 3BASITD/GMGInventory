using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Helpers
{
    public class ViewsAccessData(Access access)
    {
        public ViewsAccessVM Convert()
        {
            return new ViewsAccessVM
            {
                CanView = access.CanView,
                CanAdd = access.CanAdd,
                CanEdit = access.CanEdit,
                CanDelete = access.CanDelete,
                CanApproveDisapprove = access.CanApproveDisapprove == true,
                CanUploadDownload = access.CanUploadDownload == true,
                CanPrint = access.CanPrint == true,
                CanForceClose = access.CanForceClose == true
            };
        }
    }
}
