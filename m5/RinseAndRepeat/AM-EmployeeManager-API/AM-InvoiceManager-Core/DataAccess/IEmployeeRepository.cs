using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_EmployeeManager_Core.DataAccess
{
    public interface IEmployeeRepository
    {
        Task<Employee[]> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeAsync(int id);
        Task<bool> StoreNewEmployeeAsync(Employee Employee);
        Task<bool> UpdateEmployeeAsync(Employee Employee);
        Task<bool> DeleteEmployee(Employee Employee);
    }
}
