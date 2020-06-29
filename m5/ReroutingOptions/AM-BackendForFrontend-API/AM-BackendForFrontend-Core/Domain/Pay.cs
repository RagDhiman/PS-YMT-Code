using System;
using System.Collections.Generic;
using System.Text;
using AM_BackendForFrontend_Data.Generic;

namespace AM_BackendForFrontend_Core
{
    public class Pay : IEntity
    {
        public int Id { get; set; }
        public double HourlyRate { get; set; }
        public int EmployeeId { get; set; }
        public bool DefaultRate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
