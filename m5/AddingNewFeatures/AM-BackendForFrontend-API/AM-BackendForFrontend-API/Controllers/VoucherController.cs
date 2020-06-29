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
    public class VoucherController : ControllerBase
    {
        private readonly IAccountManagerRepository<Voucher> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<VoucherController> _logger;

        public VoucherController(IAccountManagerRepository<Voucher> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<VoucherController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int accountId)
        {
            _repository.ResourcePath = $"api/account/{accountId}/Voucher";
        }

        [HttpGet]
        public async Task<ActionResult<VoucherModel[]>> Get(int accountId)
        {
            try
            {
                SetupPath(accountId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<VoucherModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<VoucherModel>> Get(int accountId, int Id)
        {
            try
            {
                SetupPath(accountId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<VoucherModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<VoucherModel>> Post(int accountId, VoucherModel model)
        {
            try
            {
                SetupPath(accountId);

                //Make sure VoucherId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Voucher Id in Use");
                }

                //map
                var Voucher = _mapper.Map<Voucher>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Voucher))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Voucher",
                            new { Id = Voucher.Id });

                    return Created(location, _mapper.Map<VoucherModel>(Voucher));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<VoucherModel>> Put(int accountId, int Id, VoucherModel updatedModel)
        {
            try
            {
                SetupPath(accountId);

                var currentVoucher = await _repository.GetByIdAsync(Id);
                if (currentVoucher == null) return NotFound($"Could not find Voucher with Id of {Id}");

                _mapper.Map(updatedModel, currentVoucher);

                if (await _repository.UpdateAsync(currentVoucher))
                {
                    return _mapper.Map<VoucherModel>(currentVoucher);
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

                var Voucher = await _repository.GetByIdAsync(Id);
                if (Voucher == null) return NotFound();

                if (await _repository.DeleteAsync(Voucher))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Voucher");
        }

    }
}