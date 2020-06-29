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
    [Route("api/employee/{employeeId}/[controller]")]
    [ApiController]
    public class TaxInformationController : ControllerBase
    {
        private readonly IAccountManagerRepository<TaxInformation> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<TaxInformationController> _logger;

        public TaxInformationController(IAccountManagerRepository<TaxInformation> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<TaxInformationController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int employeeId)
        {
            _repository.ResourcePath = $"api/employee/{employeeId}/TaxInformation";
        }

        [HttpGet]
        public async Task<ActionResult<TaxInformationModel[]>> Get(int employeeId)
        {
            try
            {
                SetupPath(employeeId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<TaxInformationModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TaxInformationModel>> Get(int employeeId, int Id)
        {
            try
            {
                SetupPath(employeeId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<TaxInformationModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaxInformationModel>> Post(int employeeId, TaxInformationModel model)
        {
            try
            {
                SetupPath(employeeId);

                //Make sure TaxInformationId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("TaxInformation Id in Use");
                }

                //map
                var TaxInformation = _mapper.Map<TaxInformation>(model);

                //save and return
                if (!await _repository.StoreNewAsync(TaxInformation))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "TaxInformation",
                            new { Id = TaxInformation.Id });

                    return Created(location, _mapper.Map<TaxInformationModel>(TaxInformation));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TaxInformationModel>> Put(int employeeId, int Id, TaxInformationModel updatedModel)
        {
            try
            {
                SetupPath(employeeId);

                var currentTaxInformation = await _repository.GetByIdAsync(Id);
                if (currentTaxInformation == null) return NotFound($"Could not find TaxInformation with Id of {Id}");

                _mapper.Map(updatedModel, currentTaxInformation);

                if (await _repository.UpdateAsync(currentTaxInformation))
                {
                    return _mapper.Map<TaxInformationModel>(currentTaxInformation);
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
        public async Task<IActionResult> Delete(int employeeId, int Id)
        {
            try
            {
                SetupPath(employeeId);

                var TaxInformation = await _repository.GetByIdAsync(Id);
                if (TaxInformation == null) return NotFound();

                if (await _repository.DeleteAsync(TaxInformation))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the TaxInformation");
        }

    }
}