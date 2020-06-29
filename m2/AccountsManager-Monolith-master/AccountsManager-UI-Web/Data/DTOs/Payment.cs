using System;

namespace AccountsManager_UI_Web.Data.DTOs
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Memo { get; set; }
        public double AmountReceived { get; set; }
    }
}
