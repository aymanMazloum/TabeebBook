<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HospitalManagement.Home" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Home - Patient Dashboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin="anonymous"></script>
    <style>
        body {
            background: linear-gradient(135deg, #f4f7f6, #dfe9f3);
            font-family: 'Arial', sans-serif;
        }
        .container {
            margin-top: 50px;
        }
        .card {
            border-radius: 15px;
            transition: transform 0.3s ease-in-out;
        }
        .card:hover {
            transform: scale(1.05);
        }
        .welcome {
            font-size: 24px;
            font-weight: bold;
            color: #007bff;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="container">
            <h2 class="text-center welcome">Welcome, <asp:Label ID="lblPatientName" runat="server" Text="John Doe"></asp:Label>!</h2>
            <div class="row mt-4">
                <div class="col-md-6">
                    <div class="card shadow-sm p-3 mb-5 bg-white rounded">
                        <div class="card-body">
                            <h5 class="card-title text-primary">Upcoming Appointment</h5>
                            <p><i class="fas fa-user-md"></i> Doctor: <asp:Label ID="lblDoctor" runat="server" Text="Dr. Smith"></asp:Label></p>
                            <p><i class="far fa-calendar-alt"></i> Date: <asp:Label ID="lblDate" runat="server" Text="July 10, 2025"></asp:Label></p>
                            <p><i class="fas fa-check-circle text-success"></i> Status: <asp:Label ID="lblStatus" runat="server" Text="Confirmed"></asp:Label></p>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card shadow-sm p-3 mb-5 bg-white rounded">
                        <div class="card-body">
                            <h5 class="card-title text-success">Health Overview</h5>
                            <p><i class="fas fa-heartbeat text-danger"></i> Last Checkup: <asp:Label ID="lblLastCheckup" runat="server" Text="June 20, 2025"></asp:Label></p>
                            <p><i class="fas fa-pills text-warning"></i> Prescription: <asp:Label ID="lblPrescription" runat="server" Text="Ibuprofen"></asp:Label></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
