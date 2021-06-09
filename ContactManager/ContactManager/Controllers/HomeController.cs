using ContactManager.Models;
using ContactManager.Services;
using ContactManager.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeService employeeService { get; set; }

        public HomeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(IFormFile postedFile)
        {
            var employees = employeeService.GetAllEmployees(postedFile);

            var emp = new EmployeesViewModel
            {
                employeesViewModel = employees
            };

            return View(emp);
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
