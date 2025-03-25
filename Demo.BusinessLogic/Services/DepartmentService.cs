using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Repositories;

namespace Demo.BusinessLogic.Services
{
     class DepartmentService
    {
        public DepartmentService(IDepartmentRepository departmentRepository) //1. INJECTION
        {

        }
    }
}
