<%@ Page Title="Panel de control" Language="C#" MasterPageFile="~/CRUD-LPPA.Master" AutoEventWireup="true" CodeBehind="seePanelForm.aspx.cs" Inherits="CRUD_LPPA.seeLogsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link rel="stylesheet" type="text/css" href="./Content/stylePanelForm.css">
    <script type="text/javascript" src="./Scripts/LPPA/PanelScript.js"></script>


    
    <div class="table-wrapper-scroll-y my-custom-scrollbar">
    <table class="table table-striped" id="usersTable" cellspacing="0";>
        <thead id="headContent">
            <tr id="usersAtributes" class="panel-titles">
                <th>CINTA</th>
                <th>BRAZO</th>
                <th>PRENSA</th>
                <th>Sensor Prensa En uso</th>
                <th>Posición Sensor Prensa</th>
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

                retrieveAllPanel()
                //TestTablesPanel()
                //retieveAllPanel(true)

               // evalAccessUser()
            })
        </script>
</asp:Content>
