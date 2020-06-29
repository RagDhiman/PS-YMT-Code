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
    [Route("api/SalesReceipt/{SalesReceiptId}/[controller]")]
    [ApiController]
    public class SalesReceiptLineController : ControllerBase
    {
        private readonly ISalesReceiptLineRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SalesReceiptLineController> _logger;

        public SalesReceiptLineController(ISalesReceiptLineRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<SalesReceiptLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<SalesReceiptLineModel[]>> GetAll(int SalesReceiptId)
        {
            try
            {
                var results = await _repository.GetAllSalesReceiptLinesAsync(SalesReceiptId);

                return _mapper.Map<SalesReceiptLineModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SalesReceiptLineModel>> Get(int SalesReceiptId, int Id)
        {
            try
            {
                var result = await _repository.GetSalesReceiptLineAsync(SalesReceiptId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<SalesReceiptLineModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SalesReceiptLineModel>> Post(int SalesReceiptId, SalesReceiptLineModel model)
        {
            try
            {
                //Make sure SalesReceiptLineId is not already taken
                var existing = await _repository.GetSalesReceiptLineAsync(SalesReceiptId, model.Id);
                if (existing != null)
                {
                    return BadRequest("SalesReceiptLine Id in Use");
                }

                //map
                var SalesReceiptLine = _mapper.Map<SalesReceiptLine>(model);

                //save and return
                if (!await _repository.StoreNewSalesReceiptLineAsync(SalesReceiptId, SalesReceiptLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "SalesReceiptLine",
                            new { SalesReceiptLine.SalesReceiptId, SalesReceiptLine.Id });

                    return Created(location, _mapper.Map<SalesReceiptLineModel>(SalesReceiptLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SalesReceiptLineModel>> Put(int SalesReceiptId, int Id, SalesReceiptLineModel updatedModel)
        {
            try
            {
                var currentSalesReceiptLine = await _repository.GetSalesReceiptLineAsync(SalesReceiptId, Id);
                if (currentSalesReceiptLine == null) return NotFound($"Could not find SalesReceiptLine with Id of {Id}");

                _mapper.Map(updatedModel, currentSalesReceiptLine);

                if (await _repository.UpdateSalesReceiptLineAsync(SalesReceiptId, currentSalesReceiptLine))
                {
                    return _mapper.Map<SalesReceiptLineModel>(currentSalesReceiptLine);
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
        public async Task<IActionResult> Delete(int SalesReceiptId, int Id)
        {
            try
            {
                var SalesReceiptLine = await _repository.GetSalesReceiptLineAsync(SalesReceiptId, Id);
                if (SalesReceiptLine == null) return NotFound();

                if (await _repository.DeleteSalesReceiptLine(SalesReceiptLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the SalesReceiptLine");
        }

    }
}