<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientSignUp.aspx.cs" Inherits="HospitalManagement.PatientSignUp" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Patient Sign Up - Hospital Management</title>
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
        #error{
            color:red;
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
            <h3>Patient Sign Up</h3>
            <br />
            <form id="form1" runat="server" action="patientSignUp.aspx">
                <div class="mb-3">
                    <asp:Label ID="error" Text="" runat="server"></asp:Label>
                </div>
                <div class="mb-3">
                    <asp:TextBox type="text" class="form-control" ID="drname" placeholder="Enter your full name" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:TextBox type="email" class="form-control" ID="dremail" placeholder="Enter your email" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
              
                  <div class="mb-3">
      <asp:TextBox type="text" class="form-control" ID="phone" placeholder="Enter your phone number" runat="server"></asp:TextBox>
  </div>
  <div class="mb-3">
      <asp:TextBox type="date" class="form-control" ID="dateOfBirth" runat="server"></asp:TextBox>
  </div>
                <asp:Button class="btn btn-success w-100" ID="registerPatient" runat="server" Text="Sign Up" OnClick="registerPatient_Click" />
            </form>
            <p class="mt-3">Already have an account? <a href="Login.aspx">Login</a></p>
        </div>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

