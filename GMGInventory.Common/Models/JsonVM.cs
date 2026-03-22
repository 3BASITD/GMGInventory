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
        public int TotalQuantity { get; set; }
        public int TotalQuantity2 { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmount2 { get; set; }
        public IList<T> AdditionalData { get; set; }
    }
    public class ForDataTables<TData, TAdditional>
    {
        public string? Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IList<TData> Data { get; set; }
        public TAdditional AdditionalData { get; set; }
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

    public class ResponseModelData<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class JsonOptionsModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; } = false;
    }

    public class JsonSupplierOptionsModel : JsonOptionsModel
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; }
        public string CurrencyRate => string.Format("{0:#,##0.00}", CurrencyRateD);
        public decimal CurrencyRateD { get; set; }
    }

    public class JsonSalesOrderOptionsModel : JsonOptionsModel
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string SalesOrderDate => SalesOrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime SalesOrderDateD { get; set; }
        public string? PurchaseOrder { get; set; }
        public string? ForwarderName { get; set; }
        public int RegionId { get; set; }
        public string Invoices { get; set; }

    }
    public class JsonShippingOptionsModel : JsonOptionsModel
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string SalesOrderDate => SalesOrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime SalesOrderDateD { get; set; }
        public string ForwarderName { get; set; }
        public string PairedInvoices { get; set; }
        public string Packaging { get; set; }


    }

    public class JsonItemOptionsModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string? OldCode { get; set; }
        public string PartNumber { get; set; }
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }
        public int UnitId { get; set; }
        public string Unit { get; set; }
        public string NetPrice => string.Format(SystemFormat.StringDecimalComma, NetPriceD);
        public decimal NetPriceD { get; set; }
        public string RetailPrice => string.Format(SystemFormat.StringDecimalComma, RetailPriceD);
        public decimal RetailPriceD { get; set; }
        public string PromoPrice => string.Format(SystemFormat.StringDecimalComma, PromoPriceD);
        public decimal PromoPriceD { get; set; }
        public int? ItemPromoId { get; set; }
        public string ItemPromoName { get; set; }
        public int InventoryCount { get; set; }
        public int BrandId { get; set; }
       
    }

    public class JsonNameOptionsModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; } = false;
    }

    public class JsonNameStatusOptionsModel : JsonNameOptionsModel
    {
        public string Status { get; set; }
    }

    public class JsonCodeOptionsModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
        public bool Selected { get; set; } = false;
    }

    public class JsonCountryOptionsModel : JsonOptionsModel
    {
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
    }

    public class JsonOrderTypeOptionsModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
        public int RequestTypeId { get; set; }
        public bool Selected { get; set; } = false;
    }

    public class JsonPurchaseOptionModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalIncompleteQuantity { get; set; }
        public bool Selected { get; set; } = false;
        public string? AlreadyOn { get; set; }
        public bool Disabled { get; set; }

    }

    public class JsonPurchaseItemOptionsModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public int LineNumber { get; set; }
        public int? ItemId { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemDescription { get; set; }
        public string? PartNumber { get; set; }
        public int Quantity { get; set; }
        public int UnitId { get; set; }
        public string Unit { get; set; }
        public decimal UnitCost { get; set; }
        public int Served { get; set; }
        public int UnorderedQuantity => Math.Max(0, Quantity - Served);
        public string? OrderedOn { get; set; }
        public bool Disabled { get; set; }
        public string? PreviousCost => PreviousCurrency == null || PreviousCurrencyRateD == null || PreviousCostD == null
                                     ? null
                                     : (PreviousCurrency.Split('/')[0].Trim() + " " + string.Format(SystemFormat.StringDecimalComma, PreviousCostD)
                                      + (PreviousCurrency.Split('/')[0].Trim() == "PHP"
                                       ? ""
                                       : " = PHP " + string.Format(SystemFormat.StringDecimalComma, Math.Round(PreviousCostD.Value / PreviousCurrencyRateD.Value, 2))));
        public string? PreviousCurrency { get; set; }
        public decimal? PreviousCurrencyRateD { get; set; }
        public decimal? PreviousCostD { get; set; }
    }

    public class JsonCustomerOptionsModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CreditTerm { get; set; }
        public string Region { get; set; }
        public string Agent { get; set; }
        public string AgentCode { get; set; }
        public int AgentId { get; set; }
        public string OutstandingBalance => string.Format(SystemFormat.StringDecimalComma, OutstandingBalanceD);
        public decimal OutstandingBalanceD { get; set; }
        public string CustomerStatus { get; set; }
    }

    public class JsonInvoiceOptionsModel
    {
        public string Id { get; set; }
        public string Customer { get; set; }
        public string InvoiceNumber { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
        public string TBR => string.Format(SystemFormat.StringDecimalComma, TBRD);
        public decimal? TBRD { get; set; }
        public string Credit => string.Format(SystemFormat.StringDecimalComma, CreditD);
        public decimal? CreditD { get; set; }
        public string Debit => string.Format(SystemFormat.StringDecimalComma, DebitD);
        public decimal? DebitD { get; set; }
        public string PaidAmount => string.Format(SystemFormat.StringDecimalComma, PaidAmountD);
        public decimal? PaidAmountD { get; set; }
        public string FreightCharge => string.Format(SystemFormat.StringDecimalComma, FreightChargeD);
        public decimal? FreightChargeD { get; set; }
        public string Balance => string.Format(SystemFormat.StringDecimalComma, BalanceD);
        public decimal? BalanceD { get; set; }
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

    public class JsonShippingModel
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string? Desc { get; set; }
        public string Freight { get; set; }
        public decimal Value { get; set; }

    }

    public class JsonCurrencyOptionModel : JsonOptionsModel
    {
        public decimal Rate { get; set; }
    }

    public class Select2Model<T>
    {
        //public bool incomplete_results { get; set; }
        public IList<T> items { get; set; }
        public int total_count { get; set; }
    }

    public class ListFilterInventoryModel : ListFilterCommonModel
    {
        public int? Brand { get; set; }
        public int? Category { get; set; }
        public int? Item { get; set; }
        public int? ItemType { get; set; }
        public int? Supplier { get; set; }
        public int? Warehouse { get; set; }
    }

    public class ListFilterTransferModel : ListFilterCommonModel
    {
        public int? Brand { get; set; }
        public int? Destination { get; set; }
        public int? Source { get; set; }
    }

    public class ListFilterSalesModel : ListFilterCommonModel
    {
        public int? Agent { get; set; }
        public int? Area { get; set; }
        public string? Basis { get; set; }
        public int? Brand { get; set; }
        public int? Customer { get; set; }
        public string? InvoiceNumberFrom { get; set; }
        public string? InvoiceNumberTo { get; set; }
        public int? Item { get; set; }
        public string? ItemKeyword { get; set; }
        public int? Region { get; set; }
        public string? Region2 { get; set; }
    }

    public class ListFilterCustomerModel : ListFilterCommonModel
    {
        public int? Customer { get; set; }
    }

    public class ListFilterPurchaseOrderModel : ListFilterCommonModel
    {
        public int? Item { get; set; }
        public string? ItemKeyword { get; set; }
        public int? Supplier { get; set; }
        // public int UnservedQty { get; set; }
        public bool Unserved { get; set; }

        public new string Status { get; set; }
        public string GroupBy { get; set; }
    }
    public class ListFilterReturnReceivingModel : ListFilterCommonModel
    {
        public int? Item { get; set; }
        public string? ItemKeyword { get; set; }
        public int? Supplier { get; set; }
        public int? PurchaseOrder { get; set; }
        public new string Status { get; set; }

        public string GroupBy { get; set; }

    }

    public class ListFilterChequeModel : ListFilterCommonModel
    {
        public int? CustomerId { get; set; }
        public string? CustomerKeyword { get; set; }
        public int? StatusId { get; set; }
        public string? UserCreated { get; set; }

    }
    public class ListFilterCommonModel
    {
        DateTime? _DateFrom = null, _DateTo = null;

        public int? Status { get; set; }
        public string? StatusCode { get; set; }
        public string? SortColumn { get; set; }
        public string? SortColumnDirection { get; set; }
        public string? SearchValue { get; set; }
        public int? PageSize { get; set; }
        public int? PageSkip { get; set; }

        public DateTime? DateFrom
        {
            get
            {
                SetDates();
                return _DateFrom;
            }
            set
            {
                _DateFrom = value;
            }
        }
        public DateTime? DateTo
        {
            get
            {
                SetDates();
                return _DateTo;
            }
            set
            {
                _DateTo = value;
            }
        }

        private void SetDates()
        {

            if (_DateTo == null)
            {
                if (_DateFrom == null) { _DateTo = DateTime.Now; }
                else { _DateTo = _DateFrom.Value.AddMonths(1); }
            }
            if (_DateFrom == null)
            {
                _DateFrom = _DateTo.Value.AddMonths(-1);
            }
        }
    }
}
