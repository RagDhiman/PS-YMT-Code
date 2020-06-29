using AccountsManager_Domain;
using AccountsManager_Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Data.Repositories
{
    public class ExpenseLineRepository : IExpenseLineRepository
    {
        private readonly AccountManagerContext _context;

        public ExpenseLineRepository(AccountManagerContext context)
        {
            _context = context;
        }
        public async Task<ExpenseLine[]> GetAllExpenseLinesAsync(int ExpenseId)
        {
            IQueryable<ExpenseLine> query = _context.ExpenseLines;

            query = query
                .Where(a => a.ExpenseId == ExpenseId);

            return await query.ToArrayAsync();
        }

        public async Task<ExpenseLine> GetExpenseLineAsync(int ExpenseId, int id)
        {
            IQueryable<ExpenseLine> query = _context.ExpenseLines;

            return await query.Where(a => a.ExpenseId == ExpenseId && a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> StoreNewExpenseLineAsync(int ExpenseId, ExpenseLine ExpenseLine)
        {
            ExpenseLine.ExpenseId = ExpenseId;

            _context.ExpenseLines.Add(ExpenseLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateExpenseLineAsync(int ExpenseId, ExpenseLine ExpenseLine)
        {
            ExpenseLine.ExpenseId = ExpenseId;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteExpenseLine(ExpenseLine ExpenseLine)
        {
            _context.Remove(ExpenseLine);
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
