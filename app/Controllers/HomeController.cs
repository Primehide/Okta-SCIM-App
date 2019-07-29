using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Services;

namespace app.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IApiService apiService;
        public HomeController(IApiService apiService)
        {
            this.apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            var values = await apiService.GetValues();
            return View(values);
        }

        public async Task<IActionResult> User()
        {
            var user = await apiService.GetUser("2819c223-7f76-453a-919d-413861904646");
            return View(user);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
