<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="HospitalManagement.AdminDashboard" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Admin Dashboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin="anonymous"></script>
        <style>
        body {
            background-color: #f4f7f6;
            display: flex;
            transition: background-color 0.3s, color 0.3s;
        }
        .dark-mode {
            background-color: #121212;
            color: white;
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
           .navbar {
            background-color: #007bff !important;
            padding: 15px;
            border-radius: 10px;
        }
        .navbar .navbar-brand {
            color: white;
            font-size: 20px;
            font-weight: bold;
        }
        .navbar .search-box {
            display: flex;
            align-items: center;
            margin-left: auto;
        }
        .navbar .search-input {
            border-radius: 5px;
            border: none;
            padding: 8px 12px;
            width: 250px;
        }
        .navbar .search-icon {
            background-color: white;
            border: none;
            padding: 8px 12px;
            margin-left: 5px;
            cursor: pointer;
            border-radius: 5px;
        }
        .dark-theme {
    background-color: #121212 !important; 
    color: white !important;
}

.dark-theme .container,
.dark-theme .card,
.dark-theme .content,
.dark-theme .navbar{
    background-color: #121212 !important; 
    color: white !important;
    border: 0px solid #444;
}

.dark-theme input,
.dark-theme textarea {
    background-color: #333 !important;
    color: white !important;
    border: 2px solid #666 !important;
}

.bbb{
    width:600px;
    margin-left:200px;
}
.pp{
    width:500px;
    margin-left:250px;
    margin-top:80px;
    height:500px;
}

.chat-container {
        width: 600px;
margin-left: 220px;
    margin-top:55px;
    background: #f9f9f9;
    border-radius: 15px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    overflow: hidden;
    display: flex;
    flex-direction: column;
    height: 500px;
}

.chat-box {
    
    flex: 1;
    overflow-y: auto;
    padding: 15px;
    display: flex;
    flex-direction: column;
}

.chat-message {
    max-width: 80%;
    padding: 10px 15px;
    margin: 8px 0;
    border-radius: 15px;
    font-size: 14px;
    position: relative;
}

.chat-message.sent {
    background: linear-gradient(to right, #4CAF50, #2E7D32);
    color: white;
    align-self: flex-end;
    border-bottom-right-radius: 3px;
}

.chat-message.received {
    background: linear-gradient(to right, #ffffff, #e0e0e0);
    color: #333;
    align-self: flex-start;
    border-bottom-left-radius: 3px;
}

.chat-time {
    font-size: 12px;
    opacity: 0.7;
    display: block;
    margin-top: 5px;
    text-align: right;
}

.chat-input {
    display: flex;
    padding: 10px;
    background: #ffffff;
    border-top: 1px solid #ddd;
}


.chat-textbox {
    flex: 1;
    padding: 10px;
    border: none;
    border-radius: 25px;
    outline: none;
    font-size: 14px;
    background: #f0f0f0;
}


.chat-send-btn {
    margin-left: 10px;
    background: #4CAF50;
    color: white;
    border: none;
    padding: 10px 15px;
    border-radius: 25px;
    cursor: pointer;
    transition: 0.3s;
}

.chat-send-btn:hover {
    background: #388E3C;
}

.patient-selector {
    display: flex;
    gap: 10px;
}

.patient-dropdown {
    padding: 8px;
    border-radius: 5px;
    border: 1px solid #ddd;
}

.btn-load-chat {
    background: #28a745;
    color: white;
    padding: 8px 15px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: 0.3s;
}

.btn-load-chat:hover {
    background: #218838;
}

body {
    position: relative;
}

.blackout {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.8);
    z-index: 1000;
    opacity: 0;
    transition: opacity 0.5s ease;
    pointer-events: none;
    display: flex;
    justify-content: center;
    align-items: center;
}

.spinner-container {
    text-align: center;
}

.spinner {
    display: flex;
    justify-content: center;
    align-items: center;
}

.spinner div {
    width: 15px;
    height: 15px;
    margin: 0 5px;
    background-color: #3498db;
    border-radius: 50%;
    animation: bounce 0.6s infinite alternate;
}

.spinner div:nth-child(2) {
    animation-delay: 0.2s; 
}

.spinner div:nth-child(3) {
    animation-delay: 0.4s;
}

@keyframes bounce {
    0% {
        transform: translateY(0);
    }
    100% {
        transform: translateY(-15px);
    }
}




    </style>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

</head>
<body>
    <form runat="server">
     
        <div class="sidebar overflow-auto">
            <h4 class="text-center">Admin Dashboard</h4><br /><br />
            <a onclick="ChangeView(0)"><i class="fas fa-home"></i>🏠 Home</a><hr />
            <a onclick="ChangeView(1)"><i class="fas fa-user"></i>👤 Profile</a><hr />
            <a onclick="ChangeView(2)"><i class="fas fa-calendar-alt"></i>🩺 Patients List</a><hr />
            <a onclick="ChangeView(3)"><i class="fas fa-calendar-alt"></i>👨‍⚕️ Doctors List</a><hr />
            <a onclick="ChangeView(4)"><i class="fas fa-calendar-alt"></i>📅 Appointments</a><hr />
            <a onclick="ChangeView(5)"><i class="fas fa-file-medical"></i>⚙️ Settings</a><hr />
            <a onclick="ChangeView(6)"><i class="fas fa-sign-out-alt"></i>🚪 Logout</a>
        </div>

  
        <div class="content">
            <asp:MultiView ID="MainMultiView" runat="server" ActiveViewIndex="0">
                
            
                <asp:View ID="HomeView" runat="server">
                    <div style="width:1000px;">
                        <h2 runat="server" id="pp" style="font-size:70px;">Welcome, </h2>
<br /><br /><hr />
                    </div>
                </asp:View>

                <asp:View ID="ProfileView" runat="server">
    <div class="container mt-4">
        <div class="card shadow-lg p-4" style="width:800px;height:400px;margin-left:110px;margin-top:95px;">
            <h3 class="text-center mb-4">Profile Information</h3>
            
            <div class="mb-3">
                <label for="txtFullName" class="form-label">Full Name:</label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="Enter your full name" Enabled="false"></asp:TextBox>
            </div>
            <br />
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" Placeholder="Enter your email" Enabled="false"></asp:TextBox>
            </div>
           
            <br /><br />
            
            <div class="text-center">
                <asp:Button ID="btnUpdate" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnUpdate_Click"/>
                <asp:Button ID="en" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="en_Click"/>

            </div>
        </div>
    </div>
</asp:View>
                <asp:View ID="PatientsView" runat="server">

    <nav class="navbar navbar-expand-lg navbar-light navbar-primary">
        <div class="collapse navbar-collapse" id="navbarNav">
            <div class="ml-auto">
                <div class="search-box">
                    <h4><asp:Label runat="server" ID="lblPatients" ForeColor="White"></asp:Label></h4>
                    <asp:TextBox ID="SearchPatientsTextBox" runat="server" CssClass="search-input" placeholder="Search here" />
                    <asp:Button ID="SearchPatientsButton" runat="server" class="search-icon" Text="&#128269;" CssClass="btn" OnClick="SearchPatientsButton_Click"/>
                </div>
            </div>
        </div>
    </nav>

    <hr /><br /><h2>🩺 Patients List</h2><br />

    <asp:GridView ID="gvPatients" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Patient ID" />
            <asp:BoundField DataField="FullName" HeaderText="Patient Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" />
            <asp:BoundField DataField="DateOfBirth" HeaderText="Date of Birth" DataFormatString="{0:yyyy-MM-dd}" />
        </Columns>
    </asp:GridView>

</asp:View>



                <asp:View ID="DoctorsView" runat="server">

    <nav class="navbar navbar-expand-lg navbar-light navbar-primary">
        <div class="collapse navbar-collapse">
            <div class="ml-auto">
                <div class="search-box">
                    <h4><asp:Label runat="server" ID="lblDoctors" ForeColor="White"></asp:Label></h4>
                    <asp:TextBox ID="SearchDoctorsTextBox" runat="server" CssClass="search-input" placeholder="Search here" />
                    <asp:Button ID="SearchDoctorsButton" runat="server" class="search-icon" Text="&#128269;" CssClass="btn" OnClick="SearchDoctorsButton_Click"/>
                </div>
            </div>
        </div>
    </nav>

    <hr /><br /><h2>👨‍⚕️ Doctors List</h2><br />

    <asp:GridView ID="gvDoctors" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Doctor ID" />
            <asp:BoundField DataField="FullName" HeaderText="Doctor Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" />
            <asp:BoundField DataField="Speciality" HeaderText="Speciality" />
        </Columns>
    </asp:GridView>

</asp:View>





                <asp:View ID="AppointmentsView" runat="server">
                    
                                        <nav class="navbar navbar-expand-lg navbar-light navbar-primary">
    
    <div class="collapse navbar-collapse" id="navbar Nav">
        
        <div class="ml-auto">
            <div class="search-box">
                <h4><asp:Label runat="server" ID="lblds" ForeColor="White"></asp:Label></h4>

                <asp:TextBox ID="SearchTextBox" runat="server" CssClass="search-input" placeholder="Search here" />
                <asp:Button ID="SearchButton" runat="server" class="search-icon" Text="&#128269;" CssClass="btn" OnClick="SearchButton_Click"/>
            </div>
        </div>
    </div>
</nav>
           <hr /><br /><h2>📅Appointments</h2><br />
                <asp:GridView ID="gvAppointments" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White">
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" />
        <asp:BoundField DataField="PatientName" HeaderText="Patient Name" />
        <asp:BoundField DataField="DoctorName" HeaderText="Doctor Name" />
        <asp:BoundField DataField="Speciality" HeaderText="Speciality" />
        <asp:BoundField DataField="AppointmentDate" HeaderText="Date & Time" DataFormatString="{0:yyyy-MM-dd HH:mm}" />  
        <asp:BoundField DataField="Status" HeaderText="Status" />
    </Columns>
</asp:GridView>


           </asp:View>









                                  <asp:View ID="SettingsView" runat="server">
    <div class="container mt-4">
        <div class="mb-3 text-center" style="width:400px;margin-left:260px;">
    <button type="button" onclick="toggleDarkMode()" class="btn btn-dark">
        🌙 Toggle Dark Mode
    </button>
</div>
        <div class="card shadow p-4" style="width:480px;height:450px;margin-left:230px;">
            

            
            <h4 class="text-secondary">🔒 Change Password</h4><br />
            <div class="mb-3">
                <label for="txtOldPassword" class="form-label fw-bold">Current Password:</label>
                <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtNewPassword" class="form-label fw-bold">New Password:</label>
                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtConfirmPassword" class="form-label fw-bold">Confirm New Password:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <br />
            <div class="d-grid">
                <asp:Button ID="btnChangePassword" runat="server" CssClass="btn btn-primary btn-lg" Text="Update Password" OnClick="btnChangePassword_Click" />
            </div>

            <asp:Label ID="Label1" runat="server" CssClass="text-success mt-3 fw-bold" Visible="false"></asp:Label>
        </div>
    </div>
</asp:View>


   <asp:View ID="LogoutView" runat="server">
    <div class="container d-flex justify-content-center align-items-center mt-5" style="margin-top:150px;margin-left:140px;width:750px;height:500px;">
        <div class="card shadow-lg p-4 text-center" style="width: 400px; border-radius: 15px;">
            <h2 class="mb-3 text-danger fw-bold">Are you sure you want to logout?</h2>

            <div class="position-relative">
                <img src="https://i.pinimg.com/736x/9e/b4/f7/9eb4f7fec297f41f40d161b5ef424c2c.jpg"
                     class="img-fluid"
                     style="width: 180px; height: 180px; object-fit: cover;"
                     onmouseover="this.style.backgroundColor='#c82333'; this.style.transform='scale(1.06)';"
                     onmouseout="this.style.backgroundColor='#dc3545'; this.style.transform='scale(1)';" />
            </div>

            <asp:Label runat="server" ID="logtxt" CssClass="d-block mt-3 text-muted fw-bold"></asp:Label>

<asp:Button ID="signout" runat="server" Text="Logout" CssClass="btn btn-danger fw-bold"
            OnClientClick="return logout();" style="margin-top: 20px; padding: 12px 30px; font-size: 18px; border-radius: 10px;
            transition: all 0.3s ease-in-out;"
            onmouseover="this.style.backgroundColor='#c82333'; this.style.transform='scale(1.06)';"
            onmouseout="this.style.backgroundColor='#dc3545'; this.style.transform='scale(1)';" />

        </div>
    </div>

  <div id="blackout" class="blackout">
    <div class="spinner-container">
        <div class="spinner">
    <div></div>
    <div></div>
    <div></div>
</div>

        <h2 class="text-light">Logging out...</h2>
    </div>
</div>

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
        function toggleDarkMode() {
            document.body.classList.toggle("dark-mode");

            let isDark = document.body.classList.contains("dark-mode");
            localStorage.setItem("darkMode", isDark);

          
            let elements = document.querySelectorAll(".container, .card, .content, .navbar");
            elements.forEach(el => {
                if (isDark) {
                    el.classList.add("dark-theme");
                } else {
                    el.classList.remove("dark-theme");
                }
            });
        }

        window.onload = function () {
            let isDark = localStorage.getItem("darkMode") === "true";
            if (isDark) {
                document.body.classList.add("dark-mode");

                let elements = document.querySelectorAll(".container, .card, .content, .navbar");
                elements.forEach(el => el.classList.add("dark-theme"));
            }
        };

    </script>

    <script type="text/javascript">
        window.history.forward();
        function preventBack() {
            window.history.forward();
        }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
   <script>
       function logout() {
           var blackout = document.getElementById("blackout");
           blackout.style.opacity = "1";
           blackout.style.pointerEvents = "auto";

           setTimeout(function () {
               document.getElementById('<%= signout.ClientID %>').click();

        window.location.href = "Login.aspx";
    }, 2000);

           return false;
       }
   </script>



</body>

</html>
