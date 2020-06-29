using System;
using System.Collections.Generic;
using System.Text;

namespace AM_EmployeeManager_Core {
    public class Equipment
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime LoanStartDateTime { get; set; }
        public DateTime LoanEndDateTime { get; set; }
        public String Reference { get; set; }
        public String Name { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }
}
