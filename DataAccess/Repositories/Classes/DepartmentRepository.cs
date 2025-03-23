using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data.Contexts;
using DataAccess.Models.DepartmentModel;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Classes
{
    /// <summary>
    /// Primary Constructors .Net C#12
    /// </summary>
    public class DepartmentRepository(AppDbContexts _dbContext) :GenericRepository<Department>(_dbContext), IDepartmentRepository
    {

    }
}
