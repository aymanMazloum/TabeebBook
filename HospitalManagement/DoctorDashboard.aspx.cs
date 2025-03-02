using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using ZXing;
using System.Data;
using static ZXing.QrCode.Internal.Mode;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace HospitalManagement
{
    public partial class DoctorDashboard : Page
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
                    LoadPatients();

                    LoadUserData(id);
                    LoadAppointments();
                    lblds.Text = "Search for doctors by their names or specialties:  \t";
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

        UPDATE Doctors
        SET Phone = @Phone, OfficeNumber = @onum
        FROM Doctors
        WHERE UserId = @UserId;
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Speciality", spec.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhoneNumber.Text);
                    cmd.Parameters.AddWithValue("@onum", onum.Text);
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
            onum.Enabled = false;
            spec.Enabled = false;
        }

        protected void en_Click(object sender, EventArgs e)
        {
            spec.Enabled = true;
            txtFullName.Enabled = true;
            txtEmail.Enabled = true;
            onum.Enabled = true;
            txtPhoneNumber.Enabled = true;

        }
        private void LoadUserData(int userID)
        {
            string query = @"
        SELECT 
            u.FullName, 
            u.Email, 
            u.ProfileImage,
            d.Speciality,
            d.Phone, 
            d.OfficeNumber
        FROM Users u
        INNER JOIN Doctors d ON u.Id = d.UserId
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
                                spec.Text = reader["Speciality"].ToString();
                                onum.Text = reader["OfficeNumber"].ToString();
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
                                pp.InnerText += y + " 👋";
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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();

            if (!string.IsNullOrEmpty(message) && Session["Id"] != null)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Messages (SenderID, ReceiverID, MessageText, Timestamp) VALUES (@SenderID, @ReceiverID, @MessageText, @Timestamp)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SenderID", Convert.ToInt32(Session["Id"]));
                        cmd.Parameters.AddWithValue("@ReceiverID", Convert.ToInt32(Session["SelectedPatientID"].ToString()));
                        cmd.Parameters.AddWithValue("@MessageText", message);
                        cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                txtMessage.Text = "";
                LoadChatMessages();

                ClientScript.RegisterStartupScript(this.GetType(), "scrollToBottom", "scrollToBottom();", true);
            }
        }

        private void LoadAppointments()
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT A.Id, P.FullName AS PatientName, A.AppointmentDate, A.Status
FROM Appointments A
JOIN Patients PA ON A.PatientId = PA.UserId
JOIN Users P ON PA.UserId = P.Id
WHERE A.DoctorId = @DoctorId
ORDER BY A.AppointmentDate;
                ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorId", Session["Id"]);
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

        protected void btnLoadChat_Click(object sender, EventArgs e)
        {
            Session["SelectedPatientID"] = ddlPatients.SelectedValue;
            Session["PatientName"] = ddlPatients.SelectedItem.ToString();

            if (ddlPatients.SelectedItem.ToString() == "-- Select a Patient --")
            {


                ChatWithPatient.Text = "<i style=margin-left:40px;color:white;text-shadow:-2px -2px 0 black,2px -2px 0 black,-2px  2px 0 black, 2px  2px 0 black;font-size:20px;font-weight:bold;>Please Select a Patient to Load his/her Messages</i>";
            }
            else { ChatWithPatient.Text = "💬 Chat with Patient: " + Session["PatientName"]; }
            LoadChatMessages();
        }

        private void LoadChatMessages()
        {
            string patientId = Session["SelectedPatientID"]?.ToString();
            if (string.IsNullOrEmpty(patientId)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Messages WHERE SenderID = @UserID AND ReceiverID = @PatientID OR ReceiverID = @UserID AND SenderID = @PatientID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["Id"]);
                cmd.Parameters.AddWithValue("@PatientID", Session["SelectedPatientID"]);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptChatMessages.DataSource = dt;
                rptChatMessages.DataBind();
            }
        }
        private void LoadPatients()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT U.Id AS UserId, U.FullName AS PatientName
            FROM Users U
            INNER JOIN Patients P ON U.Id = P.UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    ddlPatients.DataSource = cmd.ExecuteReader();
                    ddlPatients.DataTextField = "PatientName";
                    ddlPatients.DataValueField = "UserId";
                    ddlPatients.DataBind();

                }
            }
            ddlPatients.Items.Insert(0, new ListItem("-- Select a Patient --", "0"));
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

