using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace AccountsManager_UI_Web.Models
{
    public class CustomerDetailsModel
    {
        [ReadOnly(true)]
        public int Id { get; set; }
        [ReadOnly(true)]
        public int AccountId { get; set; }
        [ReadOnly(true)]
        public string Title { get; set; }
        [ReadOnly(true)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [ReadOnly(true)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Company { get; set; }
        [Display(Name = "Display Name As")]
        public string DisplayNameAs { get; set; }
        [ReadOnly(true)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Display(Name = "CA Sent")]
        public bool CreditAgreement { get; set; }
    }
}
