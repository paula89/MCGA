


async function loginRequest(userName, password) {
    let response = await fetch('https://localhost:44307/user/LogIn', {
        
        method: 'POST', 
        body: '{"UserName":"' + userName + '","Password":"' + password + '"}',
        headers: { 'Content-Type': 'application/json' }
    })

    return response.json()
}


function logout() {
    localStorage.clear()
    location.replace('Index.aspx')
}




const inputs = document.querySelectorAll(".input");


function addcl() {
    let parent = this.parentNode.parentNode;
    parent.classList.add("focus");
}

function remcl() {
    let parent = this.parentNode.parentNode;
    if (this.value == "") {
        parent.classList.remove("focus");
    }
}


inputs.forEach(input => {
    input.addEventListener("focus", addcl);
    input.addEventListener("blur", remcl);
});

function validateInputs(usuario, password) {

    loginRequest(usuario, password).then(returnedData => {

        if (returnedData == '0') {
            Swal.fire('Usuario o Clave erronea')
        }
        else {
            var priv = ""
            localStorage.setItem("token", returnedData["Token"])
            localStorage.setItem("timeStamp", returnedData["TimeStamp"])
            for (i in returnedData["Privileges"]) {
                priv += returnedData["Privileges"][i]['Id'] + ","
            }
            localStorage.setItem("privileges", priv)
            location.replace("Default.aspx?user=" + usuario)
        }
    })
}

function login() {
    let usuario = document.getElementById('usuario').value;
    let password = document.getElementById('password').value;
    console.log(usuario + password)
    validateInputs(usuario, password)
    
}

