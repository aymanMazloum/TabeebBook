<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="HospitalManagement.Index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Hospital Management</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
    <style>
             body {
         height: 100vh;
         display: flex;
         flex-direction: column;
         overflow: hidden;
         background: linear-gradient(135deg, #e0c3fc, #8ec5fc);
         animation: bgAnimation 10s infinite alternate ease-in-out;
         position: relative;
     }

     @keyframes bgAnimation {
         0% { filter: hue-rotate(0deg); }
         100% { filter: hue-rotate(30deg); }
     }

     .glow {
         position: absolute;
         width: 100%;
         height: 100%;
         background: radial-gradient(circle, rgba(255, 255, 255, 0.1) 10%, transparent 70%);
         mix-blend-mode: overlay;
         animation: moveGlow 6s infinite alternate ease-in-out;
     }

     @keyframes moveGlow {
         from { transform: translateY(-10%) scale(1.1); }
         to { transform: translateY(10%) scale(1); }
     }

     .navbar {
         background: rgba(255, 255, 255, 0.2) !important;
         backdrop-filter: blur(10px);
         box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
     }

     .card {
         background: rgba(255, 255, 255, 0.85);
         border-radius: 15px;
         padding: 20px;
         transition: transform 0.3s ease-in-out;
     }

     .card:hover {
         transform: scale(1.05);
         box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.2);
     }

     .btn {
         transition: transform 0.3s ease-in-out, background 0.3s;
     }

     .btn:hover {
         transform: scale(1.1);
         background: linear-gradient(90deg, #6a11cb, #2575fc) !important;
         color: white !important;
     }
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
        .nav-link{
         color:white;

        }
        .nav-link:hover{
            color:gold;
            font-size:large;
        }
    </style>
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark">
        <div class="container">
            <a class="navbar-brand" href="Index.aspx">
                <i class="fas fa-hospital-alt"></i> Hospital Management
            </a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="Login.aspx">Login</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="SignUp.aspx">Sign Up</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>

    <div class="container d-flex justify-content-center align-items-center flex-grow-1">
        <div class="card shadow p-4 text-center" style="width: 400px;">
            <h3>Welcome to Hospital Management</h3>
            <br /><br />
            <div class="d-grid gap-2">
                <a href="Login.aspx" class="btn btn-primary">Login</a>
                <a href="SignUp.aspx" class="btn btn-success">Sign Up</a>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
