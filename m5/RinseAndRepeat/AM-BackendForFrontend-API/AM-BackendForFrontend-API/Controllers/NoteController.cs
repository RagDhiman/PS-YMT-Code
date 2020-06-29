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
    public class NoteController : ControllerBase
    {
        private readonly ICustomerManagerRepository<Note> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<NoteController> _logger;

        public NoteController(ICustomerManagerRepository<Note> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<NoteController> logger)
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
        public async Task<ActionResult<NoteModel[]>> Get(int customerId)
        {
            try
            {
                SetupPath(customerId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<NoteModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<NoteModel>> Get(int customerId, int Id)
        {
            try
            {
                SetupPath(customerId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<NoteModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<NoteModel>> Post(int customerId, NoteModel model)
        {
            try
            {
                SetupPath(customerId);

                //Make sure NoteId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Note Id in Use");
                }

                //map
                var Note = _mapper.Map<Note>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Note))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Note",
                            new { Id = Note.Id });

                    return Created(location, _mapper.Map<NoteModel>(Note));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<NoteModel>> Put(int customerId, int Id, NoteModel updatedModel)
        {
            try
            {
                SetupPath(customerId);

                var currentNote = await _repository.GetByIdAsync(Id);
                if (currentNote == null) return NotFound($"Could not find Note with Id of {Id}");

                _mapper.Map(updatedModel, currentNote);

                if (await _repository.UpdateAsync(currentNote))
                {
                    return _mapper.Map<NoteModel>(currentNote);
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

                var Note = await _repository.GetByIdAsync(Id);
                if (Note == null) return NotFound();

                if (await _repository.DeleteAsync(Note))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Note");
        }

    }
}