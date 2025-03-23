using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;

namespace Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto
{
    public class EmployeeDetailsDto
    {
        public string Name { get; set; } = string.Empty;

        public int Id { get; set; }//PK
        public int CreatedBy { get; set; }//Used ID

        public DateOnly CreatedOn { get; set; }

        public int LastModifiedBy { get; set; }// User ID
        public DateOnly LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; } //Flag Soft Delete
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }


    }
}
