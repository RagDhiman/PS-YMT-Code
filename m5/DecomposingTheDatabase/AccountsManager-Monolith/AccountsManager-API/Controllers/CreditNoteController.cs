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
    [Route("api/invoice/{invoiceId}/[controller]")]
    [ApiController]
    public class CreditNoteController : ControllerBase
    {
        private readonly ICreditNoteRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CreditNoteController> _logger;

        public CreditNoteController(ICreditNoteRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<CreditNoteController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CreditNoteModel[]>> GetAll(int invoiceId)
        {
            try
            {
                var results = await _repository.GetAllCreditNotesAsync(invoiceId);

                return _mapper.Map<CreditNoteModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CreditNoteModel>> Get(int invoiceId, int Id)
        {
            try
            {
                var result = await _repository.GetCreditNoteAsync(invoiceId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<CreditNoteModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreditNoteModel>> Post(int invoiceId, CreditNoteModel model)
        {
            try
            {
                //Make sure CreditNoteId is not already taken
                var existing = await _repository.GetCreditNoteAsync(invoiceId, model.Id);
                if (existing != null)
                {
                    return BadRequest("CreditNote Id in Use");
                }

                //map
                var CreditNote = _mapper.Map<CreditNote>(model);

                //save and return
                if (!await _repository.StoreNewCreditNoteAsync(invoiceId, CreditNote))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "CreditNote",
                            new { invoiceId = CreditNote.InvoiceId, CreditNote.Id });

                    return Created(location, _mapper.Map<CreditNoteModel>(CreditNote));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CreditNoteModel>> Put(int invoiceId, int Id, CreditNoteModel updatedModel)
        {
            try
            {
                var currentCreditNote = await _repository.GetCreditNoteAsync(invoiceId, Id);
                if (currentCreditNote == null) return NotFound($"Could not find CreditNote with Id of {Id}");

                _mapper.Map(updatedModel, currentCreditNote);

                if (await _repository.UpdateCreditNoteAsync(invoiceId, currentCreditNote))
                {
                    return _mapper.Map<CreditNoteModel>(currentCreditNote);
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
                var CreditNote = await _repository.GetCreditNoteAsync(invoiceId, Id);
                if (CreditNote == null) return NotFound();

                if (await _repository.DeleteCreditNote(CreditNote))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the CreditNote");
        }

    }
}