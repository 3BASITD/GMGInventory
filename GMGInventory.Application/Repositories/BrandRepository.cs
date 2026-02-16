using AutoMapper;
using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public BrandRepository(ApplicationDbContext context
                             , IHttpContextAccessor httpContextAccessor
                             , UserManager<User> userManager
                             , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IList<JsonNameOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Brands
                                .Where(s => s.IsActive || s.Id == IncludeId)
                                .Select(s => new JsonNameOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Name = s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page)
        {
            var areas = await context.Brands
                                     .Where(s => s.IsActive
                                              && (string.IsNullOrWhiteSpace(search)
                                              || (s.Code.ToLower().Contains(search.ToLower()))
                                              || (s.Name.ToLower().Contains(search.ToLower()))))
                                     .Select(s => new JsonNameOptionsModel
                                     {
                                         Id = s.Id.ToString(),
                                         Text = s.Code,
                                         Name = s.Name
                                     })
                                     .OrderBy(s => s.Text)
                                     .ToListAsync();
            var areasFiltered = areas.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonNameOptionsModel>
            {
                //incomplete_results = false,
                items = areasFiltered,
                total_count = areas.Count
            };
        }

        public async Task<IList<BrandViewVM>> GetListAsync()
        {
            var brands = from brand in context.Brands
                         join userc in userManager.Users on brand.UserCreated equals userc.Id
                         join userm in userManager.Users on brand.UserModified equals userm.Id into m
                         from userm in m.DefaultIfEmpty()
                         select new BrandViewVM
                         {
                             Id = brand.Id,
                             Code = brand.Code,
                             DateCreatedD = brand.DateCreated,
                             DateModifiedD = brand.DateModified,
                             IsActive = brand.IsActive,
                             Name = brand.Name,
                             UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                             UserModified = userm == null ? "-" : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname)
                         };
            return await brands.ToListAsync();
        }

        public async Task<ForDataTables<BrandViewVM>> GetListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Name.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.UserCreated.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.DateCreated.ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.UserModified + "").ToLower().Contains(form.SearchValue.ToLower())
                                    || (x.DateModified + "").ToLower().Contains(form.SearchValue.ToLower()));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "UserCreated": data = data.OrderBy(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderBy(s => s.DateCreatedD); break;
                        case "UserModified": data = data.OrderBy(s => s.UserModified); break;
                        case "DateModified": data = data.OrderBy(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "UserCreated": data = data.OrderByDescending(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateModified); break;
                        case "UserModified": data = data.OrderByDescending(s => s.UserModified); break;
                        case "DateModified": data = data.OrderByDescending(s => s.DateModified); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<BrandViewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<APIBrand>> GetListAPI()
        {
            var brands = from brand in context.Brands
                         where brand.IsActive == true
                         select new APIBrand
                         {
                             Id = brand.Id,
                             Code = brand.Code,
                             Name = brand.Name
                         };
            return await brands.ToListAsync();
        }

        public async Task<BrandViewVM?> GetBrandViewVM(int? id)
        {
            var brandVM = (await GetListAsync()).Where(s => s.Id == id).FirstOrDefault();
            if (brandVM == null)
            {
                return null;
            }
            return brandVM;
        }

        public async Task<bool> UploadCombinationExistAsync(BrandVM brandVM)
        {
            return (await GetListAsync()).Any(s => s.Code == brandVM.Code
                                                && s.Name == brandVM.Name);
        }

        public async Task<BrandVM?> GetBrandVMAsync(int? id)
        {
            var brand = await context.Brands
                                     .Where(s => s.Id == id)
                                     .FirstOrDefaultAsync();
            if (brand == null)
            {
                return null;
            }
            var brandViewVM = mapper.Map<BrandVM>(brand);
            return brandViewVM;
        }

        public async Task<IList<Brand>> GetAllActiveBrandAsync(int includeId)
        {
            return await context.Brands.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsBrandCodeExist(string code)
        {
            return await context.Brands.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsBrandInUse(int id)
        {
            return await context.Items.AnyAsync(s => s.BrandId == id);
        }

        public async Task<bool> IsBrandIdExistAsync(int id)
        {
            return await context.Brands.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateBrandAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var brand = await context.Brands.FirstAsync(s => s.Id == id);
            brand.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateBrandAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var brand = await context.Brands.FirstAsync(s => s.Id == id);
            brand.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(BrandVM brandVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            //var itemType = mapper.Map<ItemType>(itemTypeVM);
            var brand = new Brand
            {
                Code = brandVM.Code,
                Name = brandVM.Name,
                IsActive = true
            };
            await context.Brands.AddAsync(brand);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(BrandVM brandVM)
        {
            var brand = context.Brands.Where(s => s.Id == brandVM.Id).FirstOrDefault();
            if (brand == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            mapper.Map(brandVM, brand);
            context.Brands.Update(brand);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var brand = await context.Brands
                                     .Where(s => s.Name.ToLower().Replace("\n", "").Replace("\r", "").Trim() == (Name + "").ToLower().Replace("\n", "").Replace("\r", "").Trim())
                                     .FirstOrDefaultAsync();
            return brand?.Id;
        }

        public async Task<ResponseModelData<FileVM>> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return new ResponseModelData<FileVM> { Status = "Error", Message = "Invalid file" };

                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheets.First();

                var headers = worksheet.Row(1).Cells().Select(c => c.GetString().Trim()).ToList();
                var requiredHeaders = new[] { "Code", "Name" };

                var missingColumns = new List<string>();
                foreach (var col in requiredHeaders)
                {
                    if (!headers.Contains(col))
                        missingColumns.Add(col);
                }
                if (missingColumns.Count > 0)
                    return new ResponseModelData<FileVM> { Status = "Error", Message = "Missing column(s): " + string.Join(", ", missingColumns) };

                int codeCol = headers.IndexOf("Code") + 1;
                int nameCol = headers.IndexOf("Name") + 1;

                var xclitems = new List<BrandFileVM>();
                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var xclitem = new BrandFileVM
                    {
                        Code = row.Cell(codeCol).GetString(),
                        Name = row.Cell(nameCol).GetString()
                    };

                    xclitem.Status = await SaveItemFromFile(xclitem);

                    xclitems.Add(xclitem);
                }

                using var newWorkbook = new XLWorkbook();
                var newSheet = newWorkbook.AddWorksheet("Result");

                newSheet.Range("A1:C1").Merge();
                newSheet.Cell(1, 1).MergedRange().Value = "This file is a result of item upload. Don't use this file for uploading";

                newSheet.Cell(2, 1).Value = "Code";
                newSheet.Cell(2, 2).Value = "Name";
                newSheet.Cell(2, 3).Value = "Status";

                for (int i = 0; i < xclitems.Count; i++)
                {
                    var p = xclitems[i];
                    newSheet.Cell(i + 3, 1).Value = p.Code;
                    newSheet.Cell(i + 3, 2).Value = p.Name;
                    newSheet.Cell(i + 3, 3).Value = p.Status;
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
                        FileName = $"{DateTime.Now.ToString("yyMMddHHmm")} Brand Upload Processed --- {file.FileName}",
                        Content = outStream.ToArray()
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseModelData<FileVM> { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<string> SaveItemFromFile(BrandFileVM brandVM)
        {
            try
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                if (string.IsNullOrWhiteSpace(brandVM.Code)) return "Brand Code is required";
                if (string.IsNullOrWhiteSpace(brandVM.Name)) return "Brand Name is required";

                var _code = brandVM.Code.ToLower().Replace("\r", "").Replace("\n", "").Trim();
                var code = _code.Length > 10 ? _code[..10] : _code;
                var _name = brandVM.Name.ToLower().Replace("\r", "").Replace("\n", "").Trim();
                var name = _name.Length > 100 ? _name[..100] : _name;

                var brand = await context.Brands.Where(s => s.Code.ToLower() == code.ToLower()).FirstOrDefaultAsync();
                if (brand != null)
                {
                    brand.Name = name;
                    brand.IsActive = true;

                    context.Brands.Update(brand);
                    await context.SaveChangesAsync(currentUserId);
                    return "Brand updated";
                }
                else
                {
                    brand = new Brand
                    {
                        Code = code,
                        Name = name,
                        IsActive = true
                    };

                    await context.Brands.AddAsync(brand);
                    await context.SaveChangesAsync(currentUserId);
                    return "Brand added";
                }
            }
            catch (Exception ex)
            {
                return SystemFormat.GetInnermostExceptionMessage(ex);
            }
        }
    }
}