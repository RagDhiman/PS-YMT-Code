using AM_SupplierManager_API.Models;
using AM_SupplierManager_Core;
using AM_SupplierManager_Core.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
namespace AM_CustomerManager_API.Controllers
{
    [Route("api/supplier/{supplierId}/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<AttachmentController> _logger;

        public AttachmentController(IAttachmentRepository repository, IMapper mapper, LinkGenerator linkGenerator,
            ILogger<AttachmentController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<AttachmentModel[]>> Get(int supplierId)
        {
            try
            {
                var results = await _repository.GetAllAttachmentsAsync(supplierId);

                return _mapper.Map<AttachmentModel[]>(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AttachmentModel>> Get(int supplierId, int Id)
        {
            try
            {
                var result = await _repository.GetAttachmentAsync(supplierId, Id);

                if (result == null) return NotFound();

                return _mapper.Map<AttachmentModel>(result);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AttachmentModel>> Post(int supplierId, AttachmentModel model)
        {
            try
            {
                //Make sure AttachmentId is not already taken
                var existing = await _repository.GetAttachmentAsync(supplierId, model.Id);
                if (existing != null)
                {
                    return BadRequest("Attachment Id in Use");
                }

                //map
                var Attachment = _mapper.Map<Attachment>(model);

                //save and return
                if (!await _repository.StoreNewAttachmentAsync(supplierId, Attachment))
                {
                    return BadRequest("Bad request, could not create record!");
                }
                else
                {
                    var location = _linkGenerator.GetPathByAction("Get",
                             "Attachment",
                            new { supplierId = Attachment.SupplierId, Attachment.Id });

                    return Created(location, _mapper.Map<AttachmentModel>(Attachment));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<AttachmentModel>> Put(int supplierId, int Id, AttachmentModel updatedModel)
        {
            try
            {
                var currentAttachment = await _repository.GetAttachmentAsync(supplierId, Id);
                if (currentAttachment == null) return NotFound($"Could not find Attachment with Id of {Id}");

                _mapper.Map(updatedModel, currentAttachment);

                if (await _repository.UpdateAttachmentAsync(supplierId, currentAttachment))
                {
                    return _mapper.Map<AttachmentModel>(currentAttachment);
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
                var Attachment = await _repository.GetAttachmentAsync(supplierId, Id);
                if (Attachment == null) return NotFound();

                if (await _repository.DeleteAttachment(Attachment))
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the Attachment");
        }

    }
}