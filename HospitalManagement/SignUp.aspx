<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="HospitalManagement.SignUp" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sign Up - Hospital Management</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
    <style>
        body {
            background: linear-gradient(135deg, #e3f2fd, #bbdefb, #90caf9);
            height: 100vh;
            display: flex;
            flex-direction: column;
        }
        .card {
            background: rgba(255, 255, 255, 0.9);
            border-radius: 15px;
        }
        .navbar {
            background: #0d47a1 !important;
        }
        .navbar-brand {
            font-weight: bold;
        }
        #error {
            color: red;
        }
    </style>
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark">
        <div class="container">
            <a class="navbar-brand" href="Index.aspx">
                <i class="fas fa-hospital-alt"></i> Hospital Management
            </a>
        </div>
    </nav>
    
    <div class="container d-flex justify-content-center align-items-center flex-grow-1">
        <div class="card shadow p-4 text-center" style="width: 400px;">
            <h3>Sign Up</h3>
            <br />
            <form id="form1" runat="server" enctype="multipart/form-data">
                <div class="mb-3">
                    <asp:Label ID="error" Text="" runat="server"></asp:Label>
                </div>
                <div class="mb-3">
                    <asp:TextBox type="text" class="form-control" ID="fullname" placeholder="Enter your full name" runat="server"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:TextBox type="email" class="form-control ee" ID="email" placeholder="Enter your email" runat="server"></asp:TextBox>
                    <asp:Label ID="emailError" runat="server" ForeColor="Red" Visible="false" CssClass="d-block text-start" Font-Size="Small"></asp:Label>
                </div>
                <div class="mb-3">
                    <asp:TextBox type="password" class="form-control" ID="password" placeholder="Create a password" runat="server"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:DropDownList class="form-control" runat="server" ID="list">
                        <asp:ListItem Text="Select your role" Value="" />
                        <asp:ListItem Text="Patient" Value="1" />
                        <asp:ListItem Text="Doctor" Value="2" />
                    </asp:DropDownList>
                </div>
                
               
                <div class="mb-3">
                    <asp:FileUpload ID="profilePicUpload" runat="server" CssClass="form-control" />
                    <asp:Label ID="fileError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </div>
                <div class="mb-3 text-center">
                    <asp:Image ID="profileImage" runat="server" Width="100px" Height="100px" Visible="false" CssClass="img-thumbnail" />
                </div>

                <asp:Button class="btn btn-success w-100" ID="Si" runat="server" Text="Sign Up" OnClick="Si_Click" />
            </form>
            <p class="mt-3">Already have an account? <a href="Login.aspx">Login</a></p>
        </div>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
