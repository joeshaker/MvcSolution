using DataAccess.Models.EmployeeModel;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto;
using Demo.BusinessLogic.DataTransferObjects.EmployeDataTransferDto;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
