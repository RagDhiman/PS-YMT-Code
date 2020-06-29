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
    public class InvoiceManagerRepository<T> : GenericRepository<T>, IInvoiceManagerRepository<T> where T : IEntity
    {
        public InvoiceManagerRepository(IConfiguration configuration, IInvoiceManagerBaseAddress baseAddress) : base(configuration, baseAddress)
        {}
    }
}
