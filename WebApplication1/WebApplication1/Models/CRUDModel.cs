using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CRUDModel
    {
      
        public DataTable GetAll()
        {
            DataTable dt = new DataTable();
            string strConString = @"Server=.\SQLEXPRESS;Database=test1asoft;User Id=sa;Password=123456;Integrated Security=False;MultipleActiveResultSets=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from Users", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;


        }
        public int InsertUsers(string UserID, string UserName, string Password, string Email, string Tel, string Disabled)
        {
            string strConString = @"Server=.\SQLEXPRESS;Database=test1asoft;User Id=sa;Password=123456;Integrated Security=False;MultipleActiveResultSets=True;TrustServerCertificate=True";
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Insert into Users (UserID,UserName, Password, Email,Tel,Disabled) values(@UserID,@UserName, @Password, @Email,@Tel,@Disabled)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Tel", Tel);
                cmd.Parameters.AddWithValue("@Disabled", Disabled);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}