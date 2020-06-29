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
    [Route("api/creditnote/{creditnoteId}/[controller]")]
    [ApiController]
    public class CreditNoteLineController : ControllerBase
    {
        private readonly ICreditNoteLineRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CreditNoteLineController> _logger;

        public CreditNoteLineController(ICreditNoteLineRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<CreditNoteLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CreditNoteLineModel[]>> GetAll(int CreditNoteId)
        {
            try
            {
                var results = await _repository.GetAllCreditNoteLinesAsync(CreditNoteId);

                return _mapper.Map<CreditNoteLineModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CreditNoteLineModel>> Get(int CreditNoteId, int Id)
        {
            try
            {
                var result = await _repository.GetCreditNoteLineAsync(CreditNoteId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<CreditNoteLineModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreditNoteLineModel>> Post(int CreditNoteId, CreditNoteLineModel model)
        {
            try
            {
                //Make sure CreditNoteLineId is not already taken
                var existing = await _repository.GetCreditNoteLineAsync(CreditNoteId, model.Id);
                if (existing != null)
                {
                    return BadRequest("CreditNoteLine Id in Use");
                }

                //map
                var CreditNoteLine = _mapper.Map<CreditNoteLine>(model);

                //save and return
                if (!await _repository.StoreNewCreditNoteLineAsync(CreditNoteId, CreditNoteLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "CreditNoteLine",
                            new { CreditNoteLine.CreditNoteId, CreditNoteLine.Id });

                    return Created(location, _mapper.Map<CreditNoteLineModel>(CreditNoteLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CreditNoteLineModel>> Put(int CreditNoteId, int Id, CreditNoteLineModel updatedModel)
        {
            try
            {
                var currentCreditNoteLine = await _repository.GetCreditNoteLineAsync(CreditNoteId, Id);
                if (currentCreditNoteLine == null) return NotFound($"Could not find CreditNoteLine with Id of {Id}");

                _mapper.Map(updatedModel, currentCreditNoteLine);

                if (await _repository.UpdateCreditNoteLineAsync(CreditNoteId, currentCreditNoteLine))
                {
                    return _mapper.Map<CreditNoteLineModel>(currentCreditNoteLine);
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
        public async Task<IActionResult> Delete(int CreditNoteId, int Id)
        {
            try
            {
                var CreditNoteLine = await _repository.GetCreditNoteLineAsync(CreditNoteId, Id);
                if (CreditNoteLine == null) return NotFound();

                if (await _repository.DeleteCreditNoteLine(CreditNoteLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the CreditNoteLine");
        }

    }
}