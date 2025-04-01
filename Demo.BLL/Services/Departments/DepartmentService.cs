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
            var departments = _departmentRepository
                              .GetIQueryable()
                              .Where(d=>!d.IsDeleted)
                              .Select(x => new DepartmentDto
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
            var department = _departmentRepository.Get(departmentDto.Id);

            if (department == null)
                return 0; 

            department.Name = departmentDto.Name;
            department.Code = departmentDto.Code;
            department.Description = departmentDto.Description ?? department.Description; 
            department.CreationDate = departmentDto.CreationDate;
            department.LastModifiedBy = 1;
            department.LastModifiedOn = DateTime.Now;

            return _departmentRepository.Update(department);
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
