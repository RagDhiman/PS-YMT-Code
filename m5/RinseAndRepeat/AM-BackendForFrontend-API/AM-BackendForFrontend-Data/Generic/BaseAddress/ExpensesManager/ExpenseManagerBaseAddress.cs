using System;

namespace AM_BackendForFrontend_Data.Generic
{
    public class ExpenseManagerBaseAddress : IExpenseManagerBaseAddress
    {
        public Uri BaseAddress { get; set; }
        public ExpenseManagerBaseAddress(string baseAddress)
        {
            this.BaseAddress = new Uri(baseAddress);
        }
    }
}
