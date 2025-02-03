using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace HospitalManagement
{
    public partial class PatientSignUp : System.Web.UI.Page
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
            string q2 = "select Id from Users where Email='" + email+ "'";
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
        protected void registerPatient_Click(object sender, EventArgs e)
        {
            if (phone.Text == "" || dateOfBirth.Text == "")
            {
                error.Text = "Please fill all information.";
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Users (FullName, Email, Password, Role) VALUES (@FullName, @Email, @Password, @Role)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FullName", Session["fullname"].ToString());
                    cmd.Parameters.AddWithValue("@Email", Session["email"].ToString());
                    cmd.Parameters.AddWithValue("@Password", Session["pass"].ToString());
                    cmd.Parameters.AddWithValue("@Role", Session["sel"].ToString());
                    cmd.ExecuteNonQuery();
                    
                    

                    string q3 = "INSERT INTO Patients (UserId, Phone, DateOfBirth) VALUES (@UserId, @Phone, @DateOfBirth)";
                    SqlCommand cmd2 = new SqlCommand(q3, conn);
                    cmd2.Parameters.AddWithValue("@UserId", getId(Session["email"].ToString()));
                    cmd2.Parameters.AddWithValue("@Phone", phone.Text.ToString());
                    cmd2.Parameters.AddWithValue("@DateOfBirth",dateOfBirth.Text.ToString());
                    
                    
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
    }
}
