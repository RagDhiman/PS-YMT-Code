using System;
using System.Collections.Generic;
using System.Text;

namespace AM_EmployeeManager_Core
{
    public class Pay
    {
        public int Id { get; set; }
        public double HourlyRate { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public bool DefaultRate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
