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
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<VoucherController> _logger;

        public VoucherController(IVoucherRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<VoucherController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<VoucherModel[]>> GetAll(int accountId)
        {
            try
            {
                var results = await _repository.GetAllVouchersAsync(accountId);

                return _mapper.Map<VoucherModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<VoucherModel>> Get(int AccountId, int Id)
        {
            try
            {
                var result = await _repository.GetVoucherAsync(AccountId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<VoucherModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<VoucherModel>> Post(int AccountId, VoucherModel model)
        {
            try
            {
                //Make sure VoucherId is not already taken
                var existing = await _repository.GetVoucherAsync(AccountId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Voucher Id in Use");
                }

                //map
                var Voucher = _mapper.Map<Voucher>(model);

                //save and return
                if (!await _repository.StoreNewVoucherAsync(AccountId, Voucher))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Voucher",
                            new { Voucher.AccountId, Voucher.Id });

                    return Created(location, _mapper.Map<VoucherModel>(Voucher));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<VoucherModel>> Put(int AccountId, int Id, VoucherModel updatedModel)
        {
            try
            {
                var currentVoucher = await _repository.GetVoucherAsync(AccountId, Id);
                if (currentVoucher == null) return NotFound($"Could not find Voucher with Id of {Id}");

                _mapper.Map(updatedModel, currentVoucher);

                if (await _repository.UpdateVoucherAsync(AccountId, currentVoucher))
                {
                    return _mapper.Map<VoucherModel>(currentVoucher);
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
                var Voucher = await _repository.GetVoucherAsync(AccountId, Id);
                if (Voucher == null) return NotFound();

                if (await _repository.DeleteVoucher(Voucher))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Voucher");
        }

    }
}