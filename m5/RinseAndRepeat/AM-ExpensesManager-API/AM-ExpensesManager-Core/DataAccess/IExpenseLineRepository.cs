using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_ExpensesManager_Core.DataAccess
{
    public interface IExpenseLineRepository
    {
        Task<ExpenseLine[]> GetAllExpenseLinesAsync(int CustomerId);
        Task<ExpenseLine> GetExpenseLineAsync(int? CustomerId, int? id);
        Task<bool> StoreNewExpenseLineAsync(int CustomerId, ExpenseLine ExpenseLine);
        Task<bool> UpdateExpenseLineAsync(int CustomerId, ExpenseLine ExpenseLine);
        Task<bool> DeleteExpenseLine(ExpenseLine ExpenseLine);
    }
}
