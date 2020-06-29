using System;
using System.Collections.Generic;
using System.Text;

namespace AM_BackendForFrontend_Data.Generic
{
    public class CustomerManagerBaseAddress: ICustomerManagerBaseAddress
    {
        public Uri BaseAddress { get; set; }
        public CustomerManagerBaseAddress(string baseAddress)
        {
            this.BaseAddress = new Uri(baseAddress);
        }
    }
}
