using System;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayNameAs { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public DateTime DOB { get; set; }
    }
}
