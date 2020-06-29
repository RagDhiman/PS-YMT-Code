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
    [Route("api/customer/{customerId}/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<BankAccountController> _logger;

        public BankAccountController(IBankAccountRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<BankAccountController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<BankAccountModel[]>> Get(int customerId)
        {
            try
            {
                var results = await _repository.GetAllBankAccountsAsync(customerId);

                return _mapper.Map<BankAccountModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<BankAccountModel>> Get(int customerId, int Id)
        {
            try
            {
                var result = await _repository.GetBankAccountAsync(customerId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<BankAccountModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BankAccountModel>> Post(int customerId, BankAccountModel model)
        {
            try
            {
                //Make sure BankAccountId is not already taken
                var existing = await _repository.GetBankAccountAsync(customerId, model.Id);
                if (existing != null)
                {
                    return BadRequest("BankAccount Id in Use");
                }

                //map
                var BankAccount = _mapper.Map<BankAccount>(model);

                //save and return
                if (!await _repository.StoreNewBankAccountAsync(customerId, BankAccount))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "BankAccount",
                            new { BankAccount.Id });

                    return Created(location, _mapper.Map<BankAccountModel>(BankAccount));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<BankAccountModel>> Put(int customerId, int Id, BankAccountModel updatedModel)
        {
            try
            {
                var currentBankAccount = await _repository.GetBankAccountAsync(customerId, Id);
                if (currentBankAccount == null) return NotFound($"Could not find BankAccount with Id of {Id}");

                _mapper.Map(updatedModel, currentBankAccount);

                if (await _repository.UpdateBankAccountAsync(customerId, currentBankAccount))
                {
                    return _mapper.Map<BankAccountModel>(currentBankAccount);
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
                var BankAccount = await _repository.GetBankAccountAsync(customerId, Id);
                if (BankAccount == null) return NotFound();

                if (await _repository.DeleteBankAccount(BankAccount))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the BankAccount");
        }

    }
}