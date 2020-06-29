using AM_BackendForFrontend_Data.Data;

namespace AM_BackendForFrontend_Data.Generic
{
    public interface IInvoiceManagerRepository<T>: IGenericRepository<T> where T : IEntity
    {
    }
}
