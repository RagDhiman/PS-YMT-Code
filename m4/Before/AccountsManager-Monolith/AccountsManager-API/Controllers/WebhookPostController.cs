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
            catch (Exception)
            {
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
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

    }
}