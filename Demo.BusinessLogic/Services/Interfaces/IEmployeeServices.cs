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
        int CreateEmployee(CreatedEmplopyeeDto departmentDto);
        bool DeleteEmployee(int id);
        IEnumerable<EmployeeDto> GetAllEmployees(string ? EmployeeSearchName);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int UpdateEmployee(UpdateEmployeeDto departmentDto);
    }
}
