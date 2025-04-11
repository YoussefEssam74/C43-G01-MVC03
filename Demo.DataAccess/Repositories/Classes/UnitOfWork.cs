using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUintOfWork 
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<IEmployeeRepository >_employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
          _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
            this._dbContext = dbContext;
        }
        public IEmployeeRepository EmployeeRepository =>  _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository =>   _departmentRepository.Value;

        public int SaveChanges()=>   _dbContext.SaveChanges();

    }
}
