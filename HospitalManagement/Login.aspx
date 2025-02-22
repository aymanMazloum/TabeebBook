﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HospitalManagement.Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login - Hospital Management</title>
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
            <h3>Login</h3>
            <br />
            <form id="form1" runat="server">
                <div class="mb-3">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter your email"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Enter your password"></asp:TextBox>
                </div>
                <div class="d-grid">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-2"></asp:Label>
            </form>
            <p class="mt-3">Don't have an account? <a href="SignUp.aspx">Sign up</a></p>
        </div>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
