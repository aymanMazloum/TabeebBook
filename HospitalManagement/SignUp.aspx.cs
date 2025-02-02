using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagement
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Sign(object sender, EventArgs e)
        {
            lblMessage.Text = "تم تسجيل الدخول بنجاح!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMessage", "document.getElementById('messageBox').style.display = 'block';", true);
        }
    }
}