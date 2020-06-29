using System;

namespace AM_BackendForFrontend_Data.Generic
{
    public class SupplierManagerBaseAddress : ISupplierManagerBaseAddress
    {
        public Uri BaseAddress { get; set; }
        public SupplierManagerBaseAddress(string baseAddress)
        {
            this.BaseAddress = new Uri(baseAddress);
        }
    }
}
