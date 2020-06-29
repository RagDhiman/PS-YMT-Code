using AccountsManager_UI_Web.Data.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountsManager_UI_Web.Models
{
    public class ExpenseIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Payee Name")]
        public string PayeeName { get; set; }
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [Display(Name = "Supplier Id")]
        public int SupplierId { get; set; }
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Bank Account")]
        public int BankAccountId { get; set; }
        [Display(Name = "Reference")]
        public String Reference { get; set; }
        [Display(Name = "Notes")]
        public String Notes { get; set; }
    }
}
