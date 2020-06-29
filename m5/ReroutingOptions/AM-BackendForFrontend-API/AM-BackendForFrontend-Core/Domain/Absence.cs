using AM_BackendForFrontend_Data.Generic;
using System;

namespace AM_BackendForFrontend_Core
{
    public class Absence : IEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public String Description { get; set; }
        public String Notes { get; set; }
        public bool Paid { get; set; }
    }
}
