using AutoMapper;
using Demo.BLL.DTOs;
using Demo.BLL.DTOs.DepartmentDTOs;
using Demo.BLL.Services.Departments;
using Demo.DAL.Entities.Departments;
using Demp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demp.PL.Controllers
{
    [Authorize]

    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService,
            ILogger<DepartmentController> logger,
            IMapper mapper,
            IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _mapper = mapper;
            _environment = environment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var department = await _departmentService.GetAllDepartmentsAsync();
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var message = string.Empty;


            try
            {

                //var createdDepartment = new CreatedDepartmentDto()
                //{
                //    Name = departmentVM.Name,
                //    Code = departmentVM.Code,
                //    CreationDate = departmentVM.CreationDate,
                //    Description = departmentVM.Description,
                //};
                var createdDepartment = _mapper.Map<CreatedDepartmentDto>(departmentVM);
                var created = await _departmentService.CreateDepartmentAsync(createdDepartment) >0;

                if (created)
                {
                    TempData["Message"] = "Department is Created Successfully :) !";
                    
                }
                else
                {
                    message = "Department Is Not Created";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);
                    //return View(departmentVM);

                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "Department Is Not Created.";

            }
            ModelState.AddModelError(string.Empty, message);

            return View(departmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department == null)
                return NotFound();

            return View(department);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department == null)
                return NotFound();
            ViewBag.Action = "Edit";

            var departmentVM = _mapper.Map<DepartmentDetailsDto, DepartmentViewModel>(department);

            //return View( new DepartmentViewModel()
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    Description = department.Description,
            //    CreationDate = department.CreationDate,
            //});
            return View(departmentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id , DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var message = string.Empty;

            try
            {
                //var departmentToUpdate = new UpdatedDepartmentDto()
                //{
                //    Id = id,
                //    Code = departmentVM.Code,
                //    Name = departmentVM.Name,
                //    Description = departmentVM.Description,
                //    CreationDate = departmentVM.CreationDate,
                //};
                var departmentToUpdate = _mapper.Map<DepartmentViewModel, UpdatedDepartmentDto>(departmentVM);
                departmentToUpdate.Id = id;
                var Updated = await _departmentService.UpdateDepartmentAsync(departmentToUpdate) > 0;

                if (Updated)
                {
                    TempData["Message"] = "Department is Updated Successfully :) !";
                    return RedirectToAction(nameof(Index));
                }
                message = "An Error Occurred During Updating Department";
                TempData["Message"] = message;
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department == null)
                return NotFound();
            ViewBag.Action = "Delete";


            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _departmentService.DeleteDepartmentAsync(id);

                if (deleted)
                {
                    TempData["Message"] = "Department is Deleted Successfully !";
                    return RedirectToAction(nameof(Index));
                }
                message = "An Error Occurred During Deleting This Department :(";
                TempData["Message"] = message;
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
