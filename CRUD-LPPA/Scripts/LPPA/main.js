
const inputs = document.querySelectorAll(".input");


function addcl(){
	let parent = this.parentNode.parentNode;
	parent.classList.add("focus");
}

function remcl(){
	let parent = this.parentNode.parentNode;
	if(this.value == ""){
		parent.classList.remove("focus");
	}
}


inputs.forEach(input => {
	input.addEventListener("focus", addcl);
	input.addEventListener("blur", remcl);
});

function validateInputs(usuario, password) {
	// db + privilegios
	//usuario || password === '' ? false : validar contra la db
	//let response = llamar loginDB(usuario, contraseña);
	//	response === 0 ? errorEnPantalla : 'users.html'
}

function login(){
	let usuario = document.getElementById('usuario').value;
	let password = document.getElementById('password').value;
	console.log(usuario + password)
	// redirige a la pantalla principal 
	window.location.href = '/users.html';
}

