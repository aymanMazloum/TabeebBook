using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using ZXing;

namespace HospitalManagement
{
    public partial class PatientDashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MainMultiView.ActiveViewIndex = 0;

                int id;
                if (Session["Id"] != null && int.TryParse(Session["Id"].ToString(), out id))
                {
                    LoadUserData(id);
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnChangeView_Click(object sender, EventArgs e)
        {
            int selectedView;
            if (int.TryParse(hfSelectedView.Value, out selectedView))
            {
                MainMultiView.ActiveViewIndex = selectedView;
            }
        }

        private void signout()
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        private void LoadUserData(int userID)
        {
            string connectionString = @"Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True";
            string query = "SELECT FullName, Email FROM Users WHERE Id = @UserId;";
            string query2 = "SELECT Phone,DateOfBirth FROM Patients WHERE UserId = @UserId;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtFullName.Text = reader["FullName"].ToString();
                                txtEmail.Text = reader["Email"].ToString();

                                String x = txtFullName.Text;
                                String y = "";
                                for (int i = 0; i < x.Length; i++)
                                {
                                    if (x[i] == ' ')
                                    {
                                        break;
                                    }
                                    if (i == 0)
                                    {
                                        x[i].ToString().ToUpper();
                                    }
                                    y += x[i];
                                }
                                pp.InnerText += y;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query2, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    try {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtPhoneNumber.Text = reader["Phone"].ToString();
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
            }
        }
    }
}
