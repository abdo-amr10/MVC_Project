using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOs;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Repositories.Departments;

namespace Demo.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRopsitory _departmentRopsitory;

        public DepartmentService(IDepartmentRopsitory departmentRopsitory)
        {
            _departmentRopsitory = departmentRopsitory;
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartment()
        {
            var departments = _departmentRopsitory.GetAllAsQueryable().Select(x => new DepartmentToReturnDto
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                CreationDate = x.CreationDate
            });
            return departments;
        }

        public DepartmentDetailsToReturnDto? GetDepartmentById(int id)
        {
            var departments =_departmentRopsitory.Get(id);
            if (departments != null)
                return new DepartmentDetailsToReturnDto()
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
                LastModifiedBy= 1,
                LastModifiedOn = DateTime.Now,
            };
            return _departmentRopsitory.Add(department);

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
            return _departmentRopsitory.Update(updatedDepartment);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRopsitory.Get(id);

            if (department is { })
                return _departmentRopsitory.Delete(department) > 0;
            return false;
        }
    }
}
