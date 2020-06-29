using AM_BackendForFrontend_Data.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AM_BackendForFrontend_Data.Generic
{
    public class ExpenseManagerRepository<T> : GenericRepository<T>, IExpenseManagerRepository<T> where T : IEntity
    {
        public ExpenseManagerRepository(IConfiguration configuration, IExpenseManagerBaseAddress baseAddress) : base(configuration, baseAddress)
        {}
    }
}
