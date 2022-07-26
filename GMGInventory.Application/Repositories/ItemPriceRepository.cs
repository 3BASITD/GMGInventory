using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class ItemPriceRepository : IItemPriceRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ItemPriceRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ItemPriceVM?> GetAsync(int? id)
        {
            var priceVM = (from item in context.Items
                           from price in context.ItemPrices.Where(s => s.ItemId == item.Id && s.EffectiveDate >= DateTime.Now).Take(1).DefaultIfEmpty()
                           join inventory in context.ItemInventories on item.Id equals inventory.Id
                           group inventory by new { item, price } into i
                           where i.Key.item.Id == id
                           select new ItemPriceVM
                           {
                               Brand = i.Key.item.Brand.Code,
                               Category = i.Key.item.Category.Code,
                               Code = i.Key.item.Code,
                               Id = i.Key.item.Id,
                               ItemType = i.Key.item.ItemType.Code,
                               ModelNumber = i.Key.item.ModelNumber,
                               Name = i.Key.item.Name,
                               NetPrice = i.Key.price.NetSellingPrice,
                               PartNumber = i.Key.item.PartNumber,
                               Quantity = i.Sum(s => s.Quantity),
                               RetailPrice = i.Key.price.RetailSellingPrice,
                               Size = i.Key.item.Size,
                               Unit = i.Key.item.Unit.Code
                           }).FirstOrDefault();

            if (priceVM == null)
            {
                return null;
            }
            return priceVM;
        }

        public async Task<List<ItemPriceVM>> GetAllAsync(ItemFilterVM itemFilterVM)
        {
            //var pricesVM = await (from item in context.Items
            //                      from price in context.ItemPrices.Where(s => s.ItemId == item.Id && s.EffectiveDate >= DateTime.Now).Take(1).DefaultIfEmpty()
            //                      join inventory in context.ItemInventories on item.Id equals inventory.Id
            //                      group inventory by new { item, price } into i
            //                      where ((itemFilterVM.CategoryId == null || itemFilterVM.CategoryId == i.Key.item.CategoryId)
            //                          && (itemFilterVM.ItemTypeId == null || itemFilterVM.ItemTypeId == i.Key.item.ItemTypeId)
            //                          && (itemFilterVM.BrandId == null || itemFilterVM.BrandId == i.Key.item.BrandId))
            //                      select new ItemPriceVM
            //                      {
            //                          Brand = i.Key.item.Brand.Code,
            //                          Category = i.Key.item.Category.Code,
            //                          Code = i.Key.item.Code,
            //                          Id = i.Key.item.Id,
            //                          ItemType = i.Key.item.ItemType.Code,
            //                          ModelNumber = i.Key.item.ModelNumber,
            //                          Name = i.Key.item.Name,
            //                          NetPrice = i.Key.price.NetSellingPrice,
            //                          PartNumber = i.Key.item.PartNumber,
            //                          Quantity = i.Sum(s => s.Quantity),
            //                          RetailPrice = i.Key.price.RetailSellingPrice,
            //                          Size = i.Key.item.Size,
            //                          Unit = i.Key.item.Unit.Code
            //                      }).ToListAsync();
            var pricesVM = new List<ItemPriceVM>();
            return pricesVM;
        }
    }
}