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
    [Route("api/Expense/{ExpenseId}/[controller]")]
    [ApiController]
    public class ExpenseLineController : ControllerBase
    {
        private readonly IAccountManagerRepository<ExpenseLine> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<ExpenseLineController> _logger;

        public ExpenseLineController(IAccountManagerRepository<ExpenseLine> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<ExpenseLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int ExpenseId)
        {
            _repository.ResourcePath = $"api/Expense/{ExpenseId}/ExpenseLine";
        }

        [HttpGet]
        public async Task<ActionResult<ExpenseLineModel[]>> Get(int ExpenseId)
        {
            try
            {
                SetupPath(ExpenseId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<ExpenseLineModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ExpenseLineModel>> Get(int ExpenseId, int Id)
        {
            try
            {
                SetupPath(ExpenseId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<ExpenseLineModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseLineModel>> Post(int ExpenseId, ExpenseLineModel model)
        {
            try
            {
                SetupPath(ExpenseId);

                //Make sure ExpenseLineId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("ExpenseLine Id in Use");
                }

                //map
                var ExpenseLine = _mapper.Map<ExpenseLine>(model);

                //save and return
                if (!await _repository.StoreNewAsync(ExpenseLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "ExpenseLine",
                            new { Id = ExpenseLine.Id });

                    return Created(location, _mapper.Map<ExpenseLineModel>(ExpenseLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ExpenseLineModel>> Put(int ExpenseId, int Id, ExpenseLineModel updatedModel)
        {
            try
            {
                SetupPath(ExpenseId);

                var currentExpenseLine = await _repository.GetByIdAsync(Id);
                if (currentExpenseLine == null) return NotFound($"Could not find ExpenseLine with Id of {Id}");

                _mapper.Map(updatedModel, currentExpenseLine);

                if (await _repository.UpdateAsync(currentExpenseLine))
                {
                    return _mapper.Map<ExpenseLineModel>(currentExpenseLine);
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
        public async Task<IActionResult> Delete(int ExpenseId, int Id)
        {
            try
            {
                SetupPath(ExpenseId);

                var ExpenseLine = await _repository.GetByIdAsync(Id);
                if (ExpenseLine == null) return NotFound();

                if (await _repository.DeleteAsync(ExpenseLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the ExpenseLine");
        }

    }
}