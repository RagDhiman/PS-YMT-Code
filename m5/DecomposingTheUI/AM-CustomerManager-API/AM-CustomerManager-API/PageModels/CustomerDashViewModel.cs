using System.Collections.Generic;

namespace AM_CustomerManager_API.PageModels
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