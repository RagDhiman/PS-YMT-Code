using AccountsManager_UI_Web.Data.DTOs;
using AccountsManager_UI_Web.Models;
using AutoMapper;

namespace AccountsManager_UI_Web.AutoMapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            this.CreateMap<CustomerIndexModel,Customer > ()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CustomerEditModel, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CustomerDetailsModel, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<AddressIndexModel, Address>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<AccountDetailModel, Account>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<EmployeeIndexModel, Employee>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<InvoiceIndexModel, Invoice>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<InvoiceEditModel, Invoice>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<InvoiceDetailModel, Invoice>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<ExpenseIndexModel, Expense>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<ExpenseEditModel, Expense>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<SupplierIndexModel, Supplier>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<SupplierEditModel, Supplier>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CreditNoteIndexModel, CreditNote>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<PaymentIndexModel, Payment>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<EstimateIndexModel, Estimate>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<DelayedChargeIndexModel, DelayedCharge>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<InvoiceLineIndexModel, InvoiceLine>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<SalesReceiptIndexModel, SalesReceipt>()
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

            this.CreateMap<EmailIndexModel, Email>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<SMSIndexModel, SMS>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<WebhookPostIndexModel, WebhookPost>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CreditIndexModel, Credit>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CreditEditModel, Credit>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<BankAccountIndexModel, BankAccount>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
