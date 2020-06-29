using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountsManager_UI_Web.Models
{
    public class EmailIndexModel
    {
        private string _message;
        private string _subject;

        public int Id { get; set; }
        [Display(Name = "Send To")]
        public String SendTo { get; set; }
        public String Sender { get; set; }
        public string Subject
        {
            get => $"{_subject.Substring(0, Math.Min(_subject.Length, 30))}...";
            set => _subject = value;
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
