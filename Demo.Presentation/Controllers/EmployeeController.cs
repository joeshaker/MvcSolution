﻿using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeeController(IEmployeeServices _employeeServices) : Controller
    {
        public IActionResult Index()
        {
            var Employees=_employeeServices.GetAllEmployees();
            return View(Employees);
        }
    }
}
