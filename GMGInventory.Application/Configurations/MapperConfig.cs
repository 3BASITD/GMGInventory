using AutoMapper;
using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            string DateFormat = "yyyy/MM/dd hh:mm:ss tt";
            // Agent
            CreateMap<Agent, AgentVM>().ReverseMap();
            CreateMap<Agent, GenericAuditVM>().ReverseMap();

            // Area
            CreateMap<Area, AreaVM>().ReverseMap();
            CreateMap<Area, GenericAuditVM>().ReverseMap();

            // Brand
            CreateMap<Brand, BrandVM>().ReverseMap();
            CreateMap<Brand, GenericAuditVM>().ReverseMap();
            CreateMap<Brand, BrandViewVM>().ReverseMap();

            // Category
            CreateMap<Category, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString(DateFormat)))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString(DateFormat)))
                .ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();

            // Item Type
            CreateMap<ItemType, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString(DateFormat)))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString(DateFormat)))
                .ReverseMap();
            CreateMap<ItemType, ItemTypeVM>().ReverseMap();
            CreateMap<ItemType, ItemTypeViewVM>().ReverseMap();

            // Supplier
            CreateMap<Supplier, SupplierVM>().ReverseMap();
            CreateMap<Supplier, GenericAuditVM>().ReverseMap();

            // User
            CreateMap<User, UserInformationVM>().ReverseMap();
            CreateMap<User, UserInformationForUpdateVM>().ReverseMap();

            // Warehouse
            CreateMap<Warehouse, WarehouseFormVM>().ReverseMap();
            CreateMap<Warehouse, WarehouseViewVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString(DateFormat)))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString(DateFormat)))
                .ReverseMap();

        }
    }
}
