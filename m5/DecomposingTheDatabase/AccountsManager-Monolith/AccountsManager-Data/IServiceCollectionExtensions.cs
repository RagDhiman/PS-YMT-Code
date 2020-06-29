using AccountsManager_Data.Repositories;
using AccountsManager_Domain.DataAccess;
using AccountsManager_Domain.Services;
using EmployeesManager_Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AccountsManager_Data
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterDataAccess(this IServiceCollection services,
        String connectionString)
        {
            services.AddDbContext<AccountManagerContext>(options =>
                options
                .UseSqlServer(connectionString)
                //.UseLoggerFactory(loggerFactory)
                );

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IPaymentBillingRepository, PaymentBillingRepository>();
            services.AddScoped<ITaxInfoRepository, TaxInfoRepository>();


            services.AddScoped<ICreditRepository, CreditRepository>();
            services.AddScoped<IPaymentDetailsRepository, PaymentDetailsRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();

            services.AddScoped<IHolidayRepository, HolidayRepository>();
            services.AddScoped<IPayRepository, PayRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();

            services.AddScoped<IAbsenceRepository, AbsenceRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();
            services.AddScoped<ITaxInformationRepository, TaxInformationRepository>();

            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IExpenseLineRepository, ExpenseLineRepository>();

            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IInvoiceLineRepository, InvoiceLineRepository>();

            services.AddScoped<ICreditNoteRepository, CreditNoteRepository>();
            services.AddScoped<ICreditNoteLineRepository, CreditNoteLineRepository>();

            services.AddScoped<IDelayedChargeRepository, DelayedChargeRepository>();
            services.AddScoped<IDelayedChargeLineRepository, DelayedChargeLineRepository>();

            services.AddScoped<IEstimateRepository, EstimateRepository>();
            services.AddScoped<IEstimateLineRepository, EstimateLineRepository>();

            services.AddScoped<ISalesReceiptRepository, SalesReceiptRepository>();
            services.AddScoped<ISalesReceiptLineRepository, SalesReceiptLineRepository>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierNoteRepository, SupplierNoteRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();

            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<ISMSRepository, SMSRepository>();
            services.AddScoped<IWebhookPostRepository, WebhookPostRepository>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();


        }
    }
}
