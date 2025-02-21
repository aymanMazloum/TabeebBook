<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QRCodeAuth.aspx.cs" Inherits="HospitalManagement.QRCodeAuth" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>QR Code Authentication</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f4f4f4;
        }
        .card {
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
            border-radius: 12px;
            padding: 10px 10px 10px 10px;
            background: white;
        }
        .qr-container {
            display: flex;
            justify-content: center;
        }
        .qr-container img {
            border: 4px solid #007bff;
            border-radius: 10px;
            padding: 1px;
            background: white;
        }
        .btn-verify {
            width: 100%;
            transition: 0.3s;
        }
        .btn-verify:hover {
            transform: scale(1.05);
        }
    </style>
</head>
<body>
    <div class="container d-flex justify-content-center align-items-center min-vh-100">
        <div class="col-md-6">
            <div class="card text-center">
                <h2 class="mb-3 text-primary">QR Code Authentication</h2>
                <p class="text-muted">Scan the QR Code and enter the code below.</p>

               
                <form id="form1" runat="server">
                    <div class="qr-container">
                        <asp:Image ID="imgQRCode" runat="server" CssClass="img-fluid mb-3" Height="360px" />
                    </div>

                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control mb-3 text-center" placeholder="Enter code from QR"></asp:TextBox>

                    <asp:Button ID="btnVerify" runat="server" CssClass="btn btn-primary btn-verify" Text="Verify" OnClick="btnVerify_Click"/>

                    <asp:Label runat="server" ForeColor="Red" ID="lblMessage" CssClass="d-block mt-3"></asp:Label>
                </form>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
