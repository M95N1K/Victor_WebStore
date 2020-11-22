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

        public WebApiController(IValueService valueService)
        {
            _valueService = valueService;
        }

        public IActionResult Index()
        {
            var values = _valueService.Get();
            return View(values);
        }
    }
}
