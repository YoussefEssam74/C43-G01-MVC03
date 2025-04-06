using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.DepartmentModel;

namespace Demo.DataAccess.Repositories
{
    public class DepartmentRepository(ApplicationDbContext dbContext) : IDepartmentRepository
    //public 34an hst5demo bra el layer
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        //curd operation
        //get all
        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (WithTracking)

                return _dbContext.Departments.ToList();
            else
                return _dbContext.Departments.AsNoTracking().ToList();

        }
        //get by id
        public Department? GetById(int id) => _dbContext.Departments.Find(id);

        // update
        public int Update(Department department)
        {
            _dbContext.Departments.Update(department); //updated locally
            return _dbContext.SaveChanges();
        }

        // delete
        public int Remove(Department department)
        {
            _dbContext.Departments.Remove(department); //updated locally

            return _dbContext.SaveChanges();


        }

        // insert
        public int Add(Department department)
        {
            _dbContext.Departments.Add(department); //updated locally

            return _dbContext.SaveChanges();


        }





        //public Department? GetById(int id)
        //{
        //    var deparment = _dbContext.Departments.Find(id);
        //    return deparment;

        //}
    }
}
