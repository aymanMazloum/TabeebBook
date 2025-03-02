using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class DoctorSignUp : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            drname.Text = Session["fullname"].ToString();
            dremail.Text = Session["email"].ToString();
        }
        public int getId(String email)
        {
            SqlConnection conn = new SqlConnection(@"Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True");
            string q2 = "select Id from Users where Email='" + email + "'";
            SqlDataAdapter sda = new SqlDataAdapter(q2, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int id = -1;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    id = Convert.ToInt32(r["Id"]);
                }
            }
            return id;

        }
        protected void registerDoctor_Click(object sender, EventArgs e)
        {
            if (phone.Text == "" ||onum.Text == "" || list.SelectedValue=="")
            {
                error.Text = "Please fill all information.";
            }
            else
            {
                try
                {
                    byte[] imgBytes = (byte[])Session["ProfileImage"];

                    conn.Open();
                    string query = "INSERT INTO Users (FullName, Email, Password, Role,ProfileImage) VALUES (@FullName, @Email, @Password, @Role, @ProfileImage)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FullName", Session["fullname"].ToString());
                    cmd.Parameters.AddWithValue("@Email", Session["email"].ToString());
                    cmd.Parameters.AddWithValue("@Password", HashPassword(Session["pass"].ToString()));
                    cmd.Parameters.AddWithValue("@Role", Session["role"].ToString());
                    cmd.Parameters.Add("@ProfileImage", System.Data.SqlDbType.VarBinary).Value = imgBytes;

                    cmd.ExecuteNonQuery();

                    string q3 = "INSERT INTO Doctors (UserId, Speciality, Phone, OfficeNumber) VALUES (@UserId, @Speciality, @Phone, @officeNumber)";
                    SqlCommand cmd2 = new SqlCommand(q3, conn);
                    cmd2.Parameters.AddWithValue("@UserId", getId(Session["email"].ToString()));
                    cmd2.Parameters.AddWithValue("@Speciality", list.SelectedItem.ToString());
                    cmd2.Parameters.AddWithValue("@Phone", phone.Text.ToString());
                    cmd2.Parameters.AddWithValue("@officeNumber", onum.Text.ToString());


                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Login.aspx");
                }
                catch (Exception ex)
                {
                    error.Text = "An error occurred: " + ex.Message;
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