using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Demo.BusinessLogic.DataTransferObjects
{
    public class DepartmentDetailsDto
    {
        //Constructor Based Mapping
        //public DepartmentDetailsDto(Department department) {
        //    Id = department.Id;
        //    Name = department.Name;
        //    CreatedOn=DateOnly.FromDateTime(department.CreatedOn);
        //    Code = department.Code;
        //    LastModifiedOn=DateOnly.FromDateTime(department.LastModifiedOn);
            
        //}
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string Code { get; set; } = string.Empty;

        public int Id { get; set; }//PK
        public int CreatedBy { get; set; }//Used ID

        public DateOnly CreatedOn { get; set; }

        public int LastModifiedBy { get; set; }// User ID
        public DateOnly LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; } //Flag Soft Delete
    }

}
