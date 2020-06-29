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
    [Route("api/account/{accountId}/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IAccountManagerRepository<Subscription> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(IAccountManagerRepository<Subscription> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<SubscriptionController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int accountId)
        {
            _repository.ResourcePath = $"api/account/{accountId}/Subscription";
        }

        [HttpGet]
        public async Task<ActionResult<SubscriptionModel[]>> Get(int accountId)
        {
            try
            {
                SetupPath(accountId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<SubscriptionModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SubscriptionModel>> Get(int accountId, int Id)
        {
            try
            {
                SetupPath(accountId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<SubscriptionModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionModel>> Post(int accountId, SubscriptionModel model)
        {
            try
            {
                SetupPath(accountId);

                //Make sure SubscriptionId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Subscription Id in Use");
                }

                //map
                var Subscription = _mapper.Map<Subscription>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Subscription))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Subscription",
                            new { Id = Subscription.Id });

                    return Created(location, _mapper.Map<SubscriptionModel>(Subscription));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SubscriptionModel>> Put(int accountId, int Id, SubscriptionModel updatedModel)
        {
            try
            {
                SetupPath(accountId);

                var currentSubscription = await _repository.GetByIdAsync(Id);
                if (currentSubscription == null) return NotFound($"Could not find Subscription with Id of {Id}");

                _mapper.Map(updatedModel, currentSubscription);

                if (await _repository.UpdateAsync(currentSubscription))
                {
                    return _mapper.Map<SubscriptionModel>(currentSubscription);
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

                var Subscription = await _repository.GetByIdAsync(Id);
                if (Subscription == null) return NotFound();

                if (await _repository.DeleteAsync(Subscription))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Subscription");
        }

    }
}