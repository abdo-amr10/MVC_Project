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
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id);
        Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto);
        Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto departmentDto);
        Task<bool> DeleteDepartmentAsync(int id);


    }
}
