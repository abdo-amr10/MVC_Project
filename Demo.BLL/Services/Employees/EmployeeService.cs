using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOs.EmployeeDTOs;
using Demo.DAL.Entities.Departments;
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
                CreatedOn = DateTime.Now,
                CreatedBy = 1, 
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,

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

        public IEnumerable<EmployeeDto> GetEmployees(string search)
        {
            var employee = _employeeRepository.GetIQueryable()
                                              .Where(e=>!e.IsDeleted && (string.IsNullOrEmpty(search) || e.Name.ToLower().Contains(search.ToLower())))
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
                                              }).ToList();
            return employee;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.Get(id);

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
                    Department = employee.Department?.Name ?? "",
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
            var employee = _employeeRepository.Get(employeeDto.Id);

            if (employee == null)
                return 0; 

            employee.Name = employeeDto.Name;
            employee.Age = employeeDto.Age;
            employee.Address = employeeDto.Address;
            employee.IsActive = employeeDto.IsActive;
            employee.Salary = employeeDto.Salary;
            employee.Email = employeeDto.Email;
            employee.PhoneNumber = employeeDto.PhoneNumber;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.HiringDate = employeeDto.HiringDate;
            employee.Gender = employeeDto.Gender;
            employee.EmployeeType = employeeDto.EmployeeType;
            employee.LastModifiedBy = 1;
            employee.CreatedBy = 1;
            employee.LastModifiedOn = DateTime.Now;

            return _employeeRepository.Update(employee);
        }

    }
}
