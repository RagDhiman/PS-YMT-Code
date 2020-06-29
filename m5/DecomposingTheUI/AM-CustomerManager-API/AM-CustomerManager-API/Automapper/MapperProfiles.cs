using AM_CustomerManager_API.Models;
using AM_CustomerManager_API.PageModels;
using AM_CustomerManager_Core;
using AutoMapper;

namespace AM_CustomerManager_API.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateAPIModelMaps();
            CreateMVCModelMaps();

        }

        private void CreateAPIModelMaps()
        {
            this.CreateMap<CustomerModel, Customer>()
        .ForMember(c => c.Id, opt => opt.Ignore())
        .ReverseMap();

            this.CreateMap<AddressModel, Address>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<NoteModel, Note>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();


            this.CreateMap<TaxInfoModel, TaxInfo>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<PaymentBillingModel, PaymentBilling>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<BankAccountModel, BankAccount>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();
        }

        private void CreateMVCModelMaps() 
        {

            this.CreateMap<CustomerDashViewModel, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CustomerDetailsModel, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CustomerEditModel, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CustomerIndexModel, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CustomerListViewModel, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<AddressIndexModel, Address>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<BankAccountIndexModel, BankAccount>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<NoteIndexModel, Note>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<PaymentBillingIndexModel, PaymentBilling>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<TaxInfoIndexModel, TaxInfo>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
