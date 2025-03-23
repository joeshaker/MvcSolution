using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;

namespace Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto
{
    public class CreatedEmplopyeeDto
    {
        [Required(ErrorMessage = "Name is required!!!1")]
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;   

        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }  
    }
}
