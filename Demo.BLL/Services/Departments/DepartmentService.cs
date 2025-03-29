using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOs.DepartmentDTOs;
using Demo.BLL.DTOs.EmployeeDTOs;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Repositories.Departments;

namespace Demo.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentDto> GetAllDepartment()
        {
            var departments = _departmentRepository.GetAllAsQueryable().Select(x => new DepartmentDto
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                CreationDate = x.CreationDate
            });
            return departments;
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var departments = _departmentRepository.Get(id);
            if (departments != null)
                return new DepartmentDetailsDto()
                {
                    Id = departments.Id,
                    Name = departments.Name,
                    Code = departments.Code,
                    CreationDate = departments.CreationDate,
                    Description = departments.Description,
                    CreatedBy = departments.CreatedBy,
                    CreatedOn = departments.CreatedOn,
                    LastModifiedBy = departments.LastModifiedBy,
                    LastModifiedOn = departments.LastModifiedOn
                };
            return null;

        }

        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
            return _departmentRepository.Add(department);

        }
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var updatedDepartment = new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
            return _departmentRepository.Update(updatedDepartment);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.Get(id);

            if (department is { })
                return _departmentRepository.Delete(department) > 0;
            return false;
        }
    }
}
