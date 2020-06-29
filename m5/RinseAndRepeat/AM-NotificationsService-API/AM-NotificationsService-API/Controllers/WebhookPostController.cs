using AM_NotificationsService_API.Models;
using AM_NotificationsService_Core;
using AM_NotificationsService_Core.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AM_NotificationsService_API.Controllers
{
    [Route("api/notification/[controller]")]
    [ApiController]
    public class WebhookPostController : ControllerBase
    {
        private readonly IWebhookPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<WebhookPostController> _logger;

        public WebhookPostController(IWebhookPostRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<WebhookPostController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<WebhookPostModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllWebhookPostsAsync();

                return _mapper.Map<WebhookPostModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<WebhookPostModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetWebhookPostAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<WebhookPostModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<WebhookPostModel>> Post(WebhookPostModel model)
        {
            try
            {
                //Make sure WebhookPostId is not already taken
                var existing = await _repository.GetWebhookPostAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("WebhookPost Id in Use");
                }

                //map
                var customer = _mapper.Map<WebhookPost>(model);

                //save and return
                if (!await _repository.StoreNewWebhookPostAsync(customer))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "WebhookPost",
                            new { customer.Id });

                    return Created(location, _mapper.Map<WebhookPostModel>(customer));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<WebhookPostModel>> Put(int Id, WebhookPostModel updatedModel)
        {
            try
            {
                var currentWebhookPost = await _repository.GetWebhookPostAsync(Id);
                if (currentWebhookPost == null) return NotFound($"Could not find customer with Id of {Id}");

                _mapper.Map(updatedModel, currentWebhookPost);

                if (await _repository.UpdateWebhookPostAsync(currentWebhookPost))
                {
                    return _mapper.Map<WebhookPostModel>(currentWebhookPost);
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
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var customer = await _repository.GetWebhookPostAsync(Id);
                if (customer == null) return NotFound();

                if (await _repository.DeleteWebhookPost(customer))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the customer");
        }

    }
}