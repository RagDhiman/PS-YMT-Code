using System.Collections.Generic;
using AccountsManager_UI_Web.Data.DTOs;
using AccountsManager_UI_Web.Models;

namespace AccountsManager_UI_Web.ViewModels
{
    public class CustomerDashViewModel
    {
        public CustomerDetailsModel CustomerDetails { get; set; }
        public IEnumerable<AddressIndexModel> AddressesDetails { get; set; }
        public IEnumerable<NoteIndexModel> NoteDetails { get; set; }
        public IEnumerable<TaxInfoIndexModel> TaxInfoDetails { get; set; }
        public IEnumerable<BankAccountIndexModel> BankAccounts { get; set; }
        public IEnumerable<PaymentBillingIndexModel> PaymentBillingDetails { get; set; }
    }
}