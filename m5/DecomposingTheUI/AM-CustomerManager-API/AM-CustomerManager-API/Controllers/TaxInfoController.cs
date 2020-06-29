using AM_CustomerManager_API.Models;
using AM_CustomerManager_Core.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AM_CustomerManager_Core;
using AutoMapper;

namespace AM_CustomerManager_API.Controllers
{
    [Route("api/customer/{customerId}/[controller]")]
    [ApiController]
    public class TaxInfoController : ControllerBase
    {
        private readonly ITaxInfoRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<TaxInfoController> _logger;

        public TaxInfoController(ITaxInfoRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<TaxInfoController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<TaxInfoModel[]>> Get(int customerId)
        {
            try
            {
                var results = await _repository.GetAllTaxInfoesAsync(customerId);

                return _mapper.Map<TaxInfoModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TaxInfoModel>> Get(int customerId, int Id)
        {
            try
            {
                var result = await _repository.GetTaxInfoAsync(customerId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<TaxInfoModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaxInfoModel>> Post(int customerId, TaxInfoModel model)
        {
            try
            {
                //Make sure TaxInfoId is not already taken
                var existing = await _repository.GetTaxInfoAsync(customerId, model.Id);
                if (existing != null)
                {
                    return BadRequest("TaxInfo Id in Use");
                }

                //map
                var TaxInfo = _mapper.Map<TaxInfo>(model);

                //save and return
                if (!await _repository.StoreNewTaxInfoAsync(customerId, TaxInfo))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "TaxInfo",
                            new { TaxInfo.Id });

                    return Created(location, _mapper.Map<TaxInfoModel>(TaxInfo));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TaxInfoModel>> Put(int customerId, int Id, TaxInfoModel updatedModel)
        {
            try
            {
                var currentTaxInfo = await _repository.GetTaxInfoAsync(customerId, Id);
                if (currentTaxInfo == null) return NotFound($"Could not find TaxInfo with Id of {Id}");

                _mapper.Map(updatedModel, currentTaxInfo);

                if (await _repository.UpdateTaxInfoAsync(customerId, currentTaxInfo))
                {
                    return _mapper.Map<TaxInfoModel>(currentTaxInfo);
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
        public async Task<IActionResult> Delete(int customerId, int Id)
        {
            try
            {
                var TaxInfo = await _repository.GetTaxInfoAsync(customerId, Id);
                if (TaxInfo == null) return NotFound();

                if (await _repository.DeleteTaxInfo(TaxInfo))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the TaxInfo");
        }

    }
}