using AutoMapper;
using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class ChequeRepository : GenericRepository<Cheque>, IChequeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public ChequeRepository(ApplicationDbContext context
                                 , IHttpContextAccessor httpContextAccessor
                                 , UserManager<User> userManager
                                 , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }


        public async Task AddAsync(ChequeVM chequeVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var status = await context.ChequeStatuses.Where(s => s.Id == chequeVM.StatusId).FirstOrDefaultAsync();

            var cheque = new Cheque
            {
                Id = chequeVM.Id,
                CollectDate = chequeVM.CollectDateD,
                RefNo = chequeVM.RefNo,
                CustomerId = chequeVM.CustomerId,
                CheckDate = chequeVM.CheckDateD,
                Check = chequeVM.Check,
                Branch = chequeVM.Branch,
                CheckNo = chequeVM.CheckNo,
                Amount = chequeVM.AmountD,
                StatusId = status?.Id
            };
            await context.Cheques.AddAsync(cheque);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<IList<ChequeVM>> GetAllChequeAsync(ListFilterChequeModel filter)
        {
            var cheques = from cheque in context.Cheques

                          join userc in userManager.Users on cheque.UserCreated equals userc.Id
                          join userm in userManager.Users on cheque.UserModified equals userm.Id into m
                          from um in m.DefaultIfEmpty()

                          where (filter.CustomerId == null || cheque.CustomerId == filter.CustomerId)
                           && (filter.StatusId == null || cheque.StatusId == filter.StatusId)
                           && (filter.UserCreated == null || cheque.UserCreated == filter.UserCreated)

                           && filter.DateFrom <= cheque.CheckDate.Value.Date && cheque.CheckDate.Value.Date <= filter.DateTo

                          select new ChequeVM
                          {
                              Id = cheque.Id,
                              CollectDateD = cheque.CollectDate,
                              RefNo = cheque.RefNo,
                              CustomerId = cheque.CustomerId,
                              CustomerCode = cheque.Customer.Code,
                              CustomerName = cheque.Customer.Name,
                              CheckDateD = cheque.CheckDate,
                              Check = cheque.Check,
                              Branch = cheque.Branch,
                              CheckNo = cheque.CheckNo,
                              AmountD = cheque.Amount,

                              StatusId = cheque.StatusId,
                              StatusCode = cheque.Status.Code,
                              StatusName = cheque.Status.Name,

                              DateCreatedD = cheque.DateCreated,
                              DateModifiedD = cheque.DateModified == null ? null : cheque.DateModified.Value,
                              UserCreated = userc.UserName,
                              UserModified = um == null ? "-" : um.UserName
                          };
            return await cheques.ToListAsync();
        }

        public async Task<ForDataTables<ChequeVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, ListFilterChequeModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllChequeAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.RefNo,
                    s.Check,
                    s.CustomerName,
                    s.Branch,
                    s.CollectDate,
                    s.CheckNo,
                    s.CheckDate
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "CollectDate" => isAsc ? data.OrderBy(s => s.CollectDateD) : data.OrderByDescending(s => s.CollectDateD),
                    "CheckDate" => isAsc ? data.OrderBy(s => s.CheckDateD) : data.OrderByDescending(s => s.CheckDateD),
                    "Amount" => isAsc ? data.OrderBy(s => s.AmountD) : data.OrderByDescending(s => s.AmountD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ChequeVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task UpdateAsync(ChequeVM chequeVM)
        {
            var cheque = context.Cheques.Where(s => s.Id == chequeVM.Id).FirstOrDefault();
            if (cheque == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            cheque.CollectDate = chequeVM.CollectDateD;
            cheque.RefNo = chequeVM.RefNo;
            cheque.CustomerId = chequeVM.CustomerId;
            cheque.CheckDate = chequeVM.CheckDateD;
            cheque.Check = chequeVM.Check;
            cheque.Branch = chequeVM.Branch;
            cheque.CheckNo = chequeVM.CheckNo;
            cheque.Amount = chequeVM.AmountD;
            cheque.StatusId = chequeVM.StatusId;

            context.Cheques.Update(cheque);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<bool> UploadCombinationExistAsync(ChequeVM chequeVM)
        {
            return (await GetListAsync()).Any(s =>
            s.CollectDate == chequeVM.CollectDate &&
            s.RefNo == chequeVM.RefNo &&
            s.CustomerName == chequeVM.CustomerName &&
            s.CheckDate == chequeVM.CheckDate &&
            s.Check == chequeVM.Check &&
            s.Branch == chequeVM.Branch &&
            s.CheckNo == chequeVM.CheckNo &&
            s.Amount == chequeVM.Amount &&
            s.StatusCode == chequeVM.StatusCode);
        }

        public async Task<IList<ChequeVM>> GetListAsync()
        {
            var chequeVM = from cheque in context.Cheques
                                                   .Include(s => s.Customer)
                           select new ChequeVM
                           {
                               CollectDateD = cheque.CollectDate,
                               RefNo = cheque.RefNo,
                               CustomerName = cheque.Customer.Name,
                               CheckDateD = cheque.CheckDate,
                               Check = cheque.Check,
                               Branch = cheque.Branch,
                               CheckNo = cheque.CheckNo,
                               AmountD = cheque.Amount,
                               StatusCode = cheque.Status.Code
                           };
            return await chequeVM.ToListAsync();
        }

        public async Task<bool> IsRefNoExistAsync(string refNo)
        {
            return await context.Cheques.AnyAsync(s => s.RefNo == refNo);
        }

        public async Task<ChequePVM> PrintCheque(ListFilterChequeModel filter)
        {
            var items = await GetAllChequeAsync(filter);
            return new ChequePVM
            {
                Cheques = items
            };
        }

        public async Task<ResponseModelData<FileVM>> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return new ResponseModelData<FileVM>
                    {
                        Status = "Error",
                        Message = "Invalid file",
                        Data = new FileVM
                        {
                            FileName = $"{DateTime.Now.ToString("yyMMddHHmm")} Item Upload Processed --- {file.FileName}",
                            Content = []
                        }
                    };

                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheets.First();

                var headers = worksheet.Row(1).Cells().Select(c => c.GetString().Trim()).ToList();
                var requiredHeaders = new[] { "CollectDate", "RefNo", "CustomerName", "CheckDate", "Check", "Branch", "CheckNo", "Amount", "Status" };

                var missingColumns = new List<string>();
                foreach (var col in requiredHeaders)
                {
                    if (!headers.Contains(col))
                        missingColumns.Add(col);
                }
                if (missingColumns.Count > 0)
                    return new ResponseModelData<FileVM>
                    {
                        Status = "Error",
                        Message = "Missing column(s): " + string.Join(", ", missingColumns),
                        Data = new FileVM
                        {
                            FileName = $"{DateTime.Now.ToString("yyMMddHHmm")} Item Upload Processed --- {file.FileName}",
                            Content = []
                        }
                    };

                int collectDateCol = headers.IndexOf("CollectDate") + 1;
                int refNoCol = headers.IndexOf("RefNo") + 1;
                int customerCol = headers.IndexOf("CustomerName") + 1;
                int checkDateCol = headers.IndexOf("CheckDate") + 1;
                int checkCol = headers.IndexOf("Check") + 1;
                int branchCol = headers.IndexOf("Branch") + 1;
                int checkNoCol = headers.IndexOf("CheckNo") + 1;
                int amountCol = headers.IndexOf("Amount") + 1;
                int statusCol = headers.IndexOf("Status") + 1;

                var xclitems = new List<ChequeVM>();
                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var xclitem = new ChequeVM
                    {
                        CollectDateD = row.Cell(collectDateCol).GetDateTime(),
                        RefNo = row.Cell(refNoCol).GetString(),
                        CustomerName = row.Cell(customerCol).GetString(),
                        CheckDateD = string.IsNullOrWhiteSpace(row.Cell(checkDateCol).GetString()) ? DateTime.MinValue : row.Cell(checkDateCol).GetDateTime(),
                        Check = row.Cell(checkCol).GetString(),
                        Branch = row.Cell(branchCol).GetString(),
                        CheckNo = row.Cell(checkNoCol).GetString(),
                        AmountD = decimal.TryParse(row.Cell(amountCol).GetString().Replace(",", ""), out decimal amount) ? amount : -999999,
                        StatusCode = row.Cell(statusCol).GetString()
                    };

                    xclitem.ActionStatus = await SaveChequeFromFile(xclitem);

                    xclitems.Add(xclitem);
                }

                using var newWorkbook = new XLWorkbook();
                var newSheet = newWorkbook.AddWorksheet("Result");

                newSheet.Range("A1:J1").Merge();
                newSheet.Cell(1, 1).MergedRange().Value = "This file is a result of item upload. Don't use this file for uploading";

                newSheet.Cell(2, 1).Value = "CollectDate";
                newSheet.Cell(2, 2).Value = "RefNo";
                newSheet.Cell(2, 3).Value = "CustomerName";
                newSheet.Cell(2, 4).Value = "CheckDate";
                newSheet.Cell(2, 5).Value = "Check";
                newSheet.Cell(2, 6).Value = "Branch";
                newSheet.Cell(2, 7).Value = "CheckNo";
                newSheet.Cell(2, 8).Value = "Amount";
                newSheet.Cell(2, 9).Value = "StatusCode";
                newSheet.Cell(2, 10).Value = "Status for Uploading";

                for (int i = 0; i < xclitems.Count; i++)
                {
                    var p = xclitems[i];
                    newSheet.Cell(i + 3, 1).Value = p.CollectDate;
                    newSheet.Cell(i + 3, 2).Value = p.RefNo;
                    newSheet.Cell(i + 3, 3).Value = p.CustomerName;
                    newSheet.Cell(i + 3, 4).Value = p.CheckDate;
                    newSheet.Cell(i + 3, 5).Value = p.Check;
                    newSheet.Cell(i + 3, 6).Value = p.Branch;
                    newSheet.Cell(i + 3, 7).Value = p.CheckNo;
                    newSheet.Cell(i + 3, 8).Value = p.Amount;
                    newSheet.Cell(i + 3, 9).Value = p.StatusCode;
                    newSheet.Cell(i + 3, 10).Value = p.ActionStatus;
                }

                using var outStream = new MemoryStream();
                newWorkbook.SaveAs(outStream);
                outStream.Position = 0;

                return new ResponseModelData<FileVM>
                {
                    Status = "Success",
                    Message = "The Excel file has been processed. Please check the downloaded file to see the status of the results.",
                    Data = new FileVM
                    {
                        FileName = $"{DateTime.Now.ToString("yyMMddHHmm")} Cheque Upload Processed --- {file.FileName}",
                        Content = outStream.ToArray()
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseModelData<FileVM> { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<string> SaveChequeFromFile(ChequeVM cheque)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cheque.RefNo)) return "Reference Number is required";
                if (cheque.CheckDateD == DateTime.MinValue) return "Check Date is required";
                if (string.IsNullOrWhiteSpace(cheque.Check)) return "Check is required";
                if (string.IsNullOrWhiteSpace(cheque.StatusCode)) return "Status is required";

                var customer = await context.Customers
                                                .Where(s => s.Name.Trim().ToLower() == (cheque.CustomerName).Trim().ToLower())
                                                .FirstOrDefaultAsync();
                if (customer == null) return $"Invalid Customer: {cheque.CustomerName} not found";

                var status = await context.ChequeStatuses
                                            .Where(s => s.Code.Trim().ToLower() == cheque.StatusCode.Trim().ToLower()
                                                     || s.Name.Trim().ToLower() == cheque.StatusCode.Trim().ToLower())
                                            .FirstOrDefaultAsync();
                if (status == null) return $"Invalid Status: {cheque.StatusCode} not found";

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                var msg = new List<string>();

                var refno = SaveChequeFromFileTruncate(cheque.RefNo, 10, msg, "Reference Number", false);
                var check = SaveChequeFromFileTruncate(cheque.Check, 10, msg, "Check");
                var branch = SaveChequeFromFileTruncate(cheque.Branch, 50, msg, "Branch");
                var checkNo = SaveChequeFromFileTruncate(cheque.CheckNo, 10, msg, "CheckNo", false);

                var chequeRecord = await context.Cheques
                                                .Where(s => s.RefNo == refno
                                                         && s.CheckNo == checkNo)
                                                .FirstOrDefaultAsync();
                if (chequeRecord != null)
                {
                    chequeRecord.CollectDate = cheque.CollectDateD;
                    chequeRecord.CustomerId = customer.Id;
                    chequeRecord.CheckDate = cheque.CheckDateD;
                    chequeRecord.Check = check;
                    chequeRecord.Branch = branch;
                    chequeRecord.CheckNo = checkNo;
                    chequeRecord.Amount = cheque.AmountD;
                    chequeRecord.StatusId = status.Id;

                    context.Cheques.Update(chequeRecord);
                    msg.Add("Cheque updated");
                    await context.SaveChangesAsync(currentUserId);
                }
                else
                {
                    chequeRecord = new GMGInventory.Data.Cheque
                    {
                        CollectDate = cheque.CollectDateD,
                        RefNo = refno,
                        CustomerId = customer.Id,
                        CheckDate = cheque.CheckDateD,
                        Check = check,
                        Branch = branch,
                        CheckNo = checkNo,
                        Amount = cheque.AmountD,
                        StatusId = status.Id
                    };

                    await context.Cheques.AddAsync(chequeRecord);
                    msg.Add("Cheque added");
                    await context.SaveChangesAsync(currentUserId);
                }
                return string.Join("; ", msg);
            }
            catch (Exception ex)
            {
                return SystemFormat.GetInnermostExceptionMessage(ex);
            }
        }

        private string SaveChequeFromFileTruncate(string? value, int maxLength, List<string> messages, string fieldName, bool allowNull = true)
        {
            if (string.IsNullOrWhiteSpace(value)) return allowNull ? null! : "";
            if (value.Length <= maxLength) return value;
            messages?.Add($"{fieldName ?? "Field"} truncated");
            return value.Substring(0, maxLength);
        }
    }
}
