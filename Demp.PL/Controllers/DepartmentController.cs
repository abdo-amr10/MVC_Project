using Demo.BLL.DTOs;
using Demo.BLL.DTOs.DepartmentDTOs;
using Demo.BLL.Services.Departments;
using Demo.DAL.Entities.Departments;
using Demp.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demp.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService,
            ILogger<DepartmentController> logger,
            IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var department = _departmentService.GetAllDepartment();
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var message = string.Empty;


            try
            {
                var result = _departmentService.CreateDepartment(department);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                else
                {
                    message = "Department Is Not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(department);

                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "Department Is Not Created.";

            }
            ModelState.AddModelError(string.Empty, message);

            return View(department);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound();

            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound();

            return View( new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id , DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var message = string.Empty;

            try
            {
                var departmentToUpdate = new UpdatedDepartmentDto()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,
                };

                var Updated = _departmentService.UpdateDepartment(departmentToUpdate) > 0;

                if (Updated)
                    return RedirectToAction(nameof(Index));

                message = "An Error Occurred During Updating Department";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "An Error Occurred During Updating Department.";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _departmentService.DeleteDepartment(id);

                if (deleted)
                    return RedirectToAction(nameof(Index));

                message = "An Error Occurred During Deleting This Department :(";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An Error Occurred During Deleting This Department :(";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
