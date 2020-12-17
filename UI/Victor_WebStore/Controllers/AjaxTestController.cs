using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.ViewModel;

namespace Victor_WebStore.Controllers
{
    public class AjaxTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetJson(int? id, string msg, int delay = 1000)
        {
            await Task.Delay(delay);
            return Json(new
            {
                Message = $"Respown (id:{id ?? -1}): {msg ?? "<null>"}",
                ServerTime = DateTime.Now,
            });
        }

        public async Task<IActionResult> GetHtml(int? id, string msg, int delay = 2000)
        {
            await Task.Delay(delay);
            return PartialView("Partial/_DataView", new AjaxTestDataViewModel(id ?? -1, msg,DateTime.Now));
        }
    }
}
