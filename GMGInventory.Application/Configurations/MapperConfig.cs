using AutoMapper;
using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Category, GenericAuditVM>().ReverseMap();
            CreateMap<ItemType, GenericAuditVM>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<ItemType, ItemTypeVM>().ReverseMap();
            CreateMap<ItemType, ItemTypeViewVM>().ReverseMap();
            CreateMap<Agent, AgentVM>().ReverseMap();
            CreateMap<Agent, GenericAuditVM>().ReverseMap();
            CreateMap<Brand, BrandVM>().ReverseMap();
            CreateMap<Brand, GenericAuditVM>().ReverseMap();
            CreateMap<Brand, BrandViewVM>().ReverseMap();
            CreateMap<Area, AreaVM>().ReverseMap();
            CreateMap<Area, GenericAuditVM>().ReverseMap();
            CreateMap<Supplier, SupplierVM>().ReverseMap();
            CreateMap<Supplier, GenericAuditVM>().ReverseMap();
            CreateMap<User, UserInformationVM>().ReverseMap();

        }
    }
}
