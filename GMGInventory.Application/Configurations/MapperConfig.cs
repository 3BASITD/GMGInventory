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
            CreateMap<Agent, AgentViewVM>().ReverseMap();
            CreateMap<Agent, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString(DateFormat)))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString(DateFormat)))
                .ReverseMap();
            

            // Area
            CreateMap<Area, AreaVM>().ReverseMap();
            CreateMap<Area, AreaViewVM>().ReverseMap();
            CreateMap<Area, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString(DateFormat)))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString(DateFormat)))
                .ReverseMap();


            // Brand
            CreateMap<Brand, BrandVM>().ReverseMap();
            CreateMap<Brand, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString(DateFormat)))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString(DateFormat)))
                .ReverseMap();
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
            CreateMap<Supplier, SupplierViewVM>().ReverseMap();
            CreateMap<Supplier, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString(DateFormat)))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString(DateFormat)))
                .ReverseMap();

            // User
            CreateMap<User, UserInformationVM>().ReverseMap();
            CreateMap<User, UserInformationForUpdateVM>().ReverseMap();

            // Warehouse
            CreateMap<Warehouse, WarehouseFormVM>().ReverseMap();
            CreateMap<Warehouse, WarehouseViewVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString(DateFormat)))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString(DateFormat)))
                .ReverseMap();

            // Currency
            CreateMap<Currency, CurrencyVM>().ReverseMap();
            CreateMap<Currency, GenericAuditVM>().ReverseMap();

            // Region
            CreateMap<Region, RegionVM>().ReverseMap();
            CreateMap<Region, GenericAuditVM>().ReverseMap();

            // CustomerType
            CreateMap<CustomerType, CustomerTypeVM>().ReverseMap();
            CreateMap<CustomerType, GenericAuditVM>().ReverseMap();

            // CreditTerm
            CreateMap<CreditTerm, CreditTermVM>().ReverseMap();
            CreateMap<CreditTerm, GenericAuditVM>().ReverseMap();

            // Customer
            CreateMap<Customer, CustomerVM>().ReverseMap();
            CreateMap<Customer, CustomerViewVM>().ReverseMap();
            CreateMap<Customer, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString(DateFormat)))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString(DateFormat)))
                .ReverseMap();

            // Forwarder
            CreateMap<Forwarder, ForwarderVM>().ReverseMap();
            CreateMap<Forwarder, ForwarderViewVM>().ReverseMap();
            CreateMap<Forwarder, GenericAuditVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString(DateFormat)))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString(DateFormat)))
                .ReverseMap();

            // Payment
            CreateMap<Payment, PaymentVM>().ReverseMap();
            CreateMap<Payment, PaymentFormVM>().ReverseMap();
            CreateMap<Payment, PaymentViewVM>()
                .ForMember(s => s.DateCreated, o => o.MapFrom(a => a.DateCreated.ToString(DateFormat)))
                .ForMember(s => s.DateModified, o => o.MapFrom(a => a.DateModified == null ? null : a.DateModified.Value.ToString(DateFormat)))
                .ReverseMap();





        }
    }
}
