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
    [Route("api/account/{accountId}/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(ISubscriptionRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<SubscriptionController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<SubscriptionModel[]>> GetAll(int accountId)
        {
            try
            {
                var results = await _repository.GetAllSubscriptionsAsync(accountId);

                return _mapper.Map<SubscriptionModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SubscriptionModel>> Get(int AccountId, int Id)
        {
            try
            {
                var result = await _repository.GetSubscriptionAsync(AccountId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<SubscriptionModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionModel>> Post(int AccountId, SubscriptionModel model)
        {
            try
            {
                //Make sure SubscriptionId is not already taken
                var existing = await _repository.GetSubscriptionAsync(AccountId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Subscription Id in Use");
                }

                //map
                var Subscription = _mapper.Map<Subscription>(model);

                //save and return
                if (!await _repository.StoreNewSubscriptionAsync(AccountId, Subscription))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Subscription",
                            new { Subscription.AccountId, Subscription.Id });

                    return Created(location, _mapper.Map<SubscriptionModel>(Subscription));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SubscriptionModel>> Put(int AccountId, int Id, SubscriptionModel updatedModel)
        {
            try
            {
                var currentSubscription = await _repository.GetSubscriptionAsync(AccountId, Id);
                if (currentSubscription == null) return NotFound($"Could not find Subscription with Id of {Id}");

                _mapper.Map(updatedModel, currentSubscription);

                if (await _repository.UpdateSubscriptionAsync(AccountId, currentSubscription))
                {
                    return _mapper.Map<SubscriptionModel>(currentSubscription);
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
        public async Task<IActionResult> Delete(int AccountId, int Id)
        {
            try
            {
                var Subscription = await _repository.GetSubscriptionAsync(AccountId, Id);
                if (Subscription == null) return NotFound();

                if (await _repository.DeleteSubscription(Subscription))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Subscription");
        }

    }
}