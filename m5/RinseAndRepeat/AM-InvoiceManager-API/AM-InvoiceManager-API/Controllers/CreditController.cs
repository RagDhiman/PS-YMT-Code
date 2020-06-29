using AM_InvoiceManager_API.Models;
using AM_InvoiceManager_Core;
using AM_InvoiceManager_Core.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
namespace AM_CustomerManager_API.Controllers
{
    [Route("api/invoice/{invoiceId}/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly ICreditRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CreditController> _logger;

        public CreditController(ICreditRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<CreditController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CreditModel[]>> Get(int invoiceId)
        {
            try
            {
                var results = await _repository.GetAllCreditesAsync(invoiceId);

                return _mapper.Map<CreditModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CreditModel>> Get(int invoiceId, int Id)
        {
            try
            {
                var result = await _repository.GetCreditAsync(invoiceId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<CreditModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreditModel>> Post(int invoiceId, CreditModel model)
        {
            try
            {
                //Make sure CreditId is not already taken
                var existing = await _repository.GetCreditAsync(invoiceId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Credit Id in Use");
                }

                //map
                var Credit = _mapper.Map<Credit>(model);

                //save and return
                if (!await _repository.StoreNewCreditAsync(invoiceId, Credit))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Credit",
                            new { invoiceId = Credit.InvoiceId, Credit.Id });

                    return Created(location, _mapper.Map<CreditModel>(Credit));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CreditModel>> Put(int invoiceId, int Id, CreditModel updatedModel)
        {
            try
            {
                var currentCredit = await _repository.GetCreditAsync(invoiceId, Id);
                if (currentCredit == null) return NotFound($"Could not find Credit with Id of {Id}");

                _mapper.Map(updatedModel, currentCredit);

                if (await _repository.UpdateCreditAsync(invoiceId, currentCredit))
                {
                    return _mapper.Map<CreditModel>(currentCredit);
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
        public async Task<IActionResult> Delete(int invoiceId, int Id)
        {
            try
            {
                var Credit = await _repository.GetCreditAsync(invoiceId, Id);
                if (Credit == null) return NotFound();

                if (await _repository.DeleteCredit(Credit))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Credit");
        }

    }
}