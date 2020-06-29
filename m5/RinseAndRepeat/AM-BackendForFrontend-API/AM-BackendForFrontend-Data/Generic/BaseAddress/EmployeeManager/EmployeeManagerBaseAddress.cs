using System;

namespace AM_BackendForFrontend_Data.Generic
{
    public class EmployeeManagerBaseAddress: IEmployeeManagerBaseAddress
    {
        public Uri BaseAddress { get; set; }
        public EmployeeManagerBaseAddress(string baseAddress)
        {
            this.BaseAddress = new Uri(baseAddress);
        }
    }
}
