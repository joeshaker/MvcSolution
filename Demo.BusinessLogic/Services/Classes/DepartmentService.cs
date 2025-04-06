using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Interfaces;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {

            var department = _unitOfWork.DepartmentRepository.GetAll();
            return department.Select(D => D.ToDepartmentDto());
        }
        // Manual Mappings
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);

            return department is null ? null : department.ToDepartmentDetailsDto();
        }

        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var departments = departmentDto.ToEntity();
            _unitOfWork.DepartmentRepository.Add(departments);
            return _unitOfWork.SaveChanges();


        }
        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            var deptments = departmentDto.ToEntity();
            _unitOfWork.DepartmentRepository.Update(deptments);
            return _unitOfWork.SaveChanges();
        }
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Remove(department);
                int Result = _unitOfWork.SaveChanges();
                return Result> 0 ? true : false;
            }
        }

    }
}
