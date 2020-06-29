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
    [Route("api/supplier/{supplierId}/[controller]")]
    [ApiController]
    public class SupplierNoteController : ControllerBase
    {
        private readonly ISupplierNoteRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SupplierNoteController> _logger;

        public SupplierNoteController(ISupplierNoteRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<SupplierNoteController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<SupplierNoteModel[]>> GetAll(int supplierId)
        {
            try
            {
                var results = await _repository.GetAllSupplierNotesAsync(supplierId);

                return _mapper.Map<SupplierNoteModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SupplierNoteModel>> Get(int supplierId, int Id)
        {
            try
            {
                var result = await _repository.GetSupplierNoteAsync(supplierId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<SupplierNoteModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SupplierNoteModel>> Post(int supplierId, SupplierNoteModel model)
        {
            try
            {
                //Make sure SupplierNoteId is not already taken
                var existing = await _repository.GetSupplierNoteAsync(supplierId, model.Id);
                if (existing != null)
                {
                    return BadRequest("SupplierNote Id in Use");
                }

                //map
                var SupplierNote = _mapper.Map<SupplierNote>(model);

                //save and return
                if (!await _repository.StoreNewSupplierNoteAsync(supplierId, SupplierNote))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "SupplierNote",
                            new { supplierId = SupplierNote.SupplierId, SupplierNote.Id });

                    return Created(location, _mapper.Map<SupplierNoteModel>(SupplierNote));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SupplierNoteModel>> Put(int supplierId, int Id, SupplierNoteModel updatedModel)
        {
            try
            {
                var currentSupplierNote = await _repository.GetSupplierNoteAsync(supplierId, Id);
                if (currentSupplierNote == null) return NotFound($"Could not find SupplierNote with Id of {Id}");

                _mapper.Map(updatedModel, currentSupplierNote);

                if (await _repository.UpdateSupplierNoteAsync(supplierId, currentSupplierNote))
                {
                    return _mapper.Map<SupplierNoteModel>(currentSupplierNote);
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
        public async Task<IActionResult> Delete(int supplierId, int Id)
        {
            try
            {
                var SupplierNote = await _repository.GetSupplierNoteAsync(supplierId, Id);
                if (SupplierNote == null) return NotFound();

                if (await _repository.DeleteSupplierNote(SupplierNote))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the SupplierNote");
        }

    }
}