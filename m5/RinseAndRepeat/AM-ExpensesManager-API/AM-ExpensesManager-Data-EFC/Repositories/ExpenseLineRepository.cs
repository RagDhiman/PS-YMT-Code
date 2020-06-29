using AM_ExpensesManager_Core;
using AM_ExpensesManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class ExpenseLineRepository : DapperBaseRepository, IExpenseLineRepository
    {
        internal const string SQL =
            @"SELECT
            B.Id,
            B.ExpenseId,
            B.ServiceType,
            B.Description,
            B.Amount,
            B.VAT
            FROM vwExMExpenseLines B";

        public ExpenseLineRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteExpenseLine(ExpenseLine ExpenseLine)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spExMExpenseLinesDelete", new { ExpenseLine.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<ExpenseLine[]> GetAllExpenseLinesAsync(int ExpenseId)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var ExpenseLines = await con.QueryAsync<ExpenseLine>(sql: $"{SQL} where B.ExpenseId = @ExpenseId", new { ExpenseId }, commandType: CommandType.Text);

                return ExpenseLines.AsList().ToArray();
            }
        }

        public async Task<ExpenseLine> GetExpenseLineAsync(int? ExpenseId, int? Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var ExpenseLine = await con.QueryAsync<ExpenseLine>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return ExpenseLine.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewExpenseLineAsync(int ExpenseId, ExpenseLine ExpenseLine)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<ExpenseLine>(sql: "spExMExpenseLinesInsert",
                    new
                    {
                        ExpenseLine.ExpenseId,
                        ExpenseLine.ServiceType,
                        ExpenseLine.Description,
                        ExpenseLine.Amount,
                        ExpenseLine.VAT
                    },
                    commandType: CommandType.StoredProcedure);

                ExpenseLine.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateExpenseLineAsync(int ExpenseId, ExpenseLine ExpenseLine)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spExMExpenseLinesUpdate",
                    new
                    {
                        ExpenseLine.Id,
                        ExpenseLine.ExpenseId,
                        ExpenseLine.ServiceType,
                        ExpenseLine.Description,
                        ExpenseLine.Amount,
                        ExpenseLine.VAT
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
