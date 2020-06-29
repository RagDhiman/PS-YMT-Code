﻿using AccountsManager_Domain.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_Domain.Services
{
    public class NotificationService: INotificationService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly ISMSRepository _smsRepository;
        private readonly IWebhookPostRepository _webHookPostRepository;

        public NotificationService(IEmailRepository emailRepository, ISMSRepository smsRepository, IWebhookPostRepository webHookPostRepository)
        {
            _emailRepository = emailRepository;
            _smsRepository = smsRepository;
            _webHookPostRepository = webHookPostRepository;
        }

        public Task<bool> SendEmailAsync(Email email) {

            _emailRepository.StoreNewEmailAsync(email);

            return Task.FromResult(true);
        }

        public async Task<bool> SendSMSAsync(SMS sms)
        {
            bool result = await _smsRepository.StoreNewSMSAsync(sms);

            return result;
        }

        public async Task<bool> PostToWebhookAsync(WebhookPost webpost)
        {
            bool result = await _webHookPostRepository.StoreNewWebhookPostAsync(webpost);

            return result;
        }
    }
}
