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
    [Route("api/expense/{ExpenseId}/[controller]")]
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
        public async Task<ActionResult<ExpenseLineModel[]>> GetAll(int ExpenseId)
        {
            try
            {
                var results = await _repository.GetAllExpenseLinesAsync(ExpenseId);

                return _mapper.Map<ExpenseLineModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ExpenseLineModel>> Get(int ExpenseId, int Id)
        {
            try
            {
                var result = await _repository.GetExpenseLineAsync(ExpenseId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<ExpenseLineModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseLineModel>> Post(int ExpenseId, ExpenseLineModel model)
        {
            try
            {
                //Make sure ExpenseLineId is not already taken
                var existing = await _repository.GetExpenseLineAsync(ExpenseId, model.Id);
                if (existing != null)
                {
                    return BadRequest("ExpenseLine Id in Use");
                }

                //map
                var ExpenseLine = _mapper.Map<ExpenseLine>(model);

                //save and return
                if (!await _repository.StoreNewExpenseLineAsync(ExpenseId, ExpenseLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "ExpenseLine",
                            new { ExpenseLine.ExpenseId, ExpenseLine.Id });

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
        public async Task<ActionResult<ExpenseLineModel>> Put(int ExpenseId, int Id, ExpenseLineModel updatedModel)
        {
            try
            {
                var currentExpenseLine = await _repository.GetExpenseLineAsync(ExpenseId, Id);
                if (currentExpenseLine == null) return NotFound($"Could not find ExpenseLine with Id of {Id}");

                _mapper.Map(updatedModel, currentExpenseLine);

                if (await _repository.UpdateExpenseLineAsync(ExpenseId, currentExpenseLine))
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
        public async Task<IActionResult> Delete(int ExpenseId, int Id)
        {
            try
            {
                var ExpenseLine = await _repository.GetExpenseLineAsync(ExpenseId, Id);
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