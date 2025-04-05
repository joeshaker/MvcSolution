using DataAccess.Models;
using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentation.Views.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,ILogger<DepartmentController> _logger,
        IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message=new DepartmentDto() { Name= "Hello from Index" };
            ViewData["Message"] = new DepartmentDto() { Name = "Hello from Index" };
            var Departments=_departmentService.GetAllDepartments();

            return View(Departments);
        }

        #region Create 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken] //Action Filter
        public IActionResult Create(DepartmentViewEdit createdDepartment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name = createdDepartment.Name,
                        Code = createdDepartment.Code,
                        Description = createdDepartment.Description,
                        DateOfCreation = createdDepartment.DateOfCreation,
                    };
                    int Result = _departmentService.CreateDepartment(departmentDto);
                    if (Result > 0)
                    {
                        //return RedirectToAction("Index");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department not created");
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }

                }

            }
            return View(createdDepartment);

        }
        #endregion
        #region Details
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
            {
                return NotFound();
            }
            return View(department);
        }
        #endregion
        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
            {
                return NotFound();
            }
            var departmentEdit = new DepartmentViewEdit()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn,
            };
            return View(departmentEdit);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int id,DepartmentViewEdit viewmodel) {
            if (ModelState.IsValid) 
            {
                try
                {
                    var UpdatedDepartment = new UpdateDepartmentDto()
                    {
                        Id = id,
                        Name = viewmodel.Name,
                        Code = viewmodel.Code,
                        Description = viewmodel.Description,
                        DateOfCreation = viewmodel.DateOfCreation,
                    };
                    int Result = _departmentService.UpdateDepartment(UpdatedDepartment);
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department not updated");
                    }

                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("ErrorView", ex.Message);
                    }
                }
            }
            return View(viewmodel);

        }
        #endregion
        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if(!id.HasValue) return BadRequest();
        //    var department=_departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public IActionResult Delete(int id) 
        {
            if (id <= 0) return BadRequest();
            try
            {
                bool Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department not deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex.Message);
                }
            }
        }



        #endregion
    }
}
