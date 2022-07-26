using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PriceRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<PriceVM?> GetAsync(int? id)
        {
            var priceVM = (from price in context.ItemPrices
                          join item in context.Items on price.ItemId equals item.Id
                          join inven in context.ItemInventories on item.Id equals inven.ItemId
                          join categ in context.Categories on item.CategoryId equals categ.Id
                          join itemt in context.ItemTypes on item.ItemTypeId equals itemt.Id
                          join brand in context.Brands on item.BrandId equals brand.Id
                          select new PriceVM
                          {
                              Id = item.Id
                          }).FirstOrDefault();

            //var priceVM2 = context.ItemPrices
            //                .Include(s=>s.Item).ThenInclude(s=>s.)

            if (priceVM == null)
            {
                return null;
            }
            return priceVM;
        }

        public async Task<List<PriceVM>> GetAllAsync(ItemFilterVM itemFilterVM)
        {
            return new List<PriceVM>();
        }
    }
}