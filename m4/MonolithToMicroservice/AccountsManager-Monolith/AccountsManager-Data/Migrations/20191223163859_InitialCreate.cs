using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountsManager_Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Absences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Paid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyEmail = table.Column<string>(nullable: true),
                    CompanySMSSender = table.Column<string>(nullable: true),
                    NewCustomerCRMWebhook = table.Column<string>(nullable: true),
                    NoOfUserLicences = table.Column<int>(nullable: false),
                    RenewalDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    CityTown = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    AccountNo = table.Column<string>(nullable: true),
                    SortCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditNoteLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditNoteId = table.Column<int>(nullable: false),
                    Service = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    VAT = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNoteLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    CreditNoteDate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(nullable: false),
                    CreditDate = table.Column<DateTime>(nullable: false),
                    CreditAmount = table.Column<double>(nullable: false),
                    ProductCredit = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    SortCode = table.Column<string>(nullable: true),
                    HasCreditAgreement = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Suffix = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    DisplayNameAs = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    CreditAgreement = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DelayedChargeLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DelayedChargeId = table.Column<int>(nullable: false),
                    Service = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    VAT = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelayedChargeLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DelayedCharges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    DelayedChargeDate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelayedCharges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendTo = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    SentDateTime = table.Column<DateTime>(nullable: false),
                    DeliveredDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DisplayNameAs = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    LoanStartDateTime = table.Column<DateTime>(nullable: false),
                    LoanEndDateTime = table.Column<DateTime>(nullable: false),
                    Reference = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ExpectedReturnDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstimateLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstimateId = table.Column<int>(nullable: false),
                    Service = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    VAT = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimateLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estimates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    EstimateDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estimates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseId = table.Column<int>(nullable: false),
                    ServiceType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    VAT = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayeeName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    BankAccountId = table.Column<int>(nullable: false),
                    Reference = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    OnCall = table.Column<bool>(nullable: false),
                    OnCallRateMultiplier = table.Column<int>(nullable: false),
                    Paid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteText = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    AccountNo = table.Column<string>(nullable: true),
                    SortCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    AmountReceived = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HourlyRate = table.Column<double>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    DefaultRate = table.Column<bool>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(nullable: false),
                    BankAccountId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    SalesReceiptDate = table.Column<DateTime>(nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    ReferenceNo = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReceipts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SMSs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendTo = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    SentDateTime = table.Column<DateTime>(nullable: false),
                    DeliveredDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    StardDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ProductSubscription = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplierNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteText = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    ContactName = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxInformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    VAT = table.Column<bool>(nullable: false),
                    VATRef = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Certification = table.Column<bool>(nullable: false),
                    CertificationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    VoucherCode = table.Column<string>(nullable: true),
                    AppliedDate = table.Column<DateTime>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    CreditAmount = table.Column<double>(nullable: false),
                    ProductVoucher = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebhookPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    PostDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentBillings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    PrefferedMethod = table.Column<int>(nullable: false),
                    Terms = table.Column<string>(nullable: true),
                    OpeningBalance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentBillings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentBillings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxRegNo = table.Column<string>(nullable: true),
                    UTRNo = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxInfos_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesReceiptLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesReceiptId = table.Column<int>(nullable: false),
                    Service = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    VAT = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReceiptLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesReceiptLines_SalesReceipts_SalesReceiptId",
                        column: x => x.SalesReceiptId,
                        principalTable: "SalesReceipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(nullable: false),
                    Service = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    VAT = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Absences",
                columns: new[] { "Id", "Description", "EmployeeId", "EndDateTime", "Notes", "Paid", "StartDateTime" },
                values: new object[,]
                {
                    { 1, "Sickness", 1, new DateTime(2019, 12, 24, 16, 38, 58, 759, DateTimeKind.Local).AddTicks(9266), "Sickness", true, new DateTime(2019, 12, 22, 16, 38, 58, 759, DateTimeKind.Local).AddTicks(8769) },
                    { 2, "Sickness", 2, new DateTime(2019, 12, 24, 16, 38, 58, 759, DateTimeKind.Local).AddTicks(9804), "Sickness", true, new DateTime(2019, 12, 22, 16, 38, 58, 759, DateTimeKind.Local).AddTicks(9782) }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CompanyEmail", "CompanyName", "CompanySMSSender", "NewCustomerCRMWebhook", "NoOfUserLicences", "RenewalDate" },
                values: new object[] { 1987, "rag.dhiman@NehaKD.com", "NehaKD", "0712345678", "https://nehacrm.api.com/customer", 10, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityTown", "Country", "County", "CustomerId", "PostCode", "Street" },
                values: new object[,]
                {
                    { 1, "West Bromwich", "England", "West Midlands", 1123, "B32423", "29 Losonway" },
                    { 2, "Sandwell", "England", "West Midlands", 2123, "B32445", "7 Park Hill Road" },
                    { 3, "Smethwick", "England", "West Midlands", 3123, "B32434", "133 Florence Road" }
                });

            migrationBuilder.InsertData(
                table: "Attachments",
                columns: new[] { "Id", "FilePath", "SupplierId" },
                values: new object[,]
                {
                    { 1, "//supplier/234/agreement.txt", 1 },
                    { 2, "//supplier/234/agreement.txt", 2 }
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNo", "CustomerId", "SortCode" },
                values: new object[,]
                {
                    { 3, "546-456-345", 3123, "435-456-123" },
                    { 4, "546-456-345", 4345, "435-456-123" },
                    { 1, "234-2432-324", 1123, "34-234-234" },
                    { 2, "234-212-676", 2123, "567-345-234" }
                });

            migrationBuilder.InsertData(
                table: "CreditNoteLines",
                columns: new[] { "Id", "CreditNoteId", "Quantity", "Rate", "Service", "VAT" },
                values: new object[,]
                {
                    { 2, 2, 2.0, 2.3999999999999999, 1, 23.0 },
                    { 1, 1, 2.0, 2.3999999999999999, 1, 23.0 }
                });

            migrationBuilder.InsertData(
                table: "CreditNotes",
                columns: new[] { "Id", "CreditNoteDate", "CustomerId", "InvoiceId", "Message" },
                values: new object[,]
                {
                    { 7, new DateTime(2019, 12, 20, 17, 22, 58, 748, DateTimeKind.Local).AddTicks(9386), 1123, 21324, "Credit Note" },
                    { 9, new DateTime(2019, 12, 20, 17, 22, 58, 748, DateTimeKind.Local).AddTicks(9393), 1123, 21324, "Credit Note" },
                    { 1, new DateTime(2019, 12, 21, 16, 50, 58, 746, DateTimeKind.Local).AddTicks(6342), 1123, 11232, "Credit Note" },
                    { 2, new DateTime(2019, 12, 19, 18, 41, 58, 748, DateTimeKind.Local).AddTicks(9326), 1123, 11232, "Credit Note" },
                    { 3, new DateTime(2019, 12, 27, 6, 22, 58, 748, DateTimeKind.Local).AddTicks(9368), 1123, 11232, "Credit Note" },
                    { 4, new DateTime(2019, 12, 18, 16, 39, 58, 748, DateTimeKind.Local).AddTicks(9375), 1123, 11232, "Credit Note" },
                    { 5, new DateTime(2019, 12, 20, 17, 22, 58, 748, DateTimeKind.Local).AddTicks(9378), 1123, 21324, "Credit Note" },
                    { 6, new DateTime(2019, 12, 20, 17, 22, 58, 748, DateTimeKind.Local).AddTicks(9382), 1123, 21324, "Credit Note" },
                    { 8, new DateTime(2019, 12, 20, 17, 22, 58, 748, DateTimeKind.Local).AddTicks(9389), 1123, 21324, "Credit Note" }
                });

            migrationBuilder.InsertData(
                table: "Credits",
                columns: new[] { "Id", "AccountNo", "CreditAmount", "CreditDate", "CustomerName", "HasCreditAgreement", "InvoiceId", "ProductCredit", "SortCode" },
                values: new object[,]
                {
                    { 2, "234-212-676", 231.0, new DateTime(2020, 4, 21, 16, 38, 58, 758, DateTimeKind.Local).AddTicks(1986), "Mr Timi Schumacher Alfonso", true, 21324, 1, "567-345-234" },
                    { 1, "234-2432-324", 231.0, new DateTime(2020, 4, 21, 16, 38, 58, 757, DateTimeKind.Local).AddTicks(9660), "Mr Michael Raikkonin Verstaphen", true, 11232, 2, "34-234-234" },
                    { 3, "546-456-345", 231.0, new DateTime(2020, 4, 21, 16, 38, 58, 758, DateTimeKind.Local).AddTicks(2033), "Mr Lewis Rosbert Sutton", true, 32312, 4, "435-456-123" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AccountId", "Company", "CreditAgreement", "DisplayNameAs", "Email", "Fax", "FirstName", "LastName", "MiddleName", "Mobile", "Phone", "Suffix", "Title", "Website" },
                values: new object[,]
                {
                    { 4345, 1987, "Hamilton-Singh Ltd", true, null, "Jenson.Hamilton@JensonSinghHam.com", "NA", "Jenson", "Hamilton", "Singh", "07223456789", "0223456789", null, "Mr", "www.JensonSinghHam.com" },
                    { 3123, 1987, "Sutton-Rosbert Ltd", true, null, "Lewis.Button@Lewis-Rosbert-Button.com", "NA", "Lewis", "Sutton", "Rosbert", "07223456789", "0223456789", null, "Mr", "http://www.Lewis-Rosbert-Sutton.com" },
                    { 1123, 1987, "MR-Verstaphen Ltd", true, "Michael", "Michael.Raikkonin@gmail.com", "NA", "Michael", "Raikkonin", "Verstaphen", "07123456789", "0123456789", "MCP", "Mr", "http://www.MR-Verstaphen.com" },
                    { 2123, 1987, "Timi-Alfonso Ltd", true, "Timi", "Timi.Schoomacher@Timi-Alonso.com", "NA", "Timi", "Schoomacher", "Alfonso", "07223456789", "0223456789", "BTEC", "Mr", "http://www.Timi-Alfonso.com" }
                });

            migrationBuilder.InsertData(
                table: "DelayedChargeLines",
                columns: new[] { "Id", "DelayedChargeId", "Quantity", "Rate", "Service", "VAT" },
                values: new object[,]
                {
                    { 3, 3, 21.0, 23.0, 1, 17.5 },
                    { 2, 2, 21.0, 23.0, 1, 17.5 },
                    { 1, 1, 21.0, 23.0, 1, 17.5 }
                });

            migrationBuilder.InsertData(
                table: "DelayedCharges",
                columns: new[] { "Id", "CustomerId", "DelayedChargeDate", "InvoiceId", "Message" },
                values: new object[,]
                {
                    { 4, 3123, new DateTime(2020, 7, 21, 16, 38, 58, 750, DateTimeKind.Local).AddTicks(2426), 21324, "Delayed charge" },
                    { 3, 2123, new DateTime(2019, 12, 24, 16, 38, 58, 750, DateTimeKind.Local).AddTicks(2421), 32312, "Delayed charge" },
                    { 2, 2123, new DateTime(2020, 1, 13, 16, 38, 58, 750, DateTimeKind.Local).AddTicks(2389), 32312, "Delayed charge" },
                    { 9, 3123, new DateTime(2020, 1, 10, 16, 38, 58, 750, DateTimeKind.Local).AddTicks(2444), 32312, "Delayed charge" },
                    { 1, 1123, new DateTime(2020, 1, 13, 16, 38, 58, 750, DateTimeKind.Local).AddTicks(1388), 32312, "Delayed charge" },
                    { 7, 3123, new DateTime(2020, 7, 21, 16, 38, 58, 750, DateTimeKind.Local).AddTicks(2437), 32312, "Delayed charge" },
                    { 6, 3123, new DateTime(2020, 1, 10, 16, 38, 58, 750, DateTimeKind.Local).AddTicks(2433), 21324, "Delayed charge" },
                    { 5, 3123, new DateTime(2019, 12, 25, 16, 38, 58, 750, DateTimeKind.Local).AddTicks(2430), 21324, "Delayed charge" },
                    { 8, 3123, new DateTime(2019, 12, 25, 16, 38, 58, 750, DateTimeKind.Local).AddTicks(2441), 32312, "Delayed charge" }
                });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "DeliveredDateTime", "Message", "SendTo", "Sender", "SentDateTime", "Subject" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 12, 23, 17, 8, 58, 766, DateTimeKind.Local).AddTicks(386), "Invoice sent!", "2342352354324", "Bob Smith", new DateTime(2019, 12, 23, 16, 38, 58, 765, DateTimeKind.Local).AddTicks(9882), "Invoice Sent!" },
                    { 2, new DateTime(2019, 12, 23, 17, 8, 58, 766, DateTimeKind.Local).AddTicks(1769), "Invoice sent!", "2342352354324", "Bob Smith", new DateTime(2019, 12, 23, 16, 38, 58, 766, DateTimeKind.Local).AddTicks(1749), "Invoice Sent!" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AccountId", "Address", "DOB", "DisplayNameAs", "Email", "FirstName", "LastName", "Mobile", "Notes", "Phone", "Title" },
                values: new object[,]
                {
                    { 1, 1987, "23 Parkhill Road Smethwick B66 53N", new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EddieD", "eddie.dennies@AllFinances298.com", "Freddie", "Dennies", "072341123441", "Paternity due soon!", "0121324435", "Mr" },
                    { 2, 1987, "23 Windmill Road Smethwick B66 34R", new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EddieD", "ron.jordan@AllFinances298.com", "Jon", "Jordan", "072341123441", "Paternity due soon!", "0121324435", "Mr" }
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "EmployeeId", "ExpectedReturnDate", "LoanEndDateTime", "LoanStartDateTime", "Name", "Reference" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 7, 10, 16, 38, 58, 762, DateTimeKind.Local).AddTicks(1259), new DateTime(2020, 1, 12, 16, 38, 58, 762, DateTimeKind.Local).AddTicks(2253), new DateTime(2019, 12, 23, 16, 38, 58, 762, DateTimeKind.Local).AddTicks(1770), "De 23l Laptop", "D32432" },
                    { 2, 2, new DateTime(2020, 7, 10, 16, 38, 58, 762, DateTimeKind.Local).AddTicks(3641), new DateTime(2020, 1, 12, 16, 38, 58, 762, DateTimeKind.Local).AddTicks(3673), new DateTime(2019, 12, 23, 16, 38, 58, 762, DateTimeKind.Local).AddTicks(3662), "De 23l Laptop", "D32432" }
                });

            migrationBuilder.InsertData(
                table: "EstimateLines",
                columns: new[] { "Id", "EstimateId", "Quantity", "Rate", "Service", "VAT" },
                values: new object[,]
                {
                    { 2, 2, 12.0, 133.0, 1, 17.5 },
                    { 3, 3, 12.0, 133.0, 1, 17.5 },
                    { 1, 1, 12.0, 133.0, 1, 17.5 }
                });

            migrationBuilder.InsertData(
                table: "Estimates",
                columns: new[] { "Id", "CustomerId", "EstimateDate", "ExpirationDate", "InvoiceId", "Message" },
                values: new object[,]
                {
                    { 9, 3123, new DateTime(2019, 12, 24, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7198), new DateTime(2020, 1, 13, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7201), 32312, "20% Discount included!" },
                    { 7, 1123, new DateTime(2019, 12, 24, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7186), new DateTime(2020, 1, 13, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7188), 32312, "20% Discount included!" },
                    { 8, 2123, new DateTime(2019, 12, 24, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7192), new DateTime(2020, 1, 13, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7195), 32312, "20% Discount included!" },
                    { 5, 2123, new DateTime(2019, 12, 24, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7173), new DateTime(2020, 1, 13, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7175), 21324, "20% Discount included!" },
                    { 6, 3123, new DateTime(2019, 12, 24, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7179), new DateTime(2020, 1, 13, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7182), 11232, "20% Discount included!" },
                    { 4, 1123, new DateTime(2019, 12, 24, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7166), new DateTime(2020, 1, 13, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7169), 21324, "20% Discount included!" },
                    { 3, 3123, new DateTime(2019, 12, 24, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7158), new DateTime(2020, 1, 13, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7162), 21324, "20% Discount included!" },
                    { 2, 2123, new DateTime(2019, 12, 24, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7119), new DateTime(2020, 1, 13, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(7140), 11232, "20% Discount included!" },
                    { 1, 1123, new DateTime(2019, 12, 24, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(5644), new DateTime(2020, 1, 13, 16, 38, 58, 751, DateTimeKind.Local).AddTicks(6150), 11232, "20% Discount included!" }
                });

            migrationBuilder.InsertData(
                table: "ExpenseLines",
                columns: new[] { "Id", "Amount", "Description", "ExpenseId", "ServiceType", "VAT" },
                values: new object[,]
                {
                    { 2, 200.0, "Regular", 2, 1, 23.0 },
                    { 1, 200.0, "Regular", 1, 1, 23.0 }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "BankAccountId", "CustomerId", "EmployeeId", "Notes", "PayeeName", "PaymentDate", "PaymentMethod", "Reference", "SupplierId" },
                values: new object[,]
                {
                    { 1, 1987, 0, 1, "One-off", "Art Tech", new DateTime(2019, 12, 21, 16, 38, 58, 762, DateTimeKind.Local).AddTicks(8608), 1, "TESDSFD-324", 0 },
                    { 2, 1987, 0, 1, "One-off", "Art Tech", new DateTime(2019, 12, 21, 16, 38, 58, 763, DateTimeKind.Local).AddTicks(392), 1, "G43534-324", 0 }
                });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "EmployeeId", "EndDateTime", "OnCall", "OnCallRateMultiplier", "Paid", "StartDateTime" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2019, 12, 26, 16, 38, 58, 759, DateTimeKind.Local).AddTicks(2194), true, 2, true, new DateTime(2019, 12, 25, 16, 38, 58, 759, DateTimeKind.Local).AddTicks(1579) },
                    { 2, 2, new DateTime(2019, 12, 26, 16, 38, 58, 759, DateTimeKind.Local).AddTicks(2877), true, 2, true, new DateTime(2019, 12, 25, 16, 38, 58, 759, DateTimeKind.Local).AddTicks(2857) }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "CustomerId", "NoteText" },
                values: new object[,]
                {
                    { 1, 1123, "Customer prefers email rather than phone contact!" },
                    { 2, 2123, "Customer prefers phone rather than email contact!" },
                    { 3, 3123, "Customer prefers mobile rather than email or landline contact!" }
                });

            migrationBuilder.InsertData(
                table: "PaymentDetails",
                columns: new[] { "Id", "AccountId", "AccountNo", "SortCode" },
                values: new object[,]
                {
                    { 2, 1987, "567 567", "2342 234" },
                    { 1, 1987, "768 43230", "5675 234" },
                    { 3, 1987, "234 546", "234265 234" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "AmountReceived", "CustomerId", "InvoiceId", "Memo", "PaymentDate", "PaymentMethod" },
                values: new object[,]
                {
                    { 2, 200.0, 2123, 11232, "Thanks!", new DateTime(2020, 1, 13, 16, 38, 58, 754, DateTimeKind.Local).AddTicks(6908), 1 },
                    { 3, 200.0, 3123, 11232, "Thanks!", new DateTime(2020, 1, 13, 16, 38, 58, 754, DateTimeKind.Local).AddTicks(6992), 1 },
                    { 4, 200.0, 1123, 21324, "Thanks!", new DateTime(2020, 1, 13, 16, 38, 58, 754, DateTimeKind.Local).AddTicks(6998), 1 },
                    { 5, 200.0, 2123, 21324, "Thanks!", new DateTime(2020, 1, 13, 16, 38, 58, 754, DateTimeKind.Local).AddTicks(7002), 1 },
                    { 6, 200.0, 3123, 21324, "Thanks!", new DateTime(2020, 1, 13, 16, 38, 58, 754, DateTimeKind.Local).AddTicks(7005), 1 },
                    { 7, 200.0, 1123, 32312, "Thanks!", new DateTime(2020, 1, 13, 16, 38, 58, 754, DateTimeKind.Local).AddTicks(7009), 1 },
                    { 8, 200.0, 2123, 32312, "Thanks!", new DateTime(2020, 1, 13, 16, 38, 58, 754, DateTimeKind.Local).AddTicks(7012), 1 },
                    { 9, 200.0, 3123, 32312, "Thanks!", new DateTime(2020, 1, 13, 16, 38, 58, 754, DateTimeKind.Local).AddTicks(7016), 1 },
                    { 1, 200.0, 1123, 11232, "Thanks!", new DateTime(2020, 1, 13, 16, 38, 58, 754, DateTimeKind.Local).AddTicks(5074), 1 }
                });

            migrationBuilder.InsertData(
                table: "Pays",
                columns: new[] { "Id", "DefaultRate", "EmployeeId", "EndTime", "HourlyRate", "StartTime" },
                values: new object[,]
                {
                    { 2, true, 2, new DateTime(2020, 7, 10, 16, 38, 58, 761, DateTimeKind.Local).AddTicks(6998), 10.0, new DateTime(2019, 12, 23, 16, 38, 58, 761, DateTimeKind.Local).AddTicks(6979) },
                    { 1, true, 1, new DateTime(2020, 7, 10, 16, 38, 58, 761, DateTimeKind.Local).AddTicks(6462), 10.0, new DateTime(2019, 12, 23, 16, 38, 58, 761, DateTimeKind.Local).AddTicks(5964) }
                });

            migrationBuilder.InsertData(
                table: "SMSs",
                columns: new[] { "Id", "DeliveredDateTime", "Message", "SendTo", "Sender", "SentDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 12, 23, 17, 8, 58, 765, DateTimeKind.Local).AddTicks(4484), "Invoice sent!", "2342352354324", "Bob Smith", new DateTime(2019, 12, 23, 16, 38, 58, 765, DateTimeKind.Local).AddTicks(3974) },
                    { 2, new DateTime(2019, 12, 23, 17, 8, 58, 765, DateTimeKind.Local).AddTicks(5454), "Invoice sent!", "2342352354324", "Bill Smith", new DateTime(2019, 12, 23, 16, 38, 58, 765, DateTimeKind.Local).AddTicks(5434) }
                });

            migrationBuilder.InsertData(
                table: "SalesReceipts",
                columns: new[] { "Id", "BankAccountId", "CustomerId", "InvoiceId", "Message", "PaymentMethod", "ReferenceNo", "SalesReceiptDate" },
                values: new object[,]
                {
                    { 9, 1987, 3123, 32312, "New customer!", 3, "2342", new DateTime(2020, 1, 15, 16, 38, 58, 755, DateTimeKind.Local).AddTicks(6987) },
                    { 8, 1987, 2123, 32312, "New customer!", 3, "2342", new DateTime(2020, 1, 15, 16, 38, 58, 755, DateTimeKind.Local).AddTicks(6984) },
                    { 6, 1987, 3123, 21324, "New customer!", 3, "2342", new DateTime(2020, 1, 15, 16, 38, 58, 755, DateTimeKind.Local).AddTicks(6976) },
                    { 5, 1987, 2123, 21324, "New customer!", 3, "2342", new DateTime(2020, 1, 15, 16, 38, 58, 755, DateTimeKind.Local).AddTicks(6972) },
                    { 7, 1987, 1123, 32312, "New customer!", 3, "2342", new DateTime(2020, 1, 15, 16, 38, 58, 755, DateTimeKind.Local).AddTicks(6980) },
                    { 3, 1987, 3123, 11232, "New customer!", 3, "2342", new DateTime(2020, 1, 15, 16, 38, 58, 755, DateTimeKind.Local).AddTicks(6962) },
                    { 2, 1987, 2123, 11232, "New customer!", 3, "2342", new DateTime(2020, 1, 15, 16, 38, 58, 755, DateTimeKind.Local).AddTicks(6922) },
                    { 1, 1987, 1123, 11232, "New customer!", 3, "2342", new DateTime(2020, 1, 15, 16, 38, 58, 755, DateTimeKind.Local).AddTicks(5068) },
                    { 4, 1987, 1123, 21324, "New customer!", 3, "2342", new DateTime(2020, 1, 15, 16, 38, 58, 755, DateTimeKind.Local).AddTicks(6968) }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AccountId", "EndDate", "ProductSubscription", "StardDate" },
                values: new object[,]
                {
                    { 2, 1987, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2020, 4, 1, 16, 38, 58, 756, DateTimeKind.Local).AddTicks(6887) },
                    { 3, 1987, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2020, 4, 1, 16, 38, 58, 756, DateTimeKind.Local).AddTicks(7423) },
                    { 4, 1987, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2020, 4, 1, 16, 38, 58, 756, DateTimeKind.Local).AddTicks(7443) },
                    { 5, 1987, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 4, 1, 16, 38, 58, 756, DateTimeKind.Local).AddTicks(7448) },
                    { 6, 1987, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2020, 4, 1, 16, 38, 58, 756, DateTimeKind.Local).AddTicks(7451) }
                });

            migrationBuilder.InsertData(
                table: "SupplierNotes",
                columns: new[] { "Id", "NoteText", "SupplierId" },
                values: new object[,]
                {
                    { 2, "No weekend deliveries!", 2 },
                    { 1, "No weekend deliveries!", 1 }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "AccountId", "Company", "ContactName", "Email", "Fax", "Mobile", "Phone", "Website" },
                values: new object[,]
                {
                    { 2, 1987, "Renotton-Ltd", "Mr Malavio Fritorie", "Malavio.Fritorie@Renotton.com", "2242342", "4432432", "334232", "www.Renotton-ltd.com" },
                    { 1, 1987, "Berrari-Ltd", "Mr Enzo Berrari", "Enzo@Berrari.com", "2342342", "1232432", "324232", "http://www.berrariltd.com" }
                });

            migrationBuilder.InsertData(
                table: "TaxInformations",
                columns: new[] { "Id", "EmployeeId", "TaxCode", "VAT", "VATRef" },
                values: new object[,]
                {
                    { 1, 1, "DFE543R323E", true, "VD-3R32342E" },
                    { 2, 2, "DFE543R323E", true, "VD-3R32342E" }
                });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "Certification", "CertificationName", "Description", "EmployeeId", "EndDateTime", "Name", "StartDateTime" },
                values: new object[,]
                {
                    { 1, true, "MCP", "SQL Server MCP", 1, new DateTime(2020, 4, 1, 16, 38, 58, 760, DateTimeKind.Local).AddTicks(6134), "SQL MCP Training", new DateTime(2019, 9, 14, 16, 38, 58, 760, DateTimeKind.Local).AddTicks(5640) },
                    { 2, true, "MCP", "SQL Server MCP", 2, new DateTime(2020, 4, 1, 16, 38, 58, 760, DateTimeKind.Local).AddTicks(6686), "SQL MCP Training", new DateTime(2019, 9, 14, 16, 38, 58, 760, DateTimeKind.Local).AddTicks(6666) }
                });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "AccountId", "AppliedDate", "CreditAmount", "ExpireDate", "ProductVoucher", "VoucherCode" },
                values: new object[,]
                {
                    { 1, 1987, new DateTime(2019, 12, 23, 16, 38, 58, 757, DateTimeKind.Local).AddTicks(1869), 0.0, new DateTime(2020, 4, 1, 16, 38, 58, 757, DateTimeKind.Local).AddTicks(2383), 0, "dsf3234" },
                    { 2, 1987, new DateTime(2019, 12, 23, 16, 38, 58, 757, DateTimeKind.Local).AddTicks(3786), 0.0, new DateTime(2020, 1, 15, 16, 38, 58, 757, DateTimeKind.Local).AddTicks(3808), 3, "dsf3234" },
                    { 3, 1987, new DateTime(2019, 12, 23, 16, 38, 58, 757, DateTimeKind.Local).AddTicks(3842), 0.0, new DateTime(2020, 1, 15, 16, 38, 58, 757, DateTimeKind.Local).AddTicks(3845), 4, "dsf3234" }
                });

            migrationBuilder.InsertData(
                table: "WebhookPosts",
                columns: new[] { "Id", "Body", "PostDateTime", "Sender", "URL" },
                values: new object[,]
                {
                    { 1, "Invoice sent!", new DateTime(2019, 12, 23, 16, 38, 58, 766, DateTimeKind.Local).AddTicks(5190), "Bob Smith", "Invoice Sent!" },
                    { 2, "Invoice sent!", new DateTime(2019, 12, 23, 16, 38, 58, 766, DateTimeKind.Local).AddTicks(6681), "Bob Smith", "Invoice Sent!" }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "CustomerId", "DueDate", "InvoiceDate", "Message" },
                values: new object[,]
                {
                    { 11232, 1123, new DateTime(2020, 4, 1, 16, 38, 58, 752, DateTimeKind.Local).AddTicks(7108), new DateTime(2019, 12, 26, 16, 38, 58, 752, DateTimeKind.Local).AddTicks(7624), "Email invoice." },
                    { 21324, 2123, new DateTime(2020, 4, 1, 16, 38, 58, 752, DateTimeKind.Local).AddTicks(8590), new DateTime(2019, 12, 26, 16, 38, 58, 752, DateTimeKind.Local).AddTicks(8611), "Email invoice." },
                    { 32312, 3123, new DateTime(2020, 4, 1, 16, 38, 58, 752, DateTimeKind.Local).AddTicks(8628), new DateTime(2019, 12, 26, 16, 38, 58, 752, DateTimeKind.Local).AddTicks(8631), "Email invoice." }
                });

            migrationBuilder.InsertData(
                table: "PaymentBillings",
                columns: new[] { "Id", "CustomerId", "OpeningBalance", "PrefferedMethod", "Terms" },
                values: new object[,]
                {
                    { 1, 1123, 0.0, 2, "NA" },
                    { 2, 2123, 0.0, 2, "NA" },
                    { 3, 3123, 0.0, 2, "NA" }
                });

            migrationBuilder.InsertData(
                table: "SalesReceiptLines",
                columns: new[] { "Id", "Quantity", "Rate", "SalesReceiptId", "Service", "VAT" },
                values: new object[,]
                {
                    { 1, 0.0, 23.0, 1, 0, 0.0 },
                    { 2, 0.0, 23.0, 2, 0, 0.0 },
                    { 3, 0.0, 23.0, 3, 0, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "TaxInfos",
                columns: new[] { "Id", "CustomerId", "TaxRegNo", "UTRNo" },
                values: new object[,]
                {
                    { 1, 1123, "2341", "23423" },
                    { 2, 2123, "2341", "23423" },
                    { 3, 3123, "2341", "23423" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceLines",
                columns: new[] { "Id", "InvoiceId", "Quantity", "Rate", "Service", "VAT" },
                values: new object[,]
                {
                    { 1, 11232, 232.0, 24.5, 0, 0.0 },
                    { 2, 11232, 22.0, 11.0, 0, 0.0 },
                    { 3, 11232, 23.0, 11.0, 0, 0.0 },
                    { 4, 11232, 122.0, 223.0, 0, 0.0 },
                    { 5, 21324, 23.0, 11.0, 0, 0.0 },
                    { 6, 21324, 23.0, 11.0, 0, 0.0 },
                    { 7, 21324, 23.0, 11.0, 0, 0.0 },
                    { 8, 21324, 23.0, 11.0, 0, 0.0 },
                    { 9, 21324, 23.0, 11.0, 0, 0.0 },
                    { 10, 32312, 23.0, 11.0, 0, 0.0 },
                    { 11, 32312, 23.0, 11.0, 0, 0.0 },
                    { 12, 32312, 23.0, 11.0, 0, 0.0 },
                    { 13, 32312, 23.0, 11.0, 0, 0.0 },
                    { 14, 32312, 23.0, 11.0, 0, 0.0 },
                    { 15, 32312, 23.0, 11.0, 0, 0.0 },
                    { 16, 32312, 23.0, 11.0, 0, 0.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_InvoiceId",
                table: "InvoiceLines",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBillings_CustomerId",
                table: "PaymentBillings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReceiptLines_SalesReceiptId",
                table: "SalesReceiptLines",
                column: "SalesReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxInfos_CustomerId",
                table: "TaxInfos",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absences");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "CreditNoteLines");

            migrationBuilder.DropTable(
                name: "CreditNotes");

            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropTable(
                name: "DelayedChargeLines");

            migrationBuilder.DropTable(
                name: "DelayedCharges");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "EstimateLines");

            migrationBuilder.DropTable(
                name: "Estimates");

            migrationBuilder.DropTable(
                name: "ExpenseLines");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "InvoiceLines");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "PaymentBillings");

            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Pays");

            migrationBuilder.DropTable(
                name: "SalesReceiptLines");

            migrationBuilder.DropTable(
                name: "SMSs");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SupplierNotes");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "TaxInformations");

            migrationBuilder.DropTable(
                name: "TaxInfos");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "WebhookPosts");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "SalesReceipts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
