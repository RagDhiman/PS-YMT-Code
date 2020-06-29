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
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<NoteController> _logger;

        public NoteController(INoteRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<NoteController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<NoteModel[]>> Get(int customerId)
        {
            try
            {
                var results = await _repository.GetAllNotesAsync(customerId);

                return _mapper.Map<NoteModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<NoteModel>> Get(int customerId, int Id)
        {
            try
            {
                var result = await _repository.GetNoteAsync(customerId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<NoteModel>(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<NoteModel>> Post(int customerId, NoteModel model)
        {
            try
            {
                //Make sure NoteId is not already taken
                var existing = await _repository.GetNoteAsync(customerId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Note Id in Use");
                }

                //map
                var Note = _mapper.Map<Note>(model);

                //save and return
                if (!await _repository.StoreNewNoteAsync(customerId, Note))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Note",
                            new { Note.Id });

                    return Created(location, _mapper.Map<NoteModel>(Note));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<NoteModel>> Put(int customerId, int Id, NoteModel updatedModel)
        {
            try
            {
                var currentNote = await _repository.GetNoteAsync(customerId, Id);
                if (currentNote == null) return NotFound($"Could not find Note with Id of {Id}");

                _mapper.Map(updatedModel, currentNote);

                if (await _repository.UpdateNoteAsync(customerId, currentNote))
                {
                    return _mapper.Map<NoteModel>(currentNote);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int customerId, int Id)
        {
            try
            {
                var Note = await _repository.GetNoteAsync(customerId, Id);
                if (Note == null) return NotFound();

                if (await _repository.DeleteNote(Note))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Request Failure");
            }

            return BadRequest("Failed to delete the Note");
        }

    }
}