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
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Sp_Employee_Add", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Age", employee.Age);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.Add("@Emp_id", SqlDbType.Int).Direction = ParameterDirection.Output;
            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Dispose();
            employee.Id = (int)cmd.Parameters["@Emp_id"].Value;

            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = GetEmployeeById(id);
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Sp_employee_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Emp_id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Dispose();

            return employee;
        }

        public Employee GetEmployeeById(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Sp_Employee_id", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Emp_id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Employee employee = new Employee
            {
                Id = (int)dt.Rows[0]["Id"],
                Name = dt.Rows[0]["Name"].ToString(),
                Age = (int)dt.Rows[0]["Age"],
                Salary = (decimal)dt.Rows[0]["Salary"]
            };

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
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Name");
                dt.Columns.Add("Age");
                dt.Columns.Add("Salary");

                while (dr.Read())
                {
                    DataRow dataRow = dt.NewRow();
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
            
            
           

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    Employee employee = new Employee
            //    {
            //        Id = (int)dt.Rows[i]["Id"],
            //        Name = dt.Rows[i]["Name"].ToString(),
            //        Age = (int)dt.Rows[i]["Age"],
            //        Salary = (decimal)dt.Rows[i]["Salary"]
            //    };
            //    employees.Add(employee);
            //}
            return employees;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Sp_employee_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Emp_id", employee.Id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Age", employee.Age);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Dispose();

            return employee;
        }
    }
}
