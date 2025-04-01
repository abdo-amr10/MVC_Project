using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOs.EmployeeDTOs;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                DepartmentId = employeeDto.DepartmentId,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1, 
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,

            };
            return _employeeRepository.Add(employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.Get(id);

            if (employee is { })
                return _employeeRepository.Delete(employee) > 0;
            return false;
        }

        public IEnumerable<EmployeeDto> GetAllEmployee()
        {
            var employee = _employeeRepository.GetIQueryable()
                                              .Where(e=>!e.IsDeleted)
                                              .Include(d=>d.Department)
                                              .Select(employee => new EmployeeDto()
                                              {
                                                  Id = employee.Id,
                                                  Name = employee.Name,
                                                  Age = employee.Age,
                                                  Address = employee.Address,
                                                  IsActive = employee.IsActive,
                                                  Salary = employee.Salary,
                                                  Department = employee.Department.Name,
                                                  Email = employee.Email,
                                                  Gender = employee.Gender.ToString(),
                                                  EmployeeType = employee.EmployeeType.ToString(),
                                              });
            return employee;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetIQueryable()
                                     .Include(e => e.Department) 
                                     .FirstOrDefault(e => e.Id == id);
            if (employee is { })
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    Department = employee.Department?.Name??"",
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    CreatedOn = employee.CreatedOn,
                    EmployeeType = employee.EmployeeType,

                };
            return null;
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };
            return _employeeRepository.Update(employee);
        }
    }
}
