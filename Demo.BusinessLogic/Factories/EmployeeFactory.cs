using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.EmployeeModel;
using Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto;

namespace Demo.BusinessLogic.Factories
{
    public static class EmployeeFactory
    {
        public static EmployeeDto ToEmployeeDto(this Employee e)
        {
            return new EmployeeDto
            {
                Name = e.Name,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                HiringDate = DateOnly.FromDateTime(e.HiringDate),
                Gender = e.Gender,
                EmployeeType = e.EmployeeType

            };
        }
        public static EmployeeDetailsDto ToEmployeeDetailsDto(this Employee e)
        {
            return new EmployeeDetailsDto
            {
                Id = e.Id,
                Name = e.Name,
                EmployeeType = e.EmployeeType,
                Gender = e.Gender,
                CreatedBy = e.CreatedBy,
                CreatedOn = DateOnly.FromDateTime(e.CreatedOn),
                LastModifiedBy = e.LastModifiedBy,
                LastModifiedOn = DateOnly.FromDateTime(e.LastModifiedOn),
                IsDeleted = e.IsDeleted,
                Age = e.Age,
                Address = e.Address,
                Salary = e.Salary,
                IsActive = e.IsActive,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                HiringDate = DateOnly.FromDateTime(e.HiringDate)

            };
        }

        public static Employee ToEntity(this CreatedEmplopyeeDto createdEmplopyeeDto)
        {
            return new Employee()
            {
                Name= createdEmplopyeeDto.Name,
                Email = createdEmplopyeeDto.Email,
                Gender = createdEmplopyeeDto.Gender,
                EmployeeType = createdEmplopyeeDto.EmployeeType,
                HiringDate = createdEmplopyeeDto.HiringDate.ToDateTime(new TimeOnly()),

            };
        }
        public static Employee ToEntity(this UpdateEmployeeDto updateEmployeeDto)
        {
            return new Employee()
            {
                Name = updateEmployeeDto.Name,
                Email = updateEmployeeDto.Email,
                Gender = updateEmployeeDto.Gender,
                EmployeeType = updateEmployeeDto.EmployeeType,
                HiringDate = updateEmployeeDto.HiringDate.ToDateTime(new TimeOnly()),
                Id = updateEmployeeDto.Id

            };
        }

    }
}
