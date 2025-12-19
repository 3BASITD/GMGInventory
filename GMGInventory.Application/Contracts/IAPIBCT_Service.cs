using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IAPIBCT_Service
    {
        Task<IList<ResponseModelData<APIBCT_Receiving>>> GetReceiving(string apiUrl, DateTime? fromDate);
        Task<IList<ResponseModelData<APIBCT_Outbound>>> GetOutbound(string apiUrl, DateTime? fromDate);
        Task<IList<ResponseModelData<APIBCT_Return>>> GetReturn(string apiUrl, DateTime? fromDate);
        Task<IList<ResponseModelData<APIBCT_Transfer>>> GetTransfer(string apiUrl, DateTime? fromDate);
    }
}