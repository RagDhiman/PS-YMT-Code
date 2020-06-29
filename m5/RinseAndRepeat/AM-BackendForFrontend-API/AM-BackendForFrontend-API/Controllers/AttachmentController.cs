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
    public class AttachmentController : ControllerBase
    {
        private readonly ISupplierManagerRepository<Attachment> _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<AttachmentController> _logger;

        public AttachmentController(ISupplierManagerRepository<Attachment> repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<AttachmentController> logger)
        {
            _repository = repository;

            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        private void SetupPath(int supplierId)
        {
            _repository.ResourcePath = $"api/supplier/{supplierId}/Attachment";
        }

        [HttpGet]
        public async Task<ActionResult<AttachmentModel[]>> Get(int supplierId)
        {
            try
            {
                SetupPath(supplierId);

                var results = await _repository.GetAllAsync();

                return _mapper.Map<AttachmentModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AttachmentModel>> Get(int supplierId, int Id)
        {
            try
            {
                SetupPath(supplierId);

                var result = await _repository.GetByIdAsync(Id);

                if (result == null) return NotFound();

                return _mapper.Map<AttachmentModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AttachmentModel>> Post(int supplierId, AttachmentModel model)
        {
            try
            {
                SetupPath(supplierId);

                //Make sure AttachmentId is not already taken
                var existing = await _repository.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    return BadRequest("Attachment Id in Use");
                }

                //map
                var Attachment = _mapper.Map<Attachment>(model);

                //save and return
                if (!await _repository.StoreNewAsync(Attachment))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Attachment",
                            new { Id = Attachment.Id });

                    return Created(location, _mapper.Map<AttachmentModel>(Attachment));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AttachmentModel>> Put(int supplierId, int Id, AttachmentModel updatedModel)
        {
            try
            {
                SetupPath(supplierId);

                var currentAttachment = await _repository.GetByIdAsync(Id);
                if (currentAttachment == null) return NotFound($"Could not find Attachment with Id of {Id}");

                _mapper.Map(updatedModel, currentAttachment);

                if (await _repository.UpdateAsync(currentAttachment))
                {
                    return _mapper.Map<AttachmentModel>(currentAttachment);
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

                var Attachment = await _repository.GetByIdAsync(Id);
                if (Attachment == null) return NotFound();

                if (await _repository.DeleteAsync(Attachment))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed Request");
            }

            return BadRequest("Failed to delete the Attachment");
        }

    }
}