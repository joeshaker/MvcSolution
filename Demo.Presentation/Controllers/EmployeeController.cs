using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto;
using Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto;
using Demo.BusinessLogic.Services.AttatchmentServices;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentation.ViewModels;
using Demo.Presentation.Views.EmployeeViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Demo.Presentation.Controllers
{
    public class EmployeeController(IEmployeeServices _employeeServices,
        IWebHostEnvironment environment,
        ILogger<Employee> logger,
        IAttatchmentService _attatchmentService
        ) : Controller
    {
        public IActionResult Index(string ?EmployeeSearchName)
        {
            var Employees=_employeeServices.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }
        #region create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeViewModel();
            return View(model);
            //ViewBag.Departments=departmentService.GetAllDepartments();
            //ViewData["Departments"]=departmentService.GetAllDepartments();
            //return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createdEmplopyeeDto=new CreatedEmplopyeeDto() 
                    {
                        Name = employeeView.Name,
                        Age = employeeView.Age,
                        Address = employeeView.Address,
                        Salary = employeeView.Salary,
                        IsActive = employeeView.IsActive,
                        Email = employeeView.Email,
                        PhoneNumber = employeeView.PhoneNumber,
                        Gender = employeeView.Gender,
                        EmployeeType = employeeView.EmployeeType,
                        HiringDate = employeeView.HiringDate,
                        DepartmentId = employeeView.DepartmentId,
                        Image= employeeView.Image,
                    };
                    int Result = _employeeServices.CreateEmployee(createdEmplopyeeDto);
                    if (Result > 0)
                    {
                        //return RedirectToAction("Index");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee not created");
                    }
                }
                catch (Exception ex)
                {
                    if (environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        logger.LogError(ex.Message);
                    }

                }

            }
            return View(employeeView);
        }
        #endregion
        #region Details of Emp
        public IActionResult Details(int ? id) {
            if (!id.HasValue) return BadRequest();
            var employees = _employeeServices.GetEmployeeById(id.Value);
            return employees is null?NotFound() : View(employees);
        }
        #endregion
        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var employee = _employeeServices.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();

            var employeeDto = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name ?? string.Empty, 
                HiringDate = employee.HiringDate,
                Email = employee.Email ?? string.Empty,
                Gender = !string.IsNullOrEmpty(employee.Gender)
                       ? Enum.Parse<Gender>(employee.Gender)
                       : default, 
                EmployeeType = !string.IsNullOrEmpty(employee.EmployeeType)
                              ? Enum.Parse<EmployeeType>(employee.EmployeeType)
                              : default, 
                PhoneNumber = employee.PhoneNumber ?? string.Empty,
                Salary = employee.Salary,
                Address = employee.Address ?? string.Empty,
                Age = employee.Age ?? default, 
                IsActive = employee.IsActive,
                DepartmentId = employee.DepartmentId ?? default, 
                photonName = employee.Image ?? string.Empty 
            };

            return View(employeeDto);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeView)
        {
            if (!id.HasValue || id != employeeView.Id) return BadRequest();
            if (!ModelState.IsValid) return View(employeeView);

            try
            {
                string? imageName = employeeView.photonName; 

                if (employeeView.Image != null)
                {
                    if (!string.IsNullOrEmpty(employeeView.photonName))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", "images", employeeView.photonName);
                        _attatchmentService.Delete(oldImagePath);
                    }

                    imageName = _attatchmentService.Upload(employeeView.Image, "images");
                    if (imageName == null)
                    {
                        ModelState.AddModelError("Image", "Invalid image file");
                        return View(employeeView);
                    }
                }

                var updateEmployee = new UpdateEmployeeDto()
                {
                    Id = employeeView.Id,
                    Name = employeeView.Name,
                    HiringDate = employeeView.HiringDate,
                    Email = employeeView.Email,
                    Gender = employeeView.Gender,
                    EmployeeType = employeeView.EmployeeType,
                    PhoneNumber = employeeView.PhoneNumber,
                    Salary = employeeView.Salary,
                    Address = employeeView.Address,
                    Age = employeeView.Age,
                    IsActive = employeeView.IsActive,
                    DepartmentId = employeeView.DepartmentId,
                    Image = imageName 
                };

                var result = _employeeServices.UpdateEmployee(updateEmployee);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Can't be Updated");
                    return View(employeeView);
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeView);
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion
        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            try
            {
                bool Deleted = _employeeServices.DeleteEmployee(id);
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
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex.Message);
                }
            }

        }
        #endregion
    }
}
