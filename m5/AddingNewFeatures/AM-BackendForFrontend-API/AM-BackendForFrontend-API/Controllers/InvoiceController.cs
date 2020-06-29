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
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IAccountManagerRepository<Invoice> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(IAccountManagerRepository<Invoice> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<InvoiceController> logger)
        {
            _repository = repository;
            _repository.ResourcePath = "api/Invoice";

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<InvoiceModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<InvoiceModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<InvoiceModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<InvoiceModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceModel>> Post(InvoiceModel model)
        {
            try
            {
                //Make sure InvoiceId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Invoice Id in Use");
                }

                //map
                var Invoice = _mapper.Map<Invoice>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Invoice))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Invoice",
                            new { Id = Invoice.Id });

                    return Created(location, _mapper.Map<InvoiceModel>(Invoice));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<InvoiceModel>> Put(int Id, InvoiceModel updatedModel)
        {
            try
            {
                var currentInvoice = await _repository.GetByIdAsync(Id);
                if (currentInvoice == null) return NotFound($"Could not find Invoice with Id of {Id}");

                _mapper.Map(updatedModel, currentInvoice);

                if (await _repository.UpdateAsync(currentInvoice))
                {
                    return _mapper.Map<InvoiceModel>(currentInvoice);
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
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var Invoice = await _repository.GetByIdAsync(Id);
                if (Invoice == null) return NotFound();

                if (await _repository.DeleteAsync(Invoice))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Invoice");
        }

    }
}