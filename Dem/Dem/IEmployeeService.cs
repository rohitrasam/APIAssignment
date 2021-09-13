using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPIAssignment2
{
    public interface IEmployeeService
    {

        public List<Employee> GetEmployees();
        public Employee AddEmployee(Employee employee);
        public Employee GetEmployeeById(int id);
        public Employee UpdateEmployee(Employee employee);
        public Employee DeleteEmployee(int id);
    }
}
