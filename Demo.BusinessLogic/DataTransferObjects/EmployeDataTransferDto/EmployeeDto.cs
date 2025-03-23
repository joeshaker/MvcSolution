using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;

namespace Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto
{
    public class EmployeeDto
    {
        public string Name { get; set; } 
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }= string.Empty;  
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
