using AccountsManager_Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace AccountsManager_Data
{
    public class AccountManagerContext : DbContext
    {
        #region dbsets
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<PaymentBilling> PaymentBillings { get; set; }
        public DbSet<CreditNote> CreditNotes { get; set; }
        public DbSet<CreditNoteLine> CreditNoteLines { get; set; }
        public DbSet<DelayedCharge> DelayedCharges { get; set; }
        public DbSet<DelayedChargeLine> DelayedChargeLines { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<EstimateLine> EstimateLines { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SalesReceipt> SalesReceipts { get; set; }
        public DbSet<SalesReceiptLine> SalesReceiptLines { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        public DbSet<Absence> Absences { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Pay> Pays { get; set; }
        public DbSet<TaxInformation> TaxInformations { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ExpenseLine> ExpenseLines { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierNote> SupplierNotes { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<SMS> SMSs { get; set; }
        public DbSet<WebhookPost> WebhookPosts { get; set; }

        public DbSet<TaxInfo> TaxInfos { get; set; }

        #endregion

        public AccountManagerContext(DbContextOptions<AccountManagerContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureModels(modelBuilder);
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            SeedAccountData(modelBuilder);
            SeedCustomerData(modelBuilder);
            SeedEmployeeData(modelBuilder);
            SeedBankAccountData(modelBuilder);
            SeedAddressData(modelBuilder);
            SeedNoteData(modelBuilder);
            SeedPaymentBillingData(modelBuilder);
            SeedTaxInfoData(modelBuilder);
            SeedCreditNoteData(modelBuilder);
            SeedCreditNoteLineData(modelBuilder);
            SeedDelayedChargeData(modelBuilder);
            SeedDelayedChargeLineData(modelBuilder);
            SeedEstimateData(modelBuilder);
            SeedEstimateLineData(modelBuilder);
            SeedInvoiceData(modelBuilder);
            SeedInvoiceLineData(modelBuilder);
            SeedPaymentData(modelBuilder);
            SeedSalesReceiptData(modelBuilder);
            SeedSalesReceiptLineData(modelBuilder);
            SeedSubscriptionsData(modelBuilder);
            SeedVoucherData(modelBuilder);
            SeedCreditData(modelBuilder);
            SeedPaymentDetailsData(modelBuilder);

            SeedHolidayData(modelBuilder);
            SeedAbsenceData(modelBuilder);
            SeedTrainingData(modelBuilder);
            SeedTaxInformationData(modelBuilder);
            SeedPayData(modelBuilder);
            SeedEquipmentData(modelBuilder);

            SeedExpenseData(modelBuilder);
            SeedExpenseLineData(modelBuilder);

            SeedSupplierData(modelBuilder);
            SeedSupplierNoteData(modelBuilder);
            SeedAttachmentData(modelBuilder);

            SeedSMSData(modelBuilder);
            SeedEmailData(modelBuilder);
            SeedWebhookPostData(modelBuilder);

        }
        private void ConfigureModels(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>().HasKey(e => e.Id);
            modelBuilder.Entity<Customer>().HasKey(e => e.Id);
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Address>().HasKey(e => e.Id);
            modelBuilder.Entity<Note>().HasKey(e => e.Id);
            modelBuilder.Entity<PaymentBilling>().HasKey(e => e.Id);
            modelBuilder.Entity<TaxInfo>().HasKey(e => e.Id);
            modelBuilder.Entity<CreditNote>().HasKey(e => e.Id);
            modelBuilder.Entity<CreditNoteLine>().HasKey(e => e.Id);
            modelBuilder.Entity<DelayedCharge>().HasKey(e => e.Id);
            modelBuilder.Entity<DelayedChargeLine>().HasKey(e => e.Id);
            modelBuilder.Entity<Estimate>().HasKey(e => e.Id);
            modelBuilder.Entity<EstimateLine>().HasKey(e => e.Id);
            modelBuilder.Entity<Invoice>().HasKey(e => e.Id);
            modelBuilder.Entity<InvoiceLine>().HasKey(e => e.Id);
            modelBuilder.Entity<Payment>().HasKey(e => e.Id);
            modelBuilder.Entity<SalesReceipt>().HasKey(e => e.Id);
            modelBuilder.Entity<SalesReceiptLine>().HasKey(e => e.Id);
            modelBuilder.Entity<Account>().HasKey(e => e.Id);
            modelBuilder.Entity<Subscription>().HasKey(e => e.Id);
            modelBuilder.Entity<Voucher>().HasKey(e => e.Id);
            modelBuilder.Entity<Credit>().HasKey(e => e.Id);
            modelBuilder.Entity<PaymentDetails>().HasKey(e => e.Id);

            modelBuilder.Entity<Holiday>().HasKey(e => e.Id);
            modelBuilder.Entity<Absence>().HasKey(e => e.Id);
            modelBuilder.Entity<Pay>().HasKey(e => e.Id);
            modelBuilder.Entity<Training>().HasKey(e => e.Id);
            modelBuilder.Entity<TaxInformation>().HasKey(e => e.Id);
            modelBuilder.Entity<Equipment>().HasKey(e => e.Id);

            modelBuilder.Entity<Expense>().HasKey(e => e.Id);
            modelBuilder.Entity<ExpenseLine>().HasKey(e => e.Id);

            modelBuilder.Entity<CreditNote>().HasKey(e => e.Id);
            modelBuilder.Entity<CreditNoteLine>().HasKey(e => e.Id);

            modelBuilder.Entity<Supplier>().HasKey(e => e.Id);
            modelBuilder.Entity<SupplierNote>().HasKey(e => e.Id);
            modelBuilder.Entity<Attachment>().HasKey(e => e.Id);

            modelBuilder.Entity<Email>().HasKey(e => e.Id);
            modelBuilder.Entity<WebhookPost>().HasKey(e => e.Id);
            modelBuilder.Entity<SMS>().HasKey(e => e.Id);
        }

        #region seedData

        private void SeedSMSData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SMS>().HasData(
                new SMS()
                {
                    Id = 1,
                    Sender = "Bob Smith",
                    SendTo = "2342352354324",
                    SentDateTime = System.DateTime.Now,
                    DeliveredDateTime = System.DateTime.Now.AddMinutes(30),
                    Message = "Invoice sent!"
                },
                new SMS()
                {
                    Id = 2,
                    Sender = "Bill Smith",
                    SendTo = "2342352354324",
                    SentDateTime = System.DateTime.Now,
                    DeliveredDateTime = System.DateTime.Now.AddMinutes(30),
                    Message = "Invoice sent!"
                });
        }
        private void SeedEmailData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>().HasData(
                new Email()
                {
                    Id = 1,
                    Sender = "Bob Smith",
                    SendTo = "2342352354324",
                    SentDateTime = System.DateTime.Now,
                    DeliveredDateTime = System.DateTime.Now.AddMinutes(30),
                    Message = "Invoice sent!",
                    Subject = "Invoice Sent!"

                },
                new Email()
                {
                    Id = 2,
                    Sender = "Bob Smith",
                    SendTo = "2342352354324",
                    SentDateTime = System.DateTime.Now,
                    DeliveredDateTime = System.DateTime.Now.AddMinutes(30),
                    Message = "Invoice sent!",
                    Subject = "Invoice Sent!"
                });
        }
        private void SeedWebhookPostData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebhookPost>().HasData(
                new WebhookPost()
                {
                    Id = 1,
                    Sender = "Bob Smith",
                    PostDateTime = System.DateTime.Now,
                    Body = "Invoice sent!",
                    URL = "Invoice Sent!"
                },
                new WebhookPost()
                {
                    Id = 2,
                    Sender = "Bob Smith",
                    PostDateTime = System.DateTime.Now,
                    Body = "Invoice sent!",
                    URL = "Invoice Sent!"
                });
        }

        private void SeedSupplierData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier()
                {
                    Id = 1,
                    AccountId = 1987,
                    Company = "Berrari-Ltd",
                    ContactName = "Mr Enzo Berrari",
                    Email = "Enzo@Berrari.com",
                    Mobile = "1232432",
                    Fax = "2342342",
                    Phone = "324232",
                    Website = "http://www.berrariltd.com"
                },
                new Supplier()
                {
                    Id = 2,
                    AccountId = 1987,
                    Company = "Renotton-Ltd",
                    ContactName = "Mr Malavio Fritorie",
                    Email = "Malavio.Fritorie@Renotton.com",
                    Mobile = "4432432",
                    Fax = "2242342",
                    Phone = "334232",
                    Website = "www.Renotton-ltd.com"
                });
        }
        private void SeedSupplierNoteData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SupplierNote>().HasData(
                new SupplierNote()
                {
                    Id = 1,
                    SupplierId = 1,
                    NoteText = "No weekend deliveries!"
                },
                new SupplierNote()
                {
                    Id = 2,
                    SupplierId = 2,
                    NoteText = "No weekend deliveries!"
                });
        }
        private void SeedAttachmentData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>().HasData(
                new Attachment()
                {
                    Id = 1,
                    SupplierId = 1,
                    FilePath = "//supplier/234/agreement.txt"
                },
                new Attachment()
                {
                    Id = 2,
                    SupplierId = 2,
                    FilePath = "//supplier/234/agreement.txt"
                });
        }
        private void SeedCreditNoteData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditNote>().HasData(
                new CreditNote()
                {
                    Id = 1,
                    CustomerId = 1123,
                    InvoiceId = 11232,
                    CreditNoteDate = System.DateTime.Now.AddDays(-2).AddMinutes(12),
                    Message = "Credit Note"
                },
                new CreditNote()
                {
                    Id = 2,
                    CustomerId = 1123,
                    InvoiceId = 11232,
                    CreditNoteDate = System.DateTime.Now.AddDays(-4).AddMinutes(123),
                    Message = "Credit Note"
                },
                new CreditNote()
                {
                    Id = 3,
                    CustomerId = 1123,
                    InvoiceId = 11232,
                    CreditNoteDate = System.DateTime.Now.AddDays(-5).AddMinutes(12344),
                    Message = "Credit Note"
                },
                new CreditNote()
                {
                    Id = 4,
                    CustomerId = 1123,
                    InvoiceId = 11232,
                    CreditNoteDate = System.DateTime.Now.AddDays(-5).AddMinutes(1),
                    Message = "Credit Note"
                },
                new CreditNote()
                {
                    Id = 5,
                    CustomerId = 1123,
                    InvoiceId = 21324,
                    CreditNoteDate = System.DateTime.Now.AddDays(-3).AddMinutes(44),
                    Message = "Credit Note"
                },
                new CreditNote()
                {
                    Id = 6,
                    CustomerId = 1123,
                    InvoiceId = 21324,
                    CreditNoteDate = System.DateTime.Now.AddDays(-3).AddMinutes(44),
                    Message = "Credit Note"
                },
                new CreditNote()
                {
                    Id = 7,
                    CustomerId = 1123,
                    InvoiceId = 21324,
                    CreditNoteDate = System.DateTime.Now.AddDays(-3).AddMinutes(44),
                    Message = "Credit Note"
                },
                new CreditNote()
                {
                    Id = 8,
                    CustomerId = 1123,
                    InvoiceId = 21324,
                    CreditNoteDate = System.DateTime.Now.AddDays(-3).AddMinutes(44),
                    Message = "Credit Note"
                },
                new CreditNote()
                {
                    Id = 9,
                    CustomerId = 1123,
                    InvoiceId = 21324,
                    CreditNoteDate = System.DateTime.Now.AddDays(-3).AddMinutes(44),
                    Message = "Credit Note"
                });
        }

        private void SeedCreditNoteLineData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditNoteLine>().HasData(
                new CreditNoteLine()
                {
                    Id = 1,
                    CreditNoteId = 1,
                    Quantity = 2,
                    Rate = 2.4,
                    Service = ServiceType.Service,
                    VAT = 23
                },
                new CreditNoteLine()
                {
                    Id = 2,
                    CreditNoteId = 2,
                    Quantity = 2,
                    Rate = 2.4,
                    Service = ServiceType.Service,
                    VAT = 23
                });
        }

        private void SeedExpenseData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>().HasData(
                new Expense()
                {
                    Id = 1,
                    EmployeeId = 1,
                    PayeeName = "Art Tech",
                    PaymentMethod = PaymentMethod.Cash,
                    PaymentDate = System.DateTime.Now.AddDays(-2),
                    BankAccountId = 1987,
                    Notes = "One-off",
                    Reference = "TESDSFD-324"
                },
                new Expense()
                {
                    Id = 2,
                    EmployeeId = 1,
                    PayeeName = "Art Tech",
                    PaymentMethod = PaymentMethod.Cash,
                    PaymentDate = System.DateTime.Now.AddDays(-2),
                    BankAccountId = 1987,
                    Notes = "One-off",
                    Reference = "G43534-324"
                });
        }

        private void SeedExpenseLineData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseLine>().HasData(
                new ExpenseLine()
                {
                    Id = 1,
                    ExpenseId = 1,
                    Amount = 200,
                    Description = "Regular",
                    ServiceType = ServiceType.Service,
                    VAT = 23
                },
                new ExpenseLine()
                {
                    Id = 2,
                    ExpenseId = 2,
                    Amount = 200,
                    Description = "Regular",
                    ServiceType = ServiceType.Service,
                    VAT = 23
                });
        }

        private void SeedEquipmentData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment()
                {
                    Id = 1,
                    EmployeeId = 1,
                    ExpectedReturnDate = System.DateTime.Now.AddDays(200),
                    LoanStartDateTime = System.DateTime.Now,
                    LoanEndDateTime = System.DateTime.Now.AddDays(20),
                    Name = "De 23l Laptop",
                    Reference = "D32432"
                },
                new Equipment()
                {
                    Id = 2,
                    EmployeeId = 2,
                    ExpectedReturnDate = System.DateTime.Now.AddDays(200),
                    LoanStartDateTime = System.DateTime.Now,
                    LoanEndDateTime = System.DateTime.Now.AddDays(20),
                    Name = "De 23l Laptop",
                    Reference = "D32432"
                });
        }

        private void SeedPayData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pay>().HasData(
                new Pay()
                {
                    Id = 1,
                    EmployeeId = 1,
                    DefaultRate = true,
                    HourlyRate = 10.00,
                    StartTime = System.DateTime.Now,
                    EndTime = System.DateTime.Now.AddDays(200)
                },
                new Pay()
                {
                    Id = 2,
                    EmployeeId = 2,
                    DefaultRate = true,
                    HourlyRate = 10.00,
                    StartTime = System.DateTime.Now,
                    EndTime = System.DateTime.Now.AddDays(200)
                });
        }

        private void SeedTaxInformationData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxInformation>().HasData(
                new TaxInformation()
                {
                    Id = 1,
                    EmployeeId = 1,
                    TaxCode = "DFE543R323E",
                    VAT = true,
                    VATRef = "VD-3R32342E"
                },
                new TaxInformation()
                {
                    Id = 2,
                    EmployeeId = 2,
                    TaxCode = "DFE543R323E",
                    VAT = true,
                    VATRef = "VD-3R32342E"
                });
        }

        private void SeedTrainingData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Training>().HasData(
                new Training()
                {
                    Id = 1,
                    EmployeeId = 1,
                    Certification = true,
                    CertificationName = "MCP",
                    Description = "SQL Server MCP",
                    Name = "SQL MCP Training",
                    StartDateTime = System.DateTime.Now.AddDays(-100),
                    EndDateTime = System.DateTime.Now.AddDays(100)
                },
                new Training()
                {
                    Id = 2,
                    EmployeeId = 2,
                    Certification = true,
                    CertificationName = "MCP",
                    Description = "SQL Server MCP",
                    Name = "SQL MCP Training",
                    StartDateTime = System.DateTime.Now.AddDays(-100),
                    EndDateTime = System.DateTime.Now.AddDays(100)
                });
        }

        private void SeedAbsenceData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Absence>().HasData(
                new Absence()
                {
                    Id = 1,
                    EmployeeId = 1,
                    Description = "Sickness",
                    Notes = "Sickness",
                    Paid = true,
                    StartDateTime = System.DateTime.Now.AddDays(-1),
                    EndDateTime = System.DateTime.Now.AddDays(1)
                },
                new Absence()
                {
                    Id = 2,
                    EmployeeId = 2,
                    Description = "Sickness",
                    Notes = "Sickness",
                    Paid = true,
                    StartDateTime = System.DateTime.Now.AddDays(-1),
                    EndDateTime = System.DateTime.Now.AddDays(1)
                });
        }

        private void SeedHolidayData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Holiday>().HasData(
                new Holiday()
                {
                    Id = 1,
                    EmployeeId = 1,
                    OnCall = true,
                    OnCallRateMultiplier = 2,
                    Paid = true,
                    StartDateTime = System.DateTime.Now.AddDays(2),
                    EndDateTime = System.DateTime.Now.AddDays(3)
                },
                new Holiday()
                {
                    Id = 2,
                    EmployeeId = 2,
                    OnCall = true,
                    OnCallRateMultiplier = 2,
                    Paid = true,
                    StartDateTime = System.DateTime.Now.AddDays(2),
                    EndDateTime = System.DateTime.Now.AddDays(3)
                });
        }

        private void SeedEmployeeData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    Id = 1,
                    AccountId = 1987,
                    Title = "Mr",
                    FirstName = "Freddie",
                    LastName = "Dennies",
                    Mobile = "072341123441",
                    Phone = "0121324435",
                    Address = "23 Parkhill Road Smethwick B66 53N",
                    Notes = "Paternity due soon!",
                    DisplayNameAs = "EddieD",
                    Email = "eddie.dennies@AllFinances298.com",
                    DOB = new DateTime(1975, 01, 01)
                },
                new Employee()
                {
                    Id = 2,
                    AccountId = 1987,
                    Title = "Mr",
                    FirstName = "Jon",
                    LastName = "Jordan",
                    Mobile = "072341123441",
                    Phone = "0121324435",
                    Address = "23 Windmill Road Smethwick B66 34R",
                    Notes = "Paternity due soon!",
                    DisplayNameAs = "EddieD",
                    Email = "ron.jordan@AllFinances298.com",
                    DOB = new DateTime(1975, 01, 01)
                });
        }
        private void SeedAccountData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account()
                {
                    Id = 1987,
                    CompanyName = "NehaKD",
                    NoOfUserLicences = 10,
                    RenewalDate = new DateTime(2021, 01, 01),
                    CompanyEmail = "rag.dhiman@NehaKD.com",
                    CompanySMSSender = "0712345678",
                    NewCustomerCRMWebhook = "https://nehacrm.api.com/customer"
                });
        }

        private void SeedSubscriptionsData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription()
                {
                    AccountId = 1987,
                    Id = 2,
                    ProductSubscription = AccountsManager_Domain.Common.Product.CustomerManagement,
                    StardDate = System.DateTime.Now.AddDays(100)
                },
                new Subscription()
                {
                    AccountId = 1987,
                    Id = 3,
                    ProductSubscription = AccountsManager_Domain.Common.Product.EmployeeManagement,
                    StardDate = System.DateTime.Now.AddDays(100)
                },
                new Subscription()
                {
                    AccountId = 1987,
                    Id = 4,
                    ProductSubscription = AccountsManager_Domain.Common.Product.ExpenseManagement,
                    StardDate = System.DateTime.Now.AddDays(100)
                },
                new Subscription()
                {
                    AccountId = 1987,
                    Id = 5,
                    ProductSubscription = AccountsManager_Domain.Common.Product.InvoiceManagement,
                    StardDate = System.DateTime.Now.AddDays(100)
                },
                new Subscription()
                {
                    AccountId = 1987,
                    Id = 6,
                    ProductSubscription = AccountsManager_Domain.Common.Product.SupplierManagement,
                    StardDate = System.DateTime.Now.AddDays(100)
                });
        }

        private void SeedVoucherData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Voucher>().HasData(
                new Voucher()
                {
                    AccountId = 1987,
                    Id = 1,
                    AppliedDate = System.DateTime.Now,
                    ExpireDate = System.DateTime.Now.AddDays(100),
                    ProductVoucher = AccountsManager_Domain.Common.Product.CustomerManagement,
                    VoucherCode = "dsf3234"
                },
                new Voucher()
                {
                    AccountId = 1987,
                    Id = 2,
                    AppliedDate = System.DateTime.Now,
                    ExpireDate = System.DateTime.Now.AddDays(23),
                    ProductVoucher = AccountsManager_Domain.Common.Product.ExpenseManagement,
                    VoucherCode = "dsf3234"
                },
                new Voucher()
                {
                    AccountId = 1987,
                    Id = 3,
                    AppliedDate = System.DateTime.Now,
                    ExpireDate = System.DateTime.Now.AddDays(23),
                    ProductVoucher = AccountsManager_Domain.Common.Product.SupplierManagement,
                    VoucherCode = "dsf3234"
                });
        }

        private void SeedCreditData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credit>().HasData(
                new Credit()
                {
                    InvoiceId = 11232,
                    Id = 1,
                    ProductCredit = AccountsManager_Domain.Common.Product.EmployeeManagement,
                    CreditAmount = 231,
                    CreditDate = System.DateTime.Now.AddDays(-1),
                    CustomerName = "Mr Michael Raikkonin Verstaphen",
                    AccountNo = "234-2432-324",
                    HasCreditAgreement = true,
                    SortCode = "34-234-234"
                },
                new Credit()
                {
                    InvoiceId = 21324,
                    Id = 2,
                    ProductCredit = AccountsManager_Domain.Common.Product.InvoiceManagement,
                    CreditAmount = 231,
                    CreditDate = System.DateTime.Now.AddDays(-1),
                    CustomerName = "Mr Timi Schumacher Alfonso",
                    AccountNo = "234-212-676",
                    HasCreditAgreement = true,
                    SortCode = "567-345-234"
                },
                new Credit()
                {
                    InvoiceId = 32312,
                    Id = 3,
                    ProductCredit = AccountsManager_Domain.Common.Product.SupplierManagement,
                    CreditAmount = 231,
                    CreditDate = System.DateTime.Now.AddDays(-1),
                    CustomerName = "Mr Lewis Rosbert Sutton",
                    AccountNo = "546-456-345",
                    HasCreditAgreement = true,
                    SortCode = "435-456-123"
                });
        }

        private void SeedPaymentDetailsData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentDetails>().HasData(
                new PaymentDetails()
                {
                    AccountId = 1987,
                    Id = 1,
                    AccountNo = "768 43230",
                    SortCode = "5675 234"
                },
                new PaymentDetails()
                {
                    AccountId = 1987,
                    Id = 2,
                    AccountNo = "567 567",
                    SortCode = "2342 234"
                },
                new PaymentDetails()
                {
                    AccountId = 1987,
                    Id = 3,
                    AccountNo = "234 546",
                    SortCode = "234265 234"
                });
        }

        private void SeedSalesReceiptLineData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesReceiptLine>().HasData(
                new SalesReceiptLine()
                {
                    Id = 1,
                    SalesReceiptId = 1,
                    Rate = 23,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new SalesReceiptLine()
                {
                    Id = 2,
                    SalesReceiptId = 2,
                    Rate = 23,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new SalesReceiptLine()
                {
                    Id = 3,
                    SalesReceiptId = 3,
                    Rate = 23,
                    Service = ServiceType.Product,
                    VAT = 0
                }
                );
        }

        private void SeedSalesReceiptData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesReceipt>().HasData(
                new SalesReceipt()
                {
                    Id = 1,
                    CustomerId = 1123,
                    InvoiceId = 11232,
                    PaymentMethod = PaymentMethod.DirectDebit,
                    SalesReceiptDate = System.DateTime.Now.AddDays(23),
                    BankAccountId = 1987,
                    ReferenceNo = "2342",
                    Message = "New customer!"
                },
                new SalesReceipt()
                {
                    Id = 2,
                    CustomerId = 2123,
                    InvoiceId = 11232,
                    PaymentMethod = PaymentMethod.DirectDebit,
                    SalesReceiptDate = System.DateTime.Now.AddDays(23),
                    BankAccountId = 1987,
                    ReferenceNo = "2342",
                    Message = "New customer!"
                },
                new SalesReceipt()
                {
                    Id = 3,
                    CustomerId = 3123,
                    InvoiceId = 11232,
                    PaymentMethod = PaymentMethod.DirectDebit,
                    SalesReceiptDate = System.DateTime.Now.AddDays(23),
                    BankAccountId = 1987,
                    ReferenceNo = "2342",
                    Message = "New customer!"
                },
                new SalesReceipt()
                {
                    Id = 4,
                    CustomerId = 1123,
                    InvoiceId = 21324,
                    PaymentMethod = PaymentMethod.DirectDebit,
                    SalesReceiptDate = System.DateTime.Now.AddDays(23),
                    BankAccountId = 1987,
                    ReferenceNo = "2342",
                    Message = "New customer!"
                },
                new SalesReceipt()
                {
                    Id = 5,
                    CustomerId = 2123,
                    InvoiceId = 21324,
                    PaymentMethod = PaymentMethod.DirectDebit,
                    SalesReceiptDate = System.DateTime.Now.AddDays(23),
                    BankAccountId = 1987,
                    ReferenceNo = "2342",
                    Message = "New customer!"
                },
                new SalesReceipt()
                {
                    Id = 6,
                    CustomerId = 3123,
                    InvoiceId = 21324,
                    PaymentMethod = PaymentMethod.DirectDebit,
                    SalesReceiptDate = System.DateTime.Now.AddDays(23),
                    BankAccountId = 1987,
                    ReferenceNo = "2342",
                    Message = "New customer!"
                },
                new SalesReceipt()
                {
                    Id = 7,
                    CustomerId = 1123,
                    InvoiceId = 32312,
                    PaymentMethod = PaymentMethod.DirectDebit,
                    SalesReceiptDate = System.DateTime.Now.AddDays(23),
                    BankAccountId = 1987,
                    ReferenceNo = "2342",
                    Message = "New customer!"
                },
                new SalesReceipt()
                {
                    Id = 8,
                    CustomerId = 2123,
                    InvoiceId = 32312,
                    PaymentMethod = PaymentMethod.DirectDebit,
                    SalesReceiptDate = System.DateTime.Now.AddDays(23),
                    BankAccountId = 1987,
                    ReferenceNo = "2342",
                    Message = "New customer!"
                },
                new SalesReceipt()
                {
                    Id = 9,
                    CustomerId = 3123,
                    InvoiceId = 32312,
                    PaymentMethod = PaymentMethod.DirectDebit,
                    SalesReceiptDate = System.DateTime.Now.AddDays(23),
                    BankAccountId = 1987,
                    ReferenceNo = "2342",
                    Message = "New customer!"
                }
                );
        }

        private void SeedPaymentData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().HasData(
                new Payment()
                {
                    Id = 1,
                    CustomerId = 1123,
                    InvoiceId = 11232,
                    PaymentDate = System.DateTime.Now.AddDays(21),
                    PaymentMethod = PaymentMethod.Cash,
                    Memo = "Thanks!",
                    AmountReceived = 200
                },
                new Payment()
                {
                    Id = 2,
                    CustomerId = 2123,
                    InvoiceId = 11232,
                    PaymentDate = System.DateTime.Now.AddDays(21),
                    PaymentMethod = PaymentMethod.Cash,
                    Memo = "Thanks!",
                    AmountReceived = 200
                },
                new Payment()
                {
                    Id = 3,
                    CustomerId = 3123,
                    InvoiceId = 11232,
                    PaymentDate = System.DateTime.Now.AddDays(21),
                    PaymentMethod = PaymentMethod.Cash,
                    Memo = "Thanks!",
                    AmountReceived = 200
                },
                new Payment()
                {
                    Id = 4,
                    CustomerId = 1123,
                    InvoiceId = 21324,
                    PaymentDate = System.DateTime.Now.AddDays(21),
                    PaymentMethod = PaymentMethod.Cash,
                    Memo = "Thanks!",
                    AmountReceived = 200
                },
                new Payment()
                {
                    Id = 5,
                    CustomerId = 2123,
                    InvoiceId = 21324,
                    PaymentDate = System.DateTime.Now.AddDays(21),
                    PaymentMethod = PaymentMethod.Cash,
                    Memo = "Thanks!",
                    AmountReceived = 200
                },
                new Payment()
                {
                    Id = 6,
                    CustomerId = 3123,
                    InvoiceId = 21324,
                    PaymentDate = System.DateTime.Now.AddDays(21),
                    PaymentMethod = PaymentMethod.Cash,
                    Memo = "Thanks!",
                    AmountReceived = 200
                },
                new Payment()
                {
                    Id = 7,
                    CustomerId = 1123,
                    InvoiceId = 32312,
                    PaymentDate = System.DateTime.Now.AddDays(21),
                    PaymentMethod = PaymentMethod.Cash,
                    Memo = "Thanks!",
                    AmountReceived = 200
                },
                new Payment()
                {
                    Id = 8,
                    CustomerId = 2123,
                    InvoiceId = 32312,
                    PaymentDate = System.DateTime.Now.AddDays(21),
                    PaymentMethod = PaymentMethod.Cash,
                    Memo = "Thanks!",
                    AmountReceived = 200
                },
                new Payment()
                {
                    Id = 9,
                    CustomerId = 3123,
                    InvoiceId = 32312,
                    PaymentDate = System.DateTime.Now.AddDays(21),
                    PaymentMethod = PaymentMethod.Cash,
                    Memo = "Thanks!",
                    AmountReceived = 200
                }
                );
        }

        private void SeedInvoiceLineData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceLine>().HasData(
                new InvoiceLine()
                {
                    Id = 1,
                    InvoiceId = 11232,
                    Quantity = 232,
                    Rate = 24.5,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 2,
                    InvoiceId = 11232,
                    Quantity = 22,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 3,
                    InvoiceId = 11232,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 4,
                    InvoiceId = 11232,
                    Quantity = 122,
                    Rate = 223,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 5,
                    InvoiceId = 21324,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 6,
                    InvoiceId = 21324,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id =7,
                    InvoiceId = 21324,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 8,
                    InvoiceId = 21324,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 9,
                    InvoiceId = 21324,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 10,
                    InvoiceId = 32312,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 11,
                    InvoiceId = 32312,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 12,
                    InvoiceId = 32312,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 13,
                    InvoiceId = 32312,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 14,
                    InvoiceId = 32312,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 15,
                    InvoiceId = 32312,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                },
                new InvoiceLine()
                {
                    Id = 16,
                    InvoiceId = 32312,
                    Quantity = 23,
                    Rate = 11,
                    Service = ServiceType.Product,
                    VAT = 0
                }
                );
        }

        private void SeedInvoiceData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice()
                {
                    Id = 11232,
                    CustomerId = 1123,
                    DueDate = DateTime.Now.AddDays(100),
                    InvoiceDate = DateTime.Now.AddDays(3),
                    Message = "Email invoice."
                },
                new Invoice()
                {
                    Id = 21324,
                    CustomerId = 2123,
                    DueDate = DateTime.Now.AddDays(100),
                    InvoiceDate = DateTime.Now.AddDays(3),
                    Message = "Email invoice."
                },
                new Invoice()
                {
                    Id = 32312,
                    CustomerId = 3123,
                    DueDate = DateTime.Now.AddDays(100),
                    InvoiceDate = DateTime.Now.AddDays(3),
                    Message = "Email invoice."
                }
                );
        }

        private void SeedEstimateLineData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstimateLine>().HasData(
                new EstimateLine()
                {
                    Id = 1,
                    EstimateId = 1,
                    Quantity = 12,
                    Rate = 133,
                    Service = ServiceType.Service,
                    VAT = 17.5
                },
                new EstimateLine()
                {
                    Id = 2,
                    EstimateId = 2,
                    Quantity = 12,
                    Rate = 133,
                    Service = ServiceType.Service,
                    VAT = 17.5
                },
                new EstimateLine()
                {
                    Id = 3,
                    EstimateId = 3,
                    Quantity = 12,
                    Rate = 133,
                    Service = ServiceType.Service,
                    VAT = 17.5
                }
                );
        }

        private void SeedEstimateData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estimate>().HasData(
                new Estimate()
                {
                    Id = 1,
                    CustomerId = 1123,
                    InvoiceId = 11232,
                    EstimateDate = System.DateTime.Now.AddDays(1),
                    ExpirationDate = System.DateTime.Now.AddDays(21),
                    Message = "20% Discount included!"
                },
                new Estimate()
                {
                    Id = 2,
                    CustomerId = 2123,
                    InvoiceId = 11232,
                    EstimateDate = System.DateTime.Now.AddDays(1),
                    ExpirationDate = System.DateTime.Now.AddDays(21),
                    Message = "20% Discount included!"
                },
                new Estimate()
                {
                    Id = 3,
                    CustomerId = 3123,
                    InvoiceId = 21324,
                    EstimateDate = System.DateTime.Now.AddDays(1),
                    ExpirationDate = System.DateTime.Now.AddDays(21),
                    Message = "20% Discount included!"
                },
                new Estimate()
                {
                    Id = 4,
                    CustomerId = 1123,
                    InvoiceId = 21324,
                    EstimateDate = System.DateTime.Now.AddDays(1),
                    ExpirationDate = System.DateTime.Now.AddDays(21),
                    Message = "20% Discount included!"
                },
                new Estimate()
                {
                    Id = 5,
                    CustomerId = 2123,
                    InvoiceId = 21324,
                    EstimateDate = System.DateTime.Now.AddDays(1),
                    ExpirationDate = System.DateTime.Now.AddDays(21),
                    Message = "20% Discount included!"
                },
                new Estimate()
                {
                    Id = 6,
                    CustomerId = 3123,
                    InvoiceId = 11232,
                    EstimateDate = System.DateTime.Now.AddDays(1),
                    ExpirationDate = System.DateTime.Now.AddDays(21),
                    Message = "20% Discount included!"
                },
                new Estimate()
                {
                    Id = 7,
                    CustomerId = 1123,
                    InvoiceId = 32312,
                    EstimateDate = System.DateTime.Now.AddDays(1),
                    ExpirationDate = System.DateTime.Now.AddDays(21),
                    Message = "20% Discount included!"
                },
                new Estimate()
                {
                    Id = 8,
                    CustomerId = 2123,
                    InvoiceId = 32312,
                    EstimateDate = System.DateTime.Now.AddDays(1),
                    ExpirationDate = System.DateTime.Now.AddDays(21),
                    Message = "20% Discount included!"
                },
                new Estimate()
                {
                    Id = 9,
                    CustomerId = 3123,
                    InvoiceId = 32312,
                    EstimateDate = System.DateTime.Now.AddDays(1),
                    ExpirationDate = System.DateTime.Now.AddDays(21),
                    Message = "20% Discount included!"
                }
                );
        }

        private void SeedDelayedChargeLineData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DelayedChargeLine>().HasData(
                new DelayedChargeLine()
                {
                    DelayedChargeId = 1,
                    Id = 1,
                    Quantity = 21,
                    Rate = 23,
                    VAT = 17.5,
                    Service = ServiceType.Service
                },
                new DelayedChargeLine()
                {
                    DelayedChargeId = 2,
                    Id = 2,
                    Quantity = 21,
                    Rate = 23,
                    VAT = 17.5,
                    Service = ServiceType.Service
                },
                new DelayedChargeLine()
                {
                    DelayedChargeId = 3,
                    Id = 3,
                    Quantity = 21,
                    Rate = 23,
                    VAT = 17.5,
                    Service = ServiceType.Service
                }
                );
        }

        private void SeedDelayedChargeData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DelayedCharge>().HasData(
                new DelayedCharge()
                {
                    CustomerId = 1123,
                    InvoiceId = 32312,
                    Id = 1,
                    DelayedChargeDate = System.DateTime.Now.AddDays(21),
                    Message = "Delayed charge"
                },
                new DelayedCharge()
                {
                    CustomerId = 2123,
                    InvoiceId = 32312,
                    Id = 2,
                    DelayedChargeDate = System.DateTime.Now.AddDays(21),
                    Message = "Delayed charge"
                },
                new DelayedCharge()
                {
                    CustomerId = 2123,
                    InvoiceId = 32312,
                    Id = 3,
                    DelayedChargeDate = System.DateTime.Now.AddDays(1),
                    Message = "Delayed charge"
                },
                new DelayedCharge()
                {
                    CustomerId = 3123,
                    InvoiceId = 21324,
                    Id = 4,
                    DelayedChargeDate = System.DateTime.Now.AddDays(211),
                    Message = "Delayed charge"
                },
                new DelayedCharge()
                {
                    CustomerId = 3123,
                    InvoiceId = 21324,
                    Id = 5,
                    DelayedChargeDate = System.DateTime.Now.AddDays(2),
                    Message = "Delayed charge"
                },
                new DelayedCharge()
                {
                    CustomerId = 3123,
                    InvoiceId = 21324,
                    Id = 6,
                    DelayedChargeDate = System.DateTime.Now.AddDays(18),
                    Message = "Delayed charge"
                },
                new DelayedCharge()
                {
                    CustomerId = 3123,
                    InvoiceId = 32312,
                    Id = 7,
                    DelayedChargeDate = System.DateTime.Now.AddDays(211),
                    Message = "Delayed charge"
                },
                new DelayedCharge()
                {
                    CustomerId = 3123,
                    InvoiceId = 32312,
                    Id = 8,
                    DelayedChargeDate = System.DateTime.Now.AddDays(2),
                    Message = "Delayed charge"
                },
                new DelayedCharge()
                {
                    CustomerId = 3123,
                    InvoiceId = 32312,
                    Id = 9,
                    DelayedChargeDate = System.DateTime.Now.AddDays(18),
                    Message = "Delayed charge"
                }

                );
        }
        private void SeedTaxInfoData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxInfo>().HasData(
                new TaxInfo()
                {
                    CustomerId = 1123,
                    Id = 1,
                    TaxRegNo = "2341",
                    UTRNo = "23423"
                },
                new TaxInfo()
                {
                    CustomerId = 2123,
                    Id = 2,
                    TaxRegNo = "2341",
                    UTRNo = "23423"
                },
                new TaxInfo()
                {
                    CustomerId = 3123,
                    Id = 3,
                    TaxRegNo = "2341",
                    UTRNo = "23423"
                }
                );
        }

        private void SeedPaymentBillingData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentBilling>().HasData(
                new PaymentBilling()
                {
                    CustomerId = 1123,
                    Id = 1,
                    PrefferedMethod = PaymentMethod.Cheque,
                    OpeningBalance = 0.00,
                    Terms = "NA"
                },
                new PaymentBilling()
                {
                    CustomerId = 2123,
                    Id = 2,
                    PrefferedMethod = PaymentMethod.Cheque,
                    OpeningBalance = 0.00,
                    Terms = "NA"
                },
                new PaymentBilling()
                {
                    CustomerId = 3123,
                    Id = 3,
                    PrefferedMethod = PaymentMethod.Cheque,
                    OpeningBalance = 0.00,
                    Terms = "NA"
                }
                );
        }

        private void SeedNoteData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasData(
                new Note()
                {
                    CustomerId = 1123,
                    Id = 1,
                    NoteText = "Customer prefers email rather than phone contact!"
                },
                new Note()
                {
                    CustomerId = 2123,
                    Id = 2,
                    NoteText = "Customer prefers phone rather than email contact!"
                },
                new Note()
                {
                    CustomerId = 3123,
                    Id = 3,
                    NoteText = "Customer prefers mobile rather than email or landline contact!"
                }
                );
        }

        private void SeedAddressData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasData(
                new Address()
                {
                    CustomerId = 1123,
                    Id = 1,
                    Street = "29 Losonway",
                    CityTown = "West Bromwich",
                    County = "West Midlands",
                    Country = "England",
                    PostCode = "B32423"
                },
                new Address()
                {
                    CustomerId = 2123,
                    Id = 2,
                    Street = "7 Park Hill Road",
                    CityTown = "Sandwell",
                    County = "West Midlands",
                    Country = "England",
                    PostCode = "B32445"
                },
                new Address()
                {
                    CustomerId = 3123,
                    Id = 3,
                    Street = "133 Florence Road",
                    CityTown = "Smethwick",
                    County = "West Midlands",
                    Country = "England",
                    PostCode = "B32434"
                }
                );
        }

        private void SeedCustomerData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1123,
                    AccountId = 1987,
                    FirstName = "Michael",
                    LastName = "Raikkonin",
                    MiddleName = "Verstaphen",
                    Company = "MR-Verstaphen Ltd",
                    Title = "Mr",
                    Email = "Michael.Raikkonin@gmail.com",
                    Mobile = "07123456789",
                    Website = "http://www.MR-Verstaphen.com",
                    Fax = "NA",
                    Phone = "0123456789",
                    DisplayNameAs = "Michael",
                    Suffix = "MCP",
                    CreditAgreement = true
                },
                new Customer
                {
                    Id = 2123,
                    AccountId = 1987,
                    FirstName = "Timi",
                    LastName = "Schoomacher",
                    MiddleName = "Alfonso",
                    Company = "Timi-Alfonso Ltd",
                    Title = "Mr",
                    Email = "Timi.Schoomacher@Timi-Alonso.com",
                    Mobile = "07223456789",
                    Website = "http://www.Timi-Alfonso.com",
                    Fax = "NA",
                    Phone = "0223456789",
                    DisplayNameAs = "Timi",
                    Suffix = "BTEC",
                    CreditAgreement = true
                },
                new Customer
                {
                    Id = 3123,
                    AccountId = 1987,
                    FirstName = "Lewis",
                    LastName = "Sutton",
                    MiddleName = "Rosbert",
                    Company = "Sutton-Rosbert Ltd",
                    Title = "Mr",
                    Email = "Lewis.Button@Lewis-Rosbert-Button.com",
                    Mobile = "07223456789",
                    Website = "http://www.Lewis-Rosbert-Sutton.com",
                    Fax = "NA",
                    Phone = "0223456789",
                    DisplayNameAs = null,
                    Suffix = null,
                    CreditAgreement = true
                },
                new Customer
                {
                    Id = 4345,
                    AccountId = 1987,
                    FirstName = "Jenson",
                    LastName = "Hamilton",
                    MiddleName = "Singh",
                    Company = "Hamilton-Singh Ltd",
                    Title = "Mr",
                    Email = "Jenson.Hamilton@JensonSinghHam.com",
                    Mobile = "07223456789",
                    Website = "www.JensonSinghHam.com",
                    Fax = "NA",
                    Phone = "0223456789",
                    DisplayNameAs = null,
                    Suffix = null,
                    CreditAgreement = true
                }
                );
        }

        private void SeedBankAccountData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount()
                {
                    Id = 1,
                    CustomerId = 1123,
                    AccountNo = "234-2432-324",
                    SortCode = "34-234-234"
                },
                new BankAccount()
                {
                    Id = 2,
                    CustomerId = 2123,
                    AccountNo = "234-212-676",
                    SortCode = "567-345-234"
                },
                new BankAccount()
                {
                    Id = 3,
                    CustomerId = 3123,
                    AccountNo = "546-456-345",
                    SortCode = "435-456-123"
                },
                new BankAccount()
                {
                    Id = 4,
                    CustomerId = 4345,
                    AccountNo = "546-456-345",
                    SortCode = "435-456-123"
                });
        }
        #endregion
    }
}
