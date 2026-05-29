using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class SystemSettingRepository : ISystemSettingRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly string currentUser;
        private readonly IHostingEnvironment hostingEnvironment;

        public SystemSettingRepository(ApplicationDbContext context
                                      , IHttpContextAccessor httpContextAccessor
                                      , UserManager<User> userManager
                                      , IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
            this.currentUser = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<IList<SystemSettingVM>> GetSystemSettings()
        {
            var settings = from setting in context.SystemSettings
                           select new SystemSettingVM
                           {
                               Name = setting.Name,
                               Value = setting.Value
                           };
            return await settings.ToListAsync();
        }

        public async Task<ResponseModel> UpdateSettings(SystemSettingsVM ssettings)
        {
            try
            {
                foreach (var setting in ssettings.Settings)
                {
                    await SaveSettings(setting.Name, setting.Value);
                }

                return new ResponseModel { Status = "Success", Message = "Successfully saved " + ssettings.Name.ToLower() };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> UpdateSetting(string Name, string Value)
        {
            try
            {
                await SaveSettings(Name, Value);

                return new ResponseModel { Status = "Success", Message = "Successfully saved " + Name.ToLower() };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<string?> GetValue(string? name)
        {
            var setting = await context.SystemSettings
                                       .Where(s => s.Name == name)
                                       .Select(s => s.Value)
                                       .FirstOrDefaultAsync();
            return setting;
        }

        private async Task SaveSettings(string Name, string Value)
        {
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Value))
            {
                var set = await context.SystemSettings
                                           .Where(s => s.Name == Name)
                                           .FirstOrDefaultAsync();
                if (set == null)
                    await context.SystemSettings.AddAsync(new SystemSetting
                    {
                        Name = Name,
                        Value = Value
                    });
                else
                    set.Value = Value;

                await context.SaveChangesAsync(currentUser);
            }
        }

        public async Task<ResponseModel<string>> SaveImageAsync(PageImageFormVM form)
        {
            try
            {
                if (form.File == null || form.File.Length == 0)
                    return new ResponseModel<string> { Status = "Error", Message = "Invalid file" };

                var uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images\\Logo");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var settings = await context.SystemSettings.Where(s => s.Name == form.Name).FirstOrDefaultAsync();
                if (settings == null)
                {
                    settings = new SystemSetting
                    {
                        Name = form.Name,
                        Value = ""
                    };
                    await context.SystemSettings.AddAsync(settings);
                    await context.SaveChangesAsync(currentUser);
                }
                else if (!string.IsNullOrWhiteSpace(settings.Value))
                {
                    var oldFilePath = Path.Combine(uploadsFolder, settings.Value);

                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(form.File.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                var resized = "";

                using (var image = await Image.LoadAsync(form.File.OpenReadStream()))
                {
                    if (image.Width > 500 || image.Height > 500)
                    {
                        var originalSize = $"{image.Width}x{image.Height}";
                        double ratioX = (double)500 / image.Width;
                        double ratioY = (double)500 / image.Height;
                        double ratio = Math.Min(ratioX, ratioY);

                        int newWidth = (int)(image.Width * ratio);
                        int newHeight = (int)(image.Height * ratio);

                        image.Mutate(x => x.Resize(newWidth, newHeight));

                        resized = $" Image is too large, or larger than 500x500. Image is resized from {originalSize} to {newWidth}x{newHeight}";
                    }

                    await image.SaveAsync(filePath);
                }

                var savedFile = Path.Combine("images\\Logo", uniqueFileName);

                settings.Value = uniqueFileName;
                context.SystemSettings.Update(settings);
                await context.SaveChangesAsync(currentUser);

                return new ResponseModel<string> { Status = "Success", Message = "Successfully updated image." + resized, NewRow = uniqueFileName };
            }
            catch (Exception ex)
            {
                return new ResponseModel<string> { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> RemoveImageAsync(string Name)
        {
            try
            {
                var uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images\\Logo");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var settings = await context.SystemSettings.Where(s => s.Name == Name).FirstOrDefaultAsync();
                if( settings == null)
                {
                    return new ResponseModel { Status = "Error", Message = "Image wasn't replaced yet" };
                }
                else if (string.IsNullOrWhiteSpace(settings.Value))
                {
                    context.SystemSettings.Remove(settings);
                    await context.SaveChangesAsync(currentUser);

                    return new ResponseModel { Status = "Error", Message = "Invalid record. Please reload the page and try again" };
                }
                else
                {
                    var oldFilePath = Path.Combine(uploadsFolder, settings.Value);

                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }

                    context.SystemSettings.Remove(settings);
                    await context.SaveChangesAsync(currentUser);

                    return new ResponseModel { Status = "Success", Message = "Successfully deleted image." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }
    }
}