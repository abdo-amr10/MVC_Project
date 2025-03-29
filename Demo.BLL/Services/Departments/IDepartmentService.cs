using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOs.DepartmentDTOs;
using Demo.BLL.DTOs.EmployeeDTOs;
using Demo.DAL.Entities.Departments;

namespace Demo.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDto> GetAllDepartment();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int CreateDepartment(CreatedDepartmentDto departmentDto);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);


    }
}
