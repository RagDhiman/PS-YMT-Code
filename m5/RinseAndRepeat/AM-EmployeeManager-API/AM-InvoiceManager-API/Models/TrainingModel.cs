using System;
using System.Collections.Generic;
using System.Text;

namespace AM_EmployeeManager_API.Models
{
    public class TrainingModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public String Description { get; set; }
        public String Name { get; set; }
        public bool Certification { get; set; }
        public String CertificationName { get; set; }

    }
}
