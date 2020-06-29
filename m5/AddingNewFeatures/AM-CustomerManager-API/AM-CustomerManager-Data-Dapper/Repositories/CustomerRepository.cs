using AM_CustomerManager_Core;
using AM_CustomerManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_Dapper.Repositories
{
    public class CustomerRepository : DapperBaseRepository, ICustomerRepository
    {
        internal const string SQL =
            @"SELECT
                  Id, AccountId, Title, FirstName, MiddleName, LastName, Suffix, Company, ParentCompany, DisplayNameAs, Email, Phone, Mobile, Fax, Website, CreditAgreement
                FROM vwCMCustomer B";

        public CustomerRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteCustomer(Customer Customer)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spCMCustomerDelete", new { Customer.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Customer[]> GetAllCustomersAsync()
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Customers = await con.QueryAsync<Customer>(sql: $"{SQL}", commandType: CommandType.Text);

                return Customers.AsList().ToArray();
            }
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Customer = await con.QueryAsync<Customer>(sql: $"{SQL} where B.Id = @Id", new { id }, commandType: CommandType.Text);

                return Customer.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewCustomerAsync(Customer Customer)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Customer>(sql: "spCMCustomerInsert",
                    new
                    {
                        Customer.AccountId,
                        Customer.Title,
                        Customer.FirstName,
                        Customer.MiddleName,
                        Customer.LastName,
                        Customer.Suffix,
                        Customer.Company,
                        Customer.ParentCompany,
                        Customer.DisplayNameAs,
                        Customer.Email,
                        Customer.Phone,
                        Customer.Mobile,
                        Customer.Fax,
                        Customer.Website,
                        Customer.CreditAgreement
                    },
                    commandType: CommandType.StoredProcedure);

                Customer.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateCustomerAsync(Customer Customer)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spCMCustomerUpdate",
                    new
                    {
                        Customer.Id,
                        Customer.AccountId,
                        Customer.Title,
                        Customer.FirstName,
                        Customer.MiddleName,
                        Customer.LastName,
                        Customer.Suffix,
                        Customer.Company,
                        Customer.ParentCompany,
                        Customer.DisplayNameAs,
                        Customer.Email,
                        Customer.Phone,
                        Customer.Mobile,
                        Customer.Fax,
                        Customer.Website,
                        Customer.CreditAgreement
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}

