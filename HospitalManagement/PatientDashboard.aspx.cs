﻿using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using ZXing;
using System.Data;
using static ZXing.QrCode.Internal.Mode;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class PatientDashboard : Page
    {
        string connectionString = @"Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MainMultiView.ActiveViewIndex = 0;

                int id;
                if (Session["Id"] != null && int.TryParse(Session["Id"].ToString(), out id))
                {
                    LoadDoctors();
                    Label1.Visible = false;
                    LoadUserData(id);
                    LoadDoctorChatMessages();

                    LoadAppointments();
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

        UPDATE Patients
        SET Phone = @Phone, DateOfBirth = @DateOfBirth
        FROM Patients
        WHERE UserId = @UserId;
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhoneNumber.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", txtBirthDate.Text);
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
            txtPhoneNumber.Enabled = false;
            txtBirthDate.Enabled = false;
        }

        protected void en_Click(object sender, EventArgs e)
        {
            txtFullName.Enabled = true;
            txtEmail.Enabled = true;
            txtBirthDate.Enabled = true;
            txtPhoneNumber.Enabled = true;

        }
        private void LoadUserData(int userID)
        {
            string connectionString = @"Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True";
            string query = @"
        SELECT 
            u.FullName, 
            u.Email, 
            u.ProfileImage,
            p.Phone, 
            CONVERT(varchar, p.DateOfBirth, 23) AS DateOfBirth 
        FROM Users u
        INNER JOIN Patients p ON u.Id = p.UserId
        WHERE u.Id = @UserId;";

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
                                txtPhoneNumber.Text = reader["Phone"].ToString();
                                txtBirthDate.Text = reader["DateOfBirth"].ToString();

                                byte[] imageData = reader["ProfileImage"] as byte[];
                                if (imageData != null)
                                {
                                    string base64String = Convert.ToBase64String(imageData, 0, imageData.Length);
                                    imgProfilePicture.ImageUrl = "data:image/png;base64," + base64String;
                                }
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
                                pp.InnerText += y+ " 👋";
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


        protected void btnLoadDoctorChat_Click(object sender, EventArgs e)
        {
            Session["SelectedDoctorID"] = ddlDoctors1.SelectedValue;
            Session["DoctorName"] = ddlDoctors1.SelectedItem.ToString();

            if (ddlDoctors1.SelectedItem.ToString() == "-- Select a Doctor --")
            {
                ChatWithDoctor.Text = "<i style='margin-left:40px;color:white;text-shadow:-2px -2px 0 black,2px -2px 0 black,-2px  2px 0 black, 2px  2px 0 black;font-size:20px;font-weight:bold;'>Please Select a Doctor to Load his/her Messages</i>";
            }
            else
            {
                ChatWithDoctor.Text = "💬 Chat with Doctor: " + Session["DoctorName"];
            }
            LoadDoctorChatMessages();
        }

        private void LoadDoctorChatMessages()
        {
            string doctorId = Session["SelectedDoctorID"]?.ToString();
            if (string.IsNullOrEmpty(doctorId)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Messages WHERE (SenderID = @UserID AND ReceiverID = @DoctorID) OR (ReceiverID = @UserID AND SenderID = @DoctorID)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["Id"]);
                cmd.Parameters.AddWithValue("@DoctorID", Session["SelectedDoctorID"]);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptDoctorChatMessages.DataSource = dt;
                rptDoctorChatMessages.DataBind();
            }
        }


        protected void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtDoctorMessage.Text.Trim();

            if (!string.IsNullOrEmpty(message) && Session["Id"] != null)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Messages (SenderID, ReceiverID, MessageText, Timestamp) VALUES (@SenderID, @ReceiverID, @MessageText, @Timestamp)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SenderID", Convert.ToInt32(Session["Id"]));
                        cmd.Parameters.AddWithValue("@ReceiverID", Convert.ToInt32(Session["SelectedDoctorID"].ToString()));
                        cmd.Parameters.AddWithValue("@MessageText", message);
                        cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                txtDoctorMessage.Text = "";
                LoadDoctorChatMessages();

                ClientScript.RegisterStartupScript(this.GetType(), "scrollToBottom", "scrollToBottom();", true);
            }
        }


        private void LoadDoctors()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT U.Id AS UserId, U.FullName AS DoctorName
        FROM Users U
        INNER JOIN Doctors D ON U.Id = D.UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    ddlDoctors1.DataSource = cmd.ExecuteReader();
                    ddlDoctors1.DataTextField = "DoctorName";
                    ddlDoctors1.DataValueField = "UserId";
                    ddlDoctors1.DataBind();
                }
            }
            ddlDoctors1.Items.Insert(0, new ListItem("-- Select a Doctor --", "0"));
        }
        protected void LoadDoctorS(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT U.Id AS UserId, U.FullName AS DoctorName
        FROM Users U
        INNER JOIN Doctors D ON U.Id = D.UserId 
        WHERE D.Speciality = @Speciality"; 

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Speciality", spec.SelectedItem.Text);
                    conn.Open();
                    ddlDoctors.DataSource = cmd.ExecuteReader();
                    ddlDoctors.DataTextField = "DoctorName";
                    ddlDoctors.DataValueField = "UserId";
                    ddlDoctors.DataBind();
                }
            }

            ddlDoctors.Items.Insert(0, new ListItem("-- Select a Doctor --", "0"));
        }



        private void LoadAppointments()
        {
            string connString = "Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT u.FullName AS DoctorName, a.AppointmentDate, a.Status 
                             FROM Appointments a
                             JOIN Doctors d ON a.DoctorId = d.UserId
                             JOIN Users u ON d.UserId = u.Id
                             WHERE a.PatientId = @PatientId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PatientId", Session["Id"].ToString());
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvAppointments.DataSource = dt;
                    gvAppointments.DataBind();
                }
            }
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            int doctorId = int.Parse(ddlDoctors.SelectedValue);
            int patientId = Convert.ToInt32(Session["Id"].ToString());
            DateTime appointmentDate = DateTime.Parse(txtAppointmentDate.Text);

            string connString = "Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Appointments (DoctorId, PatientId, AppointmentDate, Status) VALUES (@DoctorId, @PatientId, @AppointmentDate, 'Scheduled')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DoctorId", doctorId);
                    cmd.Parameters.AddWithValue("@PatientId", patientId);
                    cmd.Parameters.AddWithValue("@AppointmentDate", appointmentDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Appointment Added Sucessfully!";
                    lblMessage.Visible = true;

                    LoadAppointments();
                }
            }
            txtAppointmentDate.Text = "";
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

            string connectionString = "Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT Password FROM Users WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                string storedHashedPassword = cmd.ExecuteScalar()?.ToString();
         
                string hashedOldPassword = HashPassword(oldPassword);
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
