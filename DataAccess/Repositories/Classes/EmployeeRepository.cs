using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data.Contexts;
using DataAccess.Models.DepartmentModel;
using DataAccess.Models.EmployeeModel;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Classes
{
    public class EmployeeRepository(AppDbContexts _dbContext) : GenericRepository<Employee>(_dbContext),IEmployeeRepository
    {

    }
}
