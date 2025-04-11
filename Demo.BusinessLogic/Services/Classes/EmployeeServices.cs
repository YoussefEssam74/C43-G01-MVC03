using AutoMapper;
using Azure;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeServices(IUintOfWork _uintOfWork, IMapper _mapper ):IEmployeeService
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
                employees = _uintOfWork.EmployeeRepository.GetAll();
            else
                employees = _uintOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
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
            var employee = _uintOfWork.EmployeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(employee);
            

        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
             _uintOfWork.EmployeeRepository.Add(employee); //add locally
                                                                // insert
                                                                // update 
                                                                // delete
            return _uintOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _uintOfWork.EmployeeRepository.GetById(id);
            if(employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                _uintOfWork.EmployeeRepository.Update(employee);
              return  _uintOfWork.SaveChanges() > 0 ? true : false;

            }
        }

      

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            _uintOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
            return _uintOfWork.SaveChanges();
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking = false)
        {
            throw new NotImplementedException();
        }
    }
}
