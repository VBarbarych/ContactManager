using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext _context;
        protected DbSet<Employee> DbSet;
        private IHostingEnvironment Environment;

        public EmployeeService(EmployeeContext context, IHostingEnvironment _environment)
        {
            _context = context;
            DbSet = context.Set<Employee>();
            Environment = _environment;
        }

        public async Task DeleteItemAsync(Employee entity)
        {
            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditItemAsync(Employee entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }



        public string GetDataFromFile(IFormFile postedFile)
        {
            string csvData = null;

            if (postedFile != null)
            {
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                csvData = System.IO.File.ReadAllText(filePath);
            }

            return csvData;
        }

        public List<Employee> GetAllEmployees(IFormFile postedFile)
        {
            if(postedFile != null)
            {
                string csvData = GetDataFromFile(postedFile);
                List<Employee> employees = new List<Employee>();

                bool firstRow = true;
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {

                        if(firstRow)
                        {
                            firstRow = false;
                        }
                        else
                        {
                            var rowData = row.Trim().Split(',');
                            var employee = new Employee
                            {
                                //Id = i,
                                Name = rowData[0],
                                DateOfBirth = Convert.ToDateTime(rowData[1]),
                                IsMarried = Convert.ToBoolean(rowData[2]),
                                Phone = rowData[3],
                                Salary = Convert.ToDecimal(rowData[4])
                            };

                            employees.Add(employee);

                            DbSet.Add(employee);
                            _context.SaveChanges();
                        }
                    }
                }

                return employees;
            }

            return new List<Employee>();
        }

        public async Task<Employee> GetItemByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await DbSet.FindAsync(id);
        }
    }
}