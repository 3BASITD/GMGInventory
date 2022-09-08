using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Common.Models
{
    public class ForDataTables<T>
    {
        public string? Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IList<T> Data { get; set; }
    }

    public class DataTablesRequestForm
    {
        public DataTablesRequestForm(IFormCollection form)
        {
            Draw = form["draw"].FirstOrDefault();
            SortColumn = form["columns[" + form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            SortColumnDirection = form["order[0][dir]"].FirstOrDefault();
            SearchValue = form["search[value]"].FirstOrDefault("").ToLower();
            PageSize = Convert.ToInt32(form["length"].FirstOrDefault() ?? "0");
            Skip = Convert.ToInt32(form["start"].FirstOrDefault() ?? "0");
        }

        public string? Draw { get; set; }
        public string? SortColumn { get; set; }
        public string? SortColumnDirection { get; set; }
        public string? SearchValue { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
    }
    public class ResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class ResponseModel<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T NewRow { get; set; }
    }

    public class JsonOptionsModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; } = false;
    }

    public class JsonSupplierOptionsModel : JsonOptionsModel
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
    }

    public class JsonItemOptionsModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }

    public class JsonNameOptionsModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
    }

    public class JsonPurchaseOrderOptionsModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Supplier { get; set; }
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime OrderDateD { get; set; }
        public string OrderBy { get; set; }
    }

    public class JsonCurrencyOptionModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
        public decimal Rate { get; set; }
    }

    public class Select2Model<T>
    {
        //public bool incomplete_results { get; set; }
        public IList<T> items { get; set; }
        public int total_count { get; set; }
    }
}
