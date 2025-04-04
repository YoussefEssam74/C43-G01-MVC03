using Demo.BusinessLogic.Services;
using Demo.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService departmentService) : Controller
    {
        // BaseURL/Department/Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentService.GetAllDepartments();
            return View(Departments);
        }
    }
}
