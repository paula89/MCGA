<%@ Page Title="Logs" Language="C#" MasterPageFile="~/CRUD-LPPA.Master" AutoEventWireup="true" CodeBehind="seeLogsForm.aspx.cs" Inherits="CRUD_LPPA.seeLogsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link rel="stylesheet" type="text/css" href="./Content/styleUsersForm.css">
    <script type="text/javascript" src="./Scripts/LPPA/LogsScript.js"></script>


    
    <div class="table-wrapper-scroll-y my-custom-scrollbar">
    <table class="table table-striped" id="usersTable" cellspacing="0";>
        <thead id="headContent">
            <tr id="logsAtributes">
                <th>Fecha</th>
                <th>Usuario</th>
                <th>Acción</th>
            </tr>
        </thead>
        <tbody id="rowContent">
        </tbody>
    </table>
    </div>
    

        <script>
            $(document).ready(function () {
                $('#logsTable').DataTable({
                    "scrollY": "50vh",
                    "scrollCollapse": true,
                    paging: false,
                    ordering: false,
                    searching: false,
                    info: false,


                });
                $(".dataTables_empty").empty();

                TestTablesLogs(false)

               // evalAccessUser()
            })
        </script>
</asp:Content>
