using AutoMapper;
using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Category, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ReverseMap();
            CreateMap<ItemType, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ReverseMap();
     
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<ItemType, ItemTypeVM>().ReverseMap();
            CreateMap<ItemType, ItemTypeViewVM>().ReverseMap();
            CreateMap<Agent, AgentVM>().ReverseMap();
            CreateMap<Agent, AgentViewVM>().ReverseMap();
            CreateMap<Agent, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ReverseMap();
            CreateMap<Brand, BrandVM>().ReverseMap();
            CreateMap<Brand, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ReverseMap();
            CreateMap<Brand, BrandViewVM>().ReverseMap();
            CreateMap<Area, AreaVM>().ReverseMap();
            CreateMap<Area, AreaViewVM>().ReverseMap();
            CreateMap<Area, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ReverseMap();

            CreateMap<Region, RegionVM>().ReverseMap();
            CreateMap<Region, GenericAuditVM>().ReverseMap();
            CreateMap<Currency, CurrencyVM>().ReverseMap();
            CreateMap<Currency, GenericAuditVM>().ReverseMap();

            CreateMap<Supplier, SupplierVM>().ReverseMap();
            CreateMap<Supplier, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ReverseMap();
            CreateMap<User, UserInformationVM>().ReverseMap();
            CreateMap<User, UserInformationForUpdateVM>().ReverseMap();
            CreateMap<Item, ItemFormVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ReverseMap();
            CreateMap<Item, ItemInformationVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString("yyyy/MM/dd hh:mm:ss tt")))
                .ReverseMap();

        }
    }
}
