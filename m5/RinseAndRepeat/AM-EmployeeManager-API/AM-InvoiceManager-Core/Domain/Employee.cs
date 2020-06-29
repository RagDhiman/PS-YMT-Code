using System;
using System.Collections.Generic;

namespace AM_EmployeeManager_Core
{
    public class Employee
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

        public List<Absence> Absences { get; set; }
        public List<Equipment> Equipments { get; set; }
        public List<Holiday> Holidays { get; set; }
        public List<Pay> Pays { get; set; }
        public List<TaxInformation> TaxInformations { get; set; }
        public List<Training> Trainings { get; set; }

    }
}
