using Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IDepartmentService
    {
        //Should based on Interface
        int CreateDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDto departmentDto);
    }
}