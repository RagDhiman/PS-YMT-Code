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
    [Route("api/customer/{customerId}/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly ICustomerManagerRepository<BankAccount> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<BankAccountController> _logger;

        public BankAccountController(ICustomerManagerRepository<BankAccount> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<BankAccountController> logger)
        {
            _repository = repository;

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int customerId)
        {
            _repository.ResourcePath = $"api/customer/{customerId}/BankAccount";
        }

        [HttpGet]
        public async Task<ActionResult<BankAccountModel[]>> Get(int customerId)
        {
            try
            {
                SetupPath(customerId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<BankAccountModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<BankAccountModel>> Get(int customerId, int Id)
        {
            try
            {
                SetupPath(customerId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<BankAccountModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BankAccountModel>> Post(int customerId, BankAccountModel model)
        {
            try
            {
                SetupPath(customerId);

                //Make sure BankAccountId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("BankAccount Id in Use");
                }

                //map
                var BankAccount = _mapper.Map<BankAccount>(model);

                //save and return
                if (!await _repository.StoreNewAsync(BankAccount))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "BankAccount",
                            new { Id = BankAccount.Id });

                    return Created(location, _mapper.Map<BankAccountModel>(BankAccount));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<BankAccountModel>> Put(int customerId, int Id, BankAccountModel updatedModel)
        {
            try
            {
                SetupPath(customerId);

                var currentBankAccount = await _repository.GetByIdAsync(Id);
                if (currentBankAccount == null) return NotFound($"Could not find BankAccount with Id of {Id}");

                _mapper.Map(updatedModel, currentBankAccount);

                if (await _repository.UpdateAsync(currentBankAccount))
                {
                    return _mapper.Map<BankAccountModel>(currentBankAccount);
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
        public async Task<IActionResult> Delete(int customerId, int Id)
        {
            try
            {
                SetupPath(customerId);

                var BankAccount = await _repository.GetByIdAsync(Id);
                if (BankAccount == null) return NotFound();

                if (await _repository.DeleteAsync(BankAccount))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the BankAccount");
        }

    }
}