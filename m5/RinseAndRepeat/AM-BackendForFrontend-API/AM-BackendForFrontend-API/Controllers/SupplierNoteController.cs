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
    [Route("api/supplier/{supplierId}/[controller]")]
    [ApiController]
    public class SupplierNoteController : ControllerBase
    {
        private readonly ISupplierManagerRepository<SupplierNote> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<SupplierNoteController> _logger;

        public SupplierNoteController(ISupplierManagerRepository<SupplierNote> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<SupplierNoteController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int supplierId)
        {
            _repository.ResourcePath = $"api/supplier/{supplierId}/SupplierNote";
        }

        [HttpGet]
        public async Task<ActionResult<SupplierNoteModel[]>> Get(int supplierId)
        {
            try
            {
                SetupPath(supplierId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<SupplierNoteModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SupplierNoteModel>> Get(int supplierId, int Id)
        {
            try
            {
                SetupPath(supplierId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<SupplierNoteModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SupplierNoteModel>> Post(int supplierId, SupplierNoteModel model)
        {
            try
            {
                SetupPath(supplierId);

                //Make sure SupplierNoteId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("SupplierNote Id in Use");
                }

                //map
                var SupplierNote = _mapper.Map<SupplierNote>(model);

                //save and return
                if (!await _repository.StoreNewAsync(SupplierNote))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "SupplierNote",
                            new { Id = SupplierNote.Id });

                    return Created(location, _mapper.Map<SupplierNoteModel>(SupplierNote));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<SupplierNoteModel>> Put(int supplierId, int Id, SupplierNoteModel updatedModel)
        {
            try
            {
                SetupPath(supplierId);

                var currentSupplierNote = await _repository.GetByIdAsync(Id);
                if (currentSupplierNote == null) return NotFound($"Could not find SupplierNote with Id of {Id}");

                _mapper.Map(updatedModel, currentSupplierNote);

                if (await _repository.UpdateAsync(currentSupplierNote))
                {
                    return _mapper.Map<SupplierNoteModel>(currentSupplierNote);
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
        public async Task<IActionResult> Delete(int supplierId, int Id)
        {
            try
            {
                SetupPath(supplierId);

                var SupplierNote = await _repository.GetByIdAsync(Id);
                if (SupplierNote == null) return NotFound();

                if (await _repository.DeleteAsync(SupplierNote))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the SupplierNote");
        }

    }
}