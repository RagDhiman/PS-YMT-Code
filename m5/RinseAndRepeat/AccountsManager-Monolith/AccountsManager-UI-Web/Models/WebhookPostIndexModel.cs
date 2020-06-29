using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountsManager_UI_Web.Models
{
    public class WebhookPostIndexModel
    {
        private string _body;

        public int Id { get; set; }
        public String URL { get; set; }
        public String Sender { get; set; }
        public string Body
        {
            get => $"{_body.Substring(0, Math.Min(_body.Length, 30))}...";
            set => _body = value;
        }

        [Display(Name = "Sent Date")]
        public DateTime PostDateTime { get; set; }
    }
}
