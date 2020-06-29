using AccountsManager_API.Models;
using AccountsManager_Domain;
using AccountsManager_Domain.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AccountsManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(IExpenseRepository repository, IMapper mapper, LinkGenerator linkGenerator, 
            ILogger<ExpenseController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ExpenseModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllExpensesAsync();

                return _mapper.Map<ExpenseModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ExpenseModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetExpenseAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<ExpenseModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseModel>> Post(ExpenseModel model)
        {
            try
            {
                //Make sure ExpenseId is not already taken
                var existing = await _repository.GetExpenseAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Expense Id in Use");
                }

                //map
                var Expense = _mapper.Map<Expense>(model);

                //save and return
                if (!await _repository.StoreNewExpenseAsync(Expense))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Expense",
                            new { Expense.Id });

                    return Created(location, _mapper.Map<ExpenseModel>(Expense));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ExpenseModel>> Put(int Id, ExpenseModel updatedModel)
        {
            try
            {
                var currentExpense = await _repository.GetExpenseAsync(Id);
                if (currentExpense == null) return NotFound($"Could not find Expense with Id of {Id}");

                _mapper.Map(updatedModel, currentExpense);

                if (await _repository.UpdateExpenseAsync(currentExpense))
                {
                    return _mapper.Map<ExpenseModel>(currentExpense);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var Expense = await _repository.GetExpenseAsync(Id);
                if (Expense == null) return NotFound();

                if (await _repository.DeleteExpense(Expense))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Expense");
        }

    }
}