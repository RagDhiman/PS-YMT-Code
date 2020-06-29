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
    [Route("api/employee/{employeeId}/[controller]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        private readonly IAccountManagerRepository<Pay> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<PayController> _logger;

        public PayController(IAccountManagerRepository<Pay> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<PayController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int employeeId)
        {
            _repository.ResourcePath = $"api/employee/{employeeId}/Pay";
        }

        [HttpGet]
        public async Task<ActionResult<PayModel[]>> Get(int employeeId)
        {
            try
            {
                SetupPath(employeeId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<PayModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PayModel>> Get(int employeeId, int Id)
        {
            try
            {
                SetupPath(employeeId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<PayModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PayModel>> Post(int employeeId, PayModel model)
        {
            try
            {
                SetupPath(employeeId);

                //Make sure PayId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Pay Id in Use");
                }

                //map
                var Pay = _mapper.Map<Pay>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Pay))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Pay",
                            new { Id = Pay.Id });

                    return Created(location, _mapper.Map<PayModel>(Pay));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<PayModel>> Put(int employeeId, int Id, PayModel updatedModel)
        {
            try
            {
                SetupPath(employeeId);

                var currentPay = await _repository.GetByIdAsync(Id);
                if (currentPay == null) return NotFound($"Could not find Pay with Id of {Id}");

                _mapper.Map(updatedModel, currentPay);

                if (await _repository.UpdateAsync(currentPay))
                {
                    return _mapper.Map<PayModel>(currentPay);
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
        public async Task<IActionResult> Delete(int employeeId, int Id)
        {
            try
            {
                SetupPath(employeeId);

                var Pay = await _repository.GetByIdAsync(Id);
                if (Pay == null) return NotFound();

                if (await _repository.DeleteAsync(Pay))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Pay");
        }

    }
}