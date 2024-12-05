using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                Amount = chequeVM.AmountD
            };
            await context.Cheques.AddAsync(cheque);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<IList<ChequeVM>> GetAllChequeAsync()
        {
            var cheques = from cheque in context.Cheques

                          join userc in userManager.Users on cheque.UserCreated equals userc.Id
                          join userm in userManager.Users on cheque.UserModified equals userm.Id into m
                          from um in m.DefaultIfEmpty()

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
                              DateCreatedD = cheque.DateCreated,
                              DateModifiedD = cheque.DateModified == null ? null : cheque.DateModified.Value,
                              UserCreated = userc.UserName,
                              UserModified = um == null ? "-" : um.UserName
                          };
            return await cheques.ToListAsync();
        }

        public async Task<ForDataTables<ChequeVM>> GetAllForDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllChequeAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.RefNo.ToLower().Contains(form.SearchValue)
                                    || x.Check.ToLower().Contains(form.SearchValue)
                                    || x.CustomerCode.ToLower().Contains(form.SearchValue)
                                    || x.Branch!.ToLower().Contains(form.SearchValue)
                                    || x.CollectDate.ToString().ToLower().Contains(form.SearchValue)
                                    || x.CheckNo!.ToLower().ToLower().Contains(form.SearchValue)
                                    || x.CheckDate.ToString().Contains(form.SearchValue));
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
                        case "RefNo": data = data.OrderBy(s => s.RefNo); break;
                        case "Check": data = data.OrderBy(s => s.Check); break;
                        case "CustomerCode": data = data.OrderBy(s => s.CustomerCode); break;
                        case "Branch": data = data.OrderBy(s => s.Branch); break;
                        case "CollectDate": data = data.OrderBy(s => s.CollectDate); break;
                        case "CheckNo": data = data.OrderBy(s => s.CheckNo); break;
                        case "CheckDate": data = data.OrderBy(s => s.CheckDate); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "RefNo": data = data.OrderByDescending(s => s.RefNo); break;
                        case "Check": data = data.OrderByDescending(s => s.Check); break;
                        case "CustomerCode": data = data.OrderByDescending(s => s.CustomerCode); break;
                        case "Branch": data = data.OrderByDescending(s => s.Branch); break;
                        case "CollectDate": data = data.OrderByDescending(s => s.CollectDate); break;
                        case "CheckNo": data = data.OrderByDescending(s => s.CheckNo); break;
                        case "CheckDate": data = data.OrderByDescending(s => s.CheckDate); break;
                        default: break;
                    }
                }
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

            context.Cheques.Update(cheque);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<bool> UploadCombinationExistAsync(ChequeVM chequeVM)
        {
            return (await GetAllChequeAsync()).Any(s =>
            s.CollectDate == chequeVM.CollectDate &&
            s.RefNo == chequeVM.RefNo &&
            s.CustomerName == chequeVM.CustomerName &&
            s.CheckDate == chequeVM.CheckDate &&
            s.Check == chequeVM.Check &&
            s.Branch == chequeVM.Branch &&
            s.CheckNo == chequeVM.CheckNo &&
            s.Amount == chequeVM.Amount);
        }

        public async Task<IList<ChequeVM>> GetListAsync()
        {
            var chequeVM = from cheque in context.Cheques
                                                   .Include(s => s.Customer)
                            select new ChequeVM
                            {
                                CollectDateD = cheque.CollectDate,
                                RefNo = cheque.RefNo,
                                CustomerCode = cheque.Customer.Code,
                                CheckDateD = cheque.CheckDate,
                                Check = cheque.Check,
                                Branch = cheque.Branch,
                                CheckNo = cheque.CheckNo,
                                AmountD = cheque.Amount,
                            };
            return await chequeVM.ToListAsync();
        }

        public async Task<bool> IsRefNoExistAsync(string refNo)
        {
            return await context.Cheques.AnyAsync(s => s.RefNo == refNo);
        }
    }
}
