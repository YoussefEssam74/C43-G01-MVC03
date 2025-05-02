using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IUintOfWork _uintOfWork) : IDepartmentService
    {

        // get all department
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _uintOfWork.DepartmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDto());
            //var departmentsToReturn = departments.Select(D => new DepartmentDto()
            //{
            //    DeptId = D.Id,
            //    Code = D.Code,
            //    Description = D.Description,
            //    Name = D.Name,
            //    DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            //});
            //return departmentsToReturn;
        }

        // get department by id

        public DepartmentDetailsDto? GetDepartmentById(int id)

        {
            var department = _uintOfWork.DepartmentRepository.GetById(id);
            //if (department is null) return null;
            //else
            //{
            //    var departmentToReturn = new DepartmentDetailsDto()
            //    {


            //        Id = department.Id,
            //        Name = department.Name,
            //        CreatedOn = DateOnly.FromDateTime(department.CreatedOn)
            //    };
            //    return departmentToReturn;
            //}

            // manual mapping
            //return department is null ? null : new DepartmentDetailsDto()
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn)
            //};
            // return department is null ? null : new DepartmentDetailsDto(department); // constructor mapping
            return department is null ? null : department.ToDepartmentDetailsDto(); //extention method

        }

        // create new department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            // _departmentRepository.Add(departmentDto);
            var department = departmentDto.ToEntity();
              _uintOfWork.DepartmentRepository.Add(department);
            return _uintOfWork.SaveChanges();
        }

        // update department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto) // num of rows
        {
              _uintOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _uintOfWork.SaveChanges();

        }
        // delete department
        public bool DeleteDepartment(int Id)
        {
            var department =  _uintOfWork.DepartmentRepository.GetById(Id);
            if (department is null) return false;
            else
            {
                    _uintOfWork.DepartmentRepository.Remove(department);
                int Result = _uintOfWork.SaveChanges();

                return Result > 0 ? true : false;
            }
        }
    }
}


