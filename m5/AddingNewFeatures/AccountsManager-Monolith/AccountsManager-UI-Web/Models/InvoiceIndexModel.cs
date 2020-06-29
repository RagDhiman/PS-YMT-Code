using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountsManager_UI_Web.Models
{
    public class InvoiceEditModel
    {
        public int Id { get; set; }
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
        public string Message { get; set; }
    }
}
