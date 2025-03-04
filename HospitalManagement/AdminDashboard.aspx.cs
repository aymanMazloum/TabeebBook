using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace HospitalManagement
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        string connectionString = "Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MainMultiView.ActiveViewIndex = 0;

                int id;
                if (Session["Id"] != null && int.TryParse(Session["Id"].ToString(), out id))
                {
                    LoadAppointments();
                    LoadPatients();
                    LoadDoctors();
                    LoadUserData(id);
                    lblds.Text = "<h3>Search by Patient or Doctor or Speciality:</h3>";
                    lblPatients.Text = "<h3>Search by Patient or Phone or Email:</h3>";
                    lblDoctors.Text = "<h4>Search by Patient or Email or Phone or Speciality:</h4>";
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

        protected void signout_Click(object sender, EventArgs e)
        {
            logtxt.Text = "You have been logged out successfully.";
            Session.Abandon();
            Session.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();
            Response.Redirect("Login.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = @"
        UPDATE Users
        SET FullName = @FullName, Email = @Email
        FROM Users 
        WHERE Id = @UserId;
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@UserId", Session["Id"]);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Updated successfully!');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('No changes detected.');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
            }
            txtFullName.Enabled = false;
            txtEmail.Enabled = false;
          
        }

        protected void en_Click(object sender, EventArgs e)
        {
            txtFullName.Enabled = true;
            txtEmail.Enabled = true;

        }
        private void LoadUserData(int userID)
        {
            string query = "SELECT FullName, Email FROM Users WHERE Id = @UserId;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userID);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtFullName.Text = reader["FullName"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                
                               
                                string x = txtFullName.Text;
                                string y = "";
                                for (int i = 0; i < x.Length; i++)
                                {
                                    if (x[i] == ' ')
                                    {
                                        break;
                                    }
                                    if (i == 0)
                                    {
                                        y += char.ToUpper(x[i]);
                                    }
                                    else
                                    {
                                        y += x[i];
                                    }
                                }
                                pp.InnerText += y + " (Admin)👋";
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
        protected void SearchButton_Click(object sender, EventArgs e) {
            LoadAppointments(SearchTextBox.Text);
        }

        private void LoadAppointments(String t="")
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT 
    A.Id, 
    P.FullName AS PatientName, 
    D.FullName AS DoctorName, 
    DT.Speciality, 
    A.AppointmentDate, 
    A.Status
FROM Appointments A
JOIN Patients PT ON A.PatientId = PT.UserId
JOIN Users P ON PT.UserId = P.Id
JOIN Doctors DT ON A.DoctorId = DT.UserId
JOIN Users D ON DT.UserId = D.Id
WHERE 
    P.FullName LIKE @t OR 
    D.FullName LIKE @t OR 
    DT.Speciality LIKE @t 
ORDER BY A.AppointmentDate;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@t", "%"+t+"%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvAppointments.DataSource = dt;
                gvAppointments.DataBind();
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["Id"].ToString());
            string oldPassword = txtOldPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (newPassword != confirmPassword)
            {
                Label1.Text = "❌ New passwords do not match!";
                Label1.CssClass = "text-danger";
                Label1.Visible = true;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT Password FROM Users WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                string storedHashedPassword = cmd.ExecuteScalar()?.ToString();

                if (storedHashedPassword == null)
                {
                    Label1.Text = "❌ Email not found!";
                    Label1.CssClass = "text-danger";
                    Label1.Visible = true;
                    return;
                }


                string hashedOldPassword = oldPassword;
                if (storedHashedPassword != hashedOldPassword)
                {
                    Label1.Text = "❌ Incorrect current password!";
                    Label1.CssClass = "text-danger";
                    Label1.Visible = true;
                    return;
                }

                string hashedNewPassword = HashPassword(newPassword);
                query = "UPDATE Users SET Password = @NewPassword WHERE Id= @id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NewPassword", hashedNewPassword);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            Label1.Text = "✅ Password changed successfully!";
            Label1.CssClass = "text-success";
            Label1.Visible = true;
        }



        private void LoadPatients(string search = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT P.UserId Id, U.FullName, U.Email, P.Phone, P.DateOfBirth
                         FROM Patients P
                         JOIN Users U ON P.UserId = U.Id
                         WHERE U.FullName LIKE @search OR P.Phone LIKE @search OR U.Email LIKE @search";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvPatients.DataSource = dt;
                gvPatients.DataBind();
            }
        }
        protected void SearchPatientsButton_Click(object sender, EventArgs e)
        {
            string searchText = SearchPatientsTextBox.Text.Trim();
            LoadPatients(searchText);
        }

        private void LoadDoctors(string search = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT D.UserId Id, U.FullName, U.Email, D.Phone, D.Speciality
                         FROM Doctors D
                         JOIN Users U ON D.UserId = U.Id
                         WHERE U.FullName LIKE @search OR D.Phone LIKE @search OR U.Email LIKE @search OR D.Speciality LIKE @search";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvDoctors.DataSource = dt;
                gvDoctors.DataBind();
            }
        }


        protected void SearchDoctorsButton_Click(object sender, EventArgs e)
        {
            string searchText = SearchDoctorsTextBox.Text.Trim();
            LoadDoctors(searchText);
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