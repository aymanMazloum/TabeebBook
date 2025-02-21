<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientDashboard.aspx.cs" Inherits="HospitalManagement.PatientDashboard" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Patient Dashboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin="anonymous"></script>
    <style>
        body {
            background-color: #f4f7f6;
            display: flex;
        }
        .sidebar {
            width: 250px;
            height: 100vh;
            background: #007bff;
            color: white;
            padding-top: 20px;
            position: fixed;
        }
        .sidebar a {
            padding: 15px;
            display: block;
            color: white;
            text-decoration: none;
            cursor: pointer;
        }
        .sidebar a:hover {
            background: #0056b3;
        }
        .content {
            margin-left: 260px;
            padding: 20px;
            flex-grow: 1;
            width: calc(100% - 260px);
        }
    </style>
</head>
<body>
    <form runat="server">
     
        <div class="sidebar">
            <h4 class="text-center">Patient Dashboard</h4><br /><br />
            <a onclick="ChangeView(0)"><i class="fas fa-home"></i>🏠 Home</a><hr />
            <a onclick="ChangeView(1)"><i class="fas fa-user"></i>👤 Profile</a><hr />
            <a onclick="ChangeView(2)"><i class="fas fa-calendar-alt"></i>📅 Appointments</a><hr />
            <a onclick="ChangeView(3)"><i class="fas fa-file-medical"></i>📂 Records</a><hr />
            <a onclick="ChangeView(4)"><i class="fas fa-sign-out-alt"></i>🚪 Logout</a>
        </div>

  
        <div class="content">
            <asp:MultiView ID="MainMultiView" runat="server" ActiveViewIndex="0">
                
            
                <asp:View ID="HomeView" runat="server">
                    <h1 runat="server" id="pp" style="font-size:70px;">Welcome, </h1>
                </asp:View>

                <asp:View ID="ProfileView" runat="server">
    <div class="container mt-4">
        <div class="card shadow-lg p-4" style="width:800px;height:550px;margin-left:110px;">
            <h3 class="text-center mb-4">Profile Information</h3>

            <div class="mb-3">
                <label for="txtFullName" class="form-label">Full Name:</label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="Enter your full name" Enabled="false"></asp:TextBox>
            </div>
            <br />
            <div class="mb-3">
                <label for="txtPhoneNumber" class="form-label">Phone Number:</label>
                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" Placeholder="Enter your phone number" Enabled="false"></asp:TextBox>
            </div>
                        <br />

            <div class="mb-3">
                <label for="txtBirthDate" class="form-label">Date of Birth:</label>
                <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>
                        <br />

            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" Placeholder="Enter your email" Enabled="false"></asp:TextBox>
            </div>
            <div class="text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                <asp:Button ID="Button1" runat="server" Text="Update" CssClass="btn btn-primary" />

            </div>
        </div>
    </div>
</asp:View>


                <asp:View ID="AppointmentsView" runat="server">
                    <h2>📅 Appointments</h2>
                    <p>Here you can check your upcoming appointments.</p>
                </asp:View>

                <asp:View ID="RecordsView" runat="server">
                    <h2>📂 Medical Records</h2>
                    <p>Here you can view your medical records.</p>
                </asp:View>

                <asp:View ID="LogoutView" runat="server">
                    <h2>🚪 Logout</h2>
                    <p>You have been logged out successfully.</p>
                </asp:View>

            </asp:MultiView>

            <asp:HiddenField ID="hfSelectedView" runat="server" />
            <asp:Button ID="btnChangeView" runat="server" OnClick="btnChangeView_Click" Style="display:none;" />
        </div>
    </form>

    <script>
        function ChangeView(index) {
            document.getElementById("<%= hfSelectedView.ClientID %>").value = index;
            document.getElementById("<%= btnChangeView.ClientID %>").click();
        }
    </script>
</body>
</html>
