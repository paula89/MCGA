<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/CRUD-LPPA.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRUD_LPPA._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <link rel="stylesheet" type="text/css" href="/Content/StyleDefault.css"/>

    <div class="jumbotron">
        <h1>Bienvenido</h1>
        <p class="lead">A la pagina de inicio de nuestro sistema de CRUD de usuarios y privilegios para el trabajo practico grupal de LPPA</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Introducción</h2>
            <p>
                En el menu de la izquierda tendra los accesos a las diferentes pantallas de la web y al final tienes el boton de logout para cerrar la session de tu cuenta.
                Si quieres aprender mas sobre el uso de la web haga click en el boton debajo para acceder al manual.
            </p>
            <p>
                <a class="btn btn-default" href="./GCU-GUIDE.pdf" target="_blank">Manual &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Documentación</h2>
            <p>
                Si tienes interes en el desarrollo de esta web te dejo la documentación aqui abajo.
            </p>
            <p>
                <a class="btn btn-default" href="/TPGRUPAL-LPPA-GCU.pdf" target="_blank">Documentación &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Contactos</h2>
            <p>
                Para mas informacion no dudes en contactarnos...
            </p>
            <p>
                <a class="btn btn-default" onclick="location.replace('Contact.aspx?user='+document.getElementById('userTitle').innerHTML);  return false;">Contactanos &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
