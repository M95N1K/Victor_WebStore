using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Victor_WebStore.Clients.Base;
using Victor_WebStore.Domain;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.Clients.Employees
{
    public class EmployeesClient: BaseClient, IEmployeesService
    {
        public EmployeesClient(IConfiguration Configuration) : base(Configuration, WebApiAdress.Employees) { }

        public void AddNew(EmployeeViewModel persona)
        {
            if (persona is null)
            {
                throw new ArgumentNullException(nameof(persona));
            }
            Post(_ServiceAddress, persona);
        }

        public void Commit(){}

        public void Delete(int id) => Delete($"{_ServiceAddress}/{id}");

        public void Edit(EmployeeViewModel persona)
        {
            if (persona is null)
            {
                throw new ArgumentNullException(nameof(persona));
            }

            Put<EmployeeViewModel>(_ServiceAddress, persona);
        }

        public IEnumerable<EmployeeViewModel> GetAll() => Get<IEnumerable<EmployeeViewModel>>(_ServiceAddress);

        public EmployeeViewModel GetById(int id) => Get<EmployeeViewModel>($"{_ServiceAddress}/{id}");
    }
}
