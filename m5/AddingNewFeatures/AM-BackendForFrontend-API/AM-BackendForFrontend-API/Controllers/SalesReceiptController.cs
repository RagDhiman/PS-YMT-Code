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
    [Route("api/invoice/{invoiceId}/[controller]")]
    [ApiController]
    public class SalesReceiptController : ControllerBase
    {
        private readonly IAccountManagerRepository<SalesReceipt> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SalesReceiptController> _logger;

        public SalesReceiptController(IAccountManagerRepository<SalesReceipt> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<SalesReceiptController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int invoiceId)
        {
            _repository.ResourcePath = $"api/invoice/{invoiceId}/SalesReceipt";
        }

        [HttpGet]
        public async Task<ActionResult<SalesReceiptModel[]>> Get(int invoiceId)
        {
            try
            {
                SetupPath(invoiceId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<SalesReceiptModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SalesReceiptModel>> Get(int invoiceId, int Id)
        {
            try
            {
                SetupPath(invoiceId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<SalesReceiptModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SalesReceiptModel>> Post(int invoiceId, SalesReceiptModel model)
        {
            try
            {
                SetupPath(invoiceId);

                //Make sure SalesReceiptId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("SalesReceipt Id in Use");
                }

                //map
                var SalesReceipt = _mapper.Map<SalesReceipt>(model);

                //save and return
                if (!await _repository.StoreNewAsync(SalesReceipt))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "SalesReceipt",
                            new { Id = SalesReceipt.Id });

                    return Created(location, _mapper.Map<SalesReceiptModel>(SalesReceipt));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SalesReceiptModel>> Put(int invoiceId, int Id, SalesReceiptModel updatedModel)
        {
            try
            {
                SetupPath(invoiceId);

                var currentSalesReceipt = await _repository.GetByIdAsync(Id);
                if (currentSalesReceipt == null) return NotFound($"Could not find SalesReceipt with Id of {Id}");

                _mapper.Map(updatedModel, currentSalesReceipt);

                if (await _repository.UpdateAsync(currentSalesReceipt))
                {
                    return _mapper.Map<SalesReceiptModel>(currentSalesReceipt);
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
        public async Task<IActionResult> Delete(int invoiceId, int Id)
        {
            try
            {
                SetupPath(invoiceId);

                var SalesReceipt = await _repository.GetByIdAsync(Id);
                if (SalesReceipt == null) return NotFound();

                if (await _repository.DeleteAsync(SalesReceipt))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the SalesReceipt");
        }

    }
}