using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto;
using Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Demo.Presentation.Controllers
{
    public class EmployeeController(IEmployeeServices _employeeServices,IWebHostEnvironment environment,ILogger<Employee> logger) : Controller
    {
        public IActionResult Index()
        {
            var Employees=_employeeServices.GetAllEmployees();
            return View(Employees);
        }
        #region create
        public IActionResult Create() =>View();
        [HttpPost]
        public IActionResult Create(CreatedEmplopyeeDto createdEmplopyeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Result = _employeeServices.CreateEmployee(createdEmplopyeeDto);
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
            return View(createdEmplopyeeDto);
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
            var employeedto = new UpdateEmployeeDto()
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
                IsActive=employee.IsActive
            };
            return View(employeedto);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, UpdateEmployeeDto updateEmployee) 
        {
            if (!id.HasValue|| id!=updateEmployee.Id) return BadRequest();
            if(!ModelState.IsValid) return  View(updateEmployee);
            try 
            {
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
                    return View(updateEmployee);
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
