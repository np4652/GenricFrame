using GenricFrame.AppCode.CustomAttributes;
using GenricFrame.AppCode.Data.Repository;
using GenricFrame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GenricFrame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDemo _demo;

        public HomeController(ILogger<HomeController> logger, IDemo demo)
        {
            _logger = logger;
            _demo = demo;
        }

        public async Task<IActionResult> Index()
        {
            var emp = await _demo.GetEmployeeAsync();
            return View(new DemoViewModel {AppicationUser =  emp });
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

        [ValidateAjax]
        public IActionResult DemoModalValidation(DemoViewModel demo)
        {
            return Json("");
        }
    }
}
