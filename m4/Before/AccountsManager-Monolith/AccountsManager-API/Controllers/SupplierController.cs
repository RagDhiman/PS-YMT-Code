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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(ISupplierRepository repository, IMapper mapper, LinkGenerator linkGenerator, 
            ILogger<SupplierController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<SupplierModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllSuppliersAsync();

                return _mapper.Map<SupplierModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SupplierModel>> Get(int Id)
        {
            try
            {
                var result = await _repository.GetSupplierAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<SupplierModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SupplierModel>> Post(SupplierModel model)
        {
            try
            {
                //Make sure SupplierId is not already taken
                var existing = await _repository.GetSupplierAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Supplier Id in Use");
                }

                //map
                var Supplier = _mapper.Map<Supplier>(model);

                //save and return
                if (!await _repository.StoreNewSupplierAsync(Supplier))
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SupplierModel>> Put(int Id, SupplierModel updatedModel)
        {
            try
            {
                var currentSupplier = await _repository.GetSupplierAsync(Id);
                if (currentSupplier == null) return NotFound($"Could not find Supplier with Id of {Id}");

                _mapper.Map(updatedModel, currentSupplier);

                if (await _repository.UpdateSupplierAsync(currentSupplier))
                {
                    return _mapper.Map<SupplierModel>(currentSupplier);
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
                var Supplier = await _repository.GetSupplierAsync(Id);
                if (Supplier == null) return NotFound();

                if (await _repository.DeleteSupplier(Supplier))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Supplier");
        }

    }
}