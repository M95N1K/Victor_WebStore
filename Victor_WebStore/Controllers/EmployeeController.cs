﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Victor_WebStore.ViewModels;

namespace Victor_WebStore.Controllers
{
    public class EmployeeController : Controller
    {
        private List<EmployeeViewModel> _employees = new List<EmployeeViewModel>()
        {
            new EmployeeViewModel()
            {
                Id = 1,
                Age = 25,
                Name = "Иван"
            },
            new EmployeeViewModel()
            {
                Id = 2,
                Age = 30,
                Name = "Алексей"
            },
            new EmployeeViewModel()
            {
                Id = 3,
                Age = 30,
                Name = "Василий"
            }
        };

        public IActionResult Employee()
        {
            return View(_employees);
        }

        public IActionResult EmployeeDetails(int id)
        {
            var employeeVM = _employees.FirstOrDefault(x => x.Id == id);

            if (employeeVM == null)
                return NotFound();

            return View(employeeVM);
        }
    }
}
