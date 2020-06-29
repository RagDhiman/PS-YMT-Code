using System;
using System.Collections.Generic;
using System.Text;

namespace AM_EmployeeManager_API.Models
{
    public class HolidayModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool OnCall { get; set; }
        public int OnCallRateMultiplier { get; set; }
        public bool Paid { get; set; }
    }
}
