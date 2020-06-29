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
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<AccountController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<AccountModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAccountsAsync();

                return _mapper.Map<AccountModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AccountModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetAccountAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<AccountModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AccountModel>> Post(AccountModel model)
        {
            try
            {
                //Make sure AccountId is not already taken
                var existing = await _repository.GetAccountAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Account Id in Use");
                }

                //map
                var Account = _mapper.Map<Account>(model);

                //save and return
                if (!await _repository.StoreNewAccountAsync(Account))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Account",
                            new { Id = Account.Id });

                    return Created(location, _mapper.Map<AccountModel>(Account));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AccountModel>> Put(int Id, AccountModel updatedModel)
        {
            try
            {
                var currentAccount = await _repository.GetAccountAsync(Id);
                if (currentAccount == null) return NotFound($"Could not find Account with Id of {Id}");

                _mapper.Map(updatedModel, currentAccount);

                if (await _repository.UpdateAccountAsync(currentAccount))
                {
                    return _mapper.Map<AccountModel>(currentAccount);
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
                var Account = await _repository.GetAccountAsync(Id);
                if (Account == null) return NotFound();

                if (await _repository.DeleteAccount(Account))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Account");
        }

    }
}