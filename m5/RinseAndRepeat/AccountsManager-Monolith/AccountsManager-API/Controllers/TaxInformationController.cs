﻿using AccountsManager_API.Models;
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
    [Route("api/employee/{employeeId}/[controller]")]
    [ApiController]
    public class TaxInformationController : ControllerBase
    {
        private readonly ITaxInformationRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<TaxInformationController> _logger;

        public TaxInformationController(ITaxInformationRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<TaxInformationController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<TaxInformationModel[]>> GetAll(int employeeId)
        {
            try
            {
                var results = await _repository.GetAllTaxInformationsAsync(employeeId);

                return _mapper.Map<TaxInformationModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TaxInformationModel>> Get(int employeeId, int Id)
        {
            try
            {
                var result = await _repository.GetTaxInformationAsync(employeeId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<TaxInformationModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaxInformationModel>> Post(int employeeId, TaxInformationModel model)
        {
            try
            {
                //Make sure TaxInformationId is not already taken
                var existing = await _repository.GetTaxInformationAsync(employeeId, model.Id);
                if (existing != null)
                {
                    return BadRequest("TaxInformation Id in Use");
                }

                //map
                var TaxInformation = _mapper.Map<TaxInformation>(model);

                //save and return
                if (!await _repository.StoreNewTaxInformationAsync(employeeId, TaxInformation))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "TaxInformation",
                            new { employeeId = TaxInformation.EmployeeId, TaxInformation.Id });

                    return Created(location, _mapper.Map<TaxInformationModel>(TaxInformation));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TaxInformationModel>> Put(int employeeId, int Id, TaxInformationModel updatedModel)
        {
            try
            {
                var currentTaxInformation = await _repository.GetTaxInformationAsync(employeeId, Id);
                if (currentTaxInformation == null) return NotFound($"Could not find TaxInformation with Id of {Id}");

                _mapper.Map(updatedModel, currentTaxInformation);

                if (await _repository.UpdateTaxInformationAsync(employeeId, currentTaxInformation))
                {
                    return _mapper.Map<TaxInformationModel>(currentTaxInformation);
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
        public async Task<IActionResult> Delete(int employeeId, int Id)
        {
            try
            {
                var TaxInformation = await _repository.GetTaxInformationAsync(employeeId, Id);
                if (TaxInformation == null) return NotFound();

                if (await _repository.DeleteTaxInformation(TaxInformation))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the TaxInformation");
        }

    }
}