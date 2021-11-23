<%@ Page Title="Privileges" Language="C#" MasterPageFile="~/CRUD-LPPA.Master" AutoEventWireup="true" CodeBehind="privilegesForm.aspx.cs" Inherits="CRUD_LPPA.privilegesForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" type="text/css" href="/Content/styleUsersForm.css">
    <script type="text/javascript" src="/Scripts/LPPA/PrivilegesScript.js"></script>


<div class="table-wrapper-scroll-y my-custom-scrollbar">
    <table class="table table-striped" id="permTable" cellspacing="0";>
        <thead id="headContent">
            <tr id="usersAtributes">
                <th>ID</th>
                <th>Descripcion</th>
                <th>Acción</th>
            </tr>
        </thead>
        <tbody id="rowContent">
        </tbody>
    </table>
    </div>
    <div class="form-group row">
          <div class="col-lg-3 text-center">
            <input type="button" class="btn btn-warning createPrivBtn" value="Nuevo" id="newRowBtn" onclick="addRow();" disabled="disabled">
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

            GenerateTablePriv()
            
        })
    </script>


</asp:Content>
