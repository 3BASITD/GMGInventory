using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Data
{
    public class KPI: BaseEntity
    {
        public int? SalesOrderId { get; set; }
        public string? UserCreated { get; set; }
        public DateTime? KpiStart { get; set; }
        public DateTime? KpiEnd { get; set; }
        public string? Department { get; set; }
    }
}
