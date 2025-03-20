using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories;
using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.Factories;

namespace Demo.BusinessLogic.Services
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {

            var department = _departmentRepository.GetAll();
            return department.Select(D => D.ToDepartmentDto());
        }
        // Manual Mappings
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);

            return department is null ? null : department.ToDepartmentDetailsDto();
        }

        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var departments = departmentDto.ToEntity();
            return _departmentRepository.Add(departments);


        }
        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            var deptments = departmentDto.ToEntity();
            return _departmentRepository.Update(deptments);
        }
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                int Result = _departmentRepository.Remove(department);
                return Result > 0 ? true : false;
            }
        }

    }
}
