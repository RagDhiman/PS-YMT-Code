using AM_NotificationsService_API.Models;
using AM_NotificationsService_Core;
using AM_NotificationsService_Core.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AM_NotificationsService_Core.Services;

namespace AM_NotificationsService_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public NotificationController(IMapper mapper, LinkGenerator linkGenerator,
            ILogger<NotificationController> logger,
            INotificationService notificationService)
        {
            _notificationService = notificationService;
            _logger = logger;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [Route("api/[controller]/SendEmail")]
        [HttpPost()]
        public async Task<ActionResult<EmailModel>> SendEmail(EmailModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Bad request, could not send email!");
                }

                var email = _mapper.Map<Email>(model);

                await _notificationService.SendEmailAsync(email);

                var location = _linkGenerator.GetPathByAction("Get",
                        "Email",
                    new { email.Id });

                return Created(location, _mapper.Map<EmailModel>(email));

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [Route("api/[controller]/SendSMS")]
        [HttpPost()]
        public async Task<ActionResult<SMSModel>> SendSMS(SMSModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Bad request, could not send SMS!");
                }

                var sms = _mapper.Map<SMS>(model);

                await _notificationService.SendSMSAsync(sms);

                var location = _linkGenerator.GetPathByAction("Get",
                        "SMS",
                    new { sms.Id });

                return Created(location, _mapper.Map<SMSModel>(sms));

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [Route("api/[controller]/PostToWebhook")]
        [HttpPost()]
        public async Task<ActionResult<WebhookPostModel>> PostToWebhook(WebhookPostModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Bad request, could not post to webhook!");
                }

                var webhookPost = _mapper.Map<WebhookPost>(model);

                await _notificationService.PostToWebhookAsync(webhookPost);

                var location = _linkGenerator.GetPathByAction("Get",
                        "WebhookPost",
                    new { webhookPost.Id });

                return Created(location, _mapper.Map<WebhookPostModel>(webhookPost));

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

    }
}