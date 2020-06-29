using AM_ExpensesManager_API.Models;
using AM_ExpensesManager_Core;
using AM_ExpensesManager_Core.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
namespace AM_CustomerManager_API.Controllers
{
    [Route("api/expense/{expenseId}/[controller]")]
    [ApiController]
    public class ExpenseLineController : ControllerBase
    {
        private readonly IExpenseLineRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<ExpenseLineController> _logger;

        public ExpenseLineController(IExpenseLineRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<ExpenseLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ExpenseLineModel[]>> Get(int expenseId)
        {
            try
            {
                var results = await _repository.GetAllExpenseLinesAsync(expenseId);

                return _mapper.Map<ExpenseLineModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ExpenseLineModel>> Get(int expenseId, int Id)
        {
            try
            {
                var result = await _repository.GetExpenseLineAsync(expenseId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<ExpenseLineModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseLineModel>> Post(int expenseId, ExpenseLineModel model)
        {
            try
            {
                //Make sure ExpenseLineId is not already taken
                var existing = await _repository.GetExpenseLineAsync(expenseId, model.Id);
                if (existing != null)
                {
                    return BadRequest("ExpenseLine Id in Use");
                }

                //map
                var ExpenseLine = _mapper.Map<ExpenseLine>(model);

                //save and return
                if (!await _repository.StoreNewExpenseLineAsync(expenseId, ExpenseLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "ExpenseLine",
                            new { expenseId = ExpenseLine.ExpenseId, ExpenseLine.Id });

                    return Created(location, _mapper.Map<ExpenseLineModel>(ExpenseLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ExpenseLineModel>> Put(int expenseId, int Id, ExpenseLineModel updatedModel)
        {
            try
            {
                var currentExpenseLine = await _repository.GetExpenseLineAsync(expenseId, Id);
                if (currentExpenseLine == null) return NotFound($"Could not find ExpenseLine with Id of {Id}");

                _mapper.Map(updatedModel, currentExpenseLine);

                if (await _repository.UpdateExpenseLineAsync(expenseId, currentExpenseLine))
                {
                    return _mapper.Map<ExpenseLineModel>(currentExpenseLine);
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
        public async Task<IActionResult> Delete(int expenseId, int Id)
        {
            try
            {
                var ExpenseLine = await _repository.GetExpenseLineAsync(expenseId, Id);
                if (ExpenseLine == null) return NotFound();

                if (await _repository.DeleteExpenseLine(ExpenseLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the ExpenseLine");
        }

    }
}