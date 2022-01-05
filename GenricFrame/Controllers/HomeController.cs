using GenricFrame.AppCode.Migrations;
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
        private readonly IServiceProvider IServiceProvider;

        public HomeController(ILogger<HomeController> logger, IServiceProvider ServiceProvider)
        {
            _logger = logger;
            IServiceProvider = ServiceProvider;
        }

        public IActionResult Index()
        {
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

        [HttpPost,Route(nameof(RunMigration))]
        public IActionResult RunMigration(string DatabaseName)
        {
            var result = MigrationManager.MigrateDatabase(IServiceProvider, DatabaseName);
            return Json(result);
        }
    }
}
