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
    [Route("api/customer/{customerId}/[controller]")]
    [ApiController]
    public class TaxInfoController : ControllerBase
    {
        private readonly ICustomerManagerRepository<TaxInfo> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<TaxInfoController> _logger;

        public TaxInfoController(ICustomerManagerRepository<TaxInfo> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<TaxInfoController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int customerId)
        {
            _repository.ResourcePath = $"api/customer/{customerId}/address";
        }

        [HttpGet]
        public async Task<ActionResult<TaxInfoModel[]>> Get(int customerId)
        {
            try
            {
                SetupPath(customerId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<TaxInfoModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TaxInfoModel>> Get(int customerId, int Id)
        {
            try
            {
                SetupPath(customerId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<TaxInfoModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaxInfoModel>> Post(int customerId, TaxInfoModel model)
        {
            try
            {
                SetupPath(customerId);

                //Make sure TaxInfoId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("TaxInfo Id in Use");
                }

                //map
                var TaxInfo = _mapper.Map<TaxInfo>(model);

                //save and return
                if (!await _repository.StoreNewAsync(TaxInfo))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "TaxInfo",
                            new { Id = TaxInfo.Id });

                    return Created(location, _mapper.Map<TaxInfoModel>(TaxInfo));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TaxInfoModel>> Put(int customerId, int Id, TaxInfoModel updatedModel)
        {
            try
            {
                SetupPath(customerId);

                var currentTaxInfo = await _repository.GetByIdAsync(Id);
                if (currentTaxInfo == null) return NotFound($"Could not find TaxInfo with Id of {Id}");

                _mapper.Map(updatedModel, currentTaxInfo);

                if (await _repository.UpdateAsync(currentTaxInfo))
                {
                    return _mapper.Map<TaxInfoModel>(currentTaxInfo);
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
        public async Task<IActionResult> Delete(int customerId, int Id)
        {
            try
            {
                SetupPath(customerId);

                var TaxInfo = await _repository.GetByIdAsync(Id);
                if (TaxInfo == null) return NotFound();

                if (await _repository.DeleteAsync(TaxInfo))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the TaxInfo");
        }

    }
}