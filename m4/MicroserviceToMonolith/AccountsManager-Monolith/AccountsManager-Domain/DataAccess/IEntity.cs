using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesManager_Domain.DataAccess
{
    public interface IEntity
    {
        int Id { get; set; }
    }
}
