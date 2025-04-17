using AutoMapper;
using Demo.BLL.DTOs;
using Demo.BLL.DTOs.EmployeeDTOs;
using Demo.BLL.Services.Departments;
using Demo.BLL.Services.Employees;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Enums;
using Demp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demp.PL.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService,
            ILogger<EmployeeController> logger,
            IMapper mapper,
            IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _mapper = mapper;
            _environment = environment;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            ViewData["Search"] = search;
            var Employee = await _employeeService.GetEmployeesAsync( search);
            return View(Employee);
        }
        [HttpGet]
        public IActionResult Create(/*[FromServices] IDepartmentService departmentService*/)
        {
            ViewBag.Action = "Create";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var message = string.Empty;


            try
            {

                var result = await _employeeService.CreateEmployeeAsync(employee);

                if (result > 0)
                {
                    TempData["Message"] = "Department is Created Successfully :) !";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Employee Is Not Created!";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);
                    return View(employee);

                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "Employee Is Not Created.";

            }
            ModelState.AddModelError(string.Empty, message);

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var Employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

            if (Employee == null)
                return NotFound();

            return View(Employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Action = "Edit";


            return View( new CreatedEmployeeDto()
            {
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Email = employee.Email,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id , UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {
                

                var Updated = await _employeeService.UpdateEmployeeAsync(employee) > 0;

                if (Updated)
                {
                    TempData["Message"] = "Department is Updated Successfully :) !";
                    return RedirectToAction(nameof(Index));
                }

                message = "An error occured during updating Employee";
                TempData["Message"] = message;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message: "An Error Occurred During Updating Employee.";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var Employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

            if (Employee == null)
                return NotFound();
            ViewBag.Action = "Delete";


            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _employeeService.DeleteEmployeeAsync(id);

                if (deleted)
                {
                    TempData["Message"] = "Department is Deleted Successfully!";

                    return RedirectToAction(nameof(Index));
                }

                message = "An Error Occurred During Deleting This Employee :(";
                TempData["Message"] = message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An Error Occurred During Deleting This Employee :(";

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
