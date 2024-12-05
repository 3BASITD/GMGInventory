using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public CustomerRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Select2Model<JsonAddressOptionsModel>> GetOptions(string search, int page)
        {
            var customers = await context.Customers
                                         .Where(s => string.IsNullOrWhiteSpace(search)
                                                 || (s.Code.ToLower().Contains(search.ToLower()))
                                                 || (s.Name.ToLower().Contains(search.ToLower())))
                                         .Select(s => new JsonAddressOptionsModel
                                         {
                                             Id = s.Id.ToString(),
                                             Text = s.Code,
                                             Name = s.Name,
                                             Address = s.Address,
                                             CreditTerm = s.CreditTerm.Name,
                                             Agent = s.Agent.FirstName +' '+ s.Agent.MiddleName +' '+ s.Agent.LastName,
                                             OutstandingBalanceD = s.OutstandingBalance
                                         })
                                         .OrderBy(s => s.Text)
                                         .ToListAsync();
            var customersFiltered = customers.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonAddressOptionsModel>
            {
                //incomplete_results = false,
                items = customersFiltered,
                total_count = customers.Count
            };
        }

        public async Task<Select2Model<JsonAddressOptionsModel>> GetByAgentOptions(string search, int page, int? agent)
        {
            var customers = await context.Customers
                                         .Where(s => (string.IsNullOrWhiteSpace(search)
                                                 || (s.Code.ToLower().Contains(search.ToLower()))
                                                 || (s.Name.ToLower().Contains(search.ToLower())))
                                                 && (agent == null || s.AgentId == agent))
                                         .Select(s => new JsonAddressOptionsModel
                                         {
                                             Id = s.Id.ToString(),
                                             Text = s.Code,
                                             Name = s.Name,
                                             Address = s.Address,
                                             CreditTerm = s.CreditTerm.Name,
                                             Agent = s.Agent.FirstName + ' ' + s.Agent.MiddleName + ' ' + s.Agent.LastName,
                                             OutstandingBalanceD = s.OutstandingBalance,
                                             CustomerStatus = s.Remarks + ""
                                         })
                                         .OrderBy(s => s.Text)
                                         .ToListAsync();
            var customersFiltered = customers.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonAddressOptionsModel>
            {
                //incomplete_results = false,
                items = customersFiltered,
                total_count = customers.Count
            };
        }

        public async Task<Select2Model<JsonAddressOptionsModel>> GetOptionsWithOrder(string search, int page)
        {
            var customers = await (from c in context.Customers
                                   where (string.IsNullOrWhiteSpace(search)
                                   || c.Code.ToLower().Contains(search.ToLower())
                                   || c.Name.ToLower().Contains(search.ToLower()))
                                   && context.SalesOrders
                                             .Any(s => s.Status.Name != "Sales Order Submitted"
                                                    && s.Status.Name != "Sales Order Disapproved"
                                                    && s.CustomerId == c.Id)
                                   select new JsonAddressOptionsModel
                                   {
                                       Id = c.Id.ToString(),
                                       Text = c.Code,
                                       Name = c.Name,
                                       Address = c.Address,
                                       CreditTerm = c.CreditTerm.Name,
                                       Agent = c.Agent.FirstName + ' ' + c.Agent.MiddleName + ' ' + c.Agent.LastName,
                                       OutstandingBalanceD = c.OutstandingBalance
                                   })
                                     .OrderBy(s => s.Text)
                                     .ToListAsync();


            var customersFiltered = customers.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonAddressOptionsModel>
            {
                //incomplete_results = false,
                items = customersFiltered,
                total_count = customers.Count
            };
        }

        public async Task<IList<JsonOptionsModel>> GetOptions2(int? IncludeId)
        {
            return await context.Customers
                .Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }


        public async Task<int?> GetIdAsync(string? Name)
        {
            var customers = await context.Customers.Where(s => s.Name == Name).FirstOrDefaultAsync();
            return customers?.Id;
        }

        public async Task<int?> GetIdByCodeAsync(string? Code)
        {
            var customers = await context.Customers.Where(s => s.Code == Code).FirstOrDefaultAsync();
            return customers?.Id;
        }
        public CustomerVM? GetCustomerDtlVM(int? id)
        {
            var customerVM = (from cust in context.Customers
                                    .Include(s => s.Area)
                                    .Include(s => s.CustomerType)
                                    .Include(s => s.Agent)
                                    .Include(s => s.CreditTerm)

                              where cust.Id == id
                              select new CustomerVM
                              {
                                  Id = cust.Id,
                                  Code = cust.Code,
                                  Name = cust.Name,
                                  AreaId = cust.Area.Id,
                                  CustomerTypeId = cust.CustomerType.Id,
                                  ContactPerson = cust.ContactPerson,
                                  ContactNumber = cust.ContactNumber,
                                  Address = cust.Address,
                                  Email = cust.Email,
                                  AgentId = cust.Agent.Id,
                                  AgentName = cust.Agent.FirstName + (cust.Agent.MiddleName == null ? "" : " " + cust.Agent.MiddleName) + " " + cust.Agent.LastName,
                                  AreaName = cust.Area.Name,
                                  CreditTermName = cust.CreditTerm.Name,
                                  RegionName = cust.Area.Region.Name,
                                  CustomerTypeName = cust.CustomerType.Name,
                                  CommissionRateD = cust.CommissionRate,
                                  FaxNumber = cust.FaxNumber,
                                  TIN = cust.TIN,
                                  VAT = cust.VAT,
                                  RegionId = cust.Area.RegionId,
                                  CreditTermId = cust.CreditTerm.Id,
                                  CreditLimitD = cust.CreditLimit,
                                  Remarks = cust.Remarks,
                                  IsActive = cust.IsActive
                              }).FirstOrDefault();
            return customerVM;
        }

        public async Task<IList<CustomerVM>> GetAllCustomerAsync()
        {
            var customers =  from cust in context.Customers
                                    .Include(s => s.Area)
                                    .Include(s => s.CustomerType)
                                    .Include(s => s.Agent)
                                    .Include(s => s.CreditTerm)

                   select new CustomerVM
                   {
                       Id = cust.Id,
                       Code = cust.Code,
                       Name = cust.Name,
                       CommissionRateD = cust.CommissionRate,
                       RegionId = cust.Area.RegionId,
                       RegionName = cust.Area.Region.Name,
                       AreaId = cust.AreaId,
                       AreaName = cust.Area.Name,
                       CustomerTypeId = cust.CustomerTypeId,
                       CustomerTypeName = cust.CustomerType.Name,
                       ContactNumber = cust.ContactNumber,
                       ContactPerson = cust.ContactPerson,
                       Address = cust.Address,
                       Email = cust.Email,
                       AgentId = cust.AgentId,
                       AgentName = cust.Agent.FirstName + (cust.Agent.MiddleName == null ? "" : " " + cust.Agent.MiddleName) + " " + cust.Agent.LastName,
                       FaxNumber = cust.FaxNumber,
                       TIN = cust.TIN,
                       VAT = cust.VAT,
                       CreditTermId = cust.CreditTermId,
                       CreditTermName = cust.CreditTerm.Name,
                       CreditLimitD = cust.CreditLimit,
                       OutstandingBalanceD = cust.OutstandingBalance,
                       Remarks = cust.Remarks,
                       IsActive = cust.IsActive
                   };
            return await customers.ToListAsync();
        }

        public async Task<ForDataTables<CustomerVM>> GetListDataTables(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            //var data = context.AuditLogs.Include(s=>s.User).AsQueryable();
            var data = (await GetAllCustomerAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.Name.ToLower().Contains(form.SearchValue)
                                    || x.AgentName.ToLower().Contains(form.SearchValue)
                                    || x.CreditLimit.ToString().Contains(form.SearchValue)
                                    || x.OutstandingBalance.ToString().Contains(form.SearchValue));
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
                        case "AgentName": data = data.OrderBy(s => s.AgentName); break;
                        case "CreditLimit": data = data.OrderBy(s => s.CreditLimit); break;
                        case "OutstandingBalance": data = data.OrderBy(s => s.OutstandingBalance); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "AgentName": data = data.OrderByDescending(s => s.AgentName); break;
                        case "CreditLimit": data = data.OrderByDescending(s => s.CreditLimit); break;
                        case "OutstandingBalance": data = data.OrderByDescending(s => s.OutstandingBalance); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<CustomerVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<IList<Customer>> GetAllActiveCustomerAsync(int includeId)
        {
            return await context.Customers.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            //var areas = await context.Areas.ToListAsync();
            //return areas;
        }

        public async Task<bool> IsCustomerCodeExist(string code)
        {
            return await context.Customers.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsCustomerInUse(int id)
        {
            return await context.Areas.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> IsCodeExistAsync(string code)
        {
            return await context.Customers.AnyAsync(s => s.Code == code);
        }

        public async Task<bool> IsCustomerIdExistAsync(int id)
        {
            return await context.Customers.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateCustomerAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var customers = await context.Customers.FirstAsync(s => s.Id == id);
            customers.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateCustomerAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var customers = await context.Customers.FirstAsync(s => s.Id == id);
            customers.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(CustomerVM custVM)
        {
            var cust = context.Customers.Where(s => s.Code == custVM.Code).FirstOrDefault();
            if (cust == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            cust.Code = custVM.Code;
            cust.Name = custVM.Name;
            cust.CommissionRate = custVM.CommissionRateD;
            cust.AreaId = custVM.AreaId;
            cust.CustomerTypeId = custVM.CustomerTypeId;
            cust.ContactPerson = custVM.ContactPerson;
            cust.ContactNumber = custVM.ContactNumber;
            cust.Address = custVM.Address;
            cust.Email = custVM.Email;
            cust.AgentId = custVM.AgentId;
            cust.FaxNumber = custVM.FaxNumber;
            cust.TIN = custVM.TIN;
            cust.VAT = custVM.VAT;
            cust.CreditTermId = custVM.CreditTermId;
            cust.CreditLimit = custVM.CreditLimitD;
            cust.Remarks = custVM.Remarks;
            cust.IsActive = true;
            cust.CommissionRate = custVM.UseAreaRate == false ? custVM.CommissionRateD : null;
            context.Customers.Update(cust);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(CustomerVM custVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            //var customerVM = (from cust in context.Customers
            //                        .Include(s => s.Area)
            //                        .Include(s => s.CustomerType)
            //                        .Include(s => s.Agent)
            //                        .Include(s => s.CreditTerm)

            var cust = new Customer
            {
                Code = custVM.Code,
                Name = custVM.Name,
                AreaId = custVM.AreaId,
                CustomerTypeId = custVM.CustomerTypeId,
                ContactPerson = custVM.ContactPerson,
                ContactNumber = custVM.ContactNumber,
                Address = custVM.Address,
                Email = custVM.Email,
                AgentId = custVM.AgentId,
                FaxNumber = custVM.FaxNumber,
                TIN = custVM.TIN,
                VAT = custVM.VAT,
                CreditTermId = custVM.CreditTermId,
                CreditLimit = custVM.CreditLimitD,
                Remarks = custVM.Remarks,
                CommissionRate = custVM.UseAreaRate == false ? custVM.CommissionRateD : null,

                IsActive = true
            };
            await context.Customers.AddAsync(cust);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddShippingAsync(CustomerVM shippingVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var shipping = new ShippingAddress
            {
                CustomerId = shippingVM.Id,
                Address = shippingVM.ShippingAddress,
                PhoneNumber = shippingVM.ShippingPhoneNo,
                FaxNumber = shippingVM.ShippingFaxNo,
                ContactPerson = shippingVM.ShippingContactPerson
            };
            await context.ShippingAddresses.AddAsync(shipping);
            await context.SaveChangesAsync(currentUserId);
        }

        //public async Task AddForwarderAsync(CustomerVM forwarderVM)
        //{
        //    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        //    var forwarders = new Forwarder
        //    {
        //        CustomerId = forwarderVM.Id,
        //        Name = forwarderVM.ForwarderName,
        //        Address = forwarderVM.ForwarderAddress,
        //        PhoneNumber = forwarderVM.ForwarderPhoneNo,
        //        FaxNumber = forwarderVM.ForwarderFaxNo
        //    };
        //    await context.Forwarders.AddAsync(forwarders);
        //    await context.SaveChangesAsync(currentUserId);
        //}

        private async Task<IList<CustomerVM>> GetCustomerDetailsAsync(int id)
        {
            var customerVM = from cust in context.Customers
                                    .Include(s => s.Area)
                                    .Include(s => s.CustomerType)
                                    .Include(s => s.Agent)
                                    .Include(s => s.CreditTerm)
                             select new CustomerVM
                             {
                                 Code = cust.Code,
                                 Name = cust.Name,
                                 AreaId = cust.AreaId,
                                 CustomerTypeId = cust.CustomerTypeId,
                                 ContactPerson = cust.ContactPerson,
                                 ContactNumber = cust.ContactNumber,
                                 Address = cust.Address,
                                 Email = cust.Email,
                                 AgentId = cust.AgentId,
                                 FaxNumber = cust.FaxNumber,
                                 TIN = cust.TIN,
                                 VAT = cust.VAT,
                                 CreditTermId = cust.CreditTermId,
                                 CreditLimitD = cust.CreditLimit,
                                 Remarks = cust.Remarks,
                                 IsActive = true
                             };
            return await customerVM.ToListAsync();
        }

        public async Task<ForDataTables<CustomerVM>> GetCustomerDetailsDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetCustomerDetailsAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                   || x.Name.ToLower().Contains(form.SearchValue)
                                   || x.AgentName.ToLower().Contains(form.SearchValue));
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
                        case "AgentName": data = data.OrderBy(s => s.AgentName); break;
                        case "CreditLimit": data = data.OrderBy(s => s.CreditLimit); break;
                        case "OutstandingBalance": data = data.OrderBy(s => s.OutstandingBalance); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "AgentName": data = data.OrderByDescending(s => s.AgentName); break;
                        case "CreditLimit": data = data.OrderByDescending(s => s.CreditLimit); break;
                        case "OutstandingBalance": data = data.OrderByDescending(s => s.OutstandingBalance); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<CustomerVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<CustomerVM>> GetCustomerShippingAsync(int id)
        {
            var customerVM = from customer in context.Customers
                             join shipping in context.ShippingAddresses
                                            on customer.Id equals shipping.CustomerId
                             where shipping.CustomerId == id
                             select new CustomerVM
                             {
                                 ShippingAddress = shipping.Address,
                                 ShippingContactPerson = shipping.ContactPerson,
                                 ShippingFaxNo = shipping.FaxNumber,
                                 ShippingPhoneNo = shipping.PhoneNumber
                             };
            return await customerVM.ToListAsync();
        }

        public async Task<ForDataTables<CustomerVM>> GetCustomerShippingDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetCustomerShippingAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.ShippingAddress.ToString().Contains(form.SearchValue)
                                    || x.ShippingContactPerson.ToString().Contains(form.SearchValue)
                                    || x.ShippingFaxNo.ToString().Contains(form.SearchValue)
                                    || x.ShippingPhoneNo.ToString().Contains(form.SearchValue));
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
                        case "ShippingAddress": data = data.OrderBy(s => s.ShippingAddress); break;
                        case "ShippingContactPerson": data = data.OrderBy(s => s.ShippingContactPerson); break;
                        case "ShippingFaxNo": data = data.OrderBy(s => s.ShippingFaxNo); break;
                        case "ShippingPhoneNo": data = data.OrderBy(s => s.ShippingPhoneNo); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "ShippingAddress": data = data.OrderByDescending(s => s.ShippingAddress); break;
                        case "ShippingContactPerson": data = data.OrderByDescending(s => s.ShippingContactPerson); break;
                        case "ShippingFaxNo": data = data.OrderByDescending(s => s.ShippingFaxNo); break;
                        case "ShippingPhoneNo": data = data.OrderByDescending(s => s.ShippingPhoneNo); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<CustomerVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        //private async Task<IList<CustomerVM>> GetCustomerForwarderAsync(int id)
        //{
        //    var customerVM = from customer in context.Customers
        //                     join frwdr in context.Forwarders
        //                                    on customer.Id equals frwdr.CustomerId
        //                     where frwdr.CustomerId == id
        //                     select new CustomerVM
        //                     {
        //                         ForwarderName = frwdr.Name,
        //                         ForwarderAddress = frwdr.Address,
        //                         ForwarderFaxNo = frwdr.FaxNumber,
        //                         ForwarderPhoneNo = frwdr.PhoneNumber
        //                     };
        //    return await customerVM.ToListAsync();
        //}

        //public async Task<ForDataTables<CustomerVM>> GetCustomerForwarderDTAsync(DataTablesRequestForm form, int id)
        //{
        //    int totalRecord = 0;
        //    int filterRecord = 0;
        //    var data = (await GetCustomerForwarderAsync(id)).AsQueryable();
        //    //get total count of data in table
        //    totalRecord = data.Count();
        //    // search data when search value found
        //    if (!string.IsNullOrWhiteSpace(form.SearchValue))
        //    {
        //        data = data.Where(x => x.ForwarderName.ToString().Contains(form.SearchValue)
        //                            || x.ForwarderAddress.ToString().Contains(form.SearchValue)
        //                            || x.ForwarderFaxNo.ToString().Contains(form.SearchValue)
        //                            || x.ForwarderPhoneNo.ToString().Contains(form.SearchValue));
        //    }
        //    // get total count of records after search
        //    filterRecord = data.Count();
        //    //sort data
        //    if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
        //    {
        //        if (form.SortColumnDirection == "asc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "ForwarderName": data = data.OrderBy(s => s.ForwarderName); break;
        //                case "ForwarderAddress": data = data.OrderBy(s => s.ForwarderAddress); break;
        //                case "ForwarderFaxNo": data = data.OrderBy(s => s.ForwarderFaxNo); break;
        //                case "ForwarderPhoneNo": data = data.OrderBy(s => s.ForwarderPhoneNo); break;
        //                default: break;
        //            }
        //        }
        //        else if (form.SortColumnDirection == "desc")
        //        {
        //            switch (form.SortColumn)
        //            {
        //                case "ForwarderName": data = data.OrderByDescending(s => s.ForwarderName); break;
        //                case "ForwarderAddress": data = data.OrderByDescending(s => s.ForwarderAddress); break;
        //                case "ForwarderFaxNo": data = data.OrderByDescending(s => s.ForwarderFaxNo); break;
        //                case "ForwarderPhoneNo": data = data.OrderByDescending(s => s.ForwarderPhoneNo); break;
        //                default: break;
        //            }
        //        }
        //    }
        //    //pagination
        //    var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
        //    return new ForDataTables<CustomerVM>
        //    {
        //        Draw = form.Draw,
        //        RecordsTotal = totalRecord,
        //        RecordsFiltered = filterRecord,
        //        Data = pageList
        //    };
        //}

        private async Task<IList<CustomerVM>> GetCustomerPaymentAsync(int id)
        {
            var customerVM = from customer in context.Customers
                             join cp in context.CustomerPayments
                                      on customer.Id equals cp.CustomerId
                             join user in userManager.Users on cp.UserCreated equals user.Id

                             where cp.CustomerId == id
                             select new CustomerVM
                             {
                                 CRNo = cp.CRNumber,
                                 SOANo = cp.SOACRId,
                                 TotalInvoiceAmountD = cp.TotalInvoiceAmount,
                                 AmountPaidD = cp.TotalAmountPaid,
                                 OverPaymentD = cp.OverPayment,
                                 TransactionDateD = cp.DateCreated,
                                 CreatedBy = user.Nickname,
                                 PaymentRemarks = cp.Remarks
                             };
            return await customerVM.ToListAsync();
        }

        public async Task<ForDataTables<CustomerVM>> GetCustomerPaymentDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetCustomerPaymentAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CRNo.ToString().Contains(form.SearchValue)
                                    || x.SOANo.ToString().Contains(form.SearchValue)
                                    || x.TotalInvoiceAmount.ToString().Contains(form.SearchValue)
                                    || x.AmountPaid.ToString().Contains(form.SearchValue)
                                    || x.OverPayment.ToString().Contains(form.SearchValue)
                                    || x.TransactionDate.ToString().Contains(form.SearchValue)
                                    || x.CreatedBy.ToString().Contains(form.SearchValue)
                                    || x.PaymentRemarks.ToString().Contains(form.SearchValue));
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
                        case "CRNo": data = data.OrderBy(s => s.CRNo); break;
                        case "SOANo": data = data.OrderBy(s => s.SOANo); break;
                        case "TotalInvoiceAmount": data = data.OrderBy(s => s.TotalInvoiceAmount); break;
                        case "AmountPaid": data = data.OrderBy(s => s.AmountPaid); break;
                        case "OverPayment": data = data.OrderBy(s => s.OverPayment); break;
                        case "TransactionDate": data = data.OrderBy(s => s.TransactionDate); break;
                        case "CreatedBy": data = data.OrderBy(s => s.CreatedBy); break;
                        case "PaymentRemarks": data = data.OrderBy(s => s.PaymentRemarks); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "CRNo": data = data.OrderByDescending(s => s.CRNo); break;
                        case "SOANo": data = data.OrderByDescending(s => s.SOANo); break;
                        case "TotalInvoiceAmount": data = data.OrderByDescending(s => s.TotalInvoiceAmount); break;
                        case "AmountPaid": data = data.OrderByDescending(s => s.AmountPaid); break;
                        case "OverPayment": data = data.OrderByDescending(s => s.OverPayment); break;
                        case "TransactionDate": data = data.OrderByDescending(s => s.TransactionDate); break;
                        case "CreatedBy": data = data.OrderByDescending(s => s.CreatedBy); break;
                        case "PaymentRemarks": data = data.OrderByDescending(s => s.PaymentRemarks); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<CustomerVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<CustomerVM>> GetCustomerReceivableAsync(int id)
        {
            //var customerVM = from inv in context.Invoices
            //                 join soali in context.SOACRLineItems
            //                            on inv.Id equals soali.InvoiceId
            //                 join soacr in context.SOACRs
            //                            on soali.SOACRId equals soacr.Id into soacrs
            //                 from soacrsGroup in soacrs.DefaultIfEmpty()
            //                 join c in context.CustomerPayments
            //                            on soacrsGroup.Id equals c.SOACRId

            //                 where c.CustomerId == id
            //                 select new CustomerVM
            //                 {
            //                     InvoiceNo = inv.InvoiceNumber,
            //                     InvoicedDateD = inv.DateModified ?? inv.DateCreated,
            //                     InvoicedAmountD = inv.Amount
            //                 };
            //return await customerVM.ToListAsync();

            var receivables = from inv in context.Invoices
                              where inv.SalesOrder.Customer.Id == id
                              select new CustomerVM
                              {
                                  InvoiceNo = inv.InvoiceNumber,
                                  InvoicedDateD = inv.InvoiceDate,
                                  InvoicedAmountD = inv.Amount
                              };
            return await receivables.ToListAsync();
        }

        public async Task<ForDataTables<CustomerVM>> GetCustomerReceivableDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetCustomerReceivableAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.InvoiceNo.ToString().Contains(form.SearchValue)
                                    || x.InvoicedDateD.ToString().Contains(form.SearchValue)
                                    || x.InvoicedAmount.ToString().Contains(form.SearchValue));
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
                        case "InvoiceNo": data = data.OrderBy(s => s.InvoiceNo); break;
                        case "InvoicedDate": data = data.OrderBy(s => s.InvoicedDateD); break;
                        case "InvoicedAmount": data = data.OrderBy(s => s.InvoicedAmount); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "InvoiceNo": data = data.OrderByDescending(s => s.InvoiceNo); break;
                        case "InvoicedDate": data = data.OrderByDescending(s => s.InvoicedDateD); break;
                        case "InvoicedAmount": data = data.OrderByDescending(s => s.InvoicedAmount); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<CustomerVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }



        public async Task<bool> UploadCombinationExistAsync(CustomerVM customerVM)
        {
            return (await GetAllCustomerAsync()).Any(s => s.Code == customerVM.Code && s.Name == customerVM.Name &&
            s.CommissionRate == customerVM.CommissionRate && s.RegionName == customerVM.RegionName &&
            s.AreaName == customerVM.AreaName && s.CustomerTypeName == customerVM.CustomerTypeName &&
            s.ContactPerson == customerVM.ContactPerson && s.ContactNumber == customerVM.ContactNumber &&
            s.Address == customerVM.Address && s.Email == customerVM.Email &&
            s.AgentName == customerVM.AgentName && s.FaxNumber == customerVM.FaxNumber &&
            s.TIN == customerVM.TIN && s.VAT == customerVM.VAT && s.CreditTermName == customerVM.CreditTermName &&
            s.CreditLimit == customerVM.CreditLimit && s.Remarks == customerVM.Remarks) ;
        }
    }
}