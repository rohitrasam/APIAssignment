using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using EmployeeAPIAssignment.Models;

namespace EmployeeAPIAssignment.DB_Access_Layer
{
    public class DB
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        public void AddEmployee(Employee emp)
        {
            SqlCommand com = new SqlCommand("Sp_employee_Add", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", emp.Name);
            com.Parameters.AddWithValue("@Age", emp.Age);
            com.Parameters.AddWithValue("@Salary", emp.Salary);

            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateEmployee(Employee emp)
        {
            SqlCommand com = new SqlCommand("Sp_employee_Update", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Emp_id", emp.Id);
            com.Parameters.AddWithValue("@Name", emp.Name);
            com.Parameters.AddWithValue("@Age", emp.Age);
            com.Parameters.AddWithValue("@Salary", emp.Salary);

            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        public DataSet GetEmployeeById(int id)
        {
            SqlCommand com = new SqlCommand("Sp_Employee_id", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Emp_id", id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public void DeleteEmployee(int id) 
        {
            SqlCommand com = new SqlCommand("Sp_employee_Delete", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Emp_id", id);

            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        public DataSet GetAllEmployees()
        {
            SqlCommand com = new SqlCommand("Sp_Employee_All", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

    }
}