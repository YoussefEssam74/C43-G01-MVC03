using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Security.Cryptography;
using Demo.DataAccess.Models.Shared.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Demo.Presentation.ViewModels;
using Demo.BusinessLogic.Services;
using Demo.BusinessLogic.Services.Classes;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService,
        ILogger<EmployeesController> _logger,
        IWebHostEnvironment _environment ) : Controller

    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        }
        #region Create Employee
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeViewModel.Name,
                        Salary = employeeViewModel.Salary,
                        Address = employeeViewModel.Address,
                        Age = employeeViewModel.Age,
                        Email = employeeViewModel.Email,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        IsActive = employeeViewModel.IsActive,
                        HiringDate = employeeViewModel.HiringDate,
                        Gender = employeeViewModel.Gender,
                        EmployeeType = employeeViewModel.EmployeeType,
                        DepartmentId= employeeViewModel.DepartmentId,
                    };
                    int Result = _employeeService.CreateEmployee(employeeDto);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index)); // XXXXXXXX 
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't Be Created");
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
            return View(employeeViewModel);
        }




        #endregion
        #region Details Of Employee
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeebyId(id.Value);
            return employee is null ? NotFound() : View(employee);
        }

        #endregion
        #region Edit Employee
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeebyId(id.Value);
            if (employee is null) return NotFound();
            var employeeVieModel = new EmployeeViewModel()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId=employee.DepartmentId,
            };
            return View(employeeVieModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeVieModel)
        {
            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return View(employeeVieModel);
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id= id.Value,
                    Name = employeeVieModel.Name,
                    Salary = employeeVieModel.Salary,
                    Address = employeeVieModel.Address,
                    Age = employeeVieModel.Age,
                    Email = employeeVieModel.Email,
                    PhoneNumber = employeeVieModel.PhoneNumber,
                    IsActive = employeeVieModel.IsActive,
                    HiringDate = employeeVieModel.HiringDate,
                    Gender = employeeVieModel.Gender,
                    EmployeeType = employeeVieModel.EmployeeType,
                    DepartmentId = employeeVieModel.DepartmentId,
                };
                var Result = _employeeService.UpdateEmployee(employeeDto);
                if (Result > 0)
                {
                    return RedirectToAction(actionName: nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is not Updated");
                    return View(employeeVieModel);
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    // 1. Development => Log Error In Console and Return Same View With Error Message 
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeVieModel);
                }
                else
                {
                    // 2. Deployment => Log Error In File | Table in Database And Return Error View 
                    _logger.LogError(ex.Message);
                    return View("Error View" , ex);
                }
            }
        }
        #endregion

        #region Delete Employee

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _employeeService.DeleteEmployee(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Deleted");
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


