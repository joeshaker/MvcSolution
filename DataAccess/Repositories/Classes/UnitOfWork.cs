using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data.Contexts;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEmployeeRepository _employeeRepository;
        private IDepartmentRepository _departmentRepository;
        private readonly AppDbContexts _dbContext;
        public UnitOfWork(IEmployeeRepository employeeRepository, 
            IDepartmentRepository departmentRepository,AppDbContexts appContext)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            this._dbContext = appContext;
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository;

        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public int SaveChanges() => _dbContext.SaveChanges();
    }
}
