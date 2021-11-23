<%@ Page Title="Users" Language="C#" MasterPageFile="~/CRUD-LPPA.Master" AutoEventWireup="true" CodeBehind="addUserForm.aspx.cs" Inherits="CRUD_LPPA.addUserForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
	<script type="text/javascript" src="/Scripts/LPPA/PrivilegesScript.js"></script>
		<link rel="stylesheet" type="text/css" href="./Content/styleManageUsers.css">
		<script type="text/javascript" src="/Scripts/LPPA/UserScript.js"></script>
	<div class="container">
		

		

				<div class="card">
					<div class="card-header">
						<h4>Información del usuario</h4>
					</div>
					<div class="card-body">
							<div class="form-group row">
								<label  class="col-lg-3 col-form-label form-control-label">ID</label>
								<div class="col-lg-9">
									<input runat="server" class="form-control" type="text" id="id" readonly>
								</div>
							</div>
							<div class="form-group row">
								<label class="col-lg-3 col-form-label form-control-label">Username</label>
								<div class="col-lg-9">
									<input class="form-control" type="text" id="Username">	
								</div>
							</div>
						<div class="form-group row">
								<label class="col-lg-3 col-form-label form-control-label">Descripcion</label>
								<div class="col-lg-9">
									<input class="form-control" type="text" id="Salt">
								</div>
							</div>
							<div class="form-group row">
								<label class="col-lg-3 col-form-label form-control-label">Privilegios</label>
								<div class="col-lg-9">
									<div class="multiselect">
										<div class="selectBox" onclick="showCheckboxes()">
										  <select>
											<option>Seleccione Privilegios</option>
										  </select>
										  <div class="overSelect"></div>
										</div>
										<div id="checkboxes">
										  
											
										</div>
									  </div>
								</div>
							</div>
							<div class="form-group row">
								<label class="col-lg-3 col-form-label form-control-label" >Contraseña</label>
								<div class="col-lg-9">
									<input class="form-control" type="password" id="password">
								</div>
							</div>
							<div class="form-group row">
								<label class="col-lg-3 col-form-label form-control-label" >Confirme la contraseña</label>
								<div class="col-lg-9">
									<input class="form-control" type="password" id="password2">
								</div>
							</div>
							<div class="form-group row">
								<div class="col-lg-12 text-center">
									<input type="reset" class="btn btn-secondary" value="Cancelar" onclick="location.replace('seeUsersFormAdmin.aspx?user='+document.getElementById('userTitle').innerHTML); return false;">
									<input type="button" class="btn btn-primary" value="Guardar" onclick="jsonForCreateModif('alta');">
								</div>
							</div>
					</div>
				</div>
            </div>

	<script>
        window.onload = function () {
			retrieveAllPrivileges()
            
		};

        var expanded = false;

        function showCheckboxes() {
            var checkboxes = document.getElementById("checkboxes");
            if (!expanded) {
                checkboxes.style.display = "block";
                expanded = true;
            } else {
                checkboxes.style.display = "none";
                expanded = false;
            }
        }

    </script>
</asp:Content>
