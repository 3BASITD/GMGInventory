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
        public List<T> Data { get; set; }
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
        public bool Selected { get; set; }
    }
}
