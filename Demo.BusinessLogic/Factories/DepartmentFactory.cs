using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.DepartmentModel;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto;

namespace Demo.BusinessLogic.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department D)
        {
            return new DepartmentDto
            {
                DeptId = D.Id,
                Code = D.Code,
                Name = D.Name,
                Description = D.Description,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)

            };
        }
        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department D) {
            return new DepartmentDetailsDto()
            {
                Description = D.Description,
                Id = D.Id,
                Name = D.Name,
                CreatedOn = DateOnly.FromDateTime(D.CreatedOn),
                Code = D.Code,
                LastModifiedOn = DateOnly.FromDateTime(D.LastModifiedOn),
                CreatedBy = D.CreatedBy,
                LastModifiedBy = D.LastModifiedBy,
                IsDeleted = D.IsDeleted,


            };
        }
        public static Department ToEntity(this CreatedDepartmentDto createdDepartmentDto)
        {
            return new Department()
            {
                Description = createdDepartmentDto.Description,
                Name = createdDepartmentDto.Name,
                Code = createdDepartmentDto.Code,
                CreatedOn = createdDepartmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };

        }
        public static Department ToEntity(this UpdateDepartmentDto updateDepartmentDto)
        {
            return new Department()
            {
                Id = updateDepartmentDto.Id,
                Description = updateDepartmentDto.Description,
                Name = updateDepartmentDto.Name,
                Code = updateDepartmentDto.Code,
                CreatedOn = updateDepartmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };

        }
    }
}
