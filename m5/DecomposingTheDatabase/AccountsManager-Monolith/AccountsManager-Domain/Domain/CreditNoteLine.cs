namespace AccountsManager_Domain
{
    public class CreditNoteLine
    {
        public int Id { get; set; }
        public int CreditNoteId { get; set; }
        public CreditNote CreditNote { get; set; }
        public ServiceType Service { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double VAT { get; set; }
    }
}
