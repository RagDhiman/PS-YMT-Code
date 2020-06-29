using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.DataAccess
{
    public interface IExpenseRepository
    {
        Task<Expense[]> GetAllExpensesAsync();
        Task<Expense> GetExpenseAsync(int id);
        Task<bool> StoreNewExpenseAsync(Expense Expense);
        Task<bool> UpdateExpenseAsync(Expense Expense);
        Task<bool> DeleteExpense(Expense Expense);
    }
}
