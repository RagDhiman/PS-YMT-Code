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
    public class SMSController : ControllerBase
    {
        private readonly ISMSRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SMSController> _logger;

        public SMSController(ISMSRepository repository, IMapper mapper, LinkGenerator linkGenerator, 
            ILogger<SMSController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<SMSModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllSMSsAsync();

                return _mapper.Map<SMSModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SMSModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetSMSAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<SMSModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

    }
}