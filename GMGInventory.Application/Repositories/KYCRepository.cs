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
    public class KYCRepository : IKYCRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public KYCRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        private async Task<IList<KYCModel>> GetKYCs()
        {
            var kycs = from customer in context.Customers
                       join kyc in context.KYCs on customer.Id equals kyc.CustomerId into gk
                       from kyc in gk.DefaultIfEmpty()
                       join userc in userManager.Users on kyc.UserCreated equals userc.Id into uc
                       from userc in uc.DefaultIfEmpty()
                       join userm in userManager.Users on kyc.UserModified equals userm.Id into um
                       from userm in um.DefaultIfEmpty()
                       select new KYCModel
                       {
                           CustomerId = customer.Id,
                           CustomerCode = customer.Code,
                           CustomerName = customer.Name,
                           Status = kyc == null ? null : kyc.Status,
                           LatestActionBy = kyc == null ? null
                                          : userm == null ? $"{userc.FirstName} {userc.LastName}"
                                          : $"{userm.FirstName} {userm.LastName}",
                           LatestActionDateD = kyc == null ? null
                                             : kyc.DateModified == null ? kyc.DateCreated
                                             : kyc.DateModified,
                           AdditionalInfo = null
                       };
            return await kycs.ToListAsync();
        }

        public async Task<ForDataTables<KYCModel>> GetKYCsDT(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetKYCs()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.CustomerCode,
                    s.CustomerName,
                    s.Status,
                    s.LatestActionBy,
                    s.LatestActionDate,
                    s.AdditionalInfo
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "LatestActionDate" => isAsc ? data.OrderBy(s => s.LatestActionDateD == null).ThenBy(s => s.LatestActionDateD)
                                        : data.OrderBy(s => s.LatestActionDateD == null).OrderByDescending(s => s.LatestActionDateD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pagedList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<KYCModel>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pagedList
            };
        }

        public async Task<KYCVM> GetDetails(int CustomerId)
        {

            var customer = await context.Customers
                                        .Where(c => c.Id == CustomerId)
                                        .FirstOrDefaultAsync();

            var kyc = await context.KYCs
                                   .Where(s => s.CustomerId == CustomerId)
                                   .FirstOrDefaultAsync();
            if (customer == null)
            {
                kyc = new KYC();
            }
            else if (kyc == null)
            {
                var tempCreditTerm = await context.CreditTerms.FirstAsync();

                kyc = new KYC
                {
                    CustomerId = CustomerId,
                    CompanyName = string.Empty,
                    RegisteredAddress = string.Empty,
                    OperatingAddress = string.Empty,
                    //WarehouseAddress = string.Empty,
                    //OperatingHours = string.Empty,
                    //Website = null,
                    Email = string.Empty,
                    ContactNumber = string.Empty,
                    OrganizationType = 1,
                    SecDtiNumber = string.Empty,
                    BirTinNumber = string.Empty,
                    VatStatus = true,
                    BusinessNature = string.Empty,
                    ProductServiceOffered = string.Empty,
                    CreditLineAmount = 0,
                    CreditTermId = tempCreditTerm.Id,
                    Status = "Draft"
                };

                await context.KYCs.AddAsync(kyc);
                await context.SaveChangesAsync(currentUserId);
            }

            var creditTerm = await context.CreditTerms
                                          .Where(ct => ct.Id == kyc.CreditTermId)
                                          .FirstOrDefaultAsync();

            var submittedby = await userManager.Users
                                               .Where(u => u.Id == kyc.SubmitterId)
                                               .FirstOrDefaultAsync();

            var approvedby = await userManager.Users
                                              .Where(u => u.Id == kyc.ApproverId)
                                              .FirstOrDefaultAsync();

            var disapprovedby = await userManager.Users
                                                 .Where(u => u.Id == kyc.DisapproverId)
                                                 .FirstOrDefaultAsync();

            var createdby = await userManager.Users
                                             .Where(c => c.Id == kyc.UserCreated)
                                             .FirstOrDefaultAsync();

            var modifiedby = await userManager.Users
                                              .Where(m => m.Id == kyc.UserModified)
                                              .FirstOrDefaultAsync();

            var kycDetails = new KYCVM
            {
                Id = kyc.Id,

                CustomerId = CustomerId,
                CustomerCode = customer == null ? "Invalid Customer" : customer.Code,
                CustomerName = customer == null ? "Invalid Customer" : customer.Name,
                Status = kyc.Status ?? "Draft",
                Remarks = kyc.Remarks,

                NameOfCompany = kyc.CompanyName,
                RegisteredAddress = kyc.RegisteredAddress,
                OperatingAddress = kyc.OperatingAddress,
                WarehouseAddress = kyc.WarehouseAddress,
                OperatingHours = kyc.OperatingHours,
                Website = kyc.Website,
                EmailAddress = kyc.Email,
                ContactNumber = kyc.ContactNumber,

                OrganizationTypeD = kyc.OrganizationType,
                SecDtiNumber = kyc.SecDtiNumber,
                BirTinNumber = kyc.BirTinNumber,
                VatStatusD = kyc.VatStatus,

                BusinessNature = kyc.BusinessNature,
                ProductServiceOffered = kyc.ProductServiceOffered,

                CreditLineAmountD = kyc.CreditLineAmount,
                CreditTerm = creditTerm == null ? null : creditTerm.Name,
                CreditTermId = creditTerm == null ? null : creditTerm.Id,

                BIRForm2303D = kyc.ExpirationBIRForm2303,
                MayorBusinessPermitD = kyc.ExpirationMayorBusinessPermit,
                SecDtiCertificateD = kyc.ExpirationSECDTICertificate,
                LatestAuditedFinancialStatementD = kyc.ExpirationLatestAuditedFinancialStatement,
                IncorporationArticleByLawsD = kyc.ExpirationIncorporationArticleByLaws,
                SecretaryCertificateD = kyc.ExpirationSecretaryCertificate,
                CompanyPortfolioD = kyc.ExpirationCompanyPortfolio,
                SignatoriesIdentificationCardD = kyc.ExpirationSignatoriesIdentificationCard,
                SampleSalesInvoiceOfficialReceiptD = kyc.ExpirationSampleSalesInvoiceOfficialReceipt,

                PrivacyConsentD = kyc.PrivacyConsent,

                SubmittedBy = submittedby == null ? null : submittedby.FirstName + " " + submittedby.LastName,
                SubmittedDateD = kyc.DateSubmitted,

                ApprovedBy = approvedby == null ? null : approvedby.FirstName + " " + approvedby.LastName,
                ApprovedDateD = kyc.DateApproved,

                DisapprovedBy = disapprovedby == null ? null : disapprovedby.FirstName + " " + disapprovedby.LastName,
                DisapprovedDateD = kyc.DateDisapproved,
                DisapprovedReason = kyc.DisapprovedReason,

                ModifiedBy = modifiedby == null ? null : modifiedby.FirstName + " " + modifiedby.LastName,
                ModifiedDateD = kyc.DateModified
            };

            return kycDetails;
        }

        public async Task<ResponseModel> SaveDetails(KYCParam param)
        {
            try
            {
                var customer = await context.Customers
                                            .Where(s => s.Id == param.CustomerId)
                                            .FirstOrDefaultAsync();

                if (customer == null) return new ResponseModel { Status = "Error", Message = $"Invalid Customer: {param.CustomerId} not found" };

                var required = new List<string>();
                if (string.IsNullOrWhiteSpace(param.NameOfCompany)) required.Add("Name of Company");
                if (string.IsNullOrWhiteSpace(param.RegisteredAddress)) required.Add("Registered Address");
                if (string.IsNullOrWhiteSpace(param.OperatingAddress)) required.Add("Operating Address");
                if (string.IsNullOrWhiteSpace(param.EmailAddress)) required.Add("Email Address");
                if (string.IsNullOrWhiteSpace(param.ContactNumber)) required.Add("Contact Number");
                if (string.IsNullOrWhiteSpace(param.SecDtiNumber)) required.Add("SEC / DTI Number");
                if (string.IsNullOrWhiteSpace(param.BirTinNumber)) required.Add("BIR TIN Number");
                if (string.IsNullOrWhiteSpace(param.BusinessNature)) required.Add("Business Nature");
                if (string.IsNullOrWhiteSpace(param.ProductServiceOffered)) required.Add("Product / Service Offered");
                if (param.CreditLineAmount == null) required.Add("Credit Line Amount");
                if (param.OrganizationType == null) required.Add("Organization Type");
                if (required.Count > 0) return new ResponseModel { Status = "Error", Message = $"Following fields are required:<ul><li>{string.Join("</li><li>", required)}</li></ul>" };

                var kyc = await context.KYCs
                                       .Where(s => s.CustomerId == param.CustomerId)
                                       .FirstOrDefaultAsync();

                var creditTerm = await context.CreditTerms
                                              .Where(s => s.Id == param.CreditTermId)
                                              .FirstOrDefaultAsync();

                if (creditTerm == null) return new ResponseModel { Status = "Error", Message = $"Invalid Credit Term: {param.CreditTermId} not found" };

                if (kyc == null)
                {
                    kyc = new KYC
                    {
                        CustomerId = param.CustomerId
                    };

                    await context.KYCs.AddAsync(kyc);
                }

                kyc.Remarks = param.Remarks;
                kyc.CompanyName = param.NameOfCompany!;
                kyc.RegisteredAddress = param.RegisteredAddress!;
                kyc.OperatingAddress = param.OperatingAddress!;
                kyc.WarehouseAddress = param.WarehouseAddress!;
                kyc.OperatingHours = param.OperatingHours;
                kyc.Website = param.Website;
                kyc.Email = param.EmailAddress!;
                kyc.ContactNumber = param.ContactNumber!;
                kyc.OrganizationType = param.OrganizationType!.Value;
                kyc.SecDtiNumber = param.SecDtiNumber!;
                kyc.BirTinNumber = param.BirTinNumber!;
                kyc.VatStatus = param.VatStatus;
                kyc.BusinessNature = param.BusinessNature!;
                kyc.ProductServiceOffered = param.ProductServiceOffered!;
                kyc.CreditLineAmount = param.CreditLineAmount ?? 0;
                kyc.CreditTermId = creditTerm!.Id;
                kyc.ExpirationBIRForm2303 = param.BIRForm2303;
                kyc.ExpirationMayorBusinessPermit = param.MayorBusinessPermit;
                kyc.ExpirationSECDTICertificate = param.SecDtiCertificate;
                kyc.ExpirationLatestAuditedFinancialStatement = param.LatestAuditedFinancialStatement;
                kyc.ExpirationIncorporationArticleByLaws = param.IncorporationArticleByLaws;
                kyc.ExpirationSecretaryCertificate = param.SecretaryCertificate;
                kyc.ExpirationCompanyPortfolio = param.CompanyPortfolio;
                kyc.ExpirationSignatoriesIdentificationCard = param.SignatoriesIdentificationCard;
                kyc.ExpirationSampleSalesInvoiceOfficialReceipt = param.SampleSalesInvoiceOfficialReceipt;
                kyc.PrivacyConsent = param.PrivacyConsent;
                if (param.Status == "Draft")
                {
                    kyc.Status = "Draft";
                }
                else if (param.Status == "Submitted")
                {
                    kyc.Status = "Submitted";
                    kyc.SubmitterId = currentUserId;
                    kyc.DateSubmitted = DateTime.Now;
                    kyc.ApproverId = null;
                    kyc.DateApproved = null;
                    kyc.DisapproverId = null;
                    kyc.DateDisapproved = null;
                    kyc.DisapprovedReason = null;
                }

                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = $"KYC successfully saved" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = $"Exception: {SystemFormat.GetInnermostExceptionMessage(ex)}" };
            }
        }

        public async Task<ResponseModel> ApproveDetails(int CustomerId)
        {
            try
            {
                var customer = await context.Customers
                                            .Where(s => s.Id == CustomerId)
                                            .FirstOrDefaultAsync();

                if (customer == null) return new ResponseModel { Status = "Error", Message = $"Invalid Customer: {CustomerId} not found" };

                var kyc = await context.KYCs
                                       .Where(s => s.CustomerId == CustomerId)
                                       .FirstOrDefaultAsync();

                if (kyc == null) return new ResponseModel { Status = "Error", Message = $"Invalid kyc. Please reload the page" };

                if (kyc.Status != "Submitted") return new ResponseModel { Status = "Error", Message = $"KYC should be submitted before approving" };
                if (kyc.Status == "Approved") return new ResponseModel { Status = "Error", Message = $"KYC already approved" };

                kyc.Status = "Approved";
                kyc.ApproverId = currentUserId;
                kyc.DateApproved = DateTime.Now;
                kyc.DisapproverId = null;
                kyc.DateDisapproved = null;
                kyc.DisapprovedReason = null;

                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = $"KYC successfully approved" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = $"Exception: {SystemFormat.GetInnermostExceptionMessage(ex)}" };
            }
        }

        public async Task<ResponseModel> DisapproveDetails(int CustomerId, string Reason)
        {
            try
            {
                var customer = await context.Customers
                                            .Where(s => s.Id == CustomerId)
                                            .FirstOrDefaultAsync();

                if (customer == null) return new ResponseModel { Status = "Error", Message = $"Invalid Customer: {CustomerId} not found" };

                var kyc = await context.KYCs
                                       .Where(s => s.CustomerId == CustomerId)
                                       .FirstOrDefaultAsync();

                if (kyc == null) return new ResponseModel { Status = "Error", Message = $"Invalid kyc. Please reload the page" };

                if (kyc.Status != "Submitted") return new ResponseModel { Status = "Error", Message = $"KYC should be submitted before disapproving" };
                if (kyc.Status == "Disapproved") return new ResponseModel { Status = "Error", Message = $"KYC already disapproved" };

                kyc.Status = "Disapproved";
                kyc.ApproverId = null;
                kyc.DateApproved = null;
                kyc.DisapproverId = currentUserId;
                kyc.DateDisapproved = DateTime.Now;
                kyc.DisapprovedReason = Reason;

                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = $"KYC successfully disapproved" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = $"Exception: {SystemFormat.GetInnermostExceptionMessage(ex)}" };
            }
        }

        public async Task<ResponseModel> EditDetails(int CustomerId)
        {
            try
            {
                var customer = await context.Customers
                                            .Where(s => s.Id == CustomerId)
                                            .FirstOrDefaultAsync();

                if (customer == null) return new ResponseModel { Status = "Error", Message = $"Invalid Customer: {CustomerId} not found" };

                var kyc = await context.KYCs
                                       .Where(s => s.CustomerId == CustomerId)
                                       .FirstOrDefaultAsync();

                if (kyc == null) return new ResponseModel { Status = "Error", Message = $"Invalid kyc. Please reload the page" };

                if (kyc.Status != "Approved" && kyc.Status != "Disapproved")
                    return new ResponseModel { Status = "Error", Message = $"KYC should be approved / disapproved before updating" };
                if (kyc.Status == "Draft") return new ResponseModel { Status = "Error", Message = $"KYC already disapproved" };

                kyc.Status = "Draft";

                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = $"KYC can now be updated" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = $"Exception: {SystemFormat.GetInnermostExceptionMessage(ex)}" };
            }
        }

        private async Task<IList<KYCSignatoryModel>> GetKYCSignatories(int CustomerId)
        {
            var signatories = from signatory in context.KYCSignatories
                              where signatory.KYC.CustomerId == CustomerId
                              select new KYCSignatoryModel
                              {
                                  Id = signatory.Id,
                                  Name = signatory.Name,
                                  Designation = signatory.Designation,
                                  MobileNumber = signatory.ContactNumber,
                                  EmailAddress = signatory.Email
                              };
            return await signatories.ToListAsync();
        }

        public async Task<ForDataTables<KYCSignatoryModel>> GetKYCSignatoriesDT(DataTablesRequestForm form, int CustomerId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetKYCSignatories(CustomerId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            //
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            //
            return new ForDataTables<KYCSignatoryModel>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = data.ToList()
            };
        }

        public async Task<ResponseModel> SaveSignatory(KYCSignatoryModel param)
        {
            try
            {
                if (param.KYCId == null) return new ResponseModel { Status = "Error", Message = "KYC is required" };
                if (string.IsNullOrWhiteSpace(param.Name)) return new ResponseModel { Status = "Error", Message = "Name is required" };
                if (string.IsNullOrWhiteSpace(param.Designation)) return new ResponseModel { Status = "Error", Message = "Designation is required" };
                if (string.IsNullOrWhiteSpace(param.MobileNumber)) return new ResponseModel { Status = "Error", Message = "Mobile number is required" };
                if (string.IsNullOrWhiteSpace(param.EmailAddress)) return new ResponseModel { Status = "Error", Message = "Email address is required" };

                var isNew = false;
                var signatory = await context.KYCSignatories
                                             .Where(s => s.Id == param.Id)
                                             .FirstOrDefaultAsync();

                if (signatory != null && signatory.KYCId != param.KYCId) return new ResponseModel { Status = "Error", Message = "Saving key personnel & authorized signatory for the wrong kyc" };

                if (signatory == null)
                {
                    signatory = new KYCSignatory();
                    signatory.KYCId = param.KYCId.Value;
                    await context.KYCSignatories.AddAsync(signatory);
                    isNew = true;
                }
                signatory.Name = param.Name;
                signatory.Designation = param.Designation;
                signatory.ContactNumber = param.MobileNumber;
                signatory.Email = param.EmailAddress;

                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = $"Successfully {(isNew ? "added" : "updated")} key personnel & authorized signatory" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = $"Exception: {SystemFormat.GetInnermostExceptionMessage(ex)}" };
            }
        }

        public async Task<ResponseModel> DeleteSignatory(int SignatoryId, int KYCId)
        {
            try
            {
                var signatory = await context.KYCSignatories
                                             .Where(s => s.Id == SignatoryId)
                                             .FirstOrDefaultAsync();

                if (signatory != null && signatory.KYCId != KYCId) return new ResponseModel { Status = "Error", Message = "Deleting key personnel & authorized signatory for the wrong kyc" };

                if (signatory == null) return new ResponseModel { Status = "Error", Message = $"Invalid key personnel & authorized signatory. {SignatoryId} not found" };

                context.KYCSignatories.Remove(signatory);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = $"Successfully deleted key personnel & authorized signatory" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = $"Exception: {SystemFormat.GetInnermostExceptionMessage(ex)}" };
            }
        }

        private async Task<IList<KYCClientModel>> GetKYCClients(int CustomerId)
        {
            var clients = from client in context.KYCClients
                          where client.KYC.CustomerId == CustomerId
                          select new KYCClientModel
                          {
                              Id = client.Id,
                              Name = client.Name,
                              ContactPerson = client.ContactPerson,
                              ContactNumber = client.ContactNumber,
                              SalesVolumeD = client.SalesVolume
                          };
            return await clients.ToListAsync();
        }

        public async Task<ForDataTables<KYCClientModel>> GetKYCClientsDT(DataTablesRequestForm form, int CustomerId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetKYCClients(CustomerId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            //
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "SalesVolume" => isAsc ? data.OrderBy(s => s.SalesVolumeD) : data.OrderByDescending(s => s.SalesVolumeD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            //
            return new ForDataTables<KYCClientModel>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = data.ToList()
            };
        }

        public async Task<ResponseModel> SaveClient(KYCClientModel param)
        {
            try
            {
                if (param.KYCId == null) return new ResponseModel { Status = "Error", Message = "KYC is required" };
                if (string.IsNullOrWhiteSpace(param.Name)) return new ResponseModel { Status = "Error", Message = "Name is required" };

                var isNew = false;
                var client = await context.KYCClients
                                          .Where(s => s.Id == param.Id)
                                          .FirstOrDefaultAsync();

                if (client != null && client.KYCId != param.KYCId) return new ResponseModel { Status = "Error", Message = "Saving major client for the wrong kyc" };

                if (client == null)
                {
                    client = new KYCClient();
                    client.KYCId = param.KYCId.Value;
                    await context.KYCClients.AddAsync(client);
                    isNew = true;
                }
                client.Name = param.Name;
                client.ContactPerson = param.ContactPerson;
                client.ContactNumber = param.ContactNumber;
                client.SalesVolume = param.SalesVolumeD;

                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = $"Successfully {(isNew ? "added" : "updated")} major client" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = $"Exception: {SystemFormat.GetInnermostExceptionMessage(ex)}" };
            }
        }

        public async Task<ResponseModel> DeleteClient(int ClientId, int KYCId)
        {
            try
            {
                var client = await context.KYCClients
                                          .Where(s => s.Id == ClientId)
                                          .FirstOrDefaultAsync();

                if (client != null && client.KYCId != KYCId) return new ResponseModel { Status = "Error", Message = "Deleting major client for the wrong kyc" };

                if (client == null) return new ResponseModel { Status = "Error", Message = $"Invalid major client. {ClientId} not found" };

                context.KYCClients.Remove(client);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = $"Successfully deleted major client" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = $"Exception: {SystemFormat.GetInnermostExceptionMessage(ex)}" };
            }
        }

        private async Task<IList<KYCBankModel>> GetKYCBanks(int CustomerId)
        {
            var banks = from bank in context.KYCBanks
                        where bank.KYC.CustomerId == CustomerId
                        select new KYCBankModel
                        {
                            Id = bank.Id,
                            Name = bank.Name,
                            Branch = bank.Branch,
                            AccountNumber = bank.AccountNumber,
                            ContactNumber = bank.ContactNumber
                        };
            return await banks.ToListAsync();
        }

        public async Task<ForDataTables<KYCBankModel>> GetKYCBanksDT(DataTablesRequestForm form, int CustomerId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetKYCBanks(CustomerId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            //
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            //
            return new ForDataTables<KYCBankModel>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = data.ToList()
            };
        }

        public async Task<ResponseModel> SaveBank(KYCBankModel param)
        {
            try
            {
                if (param.KYCId == null) return new ResponseModel { Status = "Error", Message = "KYC is required" };
                if (string.IsNullOrWhiteSpace(param.Name)) return new ResponseModel { Status = "Error", Message = "Name is required" };
                if (string.IsNullOrWhiteSpace(param.Branch)) return new ResponseModel { Status = "Error", Message = "Branch is required" };
                if (string.IsNullOrWhiteSpace(param.AccountNumber)) return new ResponseModel { Status = "Error", Message = "Account number is required" };
                if (string.IsNullOrWhiteSpace(param.ContactNumber)) return new ResponseModel { Status = "Error", Message = "Contact number is required" };

                var isNew = false;
                var bank = await context.KYCBanks
                                        .Where(s => s.Id == param.Id)
                                        .FirstOrDefaultAsync();

                if (bank != null && bank.KYCId != param.KYCId) return new ResponseModel { Status = "Error", Message = "Saving bank reference for the wrong kyc" };

                if (bank == null)
                {
                    bank = new KYCBank();
                    bank.KYCId = param.KYCId.Value;
                    await context.KYCBanks.AddAsync(bank);
                    isNew = true;
                }
                bank.Name = param.Name;
                bank.Branch = param.Branch;
                bank.AccountNumber = param.AccountNumber;
                bank.ContactNumber = param.ContactNumber;

                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = $"Successfully {(isNew ? "added" : "updated")} bank reference" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = $"Exception: {SystemFormat.GetInnermostExceptionMessage(ex)}" };
            }
        }

        public async Task<ResponseModel> DeleteBank(int BankId, int KYCId)
        {
            try
            {
                var bank = await context.KYCBanks
                                        .Where(s => s.Id == BankId)
                                        .FirstOrDefaultAsync();

                if (bank != null && bank.KYCId != KYCId) return new ResponseModel { Status = "Error", Message = "Deleting bank reference for the wrong kyc" };

                if (bank == null) return new ResponseModel { Status = "Error", Message = $"Invalid bank reference. {BankId} not found" };

                context.KYCBanks.Remove(bank);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = $"Successfully deleted bank reference" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = $"Exception: {SystemFormat.GetInnermostExceptionMessage(ex)}" };
            }
        }
    }
}