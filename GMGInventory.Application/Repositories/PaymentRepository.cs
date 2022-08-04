using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly string currentUser;

        public PaymentRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
            this.currentUser = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public IQueryable<PaymentViewVM> GetAll()
        {
            var paymentVM = (from payment in context.Payments
                             //from paymentDetails in context.PaymentDetails.Where(s => s.PaymentId == payment.Id)
                             //from invoices in context.Invoices.Where(s => s.Id == paymentDetails.InvoiceId)
                             //join invoicePayment in context.InvoicePayments on paymentDetails.InvoiceId equals invoicePayment.InvoiceId
                             //where ((paymentFilterVM.CustomerId == null || paymentFilterVM.CustomerId == payment.CustomerId)

                             //    && (paymentFilterVM.PaymentId == null || paymentFilterVM.PaymentId == paymentDetails.PaymentId))
                             select new PaymentViewVM
                             {
                                 Customer = payment.Customer.Name,
                                 ReceivedDate = payment.ReceivedDate,
                                 ReturnedAmount = payment.ReturnedAmount,
                                 AdjustedAmount = payment.AdjustedAmount,
                                 CRNumber = payment.CRNumber,
                                 PaymentMode = payment.PaymentMode,
                                 Remarks = payment.Remarks,
                                 DateCreated = payment.DateCreated.ToString("yyyy/dd/MM hh:mm:ss tt"),
                                 DateModified = payment.DateModified == null ? "-" : payment.DateModified.Value.ToString("yyyy/dd/MM hh:mm:ss tt"),
                                 UserCreated = userManager.FindByIdAsync(payment.UserCreated).Result.Nickname,
                                 UserModified = payment.UserModified == null ? "-" : userManager.FindByIdAsync(payment.UserModified).Result.Nickname
                             });

            return paymentVM;
        }
        public ForDataTables<PaymentViewVM> GetListDT(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = GetAll().ToList().AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Customer.ToLower().Contains(form.SearchValue)
                                     || x.ReceivedDate.ToString().ToLower().Contains(form.SearchValue)
                                     || x.ReturnedAmount.ToString().ToLower().Contains(form.SearchValue)
                                     || x.AdjustedAmount.ToString().ToLower().Contains(form.SearchValue)
                                     || x.CRNumber.ToString().ToLower().Contains(form.SearchValue)
                                     || x.PaymentMode.ToString().ToLower().Contains(form.SearchValue)
                                     || x.Remarks.ToString().ToLower().Contains(form.SearchValue)
                                     || x.UserModified.ToString().ToLower().Contains(form.SearchValue)
                                     || x.DateModified.ToLower().Contains(form.SearchValue)
                                     || x.DateCreated.ToString().ToLower().Contains(form.SearchValue)
                                     || x.UserCreated.ToLower().Contains(form.SearchValue));
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
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "ReceivedDate": data = data.OrderBy(s => s.ReceivedDate); break;
                        case "ReturnedAmount": data = data.OrderBy(s => s.ReturnedAmount); break;
                        case "AdjustedAmount": data = data.OrderBy(s => s.AdjustedAmount); break;
                        case "CRNumber": data = data.OrderBy(s => s.CRNumber); break;
                        case "PaymentMode": data = data.OrderBy(s => s.PaymentMode); break;
                        case "Remarks": data = data.OrderBy(s => s.Remarks); break;
                        case "UserCreated": data = data.OrderBy(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderBy(s => s.DateModified); break;
                        case "UserModified": data = data.OrderBy(s => s.UserModified); break;
                        case "DateModified": data = data.OrderBy(s => s.DateModified); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "ReceivedDate": data = data.OrderByDescending(s => s.ReceivedDate); break;
                        case "ReturnedAmount": data = data.OrderByDescending(s => s.ReturnedAmount); break;
                        case "AdjustedAmount": data = data.OrderByDescending(s => s.AdjustedAmount); break;
                        case "CRNumber": data = data.OrderByDescending(s => s.CRNumber); break;
                        case "PaymentMode": data = data.OrderByDescending(s => s.PaymentMode); break;
                        case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
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
            return new ForDataTables<PaymentViewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<PaymentViewVM?> GetAsync(int? id)
        {
            var payment = await context.Payments
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
            if (payment == null)
            {
                return null;
            }
            var paymentVM = mapper.Map<PaymentViewVM>(payment);
            paymentVM.UserCreated = await GetUserName(paymentVM.UserCreated);
            paymentVM.UserModified = await GetUserName(paymentVM.UserModified);
            return paymentVM;
        }

        public async Task<PaymentFormVM?> GetToEditAsync(int? id)
        {
            var payment = await context.Payments
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
            if (payment == null)
            {
                return null;
            }
            var paymentVM = mapper.Map<PaymentFormVM>(payment);
            return paymentVM;
        }

        public async Task<List<PaymentViewVM>> GetAllAsync()
        {
            var payments = await context.Payments.ToListAsync();
            var paymentsVM = mapper.Map<List<PaymentViewVM>>(payments);
            return paymentsVM;
        }

        public async Task<PaymentFormVM> AddAsync(PaymentFormVM paymentVM)
        {
            var payment = mapper.Map<Payment>(paymentVM);
          //  payment.IsActive = true;
            await context.Payments.AddAsync(payment);
            await context.SaveChangesAsync(currentUser);
            return mapper.Map<PaymentFormVM>(payment);
        }

        public async Task<List<Payment>> GetAllActivePaymentAsync(int includeId)
        {
          //  return await context.Payments.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            return await context.Payments.Where(s => s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsIdExists(int id)
        {
            return await context.Payments.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> IsCodeExists(string code)
        {
            return await context.Payments.AnyAsync(s => s.CRNumber == code);
        }

        public async Task DeleteAsync(int id)
        {
            var payment = context.Payments.Where(s => s.Id == id).First();
            context.Payments.Remove(payment);
            await context.SaveChangesAsync(currentUser);
        }

        public async Task UpdateAsync(PaymentFormVM paymentVM)
        {
            var payment = await context.Payments.Where(s => s.Id == paymentVM.Id).FirstAsync();
            mapper.Map(paymentVM, payment);
            context.Payments.Update(payment);
            await context.SaveChangesAsync(currentUser);
        }

        public async Task<bool> IsInUse(int id)
        {
            return await context.CustomerPaymentDetails.AnyAsync(s => s.PaymentId == id)
                || await context.SOACRChecks.AnyAsync(s => s.PaymentId == id);
        }

        public async Task ReactivateAsync(int id)
        {
            var payment = await context.Payments.Where(s => s.Id == id).FirstAsync();
           // payment.IsActive = true;
            context.Payments.Update(payment);
            await context.SaveChangesAsync(currentUser);
        }

        public async Task DeactivateAsync(int id)
        {
            var payment = await context.Payments.Where(s => s.Id == id).FirstAsync();
           // payment.IsActive = false;
            context.Payments.Update(payment);
            await context.SaveChangesAsync(currentUser);
        }

        private async Task<string> GetUserName(string? userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user != null ? user.Nickname : "-";
        }
    }
}