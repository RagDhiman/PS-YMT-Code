﻿using AM_BackendForFrontend_Data.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AM_BackendForFrontend_Data.Generic
{
    public class CustomerManagerRepository<T> : GenericRepository<T>, ICustomerManagerRepository<T> where T : IEntity
    {
        public CustomerManagerRepository(IConfiguration configuration, ICustomerManagerBaseAddress baseAddress) : base(configuration, baseAddress)
        {}
    }
}
