using EmployeeAPI.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;

        }
        [HttpGet]
        public async Task<List<Employee>> Get()
        {
            return await employeeService.GetAllEmployees();
        }

        [HttpPost]
        public async Task<Employee> AddEmployee(Employee employee)
        {
            return await employeeService.AddEmployee(employee);
        }

        [HttpGet("{id}")]
        public async Task<Employee> GetEmployeeById(int id)
        {
            return await employeeService.GetEmployeeById(id);
        }

        [HttpPut]
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            return await employeeService.UpdateEmployee(employee);
        }


        [HttpDelete("{id}")]
        public async Task<Employee> DeleteEmployee(int id)
        {
            return await employeeService.DeleteEmployee(id);
        }
    }
}
