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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CategoryRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Categories
                .Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = (s.Code == s.Name ? "" : "(" + s.Code + ") ") + s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }

        public async Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page)
        {
            var categories = await context.Categories
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
            var categoriesFiltered = categories.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonNameOptionsModel>
            {
                //incomplete_results = false,
                items = categoriesFiltered,
                total_count = categories.Count
            };
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var _name = (Name + "").ToLower().Replace("\n", "").Replace("\r", "").Trim();
            var name = _name.Length > 100 ? _name[..100] : _name;
            var code = _name.Length > 20 ? _name[..20] : _name;
            var category = await context.Categories
                                        .Where(s => s.Name.ToLower().Replace("\n", "").Replace("\r", "").Trim() == name
                                                    || s.Code.ToLower().Replace("\n", "").Replace("\r", "").Trim() == code)
                                        .FirstOrDefaultAsync();
            return category?.Id;
        }



        public async Task<IList<CategoryVM>> GetAllCategoryAsync()
        {

            var categories = from category in context.Categories

                             join userc in userManager.Users on category.UserCreated equals userc.Id
                             join userm in userManager.Users on category.UserModified equals userm.Id into m
                             from userm in m.DefaultIfEmpty()

                             select new CategoryVM
                             {
                                 Id = category.Id,
                                 Name = category.Name,
                                 Code = category.Code,
                                 IsActive = category.IsActive,
                                 DateCreatedD = category.DateCreated,
                                 DateModifiedD = category.DateModified == null ? null : category.DateModified.Value,
                                 UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                                 UserModified = userm == null ? "-" : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname)
                             };
            return await categories.ToListAsync();
        }

        public async Task<ForDataTables<CategoryVM>> GetAllForDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllCategoryAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Code,
                    s.Name,
                    s.UserCreated,
                    s.DateCreated,
                    s.UserModified,
                    s.DateModified
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
                    "DateCreated" => isAsc ? data.OrderBy(s => s.DateCreatedD) : data.OrderByDescending(s => s.DateCreatedD),
                    "DateModified" => isAsc ? data.OrderBy(s => s.DateModifiedD) : data.OrderByDescending(s => s.DateModifiedD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<CategoryVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }


        public async Task<IList<GenericAuditVM>> GetAllGenericVMAsync()
        {
            var categories = await context.Categories.ToListAsync();
            var categoriesVM = mapper.Map<List<GenericAuditVM>>(categories);
            foreach (GenericAuditVM cvm in categoriesVM)
            {
                cvm.UserCreated = await GetNickname(cvm.UserCreated);
                cvm.UserModified = await GetNickname(cvm.UserModified);
            }
            return categoriesVM;
        }

        public async Task<IList<APIGeneric>> GetListAPI()
        {
            var categories = from category in context.Categories
                             where category.IsActive == true
                             select new APIGeneric
                             {
                                 Id = category.Id,
                                 Code = category.Code,
                                 Name = category.Name
                             };

            return await categories.ToListAsync();
        }

        public async Task<GenericAuditVM?> GetGenericVMAsync(int? id)
        {
            var category = await context.Categories.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return null;
            }
            var categoryVM = mapper.Map<GenericAuditVM>(category);
            categoryVM.UserCreated = await GetNickname(categoryVM.UserCreated);
            categoryVM.UserModified = await GetNickname(categoryVM.UserModified);
            return categoryVM;
        }

        public async Task<CategoryVM?> GetCategoryVMAsync(int? id)
        {
            var category = await context.Categories.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return null;
            }
            var categoryVM = mapper.Map<CategoryVM>(category);
            return categoryVM;
        }

        public async Task<IList<Category>> GetAllActiveCategoryAsync(int includeId)
        {
            return await context.Categories.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsCategoryCodeExist(string code)
        {
            return await context.Categories.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsCategoryInUse(int id)
        {
            return await context.Items.AnyAsync(s => s.CategoryId == id);
        }


        public async Task AddAsync(CategoryVM categoryVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var category = new Category
            {
                Code = categoryVM.Code,
                Name = categoryVM.Name,
                IsActive = true

            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(CategoryVM categoryVM)
        {
            var category = context.Categories.Where(s => s.Id == categoryVM.Id).FirstOrDefault();
            if (category == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            category.Id = categoryVM.Id;

            category.Code = categoryVM.Code;
            category.Name = categoryVM.Name;
            category.IsActive = true;
            context.Categories.Update(category);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<bool> UploadCombinationExistAsync(CategoryVM categoryVM)
        {
            var _code = categoryVM.Code.ToLower().Replace("\r", "").Replace("\n", "").Trim();
            var code = _code.Length > 20 ? _code[..20] : _code;
            var _name = categoryVM.Name.ToLower().Replace("\r", "").Replace("\n", "").Trim();
            var name = _name.Length > 100 ? _name[..100] : _name;

            return (await GetAllGenericVMAsync()).Any(s => s.Code.ToLower().Replace("\r", "").Replace("\n", "").Trim() == code
                                                        && s.Name.ToLower().Replace("\r", "").Replace("\n", "").Trim() == name);
        }


        public async Task<bool> IsCodeExist(string Code)
        {
            var _code = Code.ToLower().Replace("\r", "").Replace("\n", "").Trim();
            var code = _code.Length > 20 ? _code[..20] : _code;

            return await context.Categories.AnyAsync(c => c.Code.ToLower().Replace("\r", "").Replace("\n", "").Trim() == code);
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

                var xclitems = new List<CategoryFileVM>();
                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var xclitem = new CategoryFileVM
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
                        FileName = $"{DateTime.Now.ToString("yyMMddHHmm")} Category Upload Processed --- {file.FileName}",
                        Content = outStream.ToArray()
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseModelData<FileVM> { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<string> SaveItemFromFile(CategoryFileVM categoryVM)
        {
            try
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                if (string.IsNullOrWhiteSpace(categoryVM.Code)) return "Category Code is required";
                if (string.IsNullOrWhiteSpace(categoryVM.Name)) return "Category Name is required";

                var _code = categoryVM.Code.ToLower().Replace("\r", "").Replace("\n", "").Trim();
                var code = _code.Length > 20 ? _code[..20] : _code;
                var _name = categoryVM.Name.ToLower().Replace("\r", "").Replace("\n", "").Trim();
                var name = _name.Length > 100 ? _name[..100] : _name;

                var category = await context.Categories.Where(s => s.Code.Trim().ToLower() == code.ToLower()).FirstOrDefaultAsync();
                if (category != null)
                {
                    category.Name = name;
                    category.IsActive = true;

                    context.Categories.Update(category);
                    await context.SaveChangesAsync(currentUserId);
                    return "Category updated";
                }
                else
                {
                    category = new Category
                    {
                        Code = code,
                        Name = name,
                        IsActive = true
                    };

                    await context.Categories.AddAsync(category);
                    await context.SaveChangesAsync(currentUserId);
                    return "Category added";
                }
            }
            catch (Exception ex)
            {
                return SystemFormat.GetInnermostExceptionMessage(ex);
            }
        }
    }
}