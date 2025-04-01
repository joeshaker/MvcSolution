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
                Email = e.Email,
                Name = e.Name,
                Id = e.Id,
                Age = e.Age,
                Salary = e.Salary,
                IsActive = e.IsActive,
                EmpGender=e.Gender.ToString(),
                EmpType=e.EmployeeType.ToString(),

            };
        }
        public static EmployeeDetailsDto ToEmployeeDetailsDto(this Employee e)
        {
            return new EmployeeDetailsDto
            {
                Id = e.Id,
                Name = e.Name,
                CreatedBy = e.CreatedBy,
                LastModifiedBy = e.LastModifiedBy,
                Age = e.Age,
                Address = e.Address,
                Salary = e.Salary,
                IsActive = e.IsActive,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                HiringDate = DateOnly.FromDateTime(e.HiringDate),
                Gender=e.Gender.ToString(),
                EmployeeType = e.EmployeeType.ToString(),
                CreatedOn = e.CreatedOn,
                LastModifiedOn = e.LastModifiedOn

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
                IsActive = createdEmplopyeeDto.IsActive,
                Salary = createdEmplopyeeDto.Salary,
                PhoneNumber = createdEmplopyeeDto.PhoneNumber,
                Address = createdEmplopyeeDto.Address,
                Age = (int)createdEmplopyeeDto.Age!.Value


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
                Id = updateEmployeeDto.Id,
                IsActive = updateEmployeeDto.IsActive,
                Salary = updateEmployeeDto.Salary,
                PhoneNumber = updateEmployeeDto.PhoneNumber,
                Address = updateEmployeeDto.Address,
                Age = (int)updateEmployeeDto.Age!.Value
            };
        }

    }
}
