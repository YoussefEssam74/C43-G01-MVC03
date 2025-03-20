using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;

namespace Demo.DataAccess.Repositories
{
     class DepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext) // 1. injection
        {
            this._dbContext = dbContext;
        }
        //curd operation
        //get all
        //get by id
        public Department? GetById(int id)
        {
            var deparment = _dbContext.Departments.Find(id);
            return deparment;

        }
    }
}
