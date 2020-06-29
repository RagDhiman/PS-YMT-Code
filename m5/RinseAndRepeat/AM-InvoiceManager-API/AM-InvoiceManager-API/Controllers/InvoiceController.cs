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

namespace AM_InvoiceManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(IInvoiceRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<InvoiceController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<InvoiceModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllInvoicesAsync();

                return _mapper.Map<InvoiceModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<InvoiceModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetInvoiceAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<InvoiceModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceModel>> Post(InvoiceModel model)
        {
            try
            {
                //Make sure InvoiceId is not already taken
                var existing = await _repository.GetInvoiceAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Invoice Id in Use");
                }

                //map
                var customer = _mapper.Map<Invoice>(model);

                //save and return
                if (!await _repository.StoreNewInvoiceAsync(customer))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Invoice",
                            new { customer.Id });

                    return Created(location, _mapper.Map<InvoiceModel>(customer));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<InvoiceModel>> Put(int Id, InvoiceModel updatedModel)
        {
            try
            {
                var currentInvoice = await _repository.GetInvoiceAsync(Id);
                if (currentInvoice == null) return NotFound($"Could not find customer with Id of {Id}");

                _mapper.Map(updatedModel, currentInvoice);

                if (await _repository.UpdateInvoiceAsync(currentInvoice))
                {
                    return _mapper.Map<InvoiceModel>(currentInvoice);
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
                var customer = await _repository.GetInvoiceAsync(Id);
                if (customer == null) return NotFound();

                if (await _repository.DeleteInvoice(customer))
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