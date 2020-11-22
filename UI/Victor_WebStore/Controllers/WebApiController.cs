using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Interfaces.TestApi;

namespace Victor_WebStore.Controllers
{
    public class WebApiController : Controller
    {
        private readonly IValueService _valueService;

        //[BindProperty]
        //public string Value { get; set; }

        public WebApiController(IValueService valueService)
        {
            _valueService = valueService;
        }

        public IActionResult Index()
        {
            var values = _valueService.Get();
            return View(values);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string value)
        {
            _valueService.Post(value);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id < 0)
                return RedirectToAction("NotFound404", "Home");// возвращаем результат 404 Not Found
            var values = _valueService.Get().ToList();
            if (id >= values.Count)
                return RedirectToAction("NotFound404", "Home");// возвращаем результат 404 Not Found
            string value = values[id];
            return View("Edit",value);
        }

        [HttpPost]
        public IActionResult Edit(int id, string value)
        {
            _valueService.Update(id, value);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _valueService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
