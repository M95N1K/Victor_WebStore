using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Victor_WebStore.Domain;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.ServicesHost.Controllers
{
    [Route(WebApiAdress.Employees)]
    [ApiController]
    public class EmloyeesApiController : ControllerBase, IEmployeesService
    {
        #region Конструктор
        private readonly IEmployeesService _employeesService;

        public EmloyeesApiController(IEmployeesService EmployeesService)
        {
            _employeesService = EmployeesService;
        }
        #endregion
        [HttpPost]
        public void AddNew([FromBody]EmployeeViewModel persona)
        {
            _employeesService.AddNew(persona);
        }

        [NonAction]
        public void Commit()
        {
            _employeesService.Commit();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeesService.Delete(id);
        }
        [HttpPut]
        public void Edit(EmployeeViewModel persona)
        {
            _employeesService.Edit(persona);
        }

        [HttpGet]
        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return _employeesService.GetAll();
        }

        [HttpGet("{id}")]
        public EmployeeViewModel GetById(int id)
        {
            return _employeesService.GetById(id);
        }
    }
}
