using Demo.BLL.DTOs;
using Demo.BLL.DTOs.EmployeeDTOs;
using Demo.BLL.Services.Employees;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Enums;
using Demp.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demp.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService,
            ILogger<EmployeeController> logger,
            IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _environment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Employee = _employeeService.GetAllEmployee();
            return View(Employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var message = string.Empty;


            try
            {

                var result = _employeeService.CreateEmployee(employee);

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
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var Employee = _employeeService.GetEmployeeById(id.Value);

            if (Employee == null)
                return NotFound();

            return View(Employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee == null)
                return NotFound();
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
        public IActionResult Edit([FromRoute] int id , UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {
                

                var Updated = _employeeService.UpdateEmployee(employee) > 0;

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
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var Employee = _employeeService.GetEmployeeById(id.Value);

            if (Employee == null)
                return NotFound();
            ViewBag.Action = "Delete";


            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _employeeService.DeleteEmployee(id);

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
