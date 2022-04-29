using AutoMapper;
using GenricFrame.AppCode.CustomAttributes;
using GenricFrame.AppCode.Interfaces;
using GenricFrame.AppCode.Reops;
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
        private readonly IRepository<DemoViewModel> _demo;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IRepository<DemoViewModel> demo, IMapper mapper)
        {
            _logger = logger;
            _demo = demo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            //var emp = await _demo.GetEmployeeAsync();
            //return View(new DemoViewModel {AppicationUser =  emp });
            return View(new DemoViewModel());
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

        // GET: api/<controller>  
        [HttpGet]
        public Student AutoMapper()
        {
            StudentDTO studentDTO = new StudentDTO()
            {
                Name = "Student 1",
                Age = 25,
                City = "New York"
            };

            return _mapper.Map<Student>(studentDTO);
        }
    }
}
