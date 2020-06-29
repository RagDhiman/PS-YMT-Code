using AccountsManager_Domain;
using System.Threading.Tasks;

namespace EmployeesManager_Domain.DataAccess
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
