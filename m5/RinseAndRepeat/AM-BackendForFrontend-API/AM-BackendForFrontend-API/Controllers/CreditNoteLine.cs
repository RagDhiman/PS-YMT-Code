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
    [Route("api/CreditNote/{CreditNoteId}/[controller]")]
    [ApiController]
    public class CreditNoteLineController : ControllerBase
    {
        private readonly IInvoiceManagerRepository<CreditNoteLine> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CreditNoteLineController> _logger;

        public CreditNoteLineController(IInvoiceManagerRepository<CreditNoteLine> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<CreditNoteLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int CreditNoteLineId)
        {
            _repository.ResourcePath = $"api/CreditNoteLine/{CreditNoteLineId}/CreditNoteLine";
        }

        [HttpGet]
        public async Task<ActionResult<CreditNoteLineModel[]>> Get(int CreditNoteLineId)
        {
            try
            {
                SetupPath(CreditNoteLineId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<CreditNoteLineModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CreditNoteLineModel>> Get(int CreditNoteLineId, int Id)
        {
            try
            {
                SetupPath(CreditNoteLineId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<CreditNoteLineModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreditNoteLineModel>> Post(int CreditNoteLineId, CreditNoteLineModel model)
        {
            try
            {
                SetupPath(CreditNoteLineId);

                //Make sure CreditNoteLineId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("CreditNoteLine Id in Use");
                }

                //map
                var CreditNoteLine = _mapper.Map<CreditNoteLine>(model);

                //save and return
                if (!await _repository.StoreNewAsync(CreditNoteLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "CreditNoteLine",
                            new { Id = CreditNoteLine.Id });

                    return Created(location, _mapper.Map<CreditNoteLineModel>(CreditNoteLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<CreditNoteLineModel>> Put(int CreditNoteLineId, int Id, CreditNoteLineModel updatedModel)
        {
            try
            {
                SetupPath(CreditNoteLineId);

                var currentCreditNoteLine = await _repository.GetByIdAsync(Id);
                if (currentCreditNoteLine == null) return NotFound($"Could not find CreditNoteLine with Id of {Id}");

                _mapper.Map(updatedModel, currentCreditNoteLine);

                if (await _repository.UpdateAsync(currentCreditNoteLine))
                {
                    return _mapper.Map<CreditNoteLineModel>(currentCreditNoteLine);
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
        public async Task<IActionResult> Delete(int CreditNoteLineId, int Id)
        {
            try
            {
                SetupPath(CreditNoteLineId);

                var CreditNoteLine = await _repository.GetByIdAsync(Id);
                if (CreditNoteLine == null) return NotFound();

                if (await _repository.DeleteAsync(CreditNoteLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the CreditNoteLine");
        }

    }
}