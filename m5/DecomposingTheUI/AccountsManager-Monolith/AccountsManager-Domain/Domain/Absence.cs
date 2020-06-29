using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_Domain
{
    public class Absence
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public String Description { get; set; }
        public String Notes { get; set; }
        public bool Paid { get; set; }
    }
}
