using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Models.EmployeeModel;
using DataAccess.Repositories.Classes;
using DataAccess.Repositories.Interfaces;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto;
using Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository,IMapper _mapper) : IEmployeeServices
    {
        public int CreateEmployee(CreatedEmplopyeeDto emplopyeeDto)
        {
            var employees = _mapper.Map<CreatedEmplopyeeDto,Employee>(emplopyeeDto);
            return _employeeRepository.Add(employees);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                //int Result = _employeeRepository.Remove(employee);
                //return Result > 0 ? true : false;
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee)>0?true:false;
            }
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking=false)
        {
            var employee = _employeeRepository.GetAll();
            var employeeDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(employee);
            return employeeDto;
            //return employee.Select(E => E.ToEmployeeDto());
        }


        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(employee);
        }

        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            //var employees = employeeDto.ToEntity();
            return _employeeRepository.Update(_mapper.Map<UpdateEmployeeDto,Employee>(employeeDto));
        }
    }
}
