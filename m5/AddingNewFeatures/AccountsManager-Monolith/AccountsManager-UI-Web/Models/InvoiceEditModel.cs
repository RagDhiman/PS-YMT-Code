using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountsManager_UI_Web.Models
{
    public class InvoiceDetailModel
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Customer Id")]
        [Required]

        public int CustomerId { get; set; }
        [Display(Name = "Invoice Date")]
        [Required]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "Due Date")]

        [Required]
        public DateTime DueDate { get; set; }

        [StringLength(100)]
        public string Message { get; set; }
    }
}
