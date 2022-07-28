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

        public IQueryable<ItemPriceVM> GetAll(ItemFilterVM itemFilterVM)
        {
            var pricesVM = (from item in context.Items
                            from price in context.ItemPrices.Where(s => s.ItemId == item.Id && s.EffectiveDate <= DateTime.Now).OrderByDescending(s => s.EffectiveDate).Take(1).DefaultIfEmpty()
                            let quantity = (from inventory in context.ItemInventories where inventory.ItemId == item.Id select inventory.Quantity).ToList()
                            where ((itemFilterVM.CategoryId == null || itemFilterVM.CategoryId == item.CategoryId)
                                && (itemFilterVM.ItemTypeId == null || itemFilterVM.ItemTypeId == item.ItemTypeId)
                                && (itemFilterVM.BrandId == null || itemFilterVM.BrandId == item.BrandId))
                            select new ItemPriceVM
                            {
                                Brand = item.Brand.Code,
                                Category = item.Category.Code,
                                Code = item.Code,
                                Id = item.Id,
                                ItemType = item.ItemType.Code,
                                ModelNumber = item.ModelNumber,
                                Name = item.Name,
                                NetPrice = price == null ? 0 : price.NetSellingPrice,
                                PartNumber = item.PartNumber,
                                Quantity = quantity.Count() == 0 ? 0 : quantity.Sum(s => s),
                                RetailPrice = price == null ? 0 : price.RetailSellingPrice,
                                Size = item.Size,
                                Unit = item.Unit.Code
                            });
            return pricesVM;
        }
    }
}