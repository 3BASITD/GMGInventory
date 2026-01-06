using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Helpers
{
    public class SalesHelper
    {
        public async Task<string> GetNewOrder(ApplicationDbContext context)
        {
            var soNumberPrefix = $"SO";

            var lastOrder = await context.SalesOrders
                                         .Where(s => s.SalesOrderNumber.StartsWith(soNumberPrefix))
                                         .OrderByDescending(s => s.SalesOrderNumber)
                                         .Select(s => s.SalesOrderNumber)
                                         .FirstOrDefaultAsync();

            if (lastOrder != null && lastOrder.Contains("-D"))
            {
                var splitLastOrder = lastOrder.Split("-D");
                lastOrder = splitLastOrder[0];
            }

            if (lastOrder != null && lastOrder.Contains('.'))
            {
                var splitLastOrder = lastOrder.Split('.');
                lastOrder = splitLastOrder[0];
            }

            int.TryParse(lastOrder?.Replace(soNumberPrefix, "") ?? "0", out int soNumberLast);
            return $"{soNumberPrefix}{(soNumberLast + 1).ToString("D8")}";
        }

        public async Task<string> GetNewReturn(ApplicationDbContext context)
        {
            var srNumberPrefix = $"SR-";

            var lastReturn = await context.SalesReturns
                                         .Where(s => s.SalesReturnNumber.StartsWith(srNumberPrefix))
                                         .OrderByDescending(s => s.SalesReturnNumber)
                                         .Select(s => s.SalesReturnNumber)
                                         .FirstOrDefaultAsync();

            if (lastReturn != null && lastReturn.Contains("-D"))
            {
                var splitLastReturn = lastReturn.Split("-D");
                lastReturn = splitLastReturn[0];
            }

            if (lastReturn != null && lastReturn.Contains('.'))
            {
                var splitLastReturn = lastReturn.Split('.');
                lastReturn = splitLastReturn[0];
            }

            int.TryParse(lastReturn?.Replace(srNumberPrefix, "") ?? "0", out int srNumberLast);
            return $"{srNumberPrefix}{(srNumberLast + 1).ToString("D7")}";
        }
    }
}
