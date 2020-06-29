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
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IEmailRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<EmailController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<EmailModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllEmailsAsync();

                return _mapper.Map<EmailModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EmailModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetEmailAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<EmailModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmailModel>> Post(EmailModel model)
        {
            try
            {
                //Make sure EmailId is not already taken
                var existing = await _repository.GetEmailAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Email Id in Use");
                }

                //map
                var customer = _mapper.Map<Email>(model);

                //save and return
                if (!await _repository.StoreNewEmailAsync(customer))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Email",
                            new { customer.Id });

                    return Created(location, _mapper.Map<EmailModel>(customer));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<EmailModel>> Put(int Id, EmailModel updatedModel)
        {
            try
            {
                var currentEmail = await _repository.GetEmailAsync(Id);
                if (currentEmail == null) return NotFound($"Could not find customer with Id of {Id}");

                _mapper.Map(updatedModel, currentEmail);

                if (await _repository.UpdateEmailAsync(currentEmail))
                {
                    return _mapper.Map<EmailModel>(currentEmail);
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
                var customer = await _repository.GetEmailAsync(Id);
                if (customer == null) return NotFound();

                if (await _repository.DeleteEmail(customer))
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