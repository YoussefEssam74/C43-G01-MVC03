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
        private IEmployeeRepository _employeeRepository;
        private IDepartmentRepository _departmentRepository;
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,ApplicationDbContext dbContext)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            this._dbContext = dbContext;
        }
        public IEmployeeRepository EmployeeRepository =>  _employeeRepository;

        public IDepartmentRepository DepartmentRepository =>   _departmentRepository;

        public int SaveChanges()=>   _dbContext.SaveChanges();

    }
}
