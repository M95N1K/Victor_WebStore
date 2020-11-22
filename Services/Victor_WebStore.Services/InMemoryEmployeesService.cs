using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.Services
{
    public class InMemoryEmployeesService : IEmployeesService
    {
        private readonly List<EmployeeViewModel> _employees = new List<EmployeeViewModel>()
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

        public void AddNew(EmployeeViewModel persona)
        {
            if (_employees.Count > 0) //Ищем максимальный Id если список не пуст
                persona.Id = _employees.Max(e => e.Id) + 1;
            else
                persona.Id = 1;
            _employees.Add(persona);
        }

        public void Commit()
        {
            //nope
        }

        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee is null)
                return;

            _employees.Remove(employee);
        }

        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return _employees;
        }

        public EmployeeViewModel GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id.Equals(id));
        }
    }
}
