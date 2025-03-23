using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto;
using Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeServices
    {
        int CreateDepartment(CreatedEmplopyeeDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<EmployeeDto> GetAllDepartments();
        EmployeeDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdateEmployeeDto departmentDto);
    }
}
