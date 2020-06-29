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
    [Route("api/salesreceipt/{salesreceiptId}/[controller]")]
    [ApiController]
    public class SalesReceiptLineController : ControllerBase
    {
        private readonly IAccountManagerRepository<SalesReceiptLine> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SalesReceiptLineController> _logger;

        public SalesReceiptLineController(IAccountManagerRepository<SalesReceiptLine> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<SalesReceiptLineController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int salesreceiptId)
        {
            _repository.ResourcePath = $"api/salesreceipt/{salesreceiptId}/SalesReceiptLine";
        }

        [HttpGet]
        public async Task<ActionResult<SalesReceiptLineModel[]>> Get(int salesreceiptId)
        {
            try
            {
                SetupPath(salesreceiptId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<SalesReceiptLineModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SalesReceiptLineModel>> Get(int salesreceiptId, int Id)
        {
            try
            {
                SetupPath(salesreceiptId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<SalesReceiptLineModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SalesReceiptLineModel>> Post(int salesreceiptId, SalesReceiptLineModel model)
        {
            try
            {
                SetupPath(salesreceiptId);

                //Make sure SalesReceiptLineId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("SalesReceiptLine Id in Use");
                }

                //map
                var SalesReceiptLine = _mapper.Map<SalesReceiptLine>(model);

                //save and return
                if (!await _repository.StoreNewAsync(SalesReceiptLine))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "SalesReceiptLine",
                            new { Id = SalesReceiptLine.Id });

                    return Created(location, _mapper.Map<SalesReceiptLineModel>(SalesReceiptLine));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SalesReceiptLineModel>> Put(int salesreceiptId, int Id, SalesReceiptLineModel updatedModel)
        {
            try
            {
                SetupPath(salesreceiptId);

                var currentSalesReceiptLine = await _repository.GetByIdAsync(Id);
                if (currentSalesReceiptLine == null) return NotFound($"Could not find SalesReceiptLine with Id of {Id}");

                _mapper.Map(updatedModel, currentSalesReceiptLine);

                if (await _repository.UpdateAsync(currentSalesReceiptLine))
                {
                    return _mapper.Map<SalesReceiptLineModel>(currentSalesReceiptLine);
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
        public async Task<IActionResult> Delete(int salesreceiptId, int Id)
        {
            try
            {
                SetupPath(salesreceiptId);

                var SalesReceiptLine = await _repository.GetByIdAsync(Id);
                if (SalesReceiptLine == null) return NotFound();

                if (await _repository.DeleteAsync(SalesReceiptLine))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the SalesReceiptLine");
        }

    }
}