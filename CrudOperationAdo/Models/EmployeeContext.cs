using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CrudOperationAdo.Models
{
    public class EmployeeContext
    {
        SqlConnection con = new SqlConnection("Data Source = LAPTOP-PG41E18S\\SQLEXPRESS; Initial Catalog =Customer ; integrated security = true");
        public List<EmployeeModel> GetEmployees()

        {
            List<EmployeeModel> listObj = new List<Models.EmployeeModel>();
            SqlCommand cmd = new SqlCommand("usp_getEmployees",con);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeModel emp = new EmployeeModel();
                emp.ProductId =Convert.ToInt32(dr[0]);
                emp.ProductName = Convert.ToString(dr[1]);
                emp.CategoryId = Convert.ToInt32(dr[2]);
                emp.CategoryName = Convert.ToString(dr[3]);

                listObj.Add(emp);
            }
            return listObj;
            
        }
        SqlCommand cmd=null;
        public int SaveEmployee(EmployeeModel emp)
        {
           
            if (emp.ProductId>0) {
                cmd = new SqlCommand("usp_UpdateMyEmployee", con);
                cmd.Parameters.AddWithValue("@ProductId", emp.ProductId);

            }
            else
            {
                cmd = new SqlCommand("spr_CreatEmployeeDetails", con);
                cmd.Parameters.AddWithValue("@ProductId", emp.ProductId);

            }
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@ProductName", emp.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", emp.CategoryId);
            cmd.Parameters.AddWithValue("@CategoryName", emp.CategoryName);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public EmployeeModel GetEmployeesById(int ? id)

        {

            EmployeeModel emp = new EmployeeModel();

            cmd = new SqlCommand("usp_getMyEmployeeEById", con);
            cmd.Parameters.AddWithValue("@ProductId", id);

            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                emp.ProductId = Convert.ToInt32(dr[0]);
                emp.ProductName = Convert.ToString(dr[1]);
                emp.CategoryId = Convert.ToInt32(dr[2]);
                emp.CategoryName = Convert.ToString(dr[3]);

            }
            return emp;

        }

        
        public int DeleteEmployee(int ? id)
        {

            cmd = new SqlCommand("usp_DeleteEmployeeById", con);
            cmd.Parameters.AddWithValue("@ProductId",id);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}