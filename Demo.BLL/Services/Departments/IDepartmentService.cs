using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOs;
using Demo.DAL.Entities.Departments;

namespace Demo.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentToReturnDto> GetAllDepartment();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int CreateDepartment(CreatedDepartmentDto departmentDto);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);


    }
}
