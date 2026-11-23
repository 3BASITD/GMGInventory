using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GMGInventory.Application.Helpers
{
    public class ProcurementHelper
    {
        public static readonly Regex DocNoRegex =  new(@"^(.*?-PR\d{4}-)(\d+)(.*)$", RegexOptions.Compiled);

        public async Task<string> GetNewRequest(ApplicationDbContext context, string type, string status)
        {
            var docNumberPrefix = status.ToLower() == "draft"
                ? $"D{type}-PR{DateTime.Now.Year}-"
                : $"{type}-PR{DateTime.Now.Year}-";

            var documents = await context.PurchaseRequests
                .Where(s => s.DocumentNumber.StartsWith(docNumberPrefix))
                .Select(s => s.DocumentNumber)
                .ToListAsync();

            int docNumberLast = 0;
            foreach (var doc in documents)
            {
                // strip prefix, then remove suffixes like "-D01" or ".1"
                var numberPart = doc.Substring(docNumberPrefix.Length)
                                    .Split(new[] { "-D", "." }, StringSplitOptions.None)[0];

                if (int.TryParse(numberPart, out int n) && n > docNumberLast)
                    docNumberLast = n;
            }

            return $"{docNumberPrefix}{(docNumberLast + 1).ToString("D4")}";
        }

        public async Task<string> GetNewOrder(ApplicationDbContext context, string type, string status)
        {
            var docNumberPrefix = "";
            if (status.ToLower() == "draft")
                docNumberPrefix = $"D{type}-PO{DateTime.Now.Year}-";
            else
                docNumberPrefix = $"{type}-PO{DateTime.Now.Year}-";
            var lastDocument = await context.PurchaseOrderNews
                                            .Where(s => s.DocumentNumber.StartsWith(docNumberPrefix))
                                            .OrderByDescending(s => s.DocumentNumber)
                                            .Select(s => s.DocumentNumber)
                                            .FirstOrDefaultAsync();

            if (lastDocument != null && lastDocument.Contains("-D"))
            {
                var splitLastDocument = lastDocument.Split("-D");
                lastDocument = splitLastDocument[0];
            }
            if (lastDocument != null && lastDocument.Contains("."))
            {
                var splitLastDocument = lastDocument.Split(".");
                lastDocument = splitLastDocument[0];
            }

            int.TryParse(lastDocument?.Replace(docNumberPrefix, "") ?? "0", out int docNumberLast);
            return $"{docNumberPrefix}{docNumberLast + 1:D4}";
        }

        public async Task<string> GetNewReceiving(ApplicationDbContext context, string status)
        {
            var docNumberPrefix = "";
            if (status.ToLower() == "draft")
                docNumberPrefix = $"DRR{DateTime.Now.Year % 100:D2}-";
            else
                docNumberPrefix = $"RR{DateTime.Now.Year % 100:D2}-";
            var lastDocument = await context.PurchaseReceivings
                                            .Where(s => s.DocumentNumber.StartsWith(docNumberPrefix))
                                            .OrderByDescending(s => s.DocumentNumber)
                                            .Select(s => s.DocumentNumber)
                                            .FirstOrDefaultAsync();

            if (lastDocument != null && lastDocument.Contains("-D"))
            {
                var splitLastDocument = lastDocument.Split("-D");
                lastDocument = splitLastDocument[0];
            }
            if (lastDocument != null && lastDocument.Contains("."))
            {
                var splitLastDocument = lastDocument.Split(".");
                lastDocument = splitLastDocument[0];
            }

            int.TryParse(lastDocument?.Replace(docNumberPrefix, "") ?? "0", out int docNumberLast);
            return $"{docNumberPrefix}{docNumberLast + 1:D6}";
        }

        public async Task<string> GetNewReturn(ApplicationDbContext context, string status)
        {
            var docNumberPrefix = "";
            if (status.ToLower() == "draft")
                docNumberPrefix = $"DRO{DateTime.Now.Year % 100:D2}-";
            else
                docNumberPrefix = $"RO{DateTime.Now.Year % 100:D2}-";
            var lastDocument = await context.PurchaseReturnNews
                                            .Where(s => s.DocumentNumber.StartsWith(docNumberPrefix))
                                            .OrderByDescending(s => s.DocumentNumber)
                                            .Select(s => s.DocumentNumber)
                                            .FirstOrDefaultAsync();

            if (lastDocument != null && lastDocument.Contains("-D"))
            {
                var splitLastDocument = lastDocument.Split("-D");
                lastDocument = splitLastDocument[0];
            }
            if (lastDocument != null && lastDocument.Contains("."))
            {
                var splitLastDocument = lastDocument.Split(".");
                lastDocument = splitLastDocument[0];
            }

            int.TryParse(lastDocument?.Replace(docNumberPrefix, "") ?? "0", out int docNumberLast);
            return $"{docNumberPrefix}{docNumberLast + 1:D6}";
        }

        public static (string Prefix, long Number, string Suffix) DocNoKey(string docNo)
        {
            var m = DocNoRegex.Match(docNo ?? "");
            return m.Success && long.TryParse(m.Groups[2].Value, out var n)
                ? (m.Groups[1].Value, n, m.Groups[3].Value)
                : (docNo ?? "", 0, "");
        }
    }
}
