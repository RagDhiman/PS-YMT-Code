using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IExpenseLineRepository
    {
        Task<ExpenseLine[]> GetAllExpenseLinesAsync(int ExpenseId);
        Task<ExpenseLine> GetExpenseLineAsync(int ExpenseId, int id);
        Task<bool> StoreNewExpenseLineAsync(int ExpenseId, ExpenseLine ExpenseLine);
        Task<bool> UpdateExpenseLineAsync(int ExpenseId, ExpenseLine ExpenseLine);
        Task<bool> DeleteExpenseLine(ExpenseLine ExpenseLine);
    }
}
