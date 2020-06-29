using AM_EmployeeManager_Core;
using AM_EmployeeManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class EmployeeRepository : DapperBaseRepository, IEmployeeRepository
    {
        internal const string SQL =
            @"SELECT
                B.Id,
                B.AccountId,
                B.Title,
                B.FirstName,
                B.LastName,
                B.DisplayNameAs,
                B.Address,
                B.Notes,
                B.Email,
                B.Phone,
                B.Mobile,
                B.DOB
                FROM vwEMEmployees B";

        public EmployeeRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteEmployee(Employee Employee)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMEmployeeDelete", new { Employee.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Employee[]> GetAllEmployeesAsync()
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Employees = await con.QueryAsync<Employee>(sql: $"{SQL}", commandType: CommandType.Text);

                return Employees.AsList().ToArray();
            }
        }

        public async Task<Employee> GetEmployeeAsync(int Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Employee = await con.QueryAsync<Employee>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Employee.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewEmployeeAsync(Employee Employee)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Employee>(sql: "spEMEmployeeInsert",
                    new
                    {
                        Employee.AccountId,
                        Employee.Title,
                        Employee.FirstName,
                        Employee.LastName,
                        Employee.DisplayNameAs,
                        Employee.Address,
                        Employee.Notes,
                        Employee.Email,
                        Employee.Phone,
                        Employee.Mobile,
                        Employee.DOB
                    },
                    commandType: CommandType.StoredProcedure);

                Employee.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee Employee)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spEMEmployeeUpdate",
                    new
                    {
                        Employee.Id,
                        Employee.AccountId,
                        Employee.Title,
                        Employee.FirstName,
                        Employee.LastName,
                        Employee.DisplayNameAs,
                        Employee.Address,
                        Employee.Notes,
                        Employee.Email,
                        Employee.Phone,
                        Employee.Mobile,
                        Employee.DOB
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
