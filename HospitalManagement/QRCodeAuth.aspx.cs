using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;

namespace HospitalManagement
{
    public partial class QRCodeAuth : System.Web.UI.Page
    {
        private static string secretCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateQRCode();
            }
        }

        private void GenerateQRCode()
        {
            secretCode = new Random().Next(100000, 999999).ToString();
            Session["QRCodeSecret"] = secretCode;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(secretCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            MemoryStream ms = new MemoryStream();

            qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] byteImage = ms.ToArray();
            string base64Image = Convert.ToBase64String(byteImage);
            imgQRCode.ImageUrl = "data:image/png;base64," + base64Image;

        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            string enteredCode = txtCode.Text.Trim();
            string storedCode = Session["QRCodeSecret"] as string;

            if (enteredCode == storedCode)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Success! Redirecting...";
                if (Session["role"].ToString() == "Patient")
                {
                    Response.Redirect("PatientSignUp.aspx");
                }
                else if (Session["role"].ToString()=="Doctor")
                {
                    Response.Redirect("DoctorSignUp.aspx");
                }
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Your role is: " + Session["role"].ToString();

            }
        }


    }
}
