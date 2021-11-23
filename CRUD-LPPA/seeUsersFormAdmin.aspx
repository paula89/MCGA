<%@ Page Title="Users" Language="C#" MasterPageFile="~/CRUD-LPPA.Master" AutoEventWireup="true" CodeBehind="seeUsersFormAdmin.aspx.cs" Inherits="CRUD_LPPA.seeUsersFormAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" type="text/css" href="/Content/styleUsersForm.css">
    

    <div class="table-wrapper-scroll-y my-custom-scrollbar">
    <table class="table table-striped" id="usersTable" cellspacing="0";>
        <thead id="headContent">
            <tr id="usersAtributes">
                <th>ID</th>
                <th>Username</th>
                <th>Acción</th>
            </tr>
        </thead>
        <tbody id="rowContent">
        </tbody>
    </table>
    </div>
    <div class="form-group row">
          <div class="col-lg-3 text-center">
            <input type="button" class="btn btn-warning createUserBtn" value="Nuevo Usuario"  onclick="location.replace('addUserForm.aspx?user='+document.getElementById('userTitle').innerHTML); return false;" disabled="disabled">
          </div>
        </div>

    <script>
        $(document).ready(function(){
            $('#usersTable').DataTable({
                "scrollY": "50vh",
                "scrollCollapse": true,
                paging: false,
                ordering: false,
                searching: false,
                info: false,
                
                
            });
            $(".dataTables_empty").empty();

            retieveALL(true)

            evalAccessUser()
        })
    </script>
</asp:Content>
