using AM_CustomerManager_Data_EFC.Repositories;
using AM_ExpensesManager_Core;
using AM_ExpensesManager_Core.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace AM_ExpensesManager_API.Workers
{
    public class ExpenseMessageWorker : RabMQServiceBase
    {
        private readonly ILogger<ExpenseMessageWorker> _logger;
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseMessageWorker(ILogger<ExpenseMessageWorker> logger, 
            IConfiguration configuration) : base(logger, configuration)
        {
            try
            {
                base.QueueName = "AccountManager.ExpensesIn";
                _logger = logger;

                _expenseRepository = new ExpenseRepository(configuration.GetConnectionString("ExpensesManagerConnection"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        public override bool Process(string message)
        {
            _logger.LogInformation(message);

            try
            {
                Expense expense = JsonConvert.DeserializeObject<Expense>(message);
                _expenseRepository.StoreNewExpenseAsync(expense);
            }
            catch (Exception e) {
                _logger.LogError(e,e.Message);
            }

            return true;
        }
    }
}
