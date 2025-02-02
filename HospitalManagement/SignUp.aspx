<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="HospitalManagement.SignUp" %>

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
            <h3>Sign Up</h3>
            <br />
            <form>
                <div class="mb-3">
                
                    <input type="text" class="form-control" id="fullname" placeholder="Enter your full name">
                </div>
                <div class="mb-3">
                    
                    <input type="email" class="form-control" id="email" placeholder="Enter your email">
                </div>
                <div class="mb-3">
                   
                    <input type="password" class="form-control" id="password" placeholder="Create a password">
                </div>
                 <div class="mb-3">
    <select class="form-control">
   <option>Select your role</option>
       <option>Doctor</option>
       <option>Patient</option>
       <option>Admin</option>

   </select>
 </div>
   
                <button type="submit" class="btn btn-success w-100" onClick="Sign">Sign Up</button>
            </form>
            <p class="mt-3">Already have an account? <a href="Login.aspx">Login</a></p>
        </div>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

