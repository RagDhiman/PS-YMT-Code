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
        public async Task<ActionResult<InvoiceLineModel[]>> Get(int invoiceId)
        {
            try
            {
                var results = await _repository.GetAllInvoiceLineesAsync(invoiceId);

                return _mapper.Map<InvoiceLineModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<InvoiceLineModel>> Get(int invoiceId, int Id)
        {
            try
            {
                var result = await _repository.GetInvoiceLineAsync(invoiceId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<InvoiceLineModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceLineModel>> Post(int invoiceId, InvoiceLineModel model)
        {
            try
            {
                //Make sure InvoiceLineId is not already taken
                var existing = await _repository.GetInvoiceLineAsync(invoiceId, model.Id);
                if (existing != null)
                {
                    return BadRequest("InvoiceLine Id in Use");
                }

                //map
                var InvoiceLine = _mapper.Map<InvoiceLine>(model);

                //save and return
                if (!await _repository.StoreNewInvoiceLineAsync(invoiceId, InvoiceLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "InvoiceLine",
                            new { invoiceId = InvoiceLine.InvoiceId, InvoiceLine.Id });

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
        public async Task<ActionResult<InvoiceLineModel>> Put(int invoiceId, int Id, InvoiceLineModel updatedModel)
        {
            try
            {
                var currentInvoiceLine = await _repository.GetInvoiceLineAsync(invoiceId, Id);
                if (currentInvoiceLine == null) return NotFound($"Could not find InvoiceLine with Id of {Id}");

                _mapper.Map(updatedModel, currentInvoiceLine);

                if (await _repository.UpdateInvoiceLineAsync(invoiceId, currentInvoiceLine))
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
        public async Task<IActionResult> Delete(int invoiceId, int Id)
        {
            try
            {
                var InvoiceLine = await _repository.GetInvoiceLineAsync(invoiceId, Id);
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