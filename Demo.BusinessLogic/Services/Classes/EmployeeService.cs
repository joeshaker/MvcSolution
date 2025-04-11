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
using Demo.BusinessLogic.Services.AttatchmentServices;
using Demo.BusinessLogic.Services.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork,IMapper _mapper,
        IAttatchmentService _attatchmentService) : IEmployeeServices
    {
        public int CreateEmployee(CreatedEmplopyeeDto emplopyeeDto)
        {
            var employees = _mapper.Map<CreatedEmplopyeeDto,Employee>(emplopyeeDto);

            if (emplopyeeDto.Image != null) {
                employees.ImageName = _attatchmentService.Upload(emplopyeeDto.Image, "images");

            }
            _unitOfWork.EmployeeRepository.Add(employees);


            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                //int Result = _employeeRepository.Remove(employee);
                //return Result > 0 ? true : false;
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(employee);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(string ? EmployeeSearchName)
        {
            //var employee = _employeeRepository.GetAll(E=>E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            //var employeeDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(employee);
            //return employeeDto;
            IEnumerable<Employee> employee;
            if (string.IsNullOrEmpty(EmployeeSearchName))
            {
                employee = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employee = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            }
            var employeeDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employee);
            return employeeDto;
            //return employee.Select(E => E.ToEmployeeDto());
        }


        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(employee);
        }

        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            //var employees = employeeDto.ToEntity();
            //if (employeeDto.Image != null)
            //{
            //    _attatchmentService.Upload(employeeDto.Image, "images");
            //}
            _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdateEmployeeDto,Employee>(employeeDto));
            return _unitOfWork.SaveChanges();
        }
    }
}
