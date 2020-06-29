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
    [Route("api/employee/{employeeId}/[controller]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        private readonly IPayRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<PayController> _logger;

        public PayController(IPayRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<PayController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PayModel[]>> GetAll(int employeeId)
        {
            try
            {
                var results = await _repository.GetAllPaysAsync(employeeId);

                return _mapper.Map<PayModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PayModel>> Get(int employeeId, int Id)
        {
            try
            {
                var result = await _repository.GetPayAsync(employeeId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<PayModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PayModel>> Post(int employeeId, PayModel model)
        {
            try
            {
                //Make sure PayId is not already taken
                var existing = await _repository.GetPayAsync(employeeId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Pay Id in Use");
                }

                //map
                var Pay = _mapper.Map<Pay>(model);

                //save and return
                if (!await _repository.StoreNewPayAsync(employeeId, Pay))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Pay",
                            new { employeeId = Pay.EmployeeId, Pay.Id });

                    return Created(location, _mapper.Map<PayModel>(Pay));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<PayModel>> Put(int employeeId, int Id, PayModel updatedModel)
        {
            try
            {
                var currentPay = await _repository.GetPayAsync(employeeId, Id);
                if (currentPay == null) return NotFound($"Could not find Pay with Id of {Id}");

                _mapper.Map(updatedModel, currentPay);

                if (await _repository.UpdatePayAsync(employeeId, currentPay))
                {
                    return _mapper.Map<PayModel>(currentPay);
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
        public async Task<IActionResult> Delete(int employeeId, int Id)
        {
            try
            {
                var Pay = await _repository.GetPayAsync(employeeId, Id);
                if (Pay == null) return NotFound();

                if (await _repository.DeletePay(Pay))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Pay");
        }

    }
}