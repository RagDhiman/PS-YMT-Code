using AccountsManager_API.Models;
using AccountsManager_Domain;
using AccountsManager_Domain.DataAccess;
using AccountsManager_Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AccountsManager_API.Controllers
{
    [Route("api/invoice/{InvoiceId}/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly ICreditRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CreditController> _logger;
        private readonly ICustomerCreditService _creditService;

        public CreditController(ICreditRepository repository, ICustomerCreditService creditService, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<CreditController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
            _creditService = creditService;
        }

        [HttpGet]
        public async Task<ActionResult<CreditModel[]>> GetAll(int InvoiceId)
        {
            try
            {
                var results = await _repository.GetAllCreditsAsync(InvoiceId);

                return _mapper.Map<CreditModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CreditModel>> Get(int InvoiceId, int Id)
        {
            try
            {
                var result = await _repository.GetCreditAsync(InvoiceId, Id);

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
                var credit = _mapper.Map<Credit>(model);

                //save and return
                if (!await _creditService.SaveAndProcessCredit(_repository, invoiceId, credit))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Credit",
                            new { credit.InvoiceId, credit.Id });

                    return Created(location, _mapper.Map<CreditModel>(credit));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CreditModel>> Put(int InvoiceId, int Id, CreditModel updatedModel)
        {
            try
            {
                var currentCredit = await _repository.GetCreditAsync(InvoiceId, Id);
                if (currentCredit == null) return NotFound($"Could not find Credit with Id of {Id}");

                _mapper.Map(updatedModel, currentCredit);

                if (await _repository.UpdateCreditAsync(InvoiceId, currentCredit))
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
        public async Task<IActionResult> Delete(int InvoiceId, int Id)
        {
            try
            {
                var Credit = await _repository.GetCreditAsync(InvoiceId, Id);
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