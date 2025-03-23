using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Classes;
using DataAccess.Repositories.Interfaces;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto;
using Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository) : IEmployeeServices
    {
        public int CreateDepartment(CreatedEmplopyeeDto emplopyeeDto)
        {
            var employees = emplopyeeDto.ToEntity();
            return _employeeRepository.Add(employees);
        }

        public bool DeleteDepartment(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                int Result = _employeeRepository.Remove(employee);
                return Result > 0 ? true : false;
            }
        }

        public IEnumerable<EmployeeDto> GetAllDepartments()
        {
            var employee = _employeeRepository.GetAll();
            return employee.Select(E => E.ToEmployeeDto());
        }

        public EmployeeDetailsDto? GetDepartmentById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is null ? null : employee.ToEmployeeDetailsDto();
        }

        public int UpdateDepartment(UpdateEmployeeDto departmentDto)
        {
            var employees = departmentDto.ToEntity();
            return _employeeRepository.Update(employees);
        }
    }
}
