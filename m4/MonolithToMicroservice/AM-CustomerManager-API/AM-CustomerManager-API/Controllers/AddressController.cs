using AM_CustomerManager_API.Models;
using AM_CustomerManager_Core.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AM_CustomerManager_Core;
using AutoMapper;

namespace AM_CustomerManager_API.Controllers
{
    [Route("api/customer/{customerId}/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IAddressRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<AddressController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<AddressModel[]>> Get(int customerId)
        {
            try
            {
                var results = await _repository.GetAllAddressesAsync(customerId);

                return _mapper.Map<AddressModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AddressModel>> Get(int customerId, int Id)
        {
            try
            {
                var result = await _repository.GetAddressAsync(customerId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<AddressModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AddressModel>> Post(int customerId, AddressModel model)
        {
            try
            {
                //Make sure AddressId is not already taken
                var existing = await _repository.GetAddressAsync(customerId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Address Id in Use");
                }

                //map
                var Address = _mapper.Map<Address>(model);

                //save and return
                if (!await _repository.StoreNewAddressAsync(customerId, Address))
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AddressModel>> Put(int customerId, int Id, AddressModel updatedModel)
        {
            try
            {
                var currentAddress = await _repository.GetAddressAsync(customerId, Id);
                if (currentAddress == null) return NotFound($"Could not find Address with Id of {Id}");

                _mapper.Map(updatedModel, currentAddress);

                if (await _repository.UpdateAddressAsync(customerId, currentAddress))
                {
                    return _mapper.Map<AddressModel>(currentAddress);
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
                var Address = await _repository.GetAddressAsync(customerId, Id);
                if (Address == null) return NotFound();

                if (await _repository.DeleteAddress(Address))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Address");
        }

    }
}