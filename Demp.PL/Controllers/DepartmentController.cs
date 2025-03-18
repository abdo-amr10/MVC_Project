using Demo.BLL.DTOs;
using Demo.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Demp.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _loggeer;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService,
            ILogger<DepartmentController> loggeer,
            IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _loggeer = loggeer;
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

                _loggeer.LogError(ex, ex.Message);

                if (_environment.IsDevelopment())
                {
                    message=ex.Message;
                    return View(department);    

                }
                else
                {
                    message = "Department Is Not Created";
                    return View(department);    
                }
            }
        }
        
    }
}
