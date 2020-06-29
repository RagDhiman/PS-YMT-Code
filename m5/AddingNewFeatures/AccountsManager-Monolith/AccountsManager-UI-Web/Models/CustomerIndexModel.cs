using System.ComponentModel.DataAnnotations;

namespace AccountsManager_UI_Web.Models
{
    public class CustomerIndexModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Display(Name = "CA Sent")]
        public bool CreditAgreement { get; set; }

        [Display(Name = "Parent Comapny")]
        public string ParentCompany { get; set; }
    }
}
