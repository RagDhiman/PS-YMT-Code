using AM_CustomerManager_Core;
using Microsoft.EntityFrameworkCore;

namespace AM_CustomerManager_Data_EFC
{
    public class AccountManagerContext : DbContext
    {
        #region dbsets
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<PaymentBilling> PaymentBillings { get; set; }
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
            //SeedCustomerData(modelBuilder);
            //SeedBankAccountData(modelBuilder);
            //SeedAddressData(modelBuilder);
            //SeedNoteData(modelBuilder);
            //SeedPaymentBillingData(modelBuilder);
            //SeedTaxInfoData(modelBuilder);
        }
        private void ConfigureModels(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(e => e.Id);
            modelBuilder.Entity<Address>().HasKey(e => e.Id);
            modelBuilder.Entity<Note>().HasKey(e => e.Id);
            modelBuilder.Entity<PaymentBilling>().HasKey(e => e.Id);
            modelBuilder.Entity<TaxInfo>().HasKey(e => e.Id);
        }

        #region seedData

        //private void SeedTaxInfoData(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TaxInfo>().HasData(
        //        new TaxInfo()
        //        {
        //            CustomerId = 1123,
        //            Id = 1,
        //            TaxRegNo = "2341",
        //            UTRNo = "23423"
        //        },
        //        new TaxInfo()
        //        {
        //            CustomerId = 2123,
        //            Id = 2,
        //            TaxRegNo = "2341",
        //            UTRNo = "23423"
        //        },
        //        new TaxInfo()
        //        {
        //            CustomerId = 3123,
        //            Id = 3,
        //            TaxRegNo = "2341",
        //            UTRNo = "23423"
        //        }
        //        );
        //}

        //private void SeedPaymentBillingData(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PaymentBilling>().HasData(
        //        new PaymentBilling()
        //        {
        //            CustomerId = 1123,
        //            Id = 1,
        //            PrefferedMethod = PaymentMethod.Cheque,
        //            OpeningBalance = 0.00,
        //            Terms = "NA"
        //        },
        //        new PaymentBilling()
        //        {
        //            CustomerId = 2123,
        //            Id = 2,
        //            PrefferedMethod = PaymentMethod.Cheque,
        //            OpeningBalance = 0.00,
        //            Terms = "NA"
        //        },
        //        new PaymentBilling()
        //        {
        //            CustomerId = 3123,
        //            Id = 3,
        //            PrefferedMethod = PaymentMethod.Cheque,
        //            OpeningBalance = 0.00,
        //            Terms = "NA"
        //        }
        //        );
        //}

        //private void SeedNoteData(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Note>().HasData(
        //        new Note()
        //        {
        //            CustomerId = 1123,
        //            Id = 1,
        //            NoteText = "Customer prefers email rather than phone contact!"
        //        },
        //        new Note()
        //        {
        //            CustomerId = 2123,
        //            Id = 2,
        //            NoteText = "Customer prefers phone rather than email contact!"
        //        },
        //        new Note()
        //        {
        //            CustomerId = 3123,
        //            Id = 3,
        //            NoteText = "Customer prefers mobile rather than email or landline contact!"
        //        }
        //        );
        //}

        //private void SeedAddressData(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Address>().HasData(
        //        new Address()
        //        {
        //            CustomerId = 1123,
        //            Id = 1,
        //            Street = "29 Losonway",
        //            CityTown = "West Bromwich",
        //            County = "West Midlands",
        //            Country = "England",
        //            PostCode = "B32423"
        //        },
        //        new Address()
        //        {
        //            CustomerId = 2123,
        //            Id = 2,
        //            Street = "7 Park Hill Road",
        //            CityTown = "Sandwell",
        //            County = "West Midlands",
        //            Country = "England",
        //            PostCode = "B32445"
        //        },
        //        new Address()
        //        {
        //            CustomerId = 3123,
        //            Id = 3,
        //            Street = "133 Florence Road",
        //            CityTown = "Smethwick",
        //            County = "West Midlands",
        //            Country = "England",
        //            PostCode = "B32434"
        //        }
        //        );
        //}

        //private void SeedCustomerData(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Customer>().HasData(
        //        new Customer
        //        {
        //            Id = 1123,
        //            AccountId = 1987,
        //            FirstName = "Michael",
        //            LastName = "Raikkonin",
        //            MiddleName = "Verstaphen",
        //            Company = "MR-Verstaphen Ltd",
        //            Title = "Mr",
        //            Email = "Michael.Raikkonin@gmail.com",
        //            Mobile = "07123456789",
        //            Website = "http://www.MR-Verstaphen.com",
        //            Fax = "NA",
        //            Phone = "0123456789",
        //            DisplayNameAs = "Michael",
        //            Suffix = "MCP",
        //            CreditAgreement = true
        //        },
        //        new Customer
        //        {
        //            Id = 2123,
        //            AccountId = 1987,
        //            FirstName = "Timi",
        //            LastName = "Schoomacher",
        //            MiddleName = "Alfonso",
        //            Company = "Timi-Alfonso Ltd",
        //            Title = "Mr",
        //            Email = "Timi.Schoomacher@Timi-Alonso.com",
        //            Mobile = "07223456789",
        //            Website = "http://www.Timi-Alfonso.com",
        //            Fax = "NA",
        //            Phone = "0223456789",
        //            DisplayNameAs = "Timi",
        //            Suffix = "BTEC",
        //            CreditAgreement = true
        //        },
        //        new Customer
        //        {
        //            Id = 3123,
        //            AccountId = 1987,
        //            FirstName = "Lewis",
        //            LastName = "Sutton",
        //            MiddleName = "Rosbert",
        //            Company = "Sutton-Rosbert Ltd",
        //            Title = "Mr",
        //            Email = "Lewis.Button@Lewis-Rosbert-Button.com",
        //            Mobile = "07223456789",
        //            Website = "http://www.Lewis-Rosbert-Sutton.com",
        //            Fax = "NA",
        //            Phone = "0223456789",
        //            DisplayNameAs = null,
        //            Suffix = null,
        //            CreditAgreement = true
        //        },
        //        new Customer
        //        {
        //            Id = 4345,
        //            AccountId = 1987,
        //            FirstName = "Jenson",
        //            LastName = "Hamilton",
        //            MiddleName = "Singh",
        //            Company = "Hamilton-Singh Ltd",
        //            Title = "Mr",
        //            Email = "Jenson.Hamilton@JensonSinghHam.com",
        //            Mobile = "07223456789",
        //            Website = "www.JensonSinghHam.com",
        //            Fax = "NA",
        //            Phone = "0223456789",
        //            DisplayNameAs = null,
        //            Suffix = null,
        //            CreditAgreement = true
        //        }
        //        );
        //}

        //private void SeedBankAccountData(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<BankAccount>().HasData(
        //        new BankAccount()
        //        {
        //            Id = 1,
        //            CustomerId = 1123,
        //            AccountNo = "234-2432-324",
        //            SortCode = "34-234-234"
        //        },
        //        new BankAccount()
        //        {
        //            Id = 2,
        //            CustomerId = 2123,
        //            AccountNo = "234-212-676",
        //            SortCode = "567-345-234"
        //        },
        //        new BankAccount()
        //        {
        //            Id = 3,
        //            CustomerId = 3123,
        //            AccountNo = "546-456-345",
        //            SortCode = "435-456-123"
        //        },
        //        new BankAccount()
        //        {
        //            Id = 4,
        //            CustomerId = 4345,
        //            AccountNo = "546-456-345",
        //            SortCode = "435-456-123"
        //        });
        //}
        #endregion
    }
}
