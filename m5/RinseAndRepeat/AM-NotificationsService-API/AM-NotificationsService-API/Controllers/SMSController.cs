using AM_NotificationsService_API.Models;
using AM_NotificationsService_Core;
using AM_NotificationsService_Core.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AM_NotificationsService_API.Controllers
{
    [Route("api/notification/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly ISMSRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SMSController> _logger;

        public SMSController(ISMSRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<SMSController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<SMSModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllSMSsAsync();

                return _mapper.Map<SMSModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SMSModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetSMSAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<SMSModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SMSModel>> Post(SMSModel model)
        {
            try
            {
                //Make sure SMSId is not already taken
                var existing = await _repository.GetSMSAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("SMS Id in Use");
                }

                //map
                var customer = _mapper.Map<SMS>(model);

                //save and return
                if (!await _repository.StoreNewSMSAsync(customer))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "SMS",
                            new { customer.Id });

                    return Created(location, _mapper.Map<SMSModel>(customer));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SMSModel>> Put(int Id, SMSModel updatedModel)
        {
            try
            {
                var currentSMS = await _repository.GetSMSAsync(Id);
                if (currentSMS == null) return NotFound($"Could not find customer with Id of {Id}");

                _mapper.Map(updatedModel, currentSMS);

                if (await _repository.UpdateSMSAsync(currentSMS))
                {
                    return _mapper.Map<SMSModel>(currentSMS);
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
                var customer = await _repository.GetSMSAsync(Id);
                if (customer == null) return NotFound();

                if (await _repository.DeleteSMS(customer))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the customer");
        }

    }
}