<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CRUD_LPPA.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login - LPPACRUD</title>
    <link rel="stylesheet" type="text/css" href="/Content/styleLogin.css"/>
    <script src="https://kit.fontawesome.com/a81368914c.js"></script>
</head>
<body>
    <div class="login-content">
        <form id="form1" runat="server"> 
                <img alt= "" src="https://img.icons8.com/dotty/80/000000/change-user-male.png"/>
                	<h2 class="title">Bienvenido</h2>
           		<div class="input-div one">
           		   <div class="inputLogin">
           		   		<i class="fas fa-user"></i>
           		   </div>
           		   <div class="div">
           		   		<h5>Usuario</h5>
           		   		<input id="usuario" type="text" class="input"
                        required oninvalid="this.setCustomValidity('Debe ingresar su nombre de usuario')" oninput="this.setCustomValidity('')"/>
           		   </div>
           		</div>
           		<div class="input-div pass">
           		   <div class="inputLogin"> 
           		    	<i class="fas fa-lock"></i>
           		   </div>
           		   <div class="div">
           		    	<h5>Contraseña</h5>
           		    	<input id="password" type="password" class="input" required oninvalid="this.setCustomValidity('Debe ingresar su contraseña')" oninput="this.setCustomValidity('')"/>
            	   </div>
            	</div>
            	<a href="#">¿Olvidó su contraseña?</a>
            	<input type="submit" class="btn" value="Login" onclick="login();  return false;"/>
            </form>
        </div>
    <script type="text/javascript" src="\Scripts\LPPA\LoginScript.js"></script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
   
</body>
</html>
