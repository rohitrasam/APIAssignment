using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPIAssignment2
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;
       
        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        public Employee AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_Employee_Add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Salary", (decimal)employee.Salary);
                cmd.Parameters.Add("@Emp_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                
                cmd.ExecuteNonQuery();
                employee.Id = (int)cmd.Parameters["@Emp_id"].Value;


            }
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = GetEmployeeById(id);
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("Sp_employee_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Emp_id", id);
                cmd.ExecuteNonQuery();

            }

            return employee;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_Employee_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Emp_id", id);
                using SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    employee.Id = (int)dr["Id"];
                    employee.Name = dr["Name"].ToString();
                    employee.Age = (int)dr["Age"];
                    employee.Salary = (decimal)dr["Salary"];
                }
            }

            return employee;
        }

        public List<Employee> GetEmployees()
        {

            List<Employee> employees = new List<Employee>();
            
            using(SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_Employee_All", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Employee employee = new Employee
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        Age = (int)dr["Age"],
                        Salary = (decimal)dr["Salary"]
                    };
                    employees.Add(employee);
                }
            }
            return employees;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_employee_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Emp_id", employee.Id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.ExecuteNonQuery();

            }
            return employee;
        }
    }
}
