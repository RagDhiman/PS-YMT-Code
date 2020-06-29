using AM_InvoiceManager_API.Models;
using AM_InvoiceManager_Core;
using AutoMapper;

namespace AM_InvoiceManager_API.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<InvoiceModel, Invoice>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CreditModel, Credit>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CreditNoteLineModel, CreditNoteLine>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CreditNoteModel, CreditNote>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();


            this.CreateMap<DelayedChargeLineModel, DelayedChargeLine>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<DelayedChargeModel, DelayedCharge>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<EstimateLineModel, EstimateLine>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<EstimateModel, Estimate>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<InvoiceLineModel, InvoiceLine>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<PaymentModel, Payment>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<SalesReceiptLineModel, SalesReceiptLine>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<SalesReceiptModel, SalesReceipt>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
