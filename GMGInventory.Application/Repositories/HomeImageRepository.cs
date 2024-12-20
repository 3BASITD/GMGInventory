using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class HomeImageReporitory : IHomeImageRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeImageReporitory(ApplicationDbContext context
                                 , IHttpContextAccessor httpContextAccessor
                                 , UserManager<User> userManager
                                 , IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IList<HomeImageVM>> GetListAsync()
        {
            var images = from image in context.HomeImages
                         join userc in userManager.Users on image.UserCreated equals userc.Id
                         join userm in userManager.Users on image.UserModified equals userm.Id into m
                         from userm in m.DefaultIfEmpty()
                         let isInactive = image.DisplayOrder == 0
                         select new HomeImageVM
                         {
                             Id = image.Id,
                             File = image.File,
                             Title = image.Title,
                             Description = image.Description,
                             DisplayOrder = image.DisplayOrder,
                             DisplayTimeD = image.DisplayTime * 1000,
                             UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                             DateCreatedD = image.DateCreated,
                             UserModified = userm == null ? "-" : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                             DateModifiedD = image.DateModified
                         };
            return await images.ToListAsync();
        }

        public async Task<ForDataTables<HomeImageVM>> GetListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Title,
                    s.Description,
                    s.DisplayTime,
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
                if (form.SortColumn.ToLower().Contains("date"))
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "DateCreated": data = data.OrderBy(s => s.DisplayOrder == 0).ThenBy(s => s.DateCreatedD); break;
                            case "DateModified": data = data.OrderBy(s => s.DisplayOrder == 0).ThenBy(s => s.DateModifiedD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "DateCreated": data = data.OrderBy(s => s.DisplayOrder == 0).ThenByDescending(s => s.DateCreatedD); break;
                            case "DateModified": data = data.OrderBy(s => s.DisplayOrder == 0).ThenByDescending(s => s.DateModifiedD); break;
                            default: break;
                        }
                    }
                }
                else if (form.SortColumn.ToLower().Contains("displayorder"))
                {
                    if (form.SortColumnDirection == "asc")
                        data = data.OrderBy(s => s.DisplayOrder == 0 ? int.MaxValue : s.DisplayOrder);
                    else if (form.SortColumnDirection == "desc")
                        data = data.OrderByDescending(s => s.DisplayOrder == 0 ? int.MinValue : s.DisplayOrder);
                }
                else
                {
                    data = data.OrderBy(s => s.DisplayOrder == 0).ThenBy(form.SortColumn + " " + form.SortColumnDirection);
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<HomeImageVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ResponseModel> SaveAsync(HomeImageFormVM form)
        {
            var response = new ResponseModel();


            if (form.File == null || form.File.Length == 0)
            {
                response.Status = "Error";
                response.Message = "Invalid file";
            }
            else
            {
                var uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images\\HomeImages");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(form.File.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await form.File.CopyToAsync(fileStream);
                }

                var savedFile = Path.Combine("images\\HomeImages", uniqueFileName);

                var homeImage = await context.HomeImages.Where(s => s.Id == form.Id).FirstOrDefaultAsync();
                var lastHomeImage = await context.HomeImages.OrderByDescending(s => s.DisplayOrder).FirstOrDefaultAsync();
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                if (homeImage != null)
                {
                    var filePathDelete = Path.Combine(uploadsFolder, homeImage.File);

                    if (File.Exists(filePathDelete))
                        File.Delete(filePathDelete);

                    homeImage.File = uniqueFileName;
                    homeImage.Title = form.Title;
                    homeImage.Description = form.Description;
                    homeImage.DisplayTime = form.DisplayTime;
                    homeImage.DisplayOrder = lastHomeImage == null ? 1 : (lastHomeImage.DisplayOrder + 1);

                    context.HomeImages.Update(homeImage);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Home image successfully updated.";
                }
                else
                {
                    homeImage = new HomeImage
                    {
                        Id = 0,
                        File = uniqueFileName,
                        Title = form.Title,
                        Description = form.Description,
                        DisplayTime = form.DisplayTime,
                        DisplayOrder = lastHomeImage == null ? 1 : (lastHomeImage.DisplayOrder + 1)
                    };

                    await context.HomeImages.AddAsync(homeImage);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Home image successfully added.";
                }
            }
            return response;
        }

        public async Task<ResponseModel> DeleteAsync(int ImageId)
        {
            var response = new ResponseModel();
            var image = await context.HomeImages.Where(s => s.Id == ImageId && s.DisplayOrder != 0).FirstOrDefaultAsync();
            if (image == null)
            {
                response.Status = "Error";
                response.Message = "Image record is invalid";
            }
            else
            {
                var uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images\\HomeImages");
                var filePath = Path.Combine(uploadsFolder, image.File);
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                //if (File.Exists(filePath))
                //    File.Delete(filePath);

                //context.HomeImages.Remove(image);
                image.DisplayOrder = 0;
                context.HomeImages.Update(image);
                await context.SaveChangesAsync(currentUserId);

                response.Status = "Success";
                response.Message = "Image successfully deleted";
            }

            return response;
        }

        public async Task<ResponseModel> MoveUpAsync(int ImageId)
        {
            var response = new ResponseModel();
            var image = await context.HomeImages.Where(s => s.Id == ImageId).FirstOrDefaultAsync();
            if (image == null)
            {
                response.Status = "Error";
                response.Message = "Image record is invalid";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int currentOrder = image.DisplayOrder;

                var upperImage = await context.HomeImages.Where(s => s.DisplayOrder == currentOrder - 1 && s.DisplayOrder != 0).FirstOrDefaultAsync();
                if (upperImage != null)
                {
                    upperImage.DisplayOrder = currentOrder;
                    context.HomeImages.Update(upperImage);
                }
                image.DisplayOrder = currentOrder - 1;
                context.HomeImages.Update(image);
                await context.SaveChangesAsync(currentUserId);

                response.Status = "Success";
                response.Message = "Image successfully updated";
            }

            return response;
        }

        public async Task<ResponseModel> MoveDownAsync(int ImageId)
        {
            var response = new ResponseModel();
            var image = await context.HomeImages.Where(s => s.Id == ImageId).FirstOrDefaultAsync();
            if (image == null)
            {
                response.Status = "Error";
                response.Message = "Image record is invalid";
            }
            else
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
                int currentOrder = image.DisplayOrder;

                var lowerImage = await context.HomeImages.Where(s => s.DisplayOrder == currentOrder + 1 && s.DisplayOrder != 0).FirstOrDefaultAsync();
                if (lowerImage != null)
                {
                    lowerImage.DisplayOrder = currentOrder;
                    context.HomeImages.Update(lowerImage);
                }
                image.DisplayOrder = currentOrder + 1;
                context.HomeImages.Update(image);
                await context.SaveChangesAsync(currentUserId);

                response.Status = "Success";
                response.Message = "Image successfully updated";
            }

            return response;
        }
    }
}