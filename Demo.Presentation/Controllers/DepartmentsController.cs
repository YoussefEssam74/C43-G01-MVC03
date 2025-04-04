using AspNetCoreGeneratedDocument;
using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.Services;
using Demo.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentsController(IDepartmentService _departmentService,
        ILogger<DepartmentsController> _logger,
        IWebHostEnvironment _environment) : Controller
    {
        // BaseURL/Department/Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = _departmentService.GetAllDepartments();
            return View(Departments);
        }

        #region Create Department
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Result = _departmentService.AddDepartment(departmentDto);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index)); // XXXXXXXX 
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't Be Created");
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        // 1. Development => Log Error In Console and Return Same View With Error Message 
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        // 2. Deployment => Log Error In File | Table in Database And Return Error View 
                        _logger.LogError(ex.Message);
                    }

                }
            }
            return View(departmentDto);

        }

        #endregion
        #region Details Of Department
        [HttpGet]

        public IActionResult Details(int? id)
        {

            if (!id.HasValue) return BadRequest(); // 400 
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department == null) return NotFound(); // 404 
            return View( department);
        }

        #endregion
    }
}

