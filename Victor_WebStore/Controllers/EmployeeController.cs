using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Victor_WebStore.Infrastructure.Interfaces;
using Victor_WebStore.ViewModels;

namespace Victor_WebStore.Controllers
{
    [Route("user")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeesService _employeesService;

        public EmployeeController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [Route("all")]
        public IActionResult Employee()
        {
            return View(_employeesService.GetAll());
        }

        [Route("{id?}")]
        public IActionResult EmployeeDetails(int id)
        {
            var employeeVM = _employeesService.GetById(id);

            if (employeeVM == null)
                return RedirectToAction("NotFound404", "Home");

            return View(employeeVM);
        }

        [HttpGet]
        [Route("edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new EmployeeViewModel());

            var model = _employeesService.GetById(id.Value);
            if (model == null)
                return RedirectToAction("NotFound404", "Home");// возвращаем результат 404 Not Found

            return View(model);
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public IActionResult Edit(EmployeeViewModel persona)
        {
            if(persona.Id > 0)
            {
                var item = _employeesService.GetById(persona.Id);
                if (ReferenceEquals(item, null))
                    return RedirectToAction("NotFound404", "Home");

                item.Name = persona.Name;
                item.Age = persona.Age;
            }
            else
            {
                _employeesService.AddNew(persona);
            }

            _employeesService.Commit();

            return RedirectToAction(nameof(Employee));
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int Id)
        {
            _employeesService.Delete(Id);
            return RedirectToAction(nameof(Employee));
        }
    }
}
