using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.Rendering;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SoacrController(IAccessRepository accessRepository
                               , ISoacrRepository soaRepository
                               , ISoacrLineItemRepository soacrLineItemRepository) : BaseController(accessRepository)
    {
        #region for SOACR/Index
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SOACR)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.SOACRListDT(form));
        }

        public async Task<IActionResult> GenerateCR()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateSOAJson(GenerateSOAVM soaVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();
            var soaId = await soaRepository.GenerateSOAAsync(soaVM);
            if (soaId == 0)
            {
                responseModel.Status = "Error";
                responseModel.Message = "No available invoice within selected combination";
            }
            else
            {
                responseModel.Status = "Success";
                responseModel.Message = "SOA successfully added";
                responseModel.NewRow = soaId;
            }
            return Json(responseModel);
        }
        #endregion

        #region for SOACR/Invoice
        public async Task<IActionResult> Invoice()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SOACR)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetInvoiceJson(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetInvoiceListDTAsync(form, filter));
        }

        public async Task<IActionResult> PrintInvoice(ListFilterSalesModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await soaRepository.PrintInvoice(filter));
        }
        #endregion

        #region for SOACR/Generate CR
        public async Task<IActionResult> GetCustomerInvoiceToAddDT(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetCustomerInvoiceToAddDT(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GenerateCRJson(int CustomerId, int[] InvoiceIds)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();
            responseModel.Status = "Success";
            responseModel.Message = "Counter Receipt Successfully added";
            responseModel.NewRow = await soaRepository.GenerateCRAsync(CustomerId, InvoiceIds);

            TempData["SuccessMessage"] = responseModel.Message;

            return Json(responseModel);
        }
        #endregion

        #region for SOACR/Details
        public async Task<IActionResult> Details(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SOACR)).Convert();

            var soa = await soaRepository.GetSOACRDetails(id);
            if (soa == null)
            {
                return NotFound();
            }
            return View(soa);
        }

        [HttpPost]
        public async Task<JsonResult> GetSOACRItemsJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetSOACRItemsDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> EditInvoiceJson(SOACRItemsForm soaVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            try
            {
                await soaRepository.UpdateSOACRInvoiceAsync(soaVM);
                responseModel.Status = "Success";
                responseModel.Message = "Invoice successfully updated";
            }
            catch (Exception ex)
            {
                if (!await soaRepository.IsSOACRLineItemIdExist(soaVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Invoice cannot be found";
                }
                else
                {
                    responseModel.Status = "Error";
                    responseModel.Message = SystemFormat.GetInnermostExceptionMessage(ex);
                }
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInvoiceJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await soacrLineItemRepository.IsSoaLineItemIdExistAsync(id) == false)
            {
                responseModel.Status = "Error";
                responseModel.Message = "Invoice cannot be found";
            }
            else
            {
                await soacrLineItemRepository.DeleteAsync(id);
                responseModel.Status = "Success";
                responseModel.Message = "Invoice successfully deleted";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckCheckNumberJson(string CheckNumber)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await soaRepository.IsCheckNumberInUse(CheckNumber))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Check Number already in use";
            }
            else
            {
                responseModel.Status = "Success";
                responseModel.Message = "Check Number available";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> CollectorReceiptNumberJson(string CollectorReceiptNumber)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await soaRepository.IsCollectorReceiptNumberInUse(CollectorReceiptNumber))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Collector Receipt Number already in use";
            }
            else
            {
                responseModel.Status = "Success";
                responseModel.Message = "Collector Receipt Number available";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentDetailsJson(SOACRPaymentDetails payment)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();
            if (payment.CollectorReceiptNumber == null || payment.CollectorReceiptNumber.Length > 500)
            {
                responseModel.Status = "Warning";
                responseModel.Message = payment.CollectorReceiptNumber == null
                                      ? "Collector Receipt Number cannot be empty"
                                      : "Collector Receipt Number cannot exceed 50 characters";
            }
            else if (payment.Remarks != null && payment.Remarks.Length > 500)
            {
                responseModel.Status = "Warning";
                responseModel.Message = "Remarks cannot exceed 500 characters";
            }
            else if (await soaRepository.IsCollectorReceiptNumberInUse(payment.CollectorReceiptNumber))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Collector Receipt Number already in use";
            }
            else if (await soaRepository.AddPaymentDetails(payment))
            {
                responseModel.Status = "Success";
                responseModel.Message = "Payment added successfully";
            }
            else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Adding payment failed";
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> PrintCRDetails(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await soaRepository.PrintCRDetails(id));
        }

        public async Task<IActionResult> PrintSOADetails(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await soaRepository.PrintSOADetails(id));
        }

        public async Task<IActionResult> PDFCRDetails(int Id, string Company)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var details = await soaRepository.PrintCRDetails(Id);
            var document = new Document();

            var section = document.AddSection();

            section.PageSetup.TopMargin = Unit.FromCentimeter(5.25);
            section.PageSetup.HeaderDistance = Unit.FromCentimeter(2);
            var headerTable = section.Headers.Primary.AddTable();
            headerTable.AddColumn(Unit.FromCentimeter(4));
            headerTable.AddColumn(Unit.FromCentimeter(4));
            headerTable.AddColumn(Unit.FromCentimeter(4));
            headerTable.AddColumn(Unit.FromCentimeter(4));

            var headerRow = headerTable.AddRow();

            var titleCell = headerRow.Cells[0];
            titleCell.MergeRight = 2;

            var comp = Company == "BAS" ? "BINA'S AUTO SUPPLY" : "GMG AUTO PARTS EXPRESS";
            var titleParagraph = titleCell.AddParagraph(comp);
            titleParagraph.Format = new ParagraphFormat
            {
                Alignment = ParagraphAlignment.Left,
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 10, Bold = true }
            };

            var pageCell = headerRow.Cells[3];
            var pageParagraph = pageCell.AddParagraph();
            pageParagraph.AddText("Page ");
            pageParagraph.AddPageField();
            pageParagraph.AddText(" of ");
            pageParagraph.AddNumPagesField();
            pageParagraph.Format = new ParagraphFormat
            {
                Alignment = ParagraphAlignment.Right,
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 8 }
            };

            var subtitleRow = headerTable.AddRow();
            var subtitleCell = subtitleRow.Cells[0];
            subtitleCell.MergeRight = 3;
            var subtitleParagraph1 = subtitleCell.AddParagraph("Counter Receipt");
            subtitleParagraph1.Format = new ParagraphFormat
            {
                Alignment = ParagraphAlignment.Center,
                LineSpacingRule = LineSpacingRule.Single,
                Font = new Font { Size = 8, Bold = true }
            };
            var subtitleParagraph2 = subtitleCell.AddParagraph($"CR No.: {string.Format("{0:##00000}", details.SOACRNumber)}");
            subtitleParagraph2.Format = new ParagraphFormat
            {
                Alignment = ParagraphAlignment.Center,
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 8 }
            };

            var row1 = headerTable.AddRow();
            row1.Cells[0].AddParagraph("Customer Code:").Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Single,
                Font = new Font { Size = 8 }
            };
            row1.Cells[1].AddParagraph(details.CustomerCode).Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Single,
                Font = new Font { Size = 8, Bold = true }
            };
            row1.Cells[2].AddParagraph("Counter Receipt Date:").Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Single,
                Font = new Font { Size = 8 }
            };
            row1.Cells[3].AddParagraph(details.DateFrom).Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Single,
                Font = new Font { Size = 8, Bold = true }
            };

            var row2 = headerTable.AddRow();
            row2.Cells[0].AddParagraph("Customer Name:").Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 8 }
            };
            row2.Cells[1].AddParagraph(details.CustomerName).Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 8, Bold = true }
            };
            row2.Cells[2].AddParagraph("Date to be Collected:").Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 8 }
            };
            row2.Cells[3].AddParagraph(details.DateTo).Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 8, Bold = true }
            };

            var tableHeader = section.Headers.Primary.AddTable();
            tableHeader.Borders.Bottom.Width = 0.75;

            var columnWidth = new double[] { 3.0, 2.6, 2.3, 2.7, 2.7, 2.7 };

            foreach (var w in columnWidth)
            {
                tableHeader.AddColumn(Unit.FromCentimeter(w));
            }

            var header = tableHeader.AddRow();
            var columnHeader = new string[] { "Invoice Date", "Invoice No.", "Trans Cod", "Open Amount", "Freight Cost", "Total" };
            for (var chctr = 0; chctr < columnHeader.Length; chctr++)
            {
                var chcell = header.Cells[chctr].AddParagraph(columnHeader[chctr]);
                chcell.Format = new ParagraphFormat
                {
                    Alignment = chctr < 3 ? ParagraphAlignment.Center : ParagraphAlignment.Right,
                    Font = new Font { Size = 8, Bold = true }
                };
            }

            var table = section.AddTable();

            foreach (var w in columnWidth)
            {
                table.AddColumn(Unit.FromCentimeter(w));
            }

            foreach (var item in details.Contents)
            {

                var cellFormatCenter = new ParagraphFormat
                {
                    Alignment = ParagraphAlignment.Center,
                    LineSpacingRule = LineSpacingRule.OnePtFive,
                    Font = new Font { Size = 8 }
                };
                var cellFormatRight = new ParagraphFormat
                {
                    Alignment = ParagraphAlignment.Right,
                    LineSpacingRule = LineSpacingRule.OnePtFive,
                    Font = new Font { Size = 8 }
                };
                var row = table.AddRow();
                row.Cells[0].AddParagraph(item.InvoiceDate2).Format = cellFormatCenter.Clone();
                row.Cells[1].AddParagraph(item.ControlNumber).Format = cellFormatCenter.Clone();
                row.Cells[2].AddParagraph(item.InvoiceType).Format = cellFormatCenter.Clone();
                row.Cells[3].AddParagraph(item.InvoiceAmount).Format = cellFormatRight.Clone();
                row.Cells[4].AddParagraph(item.FreightCharge).Format = cellFormatRight.Clone();
                row.Cells[5].AddParagraph(item.Balance).Format = cellFormatRight.Clone();
            }

            var cellFormatRightBold = new ParagraphFormat
            {
                Alignment = ParagraphAlignment.Right,
                Font = new Font { Size = 8, Bold = true }
            };
            var footerRow = table.AddRow();
            footerRow.Borders.Top.Width = 0.75;
            footerRow.Cells[0].AddParagraph("").Format = cellFormatRightBold.Clone();
            footerRow.Cells[1].AddParagraph("").Format = cellFormatRightBold.Clone();
            footerRow.Cells[2].AddParagraph("Total:").Format = cellFormatRightBold.Clone();
            footerRow.Cells[3].AddParagraph(details.TotalInvoiceAmount).Format = cellFormatRightBold.Clone();
            footerRow.Cells[4].AddParagraph(details.TotalFreightCost).Format = cellFormatRightBold.Clone();
            footerRow.Cells[5].AddParagraph(details.TotalBalance).Format = cellFormatRightBold.Clone();

            //var footer = section.Footers.Primary.AddParagraph();
            //footer.AddText("This is the footer of the table");

            var renderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            renderer.RenderDocument();

            await using var stream = new MemoryStream();
            renderer.PdfDocument.Save(stream, false);
            stream.Position = 0;

            Response.Headers["Content-Disposition"] = "inline; filename=PDFCRDetails.pdf";
            return File(stream.ToArray(), "application/pdf");
        }

        public async Task<IActionResult> PDFSOADetails(int Id, string Company)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var details = await soaRepository.PrintSOADetails(Id);
            var document = new Document();

            var section = document.AddSection();

            section.PageSetup.TopMargin = Unit.FromCentimeter(5.25);
            section.PageSetup.HeaderDistance = Unit.FromCentimeter(2);
            var headerTable = section.Headers.Primary.AddTable();
            headerTable.AddColumn(Unit.FromCentimeter(4));
            headerTable.AddColumn(Unit.FromCentimeter(4));
            headerTable.AddColumn(Unit.FromCentimeter(4));
            headerTable.AddColumn(Unit.FromCentimeter(4));

            var headerRow = headerTable.AddRow();

            var titleCell = headerRow.Cells[0];
            titleCell.MergeRight = 2;

            var comp = Company == "BAS" ? "BINA'S AUTO SUPPLY" : "GMG AUTO PARTS EXPRESS";
            var titleParagraph = titleCell.AddParagraph(comp);
            titleParagraph.Format = new ParagraphFormat
            {
                Alignment = ParagraphAlignment.Left,
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 10, Bold = true }
            };

            var pageCell = headerRow.Cells[3];
            var pageParagraph = pageCell.AddParagraph();
            pageParagraph.AddText("Page ");
            pageParagraph.AddPageField();
            pageParagraph.AddText(" of ");
            pageParagraph.AddNumPagesField();
            pageParagraph.Format = new ParagraphFormat
            {
                Alignment = ParagraphAlignment.Right,
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 8 }
            };

            var subtitleRow = headerTable.AddRow();
            var subtitleCell = subtitleRow.Cells[0];
            subtitleCell.MergeRight = 3;
            var subtitleParagraph1 = subtitleCell.AddParagraph("Customer Statement of Accounts");
            subtitleParagraph1.Format = new ParagraphFormat
            {
                Alignment = ParagraphAlignment.Center,
                LineSpacingRule = LineSpacingRule.Single,
                Font = new Font { Size = 8, Bold = true }
            };
            var subtitleParagraph2 = subtitleCell.AddParagraph($"From: {details.DateFrom2} To: {details.DateTo2}, SOA No.: {string.Format("{0:##00000}", details.SOACRNumber)}, Agent: {details.AgentCode}");
            subtitleParagraph2.Format = new ParagraphFormat
            {
                Alignment = ParagraphAlignment.Center,
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 8 }
            };

            var row1 = headerTable.AddRow();
            row1.Cells[0].AddParagraph("Customer:").Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Single,
                Font = new Font { Size = 8 }
            };
            var row1Cell = row1.Cells[1];
            row1Cell.AddParagraph($"{details.CustomerCode} - {details.CustomerName}").Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Single,
                Font = new Font { Size = 8, Bold = true }
            };
            row1Cell.MergeRight = 2;

            var row2 = headerTable.AddRow();
            row2.Cells[0].AddParagraph("Terms:").Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 8 }
            };
            var row2Cell = row2.Cells[1];
            row2Cell.AddParagraph(details.CreditTerm).Format = new ParagraphFormat
            {
                LineSpacingRule = LineSpacingRule.Double,
                Font = new Font { Size = 8, Bold = true }
            };
            row2Cell.MergeRight = 2;

            var tableHeader = section.Headers.Primary.AddTable();
            tableHeader.Borders.Bottom.Width = 0.75;

            var columnWidth = new double[] { 2.5, 2.5, 1.0, 2.5, 2.5, 2.5, 2.5 };

            foreach (var w in columnWidth)
            {
                tableHeader.AddColumn(Unit.FromCentimeter(w));
            }

            var header = tableHeader.AddRow();
            var columnHeader = new string[] { "Invoice Date", "Invoice No.", "Type", "Trans Amount", "Waybill No", "Freight Cost", "Total" };
            for (var chctr = 0; chctr < columnHeader.Length; chctr++)
            {
                var chcell = header.Cells[chctr].AddParagraph(columnHeader[chctr]);
                chcell.Format = new ParagraphFormat
                {
                    Alignment = chctr < 3 || chctr == 4 ? ParagraphAlignment.Center : ParagraphAlignment.Right,
                    Font = new Font { Size = 8, Bold = true }
                };
            }

            var table = section.AddTable();

            foreach (var w in columnWidth)
            {
                table.AddColumn(Unit.FromCentimeter(w));
            }

            foreach (var item in details.Contents)
            {
                var cellFormatCenter = new ParagraphFormat
                {
                    Alignment = ParagraphAlignment.Center,
                    LineSpacingRule = LineSpacingRule.OnePtFive,
                    Font = new Font { Size = 8 }
                };
                var cellFormatRight = new ParagraphFormat
                {
                    Alignment = ParagraphAlignment.Right,
                    LineSpacingRule = LineSpacingRule.OnePtFive,
                    Font = new Font { Size = 8 }
                };
                var row = table.AddRow();
                row.Cells[0].AddParagraph(item.InvoiceDate2).Format = cellFormatCenter.Clone();
                row.Cells[1].AddParagraph(item.ControlNumber).Format = cellFormatCenter.Clone();
                row.Cells[2].AddParagraph(item.InvoiceType).Format = cellFormatCenter.Clone();
                row.Cells[3].AddParagraph(item.InvoiceAmount).Format = cellFormatRight.Clone();
                row.Cells[4].AddParagraph(item.FreightReferenceNumber + "").Format = cellFormatCenter.Clone();
                row.Cells[5].AddParagraph(item.Balance).Format = cellFormatRight.Clone();
                row.Cells[6].AddParagraph(item.Balance).Format = cellFormatRight.Clone();
            }
            var cellFormatRightBold = new ParagraphFormat
            {
                Alignment = ParagraphAlignment.Right,
                Font = new Font { Size = 8, Bold = true }
            };
            var footerRow = table.AddRow();
            footerRow.Borders.Top.Width = 0.75;
            footerRow.Cells[0].AddParagraph("").Format = cellFormatRightBold.Clone();
            footerRow.Cells[1].AddParagraph("").Format = cellFormatRightBold.Clone();
            footerRow.Cells[2].AddParagraph("Total:").Format = cellFormatRightBold.Clone();
            footerRow.Cells[3].AddParagraph(details.TotalInvoiceAmount).Format = cellFormatRightBold.Clone();
            footerRow.Cells[4].AddParagraph("").Format = cellFormatRightBold.Clone();
            footerRow.Cells[5].AddParagraph(details.TotalFreightCost).Format = cellFormatRightBold.Clone();
            footerRow.Cells[6].AddParagraph(details.TotalBalance).Format = cellFormatRightBold.Clone();

            //var footer = section.Footers.Primary.AddParagraph();
            //footer.AddText("This is the footer of the table");

            var renderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            renderer.RenderDocument();

            await using var stream = new MemoryStream();
            renderer.PdfDocument.Save(stream, false);
            stream.Position = 0;

            Response.Headers["Content-Disposition"] = "inline; filename=PDFSOADetails.pdf";
            return File(stream.ToArray(), "application/pdf");
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoices(int soaId, int[] invoiceIds)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            responseModel.Status = "Success";
            responseModel.Message = "Invoice Successfully Added";

            await soaRepository.AddInvoicesAsync(soaId, invoiceIds);
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetInvoiceTBR(string search, int page, int CustomerId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await soaRepository.GetInvoicesTBRAsync(search, page, CustomerId));
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoiceTBR(int soaLineItemId, int salesReturnId, decimal amount)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            await soaRepository.AddInvoiceTBRAsync(soaLineItemId, salesReturnId, amount);
            ResponseModel responseModel = new ResponseModel();
            responseModel.Status = "Success";
            responseModel.Message = "TBR Successfully Added";

            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetInvoiceTBRHistory(int soaLineItemId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SOACR, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await soaRepository.GetInvoiceTBRHistoryDTAsync(form, soaLineItemId));
        }
        #endregion








        //public async Task<IActionResult> AddInvoice(int id, int custId)
        //{
        //    var soaInv = await soaRepository.GetContractInvoicesCustomer2(id, custId);
        //    if (soaInv == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(soaInv);


        //}





        //[HttpPost]
        //public async Task<JsonResult> CreatePaymentJson(PaymentVM paymentVM)
        //{
        //    ResponseModel responseModel = new ResponseModel();
        //    await soaRepository.AddCustomerPaymentAsync(paymentVM);
        //    responseModel.Status = "Success";
        //    responseModel.Message = "Payment successfully added";
        //    return Json(responseModel);
        //}

        //[HttpPost]
        //public async Task<JsonResult> GetPaymentDetailsJson(SoacrPaymentFilter filter)
        //{
        //    return Json(await soaRepository.GetPaymentDetails(filter));
        //}

        //[HttpPost]
        //public async Task<JsonResult> GetPaymentChecksJson(int customerPaymentId)
        //{
        //    DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
        //    return Json(await soaRepository.GetPaymentChecksDTAsync(form, customerPaymentId));
        //}

        //[HttpPost]
        //public async Task<JsonResult> AddCheckJson(SoacrPaymentCheck check)
        //{
        //    ResponseModel<int> responseModel = new ResponseModel<int>();
        //    if (check.CheckAmountD == 0)
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Check Amount cannot be 0";
        //    }
        //    else if (await soaRepository.IsCheckNumberInUse(check.CheckNumber))
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Check Number already in use";
        //    }
        //    else if (await soaRepository.AddPaymentCheck(check))
        //    {
        //        responseModel.Status = "Success";
        //        responseModel.Message = "Add Check successfully";
        //    }
        //    else
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Add Check failed";
        //    }
        //    return Json(responseModel);
        //}

        //[HttpPost]
        //public async Task<JsonResult> RemoveCheckJson(int? soaCheckId)
        //{
        //    ResponseModel<int> responseModel = new ResponseModel<int>();
        //    if (soaCheckId == null)
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Please try again later";
        //    }
        //    else if (await soaRepository.RemovePaymentCheck(soaCheckId.Value))
        //    {
        //        responseModel.Status = "Success";
        //        responseModel.Message = "Remove Check successfully";
        //    }
        //    else
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Remove Check failed";
        //    }
        //    return Json(responseModel);
        //}

        //[HttpPost]
        //public async Task<JsonResult> GetList2Json(SoaFilterVM filter)
        //{
        //    DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
        //    return Json(await soaRepository.GetAllForDataTables2Async(form, filter));
        //}

        //public ActionResult ContractReceiptDetails(int id)
        //{
        //    var soa = soaRepository.GetInvoiceVM(id);
        //    if (soa == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(soa);
        //}

        //[HttpPost]
        //public async Task<JsonResult> GetSOAInvoice(int id)
        //{
        //    //return Json(await soaRepository.GetSoaDetailsAsync(id));
        //    return Json(null);
        //}

        ////public ActionResult EditInvoice(int id)
        ////{
        ////    var inv = soaRepository.GetSoaInvoiceDtlVM(id);
        ////    if (inv == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    return Json(inv);
        ////}

        //public ActionResult ContractReceipt()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public async Task<JsonResult> GetContractReceipt2Json(SoaFilterVM filter)
        //{
        //    DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
        //    return Json(await soaRepository.GetContractReceipt2DTAsync(form, filter));
        //}

        //[HttpPost]
        //public async Task<JsonResult> GetCustomerInvoicesJson(int id, int soaId)
        //{
        //    DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
        //    return Json(await soaRepository.GetContractReceiptCustomerDTAsync(form, id, soaId));
        //}


        //[HttpPost]
        //public async Task<JsonResult> GetContractReceiptDetailsJson(int id)
        //{
        //    DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
        //    return Json(await soaRepository.GetContractReceiptDetailsDTAsync(form, id));
        //}


        //[HttpPost]
        //public async Task<JsonResult> CreateJsonCR(SoacrVM soaVM, int[] invoiceIds)
        //{
        //    ResponseModel<int> responseModel = new ResponseModel<int>();
        //    if (await soaRepository.IsSOAIdExist(soaVM.Id))
        //    {
        //        responseModel.Status = "Error";
        //        responseModel.Message = "Counter Receipt Id already used.";
        //    }
        //    else
        //    {
        //        var soaId = await soaRepository.AddCrAsync(soaVM, invoiceIds);
        //        await soaRepository.AddCrAsync(soaVM, invoiceIds);
        //        responseModel.Status = "Success";
        //        responseModel.Message = "Counter Receipt Successfully added";
        //        responseModel.NewRow = soaId;
        //    }
        //    return Json(responseModel);
        //}
    }
}