using AccountsManager_UI_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.ViewModels
{
    public class InvoiceDashViewModel
    {
        public InvoiceDetailModel Invoice { get; set; }
        public IEnumerable<InvoiceLineIndexModel> InvoiceLines { get; set; }
        public IEnumerable<PaymentIndexModel> Payments { get; set; }
        public IEnumerable<SalesReceiptIndexModel> SalesReceipts { get; set; }
        public IEnumerable<EstimateIndexModel> Estimates { get; set; }
        public IEnumerable<DelayedChargeIndexModel> DelayedCharge { get; set; }
        public IEnumerable<CreditNoteIndexModel> CreditNotes { get; set; }
        public CreditListViewModel CreditListViewModel { get; set; }
    }
}
