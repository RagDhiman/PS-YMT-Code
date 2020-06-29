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
    public class AddressController : ControllerBase
    {
        private readonly ICustomerManagerRepository<Address> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<AddressController> _logger;

        public AddressController(ICustomerManagerRepository<Address> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<AddressController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int customerId)
        {
            _repository.ResourcePath = $"api/customer/{customerId}/address";
        }

        [HttpGet]
        public async Task<ActionResult<AddressModel[]>> Get(int customerId)
        {
            try
            {
                SetupPath(customerId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<AddressModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AddressModel>> Get(int customerId, int Id)
        {
            try
            {
                SetupPath(customerId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<AddressModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AddressModel>> Post(int customerId, AddressModel model)
        {
            try
            {
                SetupPath(customerId);

                //Make sure AddressId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Address Id in Use");
                }

                //map
                var Address = _mapper.Map<Address>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Address))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Address",
                            new { Id = Address.Id });

                    return Created(location, _mapper.Map<AddressModel>(Address));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AddressModel>> Put(int customerId, int Id, AddressModel updatedModel)
        {
            try
            {
                SetupPath(customerId);

                var currentAddress = await _repository.GetByIdAsync(Id);
                if (currentAddress == null) return NotFound($"Could not find Address with Id of {Id}");

                _mapper.Map(updatedModel, currentAddress);

                if (await _repository.UpdateAsync(currentAddress))
                {
                    return _mapper.Map<AddressModel>(currentAddress);
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

                var Address = await _repository.GetByIdAsync(Id);
                if (Address == null) return NotFound();

                if (await _repository.DeleteAsync(Address))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Address");
        }

    }
}