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
            CreateMap<User, UserInformationVM>().ReverseMap();

        }
    }
}
