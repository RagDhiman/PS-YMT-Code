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
    public class SalesReceiptController : ControllerBase
    {
        private readonly ISalesReceiptRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SalesReceiptController> _logger;

        public SalesReceiptController(ISalesReceiptRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<SalesReceiptController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<SalesReceiptModel[]>> Get(int invoiceId)
        {
            try
            {
                var results = await _repository.GetAllSalesReceiptesAsync(invoiceId);

                return _mapper.Map<SalesReceiptModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SalesReceiptModel>> Get(int invoiceId, int Id)
        {
            try
            {
                var result = await _repository.GetSalesReceiptAsync(invoiceId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<SalesReceiptModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SalesReceiptModel>> Post(int invoiceId, SalesReceiptModel model)
        {
            try
            {
                //Make sure SalesReceiptId is not already taken
                var existing = await _repository.GetSalesReceiptAsync(invoiceId, model.Id);
                if (existing != null)
                {
                    return BadRequest("SalesReceipt Id in Use");
                }

                //map
                var SalesReceipt = _mapper.Map<SalesReceipt>(model);

                //save and return
                if (!await _repository.StoreNewSalesReceiptAsync(invoiceId, SalesReceipt))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "SalesReceipt",
                            new { invoiceId = SalesReceipt.InvoiceId, SalesReceipt.Id });

                    return Created(location, _mapper.Map<SalesReceiptModel>(SalesReceipt));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SalesReceiptModel>> Put(int invoiceId, int Id, SalesReceiptModel updatedModel)
        {
            try
            {
                var currentSalesReceipt = await _repository.GetSalesReceiptAsync(invoiceId, Id);
                if (currentSalesReceipt == null) return NotFound($"Could not find SalesReceipt with Id of {Id}");

                _mapper.Map(updatedModel, currentSalesReceipt);

                if (await _repository.UpdateSalesReceiptAsync(invoiceId, currentSalesReceipt))
                {
                    return _mapper.Map<SalesReceiptModel>(currentSalesReceipt);
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
                var SalesReceipt = await _repository.GetSalesReceiptAsync(invoiceId, Id);
                if (SalesReceipt == null) return NotFound();

                if (await _repository.DeleteSalesReceipt(SalesReceipt))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the SalesReceipt");
        }

    }
}