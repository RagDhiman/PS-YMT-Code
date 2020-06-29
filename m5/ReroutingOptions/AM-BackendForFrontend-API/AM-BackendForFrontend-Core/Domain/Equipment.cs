using System;
using System.Collections.Generic;
using System.Text;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core {
    public class Equipment : IEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime LoanStartDateTime { get; set; }
        public DateTime LoanEndDateTime { get; set; }
        public String Reference { get; set; }
        public String Name { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }
}
