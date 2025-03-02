using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace HospitalManagement
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = HashPassword(txtPassword.Text.Trim());

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter both email and password.";
                return;
            }

            string connectionString = @"Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True";
            string query = "SELECT Id,Role FROM Users WHERE Email = @Email AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                String role = reader["Role"].ToString();
                                Session["Id"] = id;
                                if (role == "Patient")
                                {
                                    Response.Redirect("PatientDashboard.aspx");
                                }
                                else if (role == "Doctor")
                                {
                                    Response.Redirect("DoctorDashboard.aspx");

                                }
                                else
                                {
                                    Response.Redirect("AdminDashboard.aspx");
                                }

                            }
                            else
                            {
                                lblMessage.Text = "Invalid username or password!";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                    }
                }

            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
