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
    [Route("api/Invoice/{InvoiceId}/[controller]")]
    [ApiController]
    public class InvoiceLineController : ControllerBase
    {
        private readonly IInvoiceLineRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<InvoiceLineController> _logger;

        public InvoiceLineController(IInvoiceLineRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<InvoiceLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<InvoiceLineModel[]>> GetAll(int InvoiceId)
        {
            try
            {
                var results = await _repository.GetAllInvoiceLinesAsync(InvoiceId);

                return _mapper.Map<InvoiceLineModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<InvoiceLineModel>> Get(int InvoiceId, int Id)
        {
            try
            {
                var result = await _repository.GetInvoiceLineAsync(InvoiceId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<InvoiceLineModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceLineModel>> Post(int InvoiceId, InvoiceLineModel model)
        {
            try
            {
                //Make sure InvoiceLineId is not already taken
                var existing = await _repository.GetInvoiceLineAsync(InvoiceId, model.Id);
                if (existing != null)
                {
                    return BadRequest("InvoiceLine Id in Use");
                }

                //map
                var InvoiceLine = _mapper.Map<InvoiceLine>(model);

                //save and return
                if (!await _repository.StoreNewInvoiceLineAsync(InvoiceId, InvoiceLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "InvoiceLine",
                            new { InvoiceLine.InvoiceId, InvoiceLine.Id });

                    return Created(location, _mapper.Map<InvoiceLineModel>(InvoiceLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<InvoiceLineModel>> Put(int InvoiceId, int Id, InvoiceLineModel updatedModel)
        {
            try
            {
                var currentInvoiceLine = await _repository.GetInvoiceLineAsync(InvoiceId, Id);
                if (currentInvoiceLine == null) return NotFound($"Could not find InvoiceLine with Id of {Id}");

                _mapper.Map(updatedModel, currentInvoiceLine);

                if (await _repository.UpdateInvoiceLineAsync(InvoiceId, currentInvoiceLine))
                {
                    return _mapper.Map<InvoiceLineModel>(currentInvoiceLine);
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
                var InvoiceLine = await _repository.GetInvoiceLineAsync(InvoiceId, Id);
                if (InvoiceLine == null) return NotFound();

                if (await _repository.DeleteInvoiceLine(InvoiceLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the InvoiceLine");
        }

    }
}