using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto;
using Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto;
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
        ILogger<Employee> logger
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
            //ViewBag.Departments=departmentService.GetAllDepartments();
            //ViewData["Departments"]=departmentService.GetAllDepartments();
            return View();
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
        public IActionResult Edit(int ?id) {
            if (!id.HasValue) return BadRequest();
            var employee=_employeeServices.GetEmployeeById(id.Value);
            if(employee is null) return NotFound();
            var employeedto = new EmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                HiringDate= employee.HiringDate,
                Email = employee.Email,
                Gender= Enum.Parse<Gender>(employee.Gender),
                EmployeeType=Enum.Parse<EmployeeType>(employee.EmployeeType),
                PhoneNumber= employee.PhoneNumber,
                Salary= employee.Salary,
                Address= employee.Address,
                Age=employee.Age,
                IsActive=employee.IsActive,
                DepartmentId = employee.DepartmentId,
            };
            return View(employeedto);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeView) 
        {
            if (!id.HasValue|| id!= employeeView.Id) return BadRequest();
            if(!ModelState.IsValid) return  View(employeeView);
            try 
            {
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

                };
                var result=_employeeServices.UpdateEmployee(updateEmployee);
                if (result > 0) 
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Can;t be Updated");
                    return View(updateEmployee);


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
                    return View("Error View", ex);
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
