using AccountsManager_UI_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.ViewModels
{
    public class NotificationDashViewModel
    {
        public IEnumerable<EmailIndexModel> Emails { get; set; }
        public IEnumerable<SMSIndexModel> SMSs { get; set; }
        public IEnumerable<WebhookPostIndexModel> WebhookPosts { get; set; }
    }
}
