using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data.Contexts;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Primary Constructors .Net C#12
    /// </summary>
    public class DepartmentRepository(AppDbContexts dbContext) : IDepartmentRepository
    {
        private readonly AppDbContexts _dbContext = dbContext;

        //CRUD Operations
        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
            {
                return _dbContext.Departments.ToList();
            }
            else
            {
                return _dbContext.Departments.AsNoTracking().ToList();
            }
        }

        public Department? GetById(int id) => _dbContext.Departments.Find(id);

        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }
        public int Remove(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }
        public int Add(Department department)
        {
            _dbContext.Add(department);
            return _dbContext.SaveChanges();
        }


    }
}
