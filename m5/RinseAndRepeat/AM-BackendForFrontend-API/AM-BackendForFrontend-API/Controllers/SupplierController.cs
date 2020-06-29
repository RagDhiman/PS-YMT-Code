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
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierManagerRepository<Supplier> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(ISupplierManagerRepository<Supplier> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<SupplierController> logger)
        {
            _repository = repository;
            _repository.ResourcePath = "api/Supplier";

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<SupplierModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllAsync();

                return _mapper.Map<SupplierModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SupplierModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<SupplierModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SupplierModel>> Post(SupplierModel model)
        {
            try
            {
                //Make sure SupplierId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Supplier Id in Use");
                }

                //map
                var Supplier = _mapper.Map<Supplier>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Supplier))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Supplier",
                            new { Id = Supplier.Id });

                    return Created(location, _mapper.Map<SupplierModel>(Supplier));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SupplierModel>> Put(int Id, SupplierModel updatedModel)
        {
            try
            {
                var currentSupplier = await _repository.GetByIdAsync(Id);
                if (currentSupplier == null) return NotFound($"Could not find Supplier with Id of {Id}");

                _mapper.Map(updatedModel, currentSupplier);

                if (await _repository.UpdateAsync(currentSupplier))
                {
                    return _mapper.Map<SupplierModel>(currentSupplier);
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
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var Supplier = await _repository.GetByIdAsync(Id);
                if (Supplier == null) return NotFound();

                if (await _repository.DeleteAsync(Supplier))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Supplier");
        }

    }
}