using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GMGInventory.Application.Services
{
    public class APIHM_Service : IAPIHM_Service
    {
        private readonly HttpClient client;
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public APIHM_Service(HttpClient client
                           , ApplicationDbContext context
                           , IHttpContextAccessor httpContextAccessor
                           , UserManager<User> userManager)
        {
            this.client = client;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        private async Task<ResponseModel<APIHM_AccessTokenResponse>> AccessAsync(string host, string username, string password)
        {
            /*
             * POST Login V1
             * {{host}}/api/v1/auth/login
             */
            var responseLogin = await client.PostAsync(
                $"{host}/api/v1/auth/login",
                new StringContent(JsonSerializer.Serialize(new
                {
                    login = username,
                    password = password
                }), Encoding.UTF8, "application/json")
            );

            if (!responseLogin.IsSuccessStatusCode)
            {
                return new ResponseModel<APIHM_AccessTokenResponse>
                {
                    Status = "Error",
                    Message = "HASHMICRO/LOGIN: " + ((int)responseLogin.StatusCode) + " " + responseLogin.ReasonPhrase
                };
            }
            var jsonLogin = await responseLogin.Content.ReadAsStringAsync();
            var login = JsonSerializer.Deserialize<APIHM_LoginResponse>(jsonLogin)!;

            /*
             * GET OAuth Verifier
             * {{host}}/restapi/1.0/common/oauth1/authorize?oauth_consumer_key={{consumer_key}}&oauth_token={{oauth_token}}&oauth_token_secret={{oauth_token_secret}}
             */
            var responseAuthorize = await client.GetAsync(
                    $"{host}/restapi/1.0/common/oauth1/authorize" +
                    $"?oauth_consumer_key={login.token.consumer_key}" +
                    $"&oauth_token={login.token.oauth_token}" +
                    $"&oauth_token_secret={login.token.oauth_token_secret}");

            if (!responseAuthorize.IsSuccessStatusCode)
            {
                return new ResponseModel<APIHM_AccessTokenResponse>
                {
                    Status = "Error",
                    Message = "HASHMICRO/AUTHORIZE: " + ((int)responseAuthorize.StatusCode) + " " + responseAuthorize.ReasonPhrase
                };
            }
            var jsonAuthorize = await responseAuthorize.Content.ReadAsStringAsync();
            var authorize = JsonSerializer.Deserialize<APIHM_VerifierResponse>(jsonAuthorize)!;

            /*
             * POST Access Token
             * {{host}}/restapi/1.0/common/oauth1/access_token
             */
            static string Pct(string s) => Uri.EscapeDataString(s ?? string.Empty);
            var oauthSignature = $"{Pct(login.token.consumer_secret)}&{Pct(login.token.oauth_token_secret)}";

            var oauthNonce = Guid.NewGuid().ToString("N");
            var oauthTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            var form = new[]
            {
                new KeyValuePair<string,string>("oauth_consumer_key", login.token.consumer_key),
                new KeyValuePair<string,string>("oauth_token", authorize.oauth_token),
                new KeyValuePair<string,string>("oauth_signature_method", "PLAINTEXT"),
                new KeyValuePair<string,string>("oauth_timestamp", oauthTimestamp),
                new KeyValuePair<string,string>("oauth_nonce", oauthNonce),
                new KeyValuePair<string,string>("oauth_version", "1.0"),
                new KeyValuePair<string,string>("oauth_verifier", authorize.oauth_verifier),
                new KeyValuePair<string,string>("oauth_signature", oauthSignature)
            };

            var responseAccess = await client.PostAsync(
                $"{host}/restapi/1.0/common/oauth1/access_token",
                new FormUrlEncodedContent(form)
            );

            if (!responseAccess.IsSuccessStatusCode)
            {
                var body = await responseAccess.Content.ReadAsStringAsync();
                return new ResponseModel<APIHM_AccessTokenResponse>
                {
                    Status = "Error",
                    Message = "HASHMICRO/ACCESS: " + (int)responseAccess.StatusCode + " " + responseAccess.ReasonPhrase + " | " + body
                };
            }

            var jsonAccess = await responseAccess.Content.ReadAsStringAsync();
            var access = JsonSerializer.Deserialize<APIHM_AccessTokenResponse>(jsonAccess)!;

            return new ResponseModel<APIHM_AccessTokenResponse>
            {
                Status = "Success",
                Message = ((int)responseAccess.StatusCode) + " " + responseAccess.ReasonPhrase,
                NewRow = access
            };
        }

        public async Task<ResponseModel<string>> MoveSalesInvoice(int? Id, string Host, string User, string Password)
        {
            var access = await AccessAsync(Host, User, Password);

            if (access == null)
                return new ResponseModel<string> { Status = "Error", Message = "Cannot make connection to Hash Micro", NewRow = "Invoice Id: " + Id };

            if (access.Status == "Error")
                return new ResponseModel<string> { Status = access.Status, Message = access.Message, NewRow = "Invoice Id: " + Id };

            var invoice = await context.Invoices
                                       .Where(s => s.Id == Id)
                                       .Include(s => s.Status)
                                       .Include(s => s.Pack).ThenInclude(s => s.SalesOrder).ThenInclude(s => s.Customer)
                                       .Include(s => s.InvoiceLineItems).ThenInclude(s => s.Item)
                                       .FirstOrDefaultAsync();
            if (invoice == null)
                return new ResponseModel<string> { Status = "Error", Message = "Invalid Sales Invoice", NewRow = "Invoice Id: " + Id };

            if (invoice.Status.Code.ToLower() == "cancelled")
                return new ResponseModel<string> { Status = "Error", Message = "Cannot move cancelled invoice", NewRow = "Invoice Number: " + invoice.InvoiceNumber };

            if (invoice.HashMicroMoveId != null || invoice.HashMicroMoveName != null)
                return new ResponseModel<string> { Status = "Error", Message = "To avoid duplicates, moving to HashMicro can only be do once. Already moved with name " + invoice.HashMicroMoveName, NewRow = "Invoice Number: " + invoice.InvoiceNumber };

            var invoiceMoving = new
            {
                partner_code = invoice.Pack.SalesOrder.Customer.Code,
                partner_name = invoice.Pack.SalesOrder.Customer.Name,
                partner_country = "PH",
                invoice_date = invoice.InvoiceDate.ToString("yyyy-MM-dd"),
                invoice_date_due = (invoice.DueDate == null ? invoice.InvoiceDate : invoice.DueDate.Value).ToString("yyyy-MM-dd"),
                currency_code = "PHP",
                post = invoice.Status.Code.ToLower() == "invoiced",
                origin = "S/" + invoice.InvoiceNumber + "/" + invoice.ControlNumber,
                invoice_line_ids = from ili in invoice.InvoiceLineItems
                                   join sli in context.SalesOrderLineItems on new { ili.ItemId, ili.Invoice.Pack.SalesOrderId }
                                                                       equals new { sli.ItemId, sli.SalesOrderId }
                                   let discount1 = (100 - sli.Discount1) / 100
                                   let discount2 = (100 - sli.Discount2) / 100
                                   select new
                                   {
                                       product_code = ili.Item.Code,
                                       product_name = ili.Item.Name,
                                       quantity = ili.Quantity,
                                       price_unit = ili.UnitPrice,
                                       discount_amount = (ili.Quantity * ili.UnitPrice) - (ili.Quantity * ili.UnitPrice * discount1 * discount2)
                                   }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{Host}/api/accounting/create/invoice");
            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            request.Content = new StringContent(JsonSerializer.Serialize(invoiceMoving, jsonOptions), Encoding.UTF8, "application/json");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access.NewRow.oauth_token);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            if (json.Contains("\"error\""))
            {
                var errorResponse = JsonSerializer.Deserialize<APIHM_ErrorResponse>(json)!;
                return new ResponseModel<string> { Status = "Error", Message = $"Moving to HashMicro return an error with message \"{errorResponse.error.data.message}\"", NewRow = "Invoice Number: " + invoice.InvoiceNumber };
            }
            else
            {
                var successResponse = JsonSerializer.Deserialize<APIHM_SuccessResponse>(json)!;

                var currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                var invoiceUpdate = await context.Invoices.Where(s => s.Id == Id).FirstAsync();
                invoiceUpdate.HashMicroMoveId = successResponse.move_id;
                invoiceUpdate.HashMicroMoveName = successResponse.move_name;
                invoiceUpdate.HashMicroMoveStatus = invoice.Status.Code;
                invoiceUpdate.HashMicroMoveById = currentUserId;
                invoiceUpdate.HashMicroMoveDate = DateTime.Now;

                context.Invoices.Update(invoiceUpdate);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel<string> { Status = "Success", Message = "HashMicro: " + successResponse.message, NewRow = "Invoice Number: " + invoice.InvoiceNumber + "<br>Move Name: " + successResponse.move_name };
            }
        }

        public async Task<IList<ResponseModel<string>>> MoveAllPostedSalesInvoice(DateTime DateFrom, DateTime DateTo, string Host, string User, string Password)
        {
            var access = await AccessAsync(Host, User, Password);

            if (access == null)
                return new List<ResponseModel<string>> { new ResponseModel<string> { Status = "Error", Message = "Cannot make connection to Hash Micro", NewRow = "HashMicro Access" } };

            if (access.Status == "Error")
                return new List<ResponseModel<string>> { new ResponseModel<string> { Status = access.Status, Message = access.Message, NewRow = "HashMicro Access" } };

            var responses = new List<ResponseModel<string>>();
            var invoices = await context.Invoices
                                        .Where(s => s.Status.Code.ToLower() == "invoiced"
                                                 && DateFrom <= s.InvoiceDate.Date
                                                 && s.InvoiceDate.Date <= DateTo)
                                        .Include(s => s.Status)
                                        .Include(s => s.Pack).ThenInclude(s => s.SalesOrder).ThenInclude(s => s.Customer)
                                        .Include(s => s.InvoiceLineItems).ThenInclude(s => s.Item)
                                        .ToListAsync();
            foreach (var invoice in invoices)
            {
                var invoiceMoving = new
                {
                    partner_code = invoice.Pack.SalesOrder.Customer.Code,
                    partner_name = invoice.Pack.SalesOrder.Customer.Name,
                    partner_country = "PH",
                    invoice_date = invoice.InvoiceDate.ToString("yyyy-MM-dd"),
                    invoice_date_due = (invoice.DueDate == null ? invoice.InvoiceDate : invoice.DueDate.Value).ToString("yyyy-MM-dd"),
                    currency_code = "PHP",
                    post = invoice.Status.Code.ToLower() == "invoiced",
                    origin = "M/" + invoice.InvoiceNumber + "/" + invoice.ControlNumber,
                    invoice_line_ids = from ili in invoice.InvoiceLineItems
                                       join sli in context.SalesOrderLineItems on new { ili.ItemId, ili.Invoice.Pack.SalesOrderId }
                                                                           equals new { sli.ItemId, sli.SalesOrderId }
                                       let discount1 = (100 - sli.Discount1) / 100
                                       let discount2 = (100 - sli.Discount2) / 100
                                       select new
                                       {
                                           product_code = ili.Item.Code,
                                           product_name = ili.Item.Name,
                                           quantity = ili.Quantity,
                                           price_unit = ili.UnitPrice,
                                           discount_amount = (ili.Quantity * ili.UnitPrice) - (ili.Quantity * ili.UnitPrice * discount1 * discount2)
                                       }
                };

                var request = new HttpRequestMessage(HttpMethod.Post, $"{Host}/api/accounting/create/invoice");
                var jsonOptions = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true
                };
                request.Content = new StringContent(JsonSerializer.Serialize(invoiceMoving, jsonOptions), Encoding.UTF8, "application/json");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access.NewRow.oauth_token);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                if (json.Contains("\"error\""))
                {
                    var errorResponse = JsonSerializer.Deserialize<APIHM_ErrorResponse>(json)!;
                    responses.Add(new ResponseModel<string> { Status = "Error", Message = $"Moving to HashMicro return an error with message \"{errorResponse.error.data.message}\"", NewRow = "Invoice Number: " + invoice.InvoiceNumber });
                }
                else
                {
                    var successResponse = JsonSerializer.Deserialize<APIHM_SuccessResponse>(json)!;

                    var currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                    var invoiceUpdate = await context.Invoices.Where(s => s.Id == invoice.Id).FirstAsync();
                    invoiceUpdate.HashMicroMoveId = successResponse.move_id;
                    invoiceUpdate.HashMicroMoveName = successResponse.move_name;
                    invoiceUpdate.HashMicroMoveStatus = invoice.Status.Code;
                    invoiceUpdate.HashMicroMoveById = currentUserId;
                    invoiceUpdate.HashMicroMoveDate = DateTime.Now;

                    context.Invoices.Update(invoiceUpdate);
                    await context.SaveChangesAsync(currentUserId);

                    responses.Add(new ResponseModel<string> { Status = "Success", Message = "HashMicro: " + successResponse.message, NewRow = "Invoice Number: " + invoice.InvoiceNumber + "<br>Move Name: " + successResponse.move_name });
                }
            }
            return responses;
        }

        public async Task<ResponseModel<string>> MovePurchaseOrder(int? Id, string Host, string User, string Password)
        {
            var access = await AccessAsync(Host, User, Password);

            if (access == null)
                return new ResponseModel<string> { Status = "Error", Message = "Cannot make connection to Hash Micro", NewRow = "PO Id: " + Id };

            if (access.Status == "Error")
                return new ResponseModel<string> { Status = access.Status, Message = access.Message, NewRow = "PO Id: " + Id };

            var preceiving = await context.PurchaseReceivings
                                          .Where(s => s.Id == Id)
                                          .Include(s => s.Status)
                                          .Include(s => s.PurchaseOrder).ThenInclude(s => s.Supplier).ThenInclude(s => s.Country)
                                          .Include(s=>s.PurchaseOrder).ThenInclude(s=>s.Currency)
                                          .Include(s => s.PurchaseReceivingLineItems).ThenInclude(s => s.OrderItem).ThenInclude(s => s.RequestItem).ThenInclude(s => s.Item)
                                          .FirstOrDefaultAsync();
            if (preceiving == null)
                return new ResponseModel<string> { Status = "Error", Message = "Invalid Purchase Receiving", NewRow = "PO Id: " + Id };

            if (preceiving.Status.Code.ToLower() == "disapproved")
                return new ResponseModel<string> { Status = "Error", Message = "Cannot move disapproved purchase receiving", NewRow = "Document Number: " + preceiving.DocumentNumber };

            if (preceiving.Status.Code.ToLower() == "resubmitted")
                return new ResponseModel<string> { Status = "Error", Message = "Cannot move resubmitted purchase receiving", NewRow = "Document Number: " + preceiving.DocumentNumber };

            if (preceiving.HashMicroMoveId != null || preceiving.HashMicroMoveName != null)
                return new ResponseModel<string> { Status = "Error", Message = "To avoid duplicates, moving to HashMicro can only be do once. Already moved with name " + preceiving.HashMicroMoveName, NewRow = "Document Number: " + preceiving.DocumentNumber };

            var receivingMoving = new
            {
                partner_code = preceiving.PurchaseOrder.Supplier.Code,
                partner_name = preceiving.PurchaseOrder.Supplier.Name,
                partner_country = preceiving.PurchaseOrder.Supplier.Country.Alpha2,
                invoice_date = preceiving.ReceiveDate.ToString("yyyy-MM-dd"),
                invoice_date_due = preceiving.ReceiveDate.ToString("yyyy-MM-dd"),
                currency_code = preceiving.PurchaseOrder.Currency.Name,
                post = preceiving.Status.Code.ToLower() == "approved",
                origin = "S/" + preceiving.PurchaseOrder.DocumentNumber,
                type = "?",
                invoice_line_ids = from prli in preceiving.PurchaseReceivingLineItems
                                   join poli in context.PurchaseOrderNewLineItems on prli.OrderItemId equals poli.Id
                                   let discount1 = (100 - poli.PurchaseOrder.Discount1) / 100
                                   let discount2 = (100 - poli.PurchaseOrder.Discount2) / 100
                                   let discount3 = (100 - poli.PurchaseOrder.Discount3) / 100
                                   select new
                                   {
                                       product_code = prli.OrderItem.RequestItem.Item == null
                                                    ? "-"
                                                    : prli.OrderItem.RequestItem.Item.Code,
                                       product_name = prli.OrderItem.RequestItem.Item == null
                                                    ? prli.OrderItem.RequestItem.ItemDescription
                                                    : prli.OrderItem.RequestItem.Item.Name,
                                       quantity = prli.Quantity,
                                       price_unit = poli.UnitCost,
                                       discount_amount = (prli.Quantity * poli.UnitCost) - (prli.Quantity * poli.UnitCost * discount1 * discount2 * discount3)
                                   }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"{Host}/api/accounting/create/bill");
            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            request.Content = new StringContent(JsonSerializer.Serialize(receivingMoving, jsonOptions), Encoding.UTF8, "application/json");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access.NewRow.oauth_token);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            if (json.Contains("\"error\""))
            {
                var errorResponse = JsonSerializer.Deserialize<APIHM_ErrorResponse>(json)!;
                return new ResponseModel<string> { Status = "Error", Message = $"Moving to HashMicro return an error with message \"{errorResponse.error.data.message}\"", NewRow = "Document Number: " + preceiving.DocumentNumber };
            }
            else
            {
                var successResponse = JsonSerializer.Deserialize<APIHM_SuccessResponse>(json)!;

                var currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                var receivingUpdate = await context.PurchaseReceivings.Where(s => s.Id == Id).FirstAsync();
                receivingUpdate.HashMicroMoveId = successResponse.move_id;
                receivingUpdate.HashMicroMoveName = successResponse.move_name;
                receivingUpdate.HashMicroMoveStatus = preceiving.Status.Code;
                receivingUpdate.HashMicroMoveById = currentUserId;
                receivingUpdate.HashMicroMoveDate = DateTime.Now;

                context.PurchaseReceivings.Update(receivingUpdate);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel<string> { Status = "Success", Message = "HashMicro: " + successResponse.message, NewRow = "Document Number: " + preceiving.DocumentNumber + "<br>Move Name: " + successResponse.move_name };
            }
        }

        public async Task<IList<ResponseModel<string>>> MoveAllPostedPurchaseOrder(DateTime DateFrom, DateTime DateTo, string Host, string User, string Password)
        {
            var access = await AccessAsync(Host, User, Password);

            if (access == null)
                return new List<ResponseModel<string>> { new ResponseModel<string> { Status = "Error", Message = "Cannot make connection to Hash Micro", NewRow = "HashMicro Access" } };

            if (access.Status == "Error")
                return new List<ResponseModel<string>> { new ResponseModel<string> { Status = access.Status, Message = access.Message, NewRow = "HashMicro Access" } };

            var responses = new List<ResponseModel<string>>();

            var preceivings = await context.PurchaseReceivings
                                          .Where(s => s.Status.Code.ToLower() == "approved"
                                                   && DateFrom <= s.ReceiveDate.Date
                                                   && s.ReceiveDate.Date <= DateTo)
                                          .Include(s => s.Status)
                                          .Include(s => s.PurchaseOrder).ThenInclude(s => s.Supplier)
                                          .Include(s => s.PurchaseReceivingLineItems).ThenInclude(s => s.OrderItem).ThenInclude(s => s.RequestItem).ThenInclude(s => s.Item)
                                          .ToListAsync();
            foreach (var preceiving in preceivings)
            {
                var receivingMoving = new
                {
                    partner_code = preceiving.PurchaseOrder.Supplier.Code,
                    partner_name = preceiving.PurchaseOrder.Supplier.Name,
                    partner_country = preceiving.PurchaseOrder.Supplier.Country.Alpha2,
                    invoice_date = preceiving.ReceiveDate.ToString("yyyy-MM-dd"),
                    invoice_date_due = preceiving.ReceiveDate.ToString("yyyy-MM-dd"),
                    currency_code = preceiving.PurchaseOrder.Currency.Name,
                    post = preceiving.Status.Code.ToLower() == "approved",
                    origin = "S/" + preceiving.PurchaseOrder.DocumentNumber,
                    type = "?",
                    invoice_line_ids = from prli in preceiving.PurchaseReceivingLineItems
                                       join poli in context.PurchaseOrderNewLineItems on prli.OrderItemId equals poli.Id
                                       let discount1 = (100 - poli.PurchaseOrder.Discount1) / 100
                                       let discount2 = (100 - poli.PurchaseOrder.Discount2) / 100
                                       let discount3 = (100 - poli.PurchaseOrder.Discount3) / 100
                                       select new
                                       {
                                           product_code = prli.OrderItem.RequestItem.Item == null
                                                        ? "-"
                                                        : prli.OrderItem.RequestItem.Item.Code,
                                           product_name = prli.OrderItem.RequestItem.Item == null
                                                        ? prli.OrderItem.RequestItem.ItemDescription
                                                        : prli.OrderItem.RequestItem.Item.Name,
                                           quantity = prli.Quantity,
                                           price_unit = poli.UnitCost,
                                           discount_amount = (prli.Quantity * poli.UnitCost) - (prli.Quantity * poli.UnitCost * discount1 * discount2 * discount3)
                                       }
                };

                var request = new HttpRequestMessage(HttpMethod.Post, $"{Host}/api/accounting/create/bill");
                var jsonOptions = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true
                };
                request.Content = new StringContent(JsonSerializer.Serialize(receivingMoving, jsonOptions), Encoding.UTF8, "application/json");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access.NewRow.oauth_token);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                if (json.Contains("\"error\""))
                {
                    var errorResponse = JsonSerializer.Deserialize<APIHM_ErrorResponse>(json)!;
                    responses.Add(new ResponseModel<string> { Status = "Error", Message = $"Moving to HashMicro return an error with message \"{errorResponse.error.data.message}\"", NewRow = "Document Number: " + preceiving.DocumentNumber });
                }
                else
                {
                    var successResponse = JsonSerializer.Deserialize<APIHM_SuccessResponse>(json)!;

                    var currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                    var receivingUpdate = await context.PurchaseReceivings.Where(s => s.Id == preceiving.Id).FirstAsync();
                    receivingUpdate.HashMicroMoveId = successResponse.move_id;
                    receivingUpdate.HashMicroMoveName = successResponse.move_name;
                    receivingUpdate.HashMicroMoveStatus = preceiving.Status.Code;
                    receivingUpdate.HashMicroMoveById = currentUserId;
                    receivingUpdate.HashMicroMoveDate = DateTime.Now;

                    context.PurchaseReceivings.Update(receivingUpdate);
                    await context.SaveChangesAsync(currentUserId);

                    responses.Add(new ResponseModel<string> { Status = "Success", Message = "HashMicro: " + successResponse.message, NewRow = "Document Number: " + preceiving.DocumentNumber + "<br>Move Name: " + successResponse.move_name });
                }
            }
            return responses;
        }

        public async Task<ResponseModel<APIHM_SuccessResponse>> GetTaxes(string Host, string User, string Password)
        {
            var access = await AccessAsync(Host, User, Password);

            if (access == null)
                return new ResponseModel<APIHM_SuccessResponse> { Status = "Error", Message = "Cannot make connection to Hash Micro" };

            if (access.Status == "Error")
                return new ResponseModel<APIHM_SuccessResponse> { Status = access.Status, Message = access.Message };

            var request = new HttpRequestMessage(HttpMethod.Get, $"{Host}/api/accounting/get/tax");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access.NewRow.oauth_token);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            if (json.Contains("\"error\""))
            {
                var errorResponse = JsonSerializer.Deserialize<APIHM_ErrorResponse>(json)!;
                return new ResponseModel<APIHM_SuccessResponse> { Status = "Error", Message = $"Moving to HashMicro return an error with message \"{errorResponse.error.data.message}\"" };
            }
            else
            {
                var successResponse = JsonSerializer.Deserialize<APIHM_SuccessResponse>(json)!;

                return new ResponseModel<APIHM_SuccessResponse> { Status = "Success", Message = "HashMicro: " + successResponse.message, NewRow = successResponse };
            }
        }

        public async Task<ResponseModel<APIHM_SuccessResponse>> GetCurrencies(string Host, string User, string Password)
        {
            var access = await AccessAsync(Host, User, Password);

            if (access == null)
                return new ResponseModel<APIHM_SuccessResponse> { Status = "Error", Message = "Cannot make connection to Hash Micro" };

            if (access.Status == "Error")
                return new ResponseModel<APIHM_SuccessResponse> { Status = access.Status, Message = access.Message };

            var request = new HttpRequestMessage(HttpMethod.Get, $"{Host}/api/accounting/get/currency");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access.NewRow.oauth_token);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            if (json.Contains("\"error\""))
            {
                var errorResponse = JsonSerializer.Deserialize<APIHM_ErrorResponse>(json)!;
                return new ResponseModel<APIHM_SuccessResponse> { Status = "Error", Message = $"Moving to HashMicro return an error with message \"{errorResponse.error.data.message}\"" };
            }
            else
            {
                var successResponse = JsonSerializer.Deserialize<APIHM_SuccessResponse>(json)!;

                return new ResponseModel<APIHM_SuccessResponse> { Status = "Success", Message = "HashMicro: " + successResponse.message, NewRow = successResponse };
            }
        }
    }
}