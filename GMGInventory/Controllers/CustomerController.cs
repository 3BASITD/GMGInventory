using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO.Compression;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class CustomerController(IAccessRepository accessRepository
                                  , ICustomerRepository customerRepository
                                  , IAreaRepository areaRepository
                                  , IRegionRepository regionRepository
                                  , IAgentRepository agentRepository
                                  , ICustomerTypeRepository customertypeRepository
                                  , ICreditTermRepository credittermRepository) : BaseController(accessRepository)
        
    
    {
        private readonly ControllerHelper chelper = new();

        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await customerRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> GetByAgentOptions(string search, int page, int? agent)
        {
            return Json(await customerRepository.GetByAgentOptions(search, page, agent));
        }

        public async Task<IActionResult> GetOptionsWithOrder(string search, int page)
        {
            return Json(await customerRepository.GetOptionsWithOrder(search, page));
        }

        public async Task<IActionResult> GetOptions2(int? IncludeId)
        {
            return Json(await customerRepository.GetOptions2(IncludeId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Customer)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new(Request.Form);
            return Json(await customerRepository.GetListDataTables(dtForm));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(CustomerVM custVM)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await customerRepository.IsCodeExistAsync(custVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Customer code already in use";
            }
            else
            {
                await customerRepository.AddAsync(custVM);
                responseModel.Status = "Success";
                responseModel.Message = "Customer successfully added";
            }
            return Json(responseModel);
        }

        //[HttpPost]
        //public async Task<JsonResult> CreateForwarderJson(CustomerVM forwarderVM)
        //{
        //    ResponseModel responseModel = new ResponseModel();
        //    await customerRepository.AddForwarderAsync(forwarderVM);
        //    responseModel.Status = "Success";
        //    responseModel.Message = "Forwarder successfully added";

        //    return Json(responseModel);
        //}

        [HttpPost]
        public async Task<IActionResult> EditJson(CustomerVM custVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
            try
            {
                await customerRepository.UpdateAsync(custVM);
                responseModel.Status = "Success";
                responseModel.Message = "Customer successfully updated";
            }
            catch (Exception ex)
            {
                    responseModel.Status = "Error";
                    responseModel.Message = SystemFormat.GetInnermostExceptionMessage(ex);
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Customer)).Convert();

            var cust = customerRepository.GetCustomerDtlVM(id);
            if (cust == null)
            {
                return NotFound();
            }
            return View(cust);
        }

        public async Task<IActionResult> MoreDetails(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var cust = customerRepository.GetCustomerDtlVM(id); // gawa ka panibagong GetCustomerVM(id) na andun mga kailangan mo sa modal
            if (cust == null)
            {
                return NotFound();
            }
            return Json(cust);
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await customerRepository.GetCustomerDetailsDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetShippingJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await customerRepository.GetCustomerShippingDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetDiscountJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await customerRepository.GetCustomerBrandDiscountDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> SaveShippingJson(CustomerShippingVM shippingVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await customerRepository.SaveShippingAsync(shippingVM);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveDiscountJson(CustomerBrandDiscountVM brandDiscountVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var response = await customerRepository.SaveDiscountAsync(brandDiscountVM);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SetDefaultShippingJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var response = await customerRepository.SetDefaultShippingAsync(Id);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrDeactivateShippingJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var response = await customerRepository.DeleteOrDeactivateShippingAsync(Id);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrDeactivateDiscountJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var response = await customerRepository.DeleteOrDeactivateDiscountAsync(Id);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ReactivateShippingJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var response = await customerRepository.ReactivateShippingAsync(Id);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ReactivateDiscountJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var response = await customerRepository.ReactivateDiscountAsync(Id);

            return Json(response);
        }

        //[HttpPost]
        //public async Task<JsonResult> GetForwarderJson(int id)
        //{
        //    DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
        //    return Json(await customerRepository.GetCustomerForwarderDTAsync(form, id));
        //}

        [HttpPost]
        public async Task<IActionResult> GetPaymentJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await customerRepository.GetCustomerPaymentDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetReceivableJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await customerRepository.GetCustomerReceivableDTAsync(form, id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;
            {
                await customerRepository.DeactivateCustomerAsync(id);
                TempData["WarningMessage"] = "Customer has been deactivated";
            }

            return RedirectToAction(nameof(Index));

            //var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Delete);
            //if (redirectResult != null) return redirectResult;

            //{
            //    await customerRepository.DeactivateCustomerAsync(id);
            //    TempData["WarningMessage"] = "Customer has been deactivated";
            //} else
            //{
            //    await customerRepository.DeleteAsync(id);
            //    TempData["WarningMessage"] = "Customer has been deleted";
            //}

            //return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            await customerRepository.ReactivateCustomerAsync(id);
            TempData["SuccessMessage"] = "Customer reactivated successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadFile()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;
/*
            DataTable dt = new("Customer");
            dt.Columns.AddRange([
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Commission Rate"),
                new DataColumn("Region"),
                new DataColumn("Area"),
                new DataColumn("Customer Type"),
                //new DataColumn("Address"),
                //new DataColumn("Contact Person"),
                //new DataColumn("Contact Number"),
                //new DataColumn("Fax Number"),
                new DataColumn("Email"),
                new DataColumn("Agent"),
                new DataColumn("TIN"),
                new DataColumn("VAT"),
                new DataColumn("Credit Term"),
                new DataColumn("Credit Limit"),
                new DataColumn("Remarks")
            ]);
            var customer = (await customerRepository.GetAllCustomerAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var customers in customer)
            {
                dt.Rows.Add(customers.Code, customers.Name, customers.CommissionRateD,
                    customers.RegionName, customers.AreaName, customers.CustomerTypeName,
                    //customers.Address, customers.ContactPerson, customers.ContactNumber, customers.FaxNumber,
                    customers.Email,customers.AgentName, customers.TIN, customers.VAT, customers.CreditTermName,
                    customers.CreditLimit, customers.Remarks);
            }
            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            DateTime.Now.ToString("yyyyMMdd") + " Customers.xlsx");
            */

            // Base structure
            DataTable CreateTable()
            {
                DataTable dt = new("Customer");
                dt.Columns.AddRange([
                    new DataColumn("Code"),
                    new DataColumn("Name"),
                    new DataColumn("Commission Rate"),
                    new DataColumn("Region"),
                    new DataColumn("Area"),
                    new DataColumn("Customer Type"),
                    new DataColumn("Email"),
                    new DataColumn("Agent"),
                    new DataColumn("TIN"),
                    new DataColumn("VAT"),
                    new DataColumn("Credit Term"),
                    new DataColumn("Credit Limit"),
                    new DataColumn("Remarks")
                ]);
                return dt;
            }

            var customers = await customerRepository.GetAllCustomerAsync();

            var activeCustomers = customers.Where(s => s.IsActive).ToList();
            var inactiveCustomers = customers.Where(s => !s.IsActive).ToList();

            byte[] GenerateExcel(List<CustomerVM> list, bool isInactive = false)
            {
                var dt = CreateTable();

                foreach (var c in list)
                {
                    dt.Rows.Add(
                        c.Code, c.Name, c.CommissionRateD,
                        c.RegionName, c.AreaName, c.CustomerTypeName,
                        c.Email, c.AgentName, c.TIN, c.VAT,
                        c.CreditTermName, c.CreditLimit, c.Remarks
                    );
                }

                using var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Customers");

                int startRow = 1;

                if (isInactive)
                {
                    ws.Cell(1, 1).Value = "⚠ NOTE: This file is for reference only. Uploading this will activate inactive customers.";
                    ws.Range(1, 1, 1, dt.Columns.Count).Merge();

                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Cell(1, 1).Style.Font.FontColor = XLColor.Red;
                    ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    startRow = 2;
                }

                ws.Cell(startRow, 1).InsertTable(dt);

                using var stream = new MemoryStream();
                wb.SaveAs(stream);
                return stream.ToArray();
            }

            var activeExcel = GenerateExcel(activeCustomers);
            var inactiveExcel = GenerateExcel(inactiveCustomers, true);

            using var zipStream = new MemoryStream();
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                var activeEntry = archive.CreateEntry("Active Customers.xlsx");
                using (var entryStream = activeEntry.Open())
                {
                    await entryStream.WriteAsync(activeExcel, 0, activeExcel.Length);
                }

                var inactiveEntry = archive.CreateEntry("Inactive Customers.xlsx");
                using (var entryStream = inactiveEntry.Open())
                {
                    await entryStream.WriteAsync(inactiveExcel, 0, inactiveExcel.Length);
                }
            }

            return File(
                zipStream.ToArray(),
                "application/zip",
                DateTime.Now.ToString("yyyyMMdd") + " Customers.zip"
            );
        }

        public async Task<IActionResult> DownloadDiscountFile(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Customer Brand Discount");
            dt.Columns.AddRange([


                new DataColumn(chelper.DisplayName<CustomerBrandDiscountDowloadVM>("BrandName")),
                new DataColumn(chelper.DisplayName<CustomerBrandDiscountDowloadVM>("Discount")),
                new DataColumn(chelper.DisplayName<CustomerBrandDiscountDowloadVM>("CreatedBy")),
                new DataColumn(chelper.DisplayName<CustomerBrandDiscountDowloadVM>("DateCreated")),
                new DataColumn(chelper.DisplayName<CustomerBrandDiscountDowloadVM>("Status"))
            ]);
            var cbds = await customerRepository.GetCustomerBrandDiscountDownload(id);
            foreach (var cbd in cbds)
            {
                dt.Rows.Add(
                            cbd.BrandName,
                            cbd.DiscountRate,
                            cbd.CreatedBy,
                            cbd.DateCreated,
                            cbd.Status

                            );
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") +" "+cbds.FirstOrDefault().CustomerName +" BrandDiscount.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> UploadJson(CustomerVM customerVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
            //if (await customerRepository.UploadCombinationExistAsync(customerVM))
            //{
            //    responseModel.Status = "Error";
            //    responseModel.Message = "Already exist";
            //}
            //else
            if (await customerRepository.IsCodeExistAsync(customerVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Code already in use";
            }
            else
            {
                int? AreaId = await areaRepository.GetIdAsync(customerVM.AreaName);
                int? RegionId = await regionRepository.GetIdAsync(customerVM.RegionName);
                int? AgentId = await agentRepository.GetIdAsync(customerVM.AgentName);
                int? CustomerTypeId = await customertypeRepository.GetIdAsync(customerVM.CustomerTypeName);
                int? CreditTermId = await credittermRepository.GetIdAsync(customerVM.CreditTermName);

                if (AreaId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Area";
                }
                else if (RegionId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Region";
                }
                else if (AgentId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Agent";
                }
                else if (CustomerTypeId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Customer Type";
                }
                else if (CreditTermId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Credit Term";
                }
                else
                {
                    customerVM.AreaId = AreaId.Value;
                    customerVM.RegionId = RegionId.Value;
                    customerVM.AgentId = AgentId.Value;
                    customerVM.CustomerTypeId = CustomerTypeId.Value;
                    customerVM.CreditTermId = CreditTermId.Value;
                    await customerRepository.AddAsync(customerVM);
                    responseModel.Status = "Success";
                    responseModel.Message = "Customer added";
                }
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileJson(IFormFile file)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Customer, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var response = await customerRepository.UploadFile(file);

            if (response.Data == null)
            {
                return Json(response);
            }
            else
            {
                Response.Headers["X-Status"] = response.Status;
                Response.Headers["X-Message"] = response.Message;
                return File(response.Data.Content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            response.Data.FileName);
            }
        }
    }
}