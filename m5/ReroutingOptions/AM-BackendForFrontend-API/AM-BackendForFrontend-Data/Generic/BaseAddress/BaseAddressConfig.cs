using System;
using System.Collections.Generic;
using System.Text;

namespace AM_BackendForFrontend_Data.Generic
{
    public class BaseAddressConfig : IBaseAddress
    {
        public Uri BaseAddress { get; set;  }
    }
}
