<%@ Page Title="Logs" Language="C#" MasterPageFile="~/CRUD-LPPA.Master" AutoEventWireup="true" CodeBehind="seeGraphicForm.aspx.cs" Inherits="CRUD_LPPA.seeLogsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link rel="stylesheet" type="text/css" href="./Content/styleGraphicForm.css">
    <script type="text/javascript" src="./Scripts/LPPA/GraphicScript.js"></script>


    
    <div class="table-wrapper-scroll-y my-custom-scrollbar">
    <table class="table table-striped" id="usersTable" cellspacing="0";>
        <thead id="headContent">
            <tr id="graphicColumns">
                <th>Cantidad en unidades</th>                
            </tr>
            <tr id="usersAtributes" class="graphic-td">
                <td>Bultos ingresados</td>
                <td>Bultos en proceso de prensado</td>
                <td>Bultos apilados</td>
            </tr>
        </thead>
        <tbody id="rowContent">
        </tbody>
    </table>
    </div>
    

        <script>
            $(document).ready(function () {
                $('#bultosTable').DataTable({
                    "scrollY": "50vh",
                    "scrollCollapse": true,
                    paging: false,
                    ordering: false,
                    searching: false,
                    info: false,


                });
                $(".dataTables_empty").empty();

                retieveAllGraphics();
                //TestTablesGraphic(false)

               // evalAccessUser()
            })
        </script>
</asp:Content>
