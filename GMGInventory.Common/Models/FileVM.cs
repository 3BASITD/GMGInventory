using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GMGInventory.Common.Models
{
    public class FileVM
    {
        public string FileName { get; set; } = "";
        public byte[] Content { get; set; } = Array.Empty<byte>();
    }
}
