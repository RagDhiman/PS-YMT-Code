using AM_BackendForFrontend_Data.Data;

namespace AM_BackendForFrontend_Data.Generic
{
    public interface IEmployeeManagerRepository<T>: IGenericRepository<T> where T : IEntity
    {
    }
}
