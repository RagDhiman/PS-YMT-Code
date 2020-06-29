using AM_BackendForFrontend_Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM_BackendForFrontend_Data.Generic
{
    public interface ICustomerManagerRepository<T>: IGenericRepository<T> where T : IEntity
    {
    }
}
