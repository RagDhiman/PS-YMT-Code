using System;
using System.Collections.Generic;
using System.Text;

namespace AM_BackendForFrontend_API.Models
{
    public class PayModel
    {
        public int Id { get; set; }
        public double HourlyRate { get; set; }
        public int EmployeeId { get; set; }
        public bool DefaultRate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
