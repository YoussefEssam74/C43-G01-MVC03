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

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService,
        ILogger<EmployeesController> _logger,
        IWebHostEnvironment _environment) : Controller

    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        }
        #region Create Employee
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
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
            return View(employeeDto);
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
            var employeeDto = new UpdatedEmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
            };
            return View(employeeDto);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, UpdatedEmployeeDto employeeDto)
        {
            if (!id.HasValue || id != employeeDto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(employeeDto);
            try
            {
                var Result = _employeeService.UpdateEmployee(employeeDto);
                if (Result > 0)
                {
                    return RedirectToAction(actionName: nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is not Updated");
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    // 1. Development => Log Error In Console and Return Same View With Error Message 
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeDto);
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


