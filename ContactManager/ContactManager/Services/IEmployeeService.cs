using ContactManager.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public interface IEmployeeService
    {
        Task EditItemAsync(Employee entity);

        Task DeleteItemAsync(Employee entity);

        List<Employee> GetAllItemsAsync();

        string GetDataFromFile(IFormFile postedFile);

        List<Employee> GetAllEmployees(string csvData);

        Task<Employee> GetItemByIdAsync(int? id);


    }
}
