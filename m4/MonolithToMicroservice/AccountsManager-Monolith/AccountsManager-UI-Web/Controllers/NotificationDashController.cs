using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsManager_UI_Web.Data;
using AccountsManager_UI_Web.Data.DTOs;
using AccountsManager_UI_Web.Models;
using AccountsManager_UI_Web.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountsManager_UI_Web.Controllers
{
    public class NotificationDashController : Controller
    {
        //private readonly IGenericRepository<Notification> _NotificationRepository;
        private readonly IGenericRepository<Email> _EmailRepository;
        private readonly IGenericRepository<SMS> _SMSRepository;
        private readonly IGenericRepository<WebhookPost> _WebhookPostRepository;
        private readonly IMapper _mapper;

        public NotificationDashController(IGenericRepository<Email> EmailRepository,
            IGenericRepository<SMS> SMSRepository,
            IGenericRepository<WebhookPost> WebhookPostRepository,
            IMapper mapper)
        {
            _EmailRepository = EmailRepository;
            _SMSRepository = SMSRepository;
            _WebhookPostRepository = WebhookPostRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> Main()
        {
            SetupChildRepositories();
            var emails = await _EmailRepository.GetAllAsync();
            var smss = await _SMSRepository.GetAllAsync();
            var webookposts = await _WebhookPostRepository.GetAllAsync();

            var dashViewModel = new NotificationDashViewModel();

            dashViewModel.Emails = _mapper.Map<IEnumerable<EmailIndexModel>>(emails);
            dashViewModel.SMSs = _mapper.Map<IEnumerable<SMSIndexModel>>(smss);
            dashViewModel.WebhookPosts = _mapper.Map<IEnumerable<WebhookPostIndexModel>>(webookposts);

            return View(dashViewModel);
        }

        private void SetupChildRepositories()
        {
            _EmailRepository.ResourcePath = $"api/notification/email";
            _SMSRepository.ResourcePath = $"api/notification/sms";
            _WebhookPostRepository.ResourcePath = $"api/notification/webhookpost";
        }
    }
}