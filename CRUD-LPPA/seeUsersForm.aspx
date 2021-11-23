<%@ Page Title="Users" Language="C#" MasterPageFile="~/CRUD-LPPA.Master" AutoEventWireup="true" CodeBehind="seeUsersForm.aspx.cs" Inherits="CRUD_LPPA.seeUsersForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link rel="stylesheet" type="text/css" href="./Content/styleUsersForm.css">
    

    
    <div class="table-wrapper-scroll-y my-custom-scrollbar">
    <table class="table table-striped" id="usersTable" cellspacing="0";>
        <thead id="headContent">
            <tr id="usersAtributes">
                <th>ID</th>
                <th>Username</th>
            </tr>
        </thead>
        <tbody id="rowContent">
        </tbody>
    </table>
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

            retieveALL(false)

            evalAccessUser()
        })
        </script>
</asp:Content>
