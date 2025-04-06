using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared;

namespace DataAccess.Models.DepartmentModel
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public string Code { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
