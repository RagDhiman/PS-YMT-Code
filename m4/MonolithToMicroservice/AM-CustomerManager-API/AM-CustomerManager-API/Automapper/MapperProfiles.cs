using AM_CustomerManager_API.Models;
using AM_CustomerManager_Core;
using AutoMapper;

namespace AM_CustomerManager_API.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
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
    }
}
