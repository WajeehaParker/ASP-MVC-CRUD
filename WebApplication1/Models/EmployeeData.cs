using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class EmployeeData
    {
        string connectionString = "Data Source=DESKTOP-65N8NPI\\SQLEXPRESS; Initial Catalog=employee;Persist Security Info=False;User ID=wajeeha;password=16101997;";
        
        //get all employees
        public IEnumerable<EmployeeInfo> getAllEmployee()
        {
            List<EmployeeInfo> empList = new List<EmployeeInfo>();
            using(SqlConnection con=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_getAllEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    EmployeeInfo emp = new EmployeeInfo();
                    emp.ID = Convert.ToInt32(dr["ID"].ToString());
                    emp.Name = dr["Name"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Department = dr["Department"].ToString();

                    empList.Add(emp);
                }
                con.Close();
            }
            return empList;
        }

        //insert employee
        public void addEmployee(EmployeeInfo emp)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Department", emp.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //updat eemployee
        public void updateEmployee(EmployeeInfo emp)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpId", emp.ID);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Department", emp.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //delete employee
        public void deleteEmployee(int? empId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", empId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //get employee by ID
        public EmployeeInfo getEmployeeByID(int empId)
        {
            EmployeeInfo emp = new EmployeeInfo();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_getEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", empId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    emp.ID = Convert.ToInt32(dr["ID"].ToString());
                    emp.Name = dr["Name"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Department = dr["Department"].ToString();
                }
                con.Close();
            }
            return emp;
        }
    }
}
