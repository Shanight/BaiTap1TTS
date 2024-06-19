using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Users
    {
        private string strConString = @"Server=.\SQLEXPRESS;Database=test1asoft;User Id=sa;Password=123456;Integrated Security=False;MultipleActiveResultSets=True;TrustServerCertificate=True";
        public bool CheckExistingUser(string userID)
        {
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string checkSql = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID";
                SqlCommand checkCmd = new SqlCommand(checkSql, con);
                checkCmd.Parameters.AddWithValue("@UserID", userID);
                int existingUserCount = (int)checkCmd.ExecuteScalar();

                return existingUserCount > 0;
            }
        }
        public DataTable GetUserByID(string UserID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(strConString))
            {

                con.Open();
                string query = "Select * from users where UserID=@UserID";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetAll()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from Users", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;


        }
        public string InsertUsers(string UserID, string UserName, string Password, string Email, string Tel, string Disabled)
        {
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string checkSql = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID";
                SqlCommand checkCmd = new SqlCommand(checkSql, con);
                checkCmd.Parameters.AddWithValue("@UserID", UserID);
                int existingUserCount = (int)checkCmd.ExecuteScalar();


                string sql = "Insert into Users (UserID,UserName, Password, Email,Tel,Disabled) values(@UserID,@UserName, @Password, @Email,@Tel,@Disabled)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Tel", Tel);
                cmd.Parameters.AddWithValue("@Disabled", Disabled);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return "Dữ liệu đã được chèn thành công.";
                }
                else
                {
                    return "Đã xảy ra lỗi khi chèn dữ liệu.";
                }
            }
        }

        public int UpdateUser(string UserID, string UserName, string Password, string Email, string Tel, string Disabled)
        {
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Update Users SET UserID=@UserID, UserName=@UserName, Password=@Password, Email=@Email, Tel=@Tel, Disabled=@Disabled where UserID=@UserID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", UserID.ToUpper());
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Tel", Tel);
                cmd.Parameters.AddWithValue("@Disabled", Disabled);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteUser(string UserID)
        {
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Delete from users where UserID=@UserID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                return cmd.ExecuteNonQuery();
            }
        }

        [Display(Name = "Id")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public int Disabled { get; set; }
    }
}