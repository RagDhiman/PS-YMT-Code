using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountsManager_UI_Web.Models
{
    public class NoteIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Note Text")]
        public string NoteText { get; set; }
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
    }
}
