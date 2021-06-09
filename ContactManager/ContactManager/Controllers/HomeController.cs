using ContactManager.Models;
using ContactManager.Services;
using ContactManager.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var employees = employeeService.GetAllItemsAsync();

            var emp = new EmployeesViewModel
            {
                employeesViewModel = employees
            };

            return View(emp);
        }


        [HttpPost]
        public IActionResult Index(IFormFile postedFile)
        {
            string csvData = employeeService.GetDataFromFile(postedFile);

            var employees = employeeService.GetAllEmployees(csvData);

            var emp = new EmployeesViewModel
            {
                employeesViewModel = employees
            };

            //TempData["mydata"] = csvData;


            //return RedirectToAction(nameof(Index));
            return View(emp);
        }


        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await employeeService.GetItemByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            
            return View(employee);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateOfBirth,IsMarried,Phone,Salary")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    await employeeService.EditItemAsync(employee);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (employeeService.GetItemByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            
            return View(employee);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await employeeService.GetItemByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await employeeService.GetItemByIdAsync(id);
            await employeeService.DeleteItemAsync(employee);
            return RedirectToAction(nameof(Index));
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
