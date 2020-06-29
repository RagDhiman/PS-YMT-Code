using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.Models
{
    public class CustomerEditModel
    {
        [Required]
        [Range(0,1000)]
        public int Id { get; set; }

        [Required]
        [Range(0, 1000)]
        public int AccountId { get; set; }

        [Required]
        [StringLength(10)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Suffix { get; set; }
        [StringLength(100)]
        public string Company { get; set; }
        [StringLength(100)]
        [Display(Name = "Display Name As")]
        public string DisplayNameAs { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(200)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [StringLength(100)]
        public string Phone { get; set; }
        [DataType(DataType.PhoneNumber)]
        [StringLength(100)]
        public string Mobile { get; set; }
        [DataType(DataType.PhoneNumber)]
        [StringLength(100)]
        public string Fax { get; set; }
        [DataType(DataType.Url)]
        [StringLength(100)]
        public string Website { get; set; }

        [Display(Name = "CA Sent")]
        public bool CreditAgreement { get; set; }
    }
}
