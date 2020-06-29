using AM_ExpensesManager_Core;
using AM_ExpensesManager_Core.DataAccess;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AM_CustomerManager_Data_EFC.Repositories
{
    public class ExpenseRepository : DapperBaseRepository, IExpenseRepository
    {
        internal const string SQL =
            @"SELECT
                        B.Id,
                        B.PayeeName,
                        B.CustomerId,
                        B.SupplierId,
                        B.EmployeeId,
                        B.PaymentDate,
                        B.PaymentMethod,
                        B.BankAccountId,
                        B.Reference,
                        B.Notes
                FROM vwExMExpenses B";

        public ExpenseRepository(String connectionString)
            : base(connectionString)
        { }

        public async Task<bool> DeleteExpense(Expense Expense)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spExMExpensesDelete", new { Expense.Id }, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public async Task<Expense[]> GetAllExpensesAsync()
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Expense = await con.QueryAsync<Expense>(sql: $"{SQL}", commandType: CommandType.Text);

                return Expense.AsList().ToArray();
            }
        }

        public async Task<Expense> GetExpenseAsync(int Id)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var Expense = await con.QueryAsync<Expense>(sql: $"{SQL} where B.Id = @Id", new { Id }, commandType: CommandType.Text);

                return Expense.FirstOrDefault();
            }
        }

        public async Task<bool> StoreNewExpenseAsync(Expense Expense)
        {
            using (IDbConnection con = this.GetConnection())
            {
                var result = await con.QueryAsync<Expense>(sql: "spExMExpensesInsert",
                    new
                    {
                        Expense.PayeeName,
                        Expense.CustomerId,
                        Expense.SupplierId,
                        Expense.EmployeeId,
                        Expense.PaymentDate,
                        Expense.PaymentMethod,
                        Expense.BankAccountId,
                        Expense.Reference,
                        Expense.Notes
                    },
                    commandType: CommandType.StoredProcedure);

                Expense.Id = result.FirstOrDefault().Id;
            }

            return true;
        }

        public async Task<bool> UpdateExpenseAsync(Expense Expense)
        {
            using (IDbConnection con = this.GetConnection())
            {
                await con.ExecuteAsync(sql: "spExMExpensesUpdate",
                    new
                    {
                        Expense.Id,
                        Expense.PayeeName,
                        Expense.CustomerId,
                        Expense.SupplierId,
                        Expense.EmployeeId,
                        Expense.PaymentDate,
                        Expense.PaymentMethod,
                        Expense.BankAccountId,
                        Expense.Reference,
                        Expense.Notes
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return true;
        }
    }
}
