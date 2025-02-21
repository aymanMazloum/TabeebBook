using System;
using System.Configuration;
using System.Data.SqlClient;
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
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter both email and password.";
                return;
            }

            string connectionString = @"Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True";
            string query = "SELECT Id FROM Users WHERE Email = @Email AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password); // ⚠ استخدم التشفير في المستقبل

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int id = Convert.ToInt32(result);
                            Session["Id"] = id;
                            Response.Redirect("PatientDashboard.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Invalid username or password!";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                    }
                }
            }
        }
    }
}
