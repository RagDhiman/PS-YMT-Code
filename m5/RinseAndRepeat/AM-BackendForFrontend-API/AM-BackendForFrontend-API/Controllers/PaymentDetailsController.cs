﻿using AM_BackendForFrontend_API.Models;
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
    [Route("api/account/{accountId}/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly IAccountManagerRepository<PaymentDetails> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<PaymentDetailsController> _logger;

        public PaymentDetailsController(IAccountManagerRepository<PaymentDetails> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<PaymentDetailsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int accountId)
        {
            _repository.ResourcePath = $"api/account/{accountId}/PaymentDetails";
        }

        [HttpGet]
        public async Task<ActionResult<PaymentDetailsModel[]>> Get(int accountId)
        {
            try
            {
                SetupPath(accountId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<PaymentDetailsModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PaymentDetailsModel>> Get(int accountId, int Id)
        {
            try
            {
                SetupPath(accountId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<PaymentDetailsModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetailsModel>> Post(int accountId, PaymentDetailsModel model)
        {
            try
            {
                SetupPath(accountId);

                //Make sure PaymentDetailsId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("PaymentDetails Id in Use");
                }

                //map
                var PaymentDetails = _mapper.Map<PaymentDetails>(model);

                //save and return
                if (!await _repository.StoreNewAsync(PaymentDetails))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "PaymentDetails",
                            new { Id = PaymentDetails.Id });

                    return Created(location, _mapper.Map<PaymentDetailsModel>(PaymentDetails));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<PaymentDetailsModel>> Put(int accountId, int Id, PaymentDetailsModel updatedModel)
        {
            try
            {
                SetupPath(accountId);

                var currentPaymentDetails = await _repository.GetByIdAsync(Id);
                if (currentPaymentDetails == null) return NotFound($"Could not find PaymentDetails with Id of {Id}");

                _mapper.Map(updatedModel, currentPaymentDetails);

                if (await _repository.UpdateAsync(currentPaymentDetails))
                {
                    return _mapper.Map<PaymentDetailsModel>(currentPaymentDetails);
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
        public async Task<IActionResult> Delete(int accountId, int Id)
        {
            try
            {
                SetupPath(accountId);

                var PaymentDetails = await _repository.GetByIdAsync(Id);
                if (PaymentDetails == null) return NotFound();

                if (await _repository.DeleteAsync(PaymentDetails))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the PaymentDetails");
        }

    }
}