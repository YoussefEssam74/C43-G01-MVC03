using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;
using Demo.BusinessLogic.Factories;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository = _departmentRepository;

        // get all department
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = departmentRepository.GetAll();
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
            var department = departmentRepository.GetById(id);
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
            return departmentRepository.Add(department);
        }

        // update department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto) // num of rows
        {
            return departmentRepository.Update(departmentDto.ToEntity());
        }
        // delete department
        public bool DeleteDepartment(int Id)
        {
            var department = departmentRepository.GetById(Id);
            if (department is null) return false;
            else
            {
                int Result = departmentRepository.Remove(department);
                return Result > 0 ? true : false;
            }
        }
    }
}


