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
    public class CreditController : ControllerBase
    {
        private readonly IInvoiceManagerRepository<Credit> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CreditController> _logger;

        public CreditController(IInvoiceManagerRepository<Credit> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<CreditController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int invoiceId)
        {
            _repository.ResourcePath = $"api/invoice/{invoiceId}/Credit";
        }

        [HttpGet]
        public async Task<ActionResult<CreditModel[]>> Get(int invoiceId)
        {
            try
            {
                SetupPath(invoiceId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<CreditModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CreditModel>> Get(int invoiceId, int Id)
        {
            try
            {
                SetupPath(invoiceId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<CreditModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreditModel>> Post(int invoiceId, CreditModel model)
        {
            try
            {
                SetupPath(invoiceId);

                //Make sure CreditId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Credit Id in Use");
                }

                //map
                var Credit = _mapper.Map<Credit>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Credit))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Credit",
                            new { Id = Credit.Id });

                    return Created(location, _mapper.Map<CreditModel>(Credit));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CreditModel>> Put(int invoiceId, int Id, CreditModel updatedModel)
        {
            try
            {
                SetupPath(invoiceId);

                var currentCredit = await _repository.GetByIdAsync(Id);
                if (currentCredit == null) return NotFound($"Could not find Credit with Id of {Id}");

                _mapper.Map(updatedModel, currentCredit);

                if (await _repository.UpdateAsync(currentCredit))
                {
                    return _mapper.Map<CreditModel>(currentCredit);
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

                var Credit = await _repository.GetByIdAsync(Id);
                if (Credit == null) return NotFound();

                if (await _repository.DeleteAsync(Credit))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Credit");
        }

    }
}