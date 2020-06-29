using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Company { get; set; }
        public string DisplayNameAs { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public bool CreditAgreement { get; set; } = false;
        public string ParentCompany { get; set; }

    }
}
