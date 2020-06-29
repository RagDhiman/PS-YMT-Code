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
    public class InvoiceLineController : ControllerBase
    {
        private readonly IAccountManagerRepository<InvoiceLine> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<InvoiceLineController> _logger;

        public InvoiceLineController(IAccountManagerRepository<InvoiceLine> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<InvoiceLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int invoiceId)
        {
            _repository.ResourcePath = $"api/invoice/{invoiceId}/InvoiceLine";
        }

        [HttpGet]
        public async Task<ActionResult<InvoiceLineModel[]>> Get(int invoiceId)
        {
            try
            {
                SetupPath(invoiceId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<InvoiceLineModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<InvoiceLineModel>> Get(int invoiceId, int Id)
        {
            try
            {
                SetupPath(invoiceId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<InvoiceLineModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceLineModel>> Post(int invoiceId, InvoiceLineModel model)
        {
            try
            {
                SetupPath(invoiceId);

                //Make sure InvoiceLineId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("InvoiceLine Id in Use");
                }

                //map
                var InvoiceLine = _mapper.Map<InvoiceLine>(model);

                //save and return
                if (!await _repository.StoreNewAsync(InvoiceLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "InvoiceLine",
                            new { Id = InvoiceLine.Id });

                    return Created(location, _mapper.Map<InvoiceLineModel>(InvoiceLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<InvoiceLineModel>> Put(int invoiceId, int Id, InvoiceLineModel updatedModel)
        {
            try
            {
                SetupPath(invoiceId);

                var currentInvoiceLine = await _repository.GetByIdAsync(Id);
                if (currentInvoiceLine == null) return NotFound($"Could not find InvoiceLine with Id of {Id}");

                _mapper.Map(updatedModel, currentInvoiceLine);

                if (await _repository.UpdateAsync(currentInvoiceLine))
                {
                    return _mapper.Map<InvoiceLineModel>(currentInvoiceLine);
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

                var InvoiceLine = await _repository.GetByIdAsync(Id);
                if (InvoiceLine == null) return NotFound();

                if (await _repository.DeleteAsync(InvoiceLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the InvoiceLine");
        }

    }
}