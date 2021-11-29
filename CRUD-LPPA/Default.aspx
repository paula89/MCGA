<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/CRUD-LPPA.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRUD_LPPA._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <link rel="stylesheet" type="text/css" href="/Content/StyleDefault.css"/>

    <div class="jumbotron">
        <h1>Bienvenido</h1>
        <p class="lead">A la página de inicio de nuestro panel de control para el trabajo práctico grupal de MCGA</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Introducción</h2>
            <p>
                En el menú de la izquierda tendrá los accesos a las diferentes pantallas de la web y al final el botón de logout para cerrar la sessión de tu cuenta.
                Si quiere aprender mas sobre el uso de la web, haga click en el botón debajo para acceder al manual.
            </p>
            <p>
                <a class="btn btn-default" href="./Manual de Ayuda/Ayuda.chm" target="_blank">Manual &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Documentación</h2>
            <p>
                Si tiene interés en el desarrollo de esta web, la documentación está aqui debajo.
            </p>
            <p>
                <a class="btn btn-default" href="/TP GRUPAL - MCGA.pdf" target="_blank">Documentación &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Contactos</h2>
            <p>
                Para mas información no dudes en contactarnos...
            </p>
            <p>
                <a class="btn btn-default" onclick="location.replace('Contact.aspx?user='+document.getElementById('userTitle').innerHTML);  return false;">Contactanos &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
<%--  --%>