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
    public class CreditNoteController : ControllerBase
    {
        private readonly IInvoiceManagerRepository<CreditNote> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CreditNoteController> _logger;

        public CreditNoteController(IInvoiceManagerRepository<CreditNote> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<CreditNoteController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int invoiceId)
        {
            _repository.ResourcePath = $"api/invoice/{invoiceId}/CreditNote";
        }

        [HttpGet]
        public async Task<ActionResult<CreditNoteModel[]>> Get(int invoiceId)
        {
            try
            {
                SetupPath(invoiceId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<CreditNoteModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CreditNoteModel>> Get(int invoiceId, int Id)
        {
            try
            {
                SetupPath(invoiceId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<CreditNoteModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreditNoteModel>> Post(int invoiceId, CreditNoteModel model)
        {
            try
            {
                SetupPath(invoiceId);

                //Make sure CreditNoteId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("CreditNote Id in Use");
                }

                //map
                var CreditNote = _mapper.Map<CreditNote>(model);

                //save and return
                if (!await _repository.StoreNewAsync(CreditNote))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "CreditNote",
                            new { Id = CreditNote.Id });

                    return Created(location, _mapper.Map<CreditNoteModel>(CreditNote));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CreditNoteModel>> Put(int invoiceId, int Id, CreditNoteModel updatedModel)
        {
            try
            {
                SetupPath(invoiceId);

                var currentCreditNote = await _repository.GetByIdAsync(Id);
                if (currentCreditNote == null) return NotFound($"Could not find CreditNote with Id of {Id}");

                _mapper.Map(updatedModel, currentCreditNote);

                if (await _repository.UpdateAsync(currentCreditNote))
                {
                    return _mapper.Map<CreditNoteModel>(currentCreditNote);
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

                var CreditNote = await _repository.GetByIdAsync(Id);
                if (CreditNote == null) return NotFound();

                if (await _repository.DeleteAsync(CreditNote))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the CreditNote");
        }

    }
}