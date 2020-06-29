using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountsManager_UI_Web.Models
{
    public class SMSIndexModel
    {
        private string _message;
        private string _sender;
        public int Id { get; set; }
        [Display(Name = "Sent To")]
        public String SendTo { get; set; }
        public string Sender
        {
            get => $"{_sender.Substring(0, Math.Min(_sender.Length, 30))}...";
            set => _sender = value;
        }

        public string Message
        {
            get => $"{_message.Substring(0, Math.Min(_message.Length, 30))}...";
            set => _message = value;
        }

        [Display(Name = "Sent Date")]
        public DateTime SentDateTime { get; set; }
        [Display(Name = "Receipt Date")]
        public DateTime DeliveredDateTime { get; set; }
    }
}
