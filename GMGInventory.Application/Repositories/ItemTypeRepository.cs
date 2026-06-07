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
    public class ItemTypeRepository : GenericRepository<ItemType>, IItemTypeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly string currentUserId;

        public ItemTypeRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? CategoryId, int? IncludeId)
        {
            return await context.ItemTypes
                                .Where(s => s.Id == IncludeId
                                         || (s.IsActive
                                         && (s.CategoryId == null
                                         || s.CategoryId == CategoryId)))
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = (s.Code == s.Name ? "" : "(" + s.Code + ") ") + s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page, int? CategoryId)
        {
            var itemTypes = await context.ItemTypes
                                     .Where(s => (s.IsActive
                                              && (s.CategoryId == null
                                              || s.CategoryId == CategoryId))
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
            var itemTypesFiltered = itemTypes.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonNameOptionsModel>
            {
                //incomplete_results = false,
                items = itemTypesFiltered,
                total_count = itemTypes.Count
            };
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var itemType = await context.ItemTypes.Where(s => s.Name.ToLower().Replace("\n", "").Replace("\r", "").Trim() == (Name + "").ToLower().Replace("\n", "").Replace("\r", "").Trim()).FirstOrDefaultAsync();
            return itemType?.Id;
        }

        public async Task<IList<ItemTypeViewVM>> GetListAsync()
        {
            var itemTypes = from itemType in context.ItemTypes
                            join userc in userManager.Users on itemType.UserCreated equals userc.Id
                            join userm in userManager.Users on itemType.UserModified equals userm.Id into m
                            from userm in m.DefaultIfEmpty()
                            select new ItemTypeViewVM
                            {
                                Id = itemType.Id,
                                Code = itemType.Code,
                                CategoryId = itemType.Category == null ? null : itemType.Category.Id,
                                CategoryCode = itemType.Category == null ? null : itemType.Category.Code,
                                Category = itemType.Category == null ? null : itemType.Category.Name,
                                DateCreatedD = itemType.DateCreated,
                                DateModifiedD = itemType.DateModified,
                                IsActive = itemType.IsActive,
                                Name = itemType.Name,
                                UserCreated = userc.FirstName + " " + userc.LastName,
                                UserModified = userm == null ? null : (userm.FirstName + " " + userm.LastName)
                            };
            return await itemTypes.ToListAsync();
        }

        public async Task<ForDataTables<ItemTypeViewVM>> GetListDTAsync(DataTablesRequestForm form)
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
                                    || (x.Category + "").ToLower().Contains(form.SearchValue.ToLower())
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
                        case "Category": data = data.OrderBy(s => s.Category); break;
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
                        case "Category": data = data.OrderByDescending(s => s.Category); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.UserCreated); break;
                        case "Quantity": data = data.OrderByDescending(s => s.DateCreatedD); break;
                        case "RetailPrice": data = data.OrderByDescending(s => s.UserModified); break;
                        case "Size": data = data.OrderByDescending(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemTypeViewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<APIItemType>> GetListAPI()
        {
            var itemTypes = from itemType in context.ItemTypes
                            where itemType.IsActive == true
                            select new APIItemType
                            {
                                Id = itemType.Id,
                                Code = itemType.Code,
                                Name = itemType.Name,
                                Category = itemType.Category.Name
                            };
            return await itemTypes.ToListAsync();
        }

        public async Task<ItemTypeViewVM?> GetItemTypeViewVM(int? id)
        {
            var itemTypeVM = (await GetListAsync()).Where(s => s.Id == id).FirstOrDefault();
            if (itemTypeVM == null)
            {
                return null;
            }
            return itemTypeVM;
        }

        public async Task<bool> UploadCombinationExistAsync(ItemTypeVM itemTypeVM)
        {
            return (await GetListAsync()).Any(s => s.Code == itemTypeVM.Code
                                                && s.Name == itemTypeVM.Name);
        }

        public async Task<ItemTypeVM?> GetItemTypeVMAsync(int? id)
        {
            var itemType = await context.ItemTypes
                                        .Where(s => s.Id == id)
                                        .FirstOrDefaultAsync();
            if (itemType == null) return null;

            var itemTypeViewVM = mapper.Map<ItemTypeVM>(itemType);
            return itemTypeViewVM;
        }

        public async Task<IList<ItemType>> GetAllActiveItemTypeAsync(int includeId)
        {
            return await context.ItemTypes.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsCodeExist(string code)
        {
            return await context.ItemTypes.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsItemTypeInUse(int id)
        {
            return await context.Items.AnyAsync(s => s.ItemTypeId == id);
        }

        public async Task<bool> IsItemTypeIdExistAsync(int id)
        {
            return await context.ItemTypes.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateItemTypeAsync(int id)
        {
            var itemType = await context.ItemTypes.FirstAsync(s => s.Id == id);
            itemType.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateItemTypeAsync(int id)
        {
            var itemType = await context.ItemTypes.FirstAsync(s => s.Id == id);
            itemType.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(ItemTypeVM itemTypeVM)
        {
            var itemType = new ItemType
            {
                Code = itemTypeVM.Code,
                Name = itemTypeVM.Name,
                CategoryId = itemTypeVM.CategoryId,
                IsActive = true
            };
            await context.ItemTypes.AddAsync(itemType);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(ItemTypeVM itemTypeVM)
        {
            if (itemTypeVM.CategoryId == null && itemTypeVM.Category != null && itemTypeVM.Category.Trim().ToLower() != "all")
            {
                var category = await context.Categories
                                            .Where(s => s.Code.Trim().ToLower() == itemTypeVM.Category.Trim().ToLower()
                                                     || s.Name.Trim().ToLower() == itemTypeVM.Category.Trim().ToLower())
                                            .FirstOrDefaultAsync();
                itemTypeVM.CategoryId = category == null ? null : category.Id;
            }

            var itemType = context.ItemTypes.Where(s => s.Id == itemTypeVM.Id).FirstOrDefault();
            if (itemType == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            if (itemTypeVM.Name != itemType.Name || itemTypeVM.CategoryId != itemType.CategoryId)
            {
                mapper.Map(itemTypeVM, itemType);
                context.ItemTypes.Update(itemType);
                await context.SaveChangesAsync(currentUserId);
            }
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
                var requiredHeaders = new[] { "Code", "Name", "Category" };

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
                int categoryCol = headers.IndexOf("Category") + 1;

                var xclitems = new List<ItemTypeFileVM>();
                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var xclitem = new ItemTypeFileVM
                    {
                        Code = row.Cell(codeCol).GetString(),
                        Name = row.Cell(nameCol).GetString(),
                        Category = row.Cell(categoryCol).GetString()
                    };

                    xclitem.Status = await SaveItemFromFile(xclitem);

                    xclitems.Add(xclitem);
                }

                using var newWorkbook = new XLWorkbook();
                var newSheet = newWorkbook.AddWorksheet("Result");

                newSheet.Range("A1:D1").Merge();
                newSheet.Cell(1, 1).MergedRange().Value = "This file is a result of item upload. Don't use this file for uploading";

                newSheet.Cell(2, 1).Value = "Code";
                newSheet.Cell(2, 2).Value = "Name";
                newSheet.Cell(2, 3).Value = "Category";
                newSheet.Cell(2, 4).Value = "Status";

                for (int i = 0; i < xclitems.Count; i++)
                {
                    var p = xclitems[i];
                    newSheet.Cell(i + 3, 1).Value = p.Code;
                    newSheet.Cell(i + 3, 2).Value = p.Name;
                    newSheet.Cell(i + 3, 3).Value = p.Category;
                    newSheet.Cell(i + 3, 4).Value = p.Status;
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

        public async Task<string> SaveItemFromFile(ItemTypeFileVM typeVM)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(typeVM.Code)) return "Code is required";
                if (string.IsNullOrWhiteSpace(typeVM.Name)) return "Name is required";

                Category? category = null;
                if (typeVM.Category != null && typeVM.Category.Trim().ToLower() != "all")
                {
                    category = await context.Categories
                                                .Where(s => s.Code.Trim().ToLower() == typeVM.Category.Trim().ToLower()
                                                         || s.Name.Trim().ToLower() == typeVM.Category.Trim().ToLower())
                                                .FirstOrDefaultAsync();
                    if (category == null) return $"Invalid Category: {typeVM.Category} not found";
                }

                var msg = new List<string>();

                var code = SaveFromFileTruncate(typeVM.Code, 20, msg, "Category");
                var name = SaveFromFileTruncate(typeVM.Name, 100, msg, "Category");

                var type = await context.ItemTypes
                                        .Where(s => s.Code.Trim().ToLower() == code.ToLower())
                                        .FirstOrDefaultAsync();
                if (type != null)
                {
                    if (type.Name != name 
                    || (type.CategoryId == null && category != null) 
                    || (type.CategoryId != null && (category == null || (category != null && type.CategoryId != category.Id)))) {
                        type.Name = name;
                        type.CategoryId = category == null ? null : category.Id;
                        type.IsActive = true;

                        context.ItemTypes.Update(type);
                        await context.SaveChangesAsync(currentUserId);
                        msg.Add("Item Type updated");
                    } else {
                        msg.Add("nothing to update");
                    }
                }
                else
                {
                    type = new ItemType
                    {
                        Code = code,
                        Name = name,
                        CategoryId = category == null ? null : category.Id,
                        IsActive = true
                    };

                    await context.ItemTypes.AddAsync(type);
                    await context.SaveChangesAsync(currentUserId);
                    msg.Add("Item Type added");
                }
                return string.Join("; ", msg);
            }
            catch (Exception ex)
            {
                return SystemFormat.GetInnermostExceptionMessage(ex);
            }
        }

        private string SaveFromFileTruncate(string? value, int maxLength, List<string> messages, string fieldName, bool allowNull = true)
        {
            if (string.IsNullOrWhiteSpace(value)) return allowNull ? null! : "";
            if (value.Length <= maxLength) return value;
            messages?.Add($"{fieldName ?? "Field"} truncated");
            return value.Substring(0, maxLength);
        }
    }
}