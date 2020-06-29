using AM_CustomerManager_Core;
using AM_CustomerManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_Dapper.Repositories
{
    public class BankAccountRepository : DapperBaseRepository, IBankAccountRepository
    {
        internal const string SQL =
            @"SELECT
                [Id]
                ,[CustomerId]
                ,[AccountNo]
                ,[SortCode]
                FROM vwCMBankAccount B";

        public BankAccountRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteBankAccount(BankAccount BankAccount)
        {
            using (IDbConnection con = this.GetConnection())
            {
                 await con.ExecuteAsync(sql: "spCMBankAccountDelete", new { BankAccount.Id }, commandType: CommandType.StoredProcedure); 
            }

            return true;
        }

        public async Task<BankAccount[]> GetAllBankAccountesAsync(int CustomerId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var bankAccounts = await con.QueryAsync<BankAccount>(sql: $"{SQL} where B.CustomerId = @CustomerId", new { CustomerId }, commandType: CommandType.Text);

                return bankAccounts.AsList().ToArray();
            }
        }

        public async Task<BankAccount> GetBankAccountAsync(int? CustomerId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var bankAccount = await con.QueryAsync<BankAccount>(sql: $"{SQL} where B.Id = @Id and B.CustomerId = @CustomerId", new { Id, CustomerId }, commandType: CommandType.Text);

                return bankAccount.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewBankAccountAsync(int CustomerId, BankAccount BankAccount)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<BankAccount>(sql: "spCMBankAccountInsert", 
                    new {BankAccount.CustomerId,
                    BankAccount.AccountNo,
                    BankAccount.SortCode}, 
                    commandType: CommandType.StoredProcedure);

                BankAccount.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateBankAccountAsync(int CustomerId, BankAccount BankAccount)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spCMBankAccountUpdate",
                    new
                    {
                        BankAccount.Id,
                        BankAccount.CustomerId,
                        BankAccount.AccountNo,
                        BankAccount.SortCode
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
