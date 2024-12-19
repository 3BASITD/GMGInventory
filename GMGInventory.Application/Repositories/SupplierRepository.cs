using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
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
            s.Term == supplierVM.Term &&
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
                                    Text = s.Code.Trim(),
                                    Name = s.Name.Trim(),
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
                             Term = supplier.Term.Name,
                             TermId = supplier.TermId,
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
                .Include(s => s.Term)

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

        public SupplierViewVM? GetSupplierViewVM(int? id)
        {
            var supplierVM = (from supplier in context.Suppliers
                              join userc in userManager.Users on supplier.UserCreated equals userc.Id
                              join userm in userManager.Users on supplier.UserModified equals userm.Id into m
                              from um in m.DefaultIfEmpty()
                              where supplier.Id == id

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
                              RegionName = supplier.Region.Name,
                              Term = supplier.Term.Name,
                              CurrencyName = supplier.Currency.Name,
                              BusinessStyleName = supplier.BusinessStyle.Name,
                              VAT = supplier.VAT,
                              IsActive = supplier.IsActive,
                              UserCreated =  userc.Nickname,
                              DateCreatedD = supplier.DateCreated,
                              UserModified = um == null ? "-" : um.Nickname,
                              DateModifiedD = supplier.DateModified,
                          }).FirstOrDefault();
            return supplierVM;
        }

        public async Task<SupplierVM?> GetSupplierVMAsync(int? id)
        {
            var suppliers = await context.Suppliers
                .Include(s => s.Currency)
                .Include(s => s.Region)
                .Include(s => s.BusinessStyle)
                .Include(s => s.Term)
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
                || await context.PurchaseOrderNews.AnyAsync(S => S.SupplierId == id);
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
                TermId =  supplierVM.TermId,
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
            supplier.TermId =  supplierVM.TermId;
            supplier.CurrencyId = supplierVM.CurrencyId;
            supplier.BusinessStyleId  = supplierVM.BusinessStyleId;
            supplier.VAT = supplierVM.VAT + "";
            supplier.IsActive = true;

            context.Suppliers.Update(supplier);
            await context.SaveChangesAsync(currentUserId);
        }

        private async Task<IList<SupplierDetailPurchaseVM>> GetSupplierPurchaseAsync(int id)
        {
            var poVM = from po in context.PurchaseOrderNews
                                                      .Include(s => s.Currency)
                                                      .Include(s => s.DeliveryAddress)
                                                      .Include(s => s.Status)
                                                      .Include(s => s.Supplier)

                              join purchaseItem in context.PurchaseOrderNewLineItems on po.Id equals purchaseItem.PurchaseOrderId
                               join purchaseRequest in context.PurchaseRequestLineItems on purchaseItem.RequestItemId equals purchaseRequest.Id
                               join user in userManager.Users on po.UserCreated equals user.Id
                              where po.SupplierId == id


                              select new SupplierDetailPurchaseVM
                              {
                                  ItemId = purchaseRequest.ItemId,
                                  Item = purchaseRequest.Item.Code , // purchaseRequest.Item != null ? purchaseRequest.Item.Code : purchaseRequest.ItemDescription,
                                  ItemDescription = purchaseRequest.ItemDescription,
                                  ConversionFactorD = po.CurrencyRate,
                                  Currency = po.Currency.Code,
                                  PurchaseNumber = po.DocumentNumber,
                                  QuantityD = purchaseItem.Quantity,
                                  ReplenishBy = user.Nickname,
                                  ReplenishDateD = po.DateCreated,
                                  Status = po.Status.Code,
                                  UnitCostD = purchaseItem.UnitCost, // tinanggal na sa db
                                  UnitPriceD = purchaseItem.UnitCost,
                                  Warehouse = po.DeliveryAddress.Code
                              };
            return await poVM.ToListAsync();
        }

        public async Task<ForDataTables<SupplierDetailPurchaseVM>> GetSupplierPurchaseDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetSupplierPurchaseAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.ConversionFactorD.ToString().Contains(form.SearchValue)
                                    || x.ConvertedUnitPriceD.ToString().Contains(form.SearchValue)
                                    || x.Currency.ToString().Contains(form.SearchValue)
                                    || x.PurchaseNumber.ToLower().Contains(form.SearchValue)
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.ReplenishBy.ToLower().Contains(form.SearchValue)
                                    || x.ReplenishDate.ToLower().Contains(form.SearchValue)
                                    || x.Status.ToLower().Contains(form.SearchValue)
                                    || x.UnitCostD.ToString().Contains(form.SearchValue)
                                    || x.UnitPriceD.ToString().Contains(form.SearchValue)
                                    || x.Warehouse.ToLower().Contains(form.SearchValue));
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
                        case "ConversionFactor": data = data.OrderBy(s => s.ConversionFactorD); break;
                        case "ConvertedUnitPrice": data = data.OrderBy(s => s.ConvertedUnitPriceD); break;
                        case "Currency": data = data.OrderBy(s => s.Currency); break;
                        case "PurchaseNumber": data = data.OrderBy(s => s.PurchaseNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        case "ReplenishBy": data = data.OrderBy(s => s.ReplenishBy); break;
                        case "ReplenishDate": data = data.OrderBy(s => s.ReplenishDateD); break;
                        case "Status": data = data.OrderBy(s => s.Status); break;
                        case "UnitCost": data = data.OrderBy(s => s.UnitCostD); break;
                        case "Warehouse": data = data.OrderBy(s => s.Warehouse); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "ConversionFactor": data = data.OrderByDescending(s => s.ConversionFactorD); break;
                        case "ConvertedUnitPrice": data = data.OrderByDescending(s => s.ConvertedUnitPriceD); break;
                        case "Currency": data = data.OrderByDescending(s => s.Currency); break;
                        case "PurchaseNumber": data = data.OrderByDescending(s => s.PurchaseNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        case "ReplenishBy": data = data.OrderByDescending(s => s.ReplenishBy); break;
                        case "ReplenishDate": data = data.OrderByDescending(s => s.ReplenishDateD); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        case "UnitCost": data = data.OrderByDescending(s => s.UnitCostD); break;
                        case "Warehouse": data = data.OrderByDescending(s => s.Warehouse); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<SupplierDetailPurchaseVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

    }
}