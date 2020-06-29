using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.Models
{
    public class EstimateIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Invoice Ref.")]
        public int InvoiceId { get; set; }
        [Display(Name = "Customer Ref.")]
        public int CustomerId { get; set; }
        [Display(Name = "Date of Estimate")]
        public DateTime EstimateDate { get; set; }
        [Display(Name = "Expire Date")]
        public DateTime ExpirationDate { get; set; }
        public string Message { get; set; }
    }
}
