using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.Controllers
{
    [Route("user")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeesService _employeesService;

        public EmployeeController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [Route("all")]
        [AllowAnonymous]
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
        [Authorize(Roles = "Admins")]
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
        [Authorize(Roles = "Admins")]
        public IActionResult Edit(EmployeeViewModel persona)
        {
            if (!ModelState.IsValid)
            {
                return View(persona);
            }

            if (persona.Id > 0)
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
        [Authorize(Roles = "Admins")]
        public IActionResult Delete(int Id)
        {
            _employeesService.Delete(Id);
            return RedirectToAction(nameof(Employee));
        }
    }
}
