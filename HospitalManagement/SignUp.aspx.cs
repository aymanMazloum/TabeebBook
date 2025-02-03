using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class SignUp : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Server=DESKTOP-S9UBL8M;Database=HospitalManagement;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void ed(object sender, EventArgs e)
        {
            error.Text = "";
        }
        protected void Si_Click(object sender, EventArgs e)
        {
            if (fullname.Text == "" || email.Text == "" || password.Text == "" || list.SelectedValue == "")
            {
               error.Text = "please fill all information".ToString();
            }
            else
            {
                String name, emaill, pass, sel;
                name = fullname.Text;
                emaill = email.Text;
                pass = password.Text;
                sel = list.SelectedItem.Text;

                Session["fullname"] = name;
                Session["email"] = emaill;
                Session["pass"] = pass;
                Session["sel"] = sel;

                if (list.SelectedItem.Text == "Patient")
                {
                    PatientSignUp psu = new PatientSignUp();
                    int userId = psu.getId(emaill);
                    if (userId > 0)
                    {
                        emailError.Text = "This email is already in use";
                        emailError.Visible = true;
                    }
                    else
                    {
                        Response.Redirect("PatientSignUp.aspx");
                    }
                    
                }
                else if (list.SelectedItem.Text == "Doctor")
                {
                    Response.Redirect("DoctorSignUp.aspx");
                }
                

            }
        }
    }
}