using AM_BackendForFrontend_API.Models;
using AM_BackendForFrontend_Core;
using AM_BackendForFrontend_Data.Data;
using AM_BackendForFrontend_Data.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AM_BackendForFrontend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IAccountManagerRepository<Expense> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(IAccountManagerRepository<Expense> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<ExpenseController> logger)
        {
            _repository = repository;
            _repository.ResourcePath = "api/Expense";

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ExpenseModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<ExpenseModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ExpenseModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<ExpenseModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseModel>> Post(ExpenseModel model)
        {
            try
            {
                //Make sure ExpenseId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Expense Id in Use");
                }

                //map
                var Expense = _mapper.Map<Expense>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Expense))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Expense",
                            new { Id = Expense.Id });

                    return Created(location, _mapper.Map<ExpenseModel>(Expense));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ExpenseModel>> Put(int Id, ExpenseModel updatedModel)
        {
            try
            {
                var currentExpense = await _repository.GetByIdAsync(Id);
                if (currentExpense == null) return NotFound($"Could not find Expense with Id of {Id}");

                _mapper.Map(updatedModel, currentExpense);

                if (await _repository.UpdateAsync(currentExpense))
                {
                    return _mapper.Map<ExpenseModel>(currentExpense);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var Expense = await _repository.GetByIdAsync(Id);
                if (Expense == null) return NotFound();

                if (await _repository.DeleteAsync(Expense))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Expense");
        }

    }
}