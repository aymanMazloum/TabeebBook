using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class SignUp : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e) { }

        protected void Si_Click(object sender, EventArgs e)
        {
            error.Text = "";

            if (string.IsNullOrWhiteSpace(fullname.Text) || string.IsNullOrWhiteSpace(email.Text) ||
                string.IsNullOrWhiteSpace(password.Text) || string.IsNullOrWhiteSpace(list.SelectedValue) || !profilePicUpload.HasFile)
            {
                error.Text = "Please fill all information!";
                return;
            }

            string name = fullname.Text;
            string emaill = email.Text;
            string pass = password.Text;
            string role = list.SelectedItem.Text;
            byte[] imgBytes = null;

            string uploadFolder = Server.MapPath("~/Uploads/");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            if (profilePicUpload.HasFile)
            {
                string fileName = Path.GetFileName(profilePicUpload.PostedFile.FileName);
                string filePath = Path.Combine(uploadFolder, fileName);
                profilePicUpload.PostedFile.SaveAs(filePath);

                profileImage.ImageUrl = "~/Uploads/" + fileName;
                profileImage.Visible = true;

                imgBytes = File.ReadAllBytes(filePath);
            }

            Session["fullname"] = name;
            Session["email"] = emaill;
            Session["pass"] = pass;
            Session["role"] = role;
            Session["profileImage"] = imgBytes;

            if (IsEmailInUse(emaill))
            {
                email.Attributes["style"] = "border: 1px solid red;";
                emailError.Text = "This email is already in use";
                emailError.Visible = true;
            }
            else
            {
                email.Attributes["style"] = "border: 1px solid green;";
                emailError.Visible = false;
                Response.Redirect("QRCodeAuth.aspx");
            }
        }

        private bool IsEmailInUse(string email)
        {
            bool exists = false;
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                exists = (int)cmd.ExecuteScalar() > 0;
                conn.Close();
            }
            return exists;
        }
    }
}
