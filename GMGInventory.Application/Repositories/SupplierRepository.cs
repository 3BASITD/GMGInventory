using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace GMGInventory.Application.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public SupplierRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<bool> IsCodeExist(string code)
        {
            return await context.Suppliers.AnyAsync(c => c.Code == code);
        }



        public async Task<bool> UploadCombinationExistAsync(SupplierVM supplierVM)
        {
            return (await GetAllSupplierAsync()).Any(s => 
            s.Code == supplierVM.Code &&
            s.Name == supplierVM.Name &&
            s.TIN == supplierVM.TIN &&
            s.AddressLine1 == supplierVM.AddressLine1 &&
            s.AddressLine2 == supplierVM.AddressLine2 &&
            s.OfficeTelNo == supplierVM.OfficeTelNo &&
            s.ContactPerson == supplierVM.ContactPerson && 
            s.RegionName == supplierVM.RegionName &&
            s.Terms == supplierVM.Terms &&
            s.PriceList == supplierVM.PriceList &&
            s.CurrencyName == supplierVM.CurrencyName &&
            s.BusinessStyleName == supplierVM.BusinessStyleName &&
            s.VAT == supplierVM.VAT);
        }

        public async Task<IList<JsonSupplierOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Suppliers
                                .Where(s => s.IsActive || s.Id == IncludeId)
                                .Select(s => new JsonSupplierOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = (s.Code == s.Name ? "" : "(" + s.Code + ") ") + s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id,
                                    Address1 = s.AddressLine1,
                                    Address2 = s.AddressLine2
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<IList<SupplierViewVM>> GetAllSupplierAsync()
        {

            var suppliers = from supplier in context.Suppliers
                         join region in context.Regions
                                   on supplier.RegionId equals region.Id
                         //join currency in context.Currencies
                         //          on supplier.CurrencyId equals currency.Id
                         join userc in userManager.Users on supplier.UserCreated equals userc.Id
                         join userm in userManager.Users on supplier.UserModified equals userm.Id into m
                         from um in m.DefaultIfEmpty()

                         select new SupplierViewVM
                         {
                             Id = supplier.Id,
                             Code = supplier.Code,
                             Name = supplier.Name,
                             TIN = supplier.TIN,
                             AddressLine1 = supplier.AddressLine1,
                             AddressLine2 = supplier.AddressLine2,
                             OfficeTelNo = supplier.OfficeTelNo,
                             ContactPerson = supplier.ContactPerson,
                             RegionName = region.Name,
                             RegionId = supplier.RegionId,
                             Terms = supplier.Terms,
                             PriceList = supplier.PriceList,
                             CurrencyId = supplier.CurrencyId,
                             CurrencyName = supplier.Currency.Name,
                             BusinessStyleId = supplier.BusinessStyleId,
                             BusinessStyleName = supplier.BusinessStyle.Name,
                             VAT = supplier.VAT,
                             IsActive = supplier.IsActive,
                             DateCreatedD = supplier.DateCreated,
                             DateModifiedD = supplier.DateModified,
                             UserCreated = userc.Nickname,
                             UserModified = um == null ? "-" : um.Nickname
                         };
            return await suppliers.ToListAsync();
        }

        public async Task<ForDataTables<SupplierViewVM>> GetAllForDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllSupplierAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.UserCreated.ToLower().Contains(form.SearchValue)
                                    || x.DateCreated.ToString().ToLower().Contains(form.SearchValue)
                                    || x.UserModified.ToLower().ToLower().Contains(form.SearchValue)
                                    || x.DateModified.ToString().Contains(form.SearchValue));
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
                      //  case "CreditLimit": data = data.OrderBy(s => s.CreditLimit); break;
                        case "IsActive": data = data.OrderBy(s => s.IsActive); break;
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
                        //case "CreditLimit": data = data.OrderByDescending(s => s.CreditLimit); break;
                        case "IsActive": data = data.OrderByDescending(s => s.IsActive); break;
                        case "UserCreated": data = data.OrderByDescending(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateCreatedD); break;
                        case "UserModified": data = data.OrderByDescending(s => s.UserModified); break;
                        case "DateModified": data = data.OrderByDescending(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<SupplierViewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<SupplierViewVM>> GetAllSupplierVMAsync()
        {
            var suppliers = await context.Suppliers
                .Include(s => s.Currency)
                .Include(s => s.Region)
                .ToListAsync();
            var suppliersViewVM = mapper.Map<List<SupplierViewVM>>(suppliers);
            foreach (SupplierViewVM ivm in suppliersViewVM)
            {
                ivm.UserCreated = await GetNickname(ivm.UserCreated);
                ivm.UserModified = await GetNickname(ivm.UserModified);
            }
            return suppliersViewVM;
        }

        public async Task<SupplierViewVM?> GetSupplierViewVMAsync(int? id)
        {
            var suppliers = await context.Suppliers
                .Include(s => s.Currency)
                .Include(s => s.Region)
                .Where(s => s.Id == id).FirstOrDefaultAsync();
            if (suppliers == null)
            {
                return null;
            }
            var suppliersViewVM = mapper.Map<SupplierViewVM>(suppliers);
            suppliersViewVM.UserCreated = await GetNickname(suppliersViewVM.UserCreated);
            suppliersViewVM.UserModified = await GetNickname(suppliersViewVM.UserModified);
            return suppliersViewVM;
        }

        public async Task<SupplierVM?> GetSupplierVMAsync(int? id)
        {
            var suppliers = await context.Suppliers
                .Include(s => s.Currency)
                .Include(s => s.Region)
                .Include(s => s.BusinessStyle)
                .Where(s => s.Id == id).FirstOrDefaultAsync();
            if (suppliers == null)
            {
                return null;
            }
            var suppliersViewVM = mapper.Map<SupplierVM>(suppliers);
            return suppliersViewVM;
        }

        public async Task<IList<Supplier>> GetAllActiveSupplierAsync(int includeId)
        {
            return await context.Suppliers.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            //var areas = await context.Areas.ToListAsync();
            //return areas;
        }

        public async Task<bool> IsSupplierCodeExist(string code)
        {
            return await context.Suppliers.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsSupplierInUse(int id)
        {
            return await context.Areas.AnyAsync(s => s.Id == id)
                || await context.PurchaseOrders.AnyAsync(S => S.SupplierId == id);
        }

        public async Task<bool> IsSupplierIdExistAsync(int id)
        {
            return await context.Suppliers.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateSupplierAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var suppliers = await context.Suppliers.FirstAsync(s => s.Id == id);
            suppliers.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateSupplierAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var suppliers = await context.Suppliers.FirstAsync(s => s.Id == id);
            suppliers.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(SupplierVM supplierVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var supplier = new Supplier
            {
                Code = supplierVM.Code,
                Name = supplierVM.Name,
                TIN = supplierVM.TIN + "",
                AddressLine1 = supplierVM.AddressLine1,
                AddressLine2 = supplierVM.AddressLine2,
                OfficeTelNo = supplierVM.OfficeTelNo + "",
                ContactPerson = supplierVM.ContactPerson + "",
                RegionId = supplierVM.RegionId,
                Terms = supplierVM.Terms + "",
                PriceList = supplierVM.PriceList,
                CurrencyId = supplierVM.CurrencyId,
                BusinessStyleId  = supplierVM.BusinessStyleId,
                VAT = supplierVM.VAT + "",
                IsActive = true
            };
            await context.Suppliers.AddAsync(supplier);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(SupplierVM supplierVM)
        {

            var supplier= context.Suppliers.Where(s => s.Id == supplierVM.Id).FirstOrDefault();
            if (supplier == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            supplier.Id = supplierVM.Id;
            supplier.Code = supplierVM.Code;
            supplier.Name = supplierVM.Name;
            supplier.TIN = supplierVM.TIN + "";
            supplier.AddressLine1 = supplierVM.AddressLine1;
            supplier.AddressLine2 = supplierVM.AddressLine2;
            supplier.OfficeTelNo = supplierVM.OfficeTelNo + "";
            supplier.ContactPerson = supplierVM.ContactPerson + "";
            supplier.RegionId = supplierVM.RegionId;
            supplier.Terms = supplierVM.Terms + "";
            supplier.PriceList = supplierVM.PriceList;
            supplier.CurrencyId = supplierVM.CurrencyId;
            supplier.BusinessStyleId  = supplierVM.BusinessStyleId;
            supplier.VAT = supplierVM.VAT + "";
            supplier.IsActive = true;

            context.Suppliers.Update(supplier);
            await context.SaveChangesAsync(currentUserId);
        }
    }
}