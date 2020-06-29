using System;
using System.Collections.Generic;
using System.Text;

namespace AM_BackendForFrontend_Data.Generic
{
    public class AccountManagerBaseAddress: IAccountManagerBaseAddress
    {
        public Uri BaseAddress { get; set; }
        public AccountManagerBaseAddress(string baseAddress)
        {
            this.BaseAddress = new Uri(baseAddress);
        }
    }
}
