using System;

namespace AM_BackendForFrontend_Data.Generic
{
    public class InvoiceManagerBaseAddress: IInvoiceManagerBaseAddress
    {
        public Uri BaseAddress { get; set; }
        public InvoiceManagerBaseAddress(string baseAddress)
        {
            this.BaseAddress = new Uri(baseAddress);
        }
    }
}
