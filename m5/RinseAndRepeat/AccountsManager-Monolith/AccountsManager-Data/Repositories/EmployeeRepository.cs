using AccountsManager_Data;
using AccountsManager_Domain;
using EmployeesManager_Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AccountManagerContext _context;

        public EmployeeRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<Employee[]> GetAllEmployeesAsync()
        {
            IQueryable<Employee> query = _context.Employees;

            query = query.OrderByDescending(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            IQueryable<Employee> query = _context.Employees;

            return await query.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewEmployeeAsync(Employee Employee)
        {
            _context.Employees.Add(Employee);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee Employee)
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteEmployee(Employee Employee)
        {
            _context.Remove(Employee);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
