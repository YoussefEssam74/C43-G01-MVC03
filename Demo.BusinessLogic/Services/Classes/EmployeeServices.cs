using AutoMapper;
using Azure;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeServices(IEmployeeRepository _employeeRepository, IMapper _mapper ) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            //  var Employees = _employeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            //src = Employee
            // Dest = EmployeeDto
            // var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(Employees);
            // return employeesDto;



            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace( EmployeeSearchName))
                employees = _employeeRepository.GetAll();
            else
                employees = _employeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
                var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
                return employeesDto;

                //var employeesDto = Employees.Select(Emp => new EmployeeDto()
                //{
                //    Id = Emp.Id,
                //    Name = Emp.Name,
                //    Age = Emp.Age,
                //    Email = Emp.Email,
                //    IsActive = Emp.IsActive,
                //    Salary = Emp.Salary,
                //    EmployeeType = Emp.EmployeeType.ToString(),
                //    Gender = Emp.Gender.ToString()

                //});
                //return employeesDto;
        }

        public EmployeeDetailsDto? GetEmployeebyId(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(employee);
            

        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            return _employeeRepository.Add(employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee =_employeeRepository.GetById(id);
            if(employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee)>0 ? true : false;
            }
        }

      

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            return _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
        }
    }
}
