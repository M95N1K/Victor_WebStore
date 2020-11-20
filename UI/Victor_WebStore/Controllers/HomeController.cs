using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Victor_WebStore.Domain.ViewModels;

namespace Victor_WebStore.Controllers
{
    public class HomeController : Controller
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotFound404()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }

        //public IActionResult Cart()
        //{
        //    return View();
        //}

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        //public IActionResult Login()
        //{
        //    return View();
        //}




    }
}
