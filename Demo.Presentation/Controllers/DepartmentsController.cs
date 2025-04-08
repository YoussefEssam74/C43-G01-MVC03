using AspNetCoreGeneratedDocument;
using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;
using Demo.BusinessLogic.Services;
using Demo.DataAccess.Models;
using Demo.Presentation.ViewModels;
using Demo.Presentation.ViewModels.DepartmentsViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            ViewData[ "Message"] = new DepartmentDto() { Name = "TestViewData" };
            ViewBag.Message = new DepartmentDto() { Name = "TestViewBag" };
            //  ViewData["Message"] = "Hello From View Data";
            //  ViewBag.Message = "Hello From View Bag";
            var Departments = _departmentService.GetAllDepartments();
            return View(Departments);
        }

        #region Create Department
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        Description = departmentViewModel.Description,
                        DateOfCreation = departmentViewModel.CreatedOn
                    };
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
            return View(departmentViewModel);

        }

        #endregion
        #region Details Of Department
        [HttpGet]

        public IActionResult Details(int? id)
        {

            if (!id.HasValue) return BadRequest(); // 400 
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound(); // 404 
            return View( department);
        }

        #endregion
        #region Edit
        [HttpGet]

        public IActionResult Edit(int? id)
        {

            if (!id.HasValue) return BadRequest(); // 400 
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound(); // 404 
            var departmentViewModel = new DepartmentViewModel()
            {
                Name =department.Name,
                Code =department.Code,
                CreatedOn=department.CreatedOn,
                Description =department.Description,
            };
            return View(departmentViewModel);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int id,DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    var UpdatedDpartment = new UpdatedDepartmentDto()
                    {
                        Id = id,
                        Name = viewModel.Name,
                        Code = viewModel.Code,
                        DateOfCreation = viewModel.CreatedOn,
                        Description = viewModel.Description,
                    };
                    int Result = _departmentService.UpdateDepartment(UpdatedDpartment);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department is Not Updated");
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
                        return View("Error View", ex);
                    }
                }

            }
            return View(viewModel);
        }
        #endregion
        #region Delete
        [HttpGet]
        //public IActionResult Delete(int? id)
        //{

        //    if (!id.HasValue) return BadRequest(); // 400 
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound(); // 404 
        //    return View(department);
        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Is Not Deleted");
                    return RedirectToAction(nameof(Delete), new { id });

                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    // 1. Development => Log Error In Console and Return Same View With Error Message 
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    // 2. Deployment => Log Error In File | Table in Database And Return Error View 
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }

            }
        }




            #endregion
        }
}

