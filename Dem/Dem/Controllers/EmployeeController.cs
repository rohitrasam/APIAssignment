using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeAPIAssignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;

        }

        [HttpGet]
        public List<Employee> GetEmployees()
        {
            return employeeService.GetEmployees();
            
        }

        [HttpPost]
        public Employee AddEmployee(Employee employee)
        {
            return employeeService.AddEmployee(employee);
        }

        [HttpGet("{id}")]
        public Employee GetEmployeeById(int id)
        {
            return employeeService.GetEmployeeById(id);
        }

        [HttpPut]
        public Employee UpdateEmployee(Employee employee)
        {
            return employeeService.UpdateEmployee(employee);
        }


        [HttpDelete("{id}")]
        public Employee DeleteEmployee(int id)
        {
            return employeeService.DeleteEmployee(id);
        }
    }
}
