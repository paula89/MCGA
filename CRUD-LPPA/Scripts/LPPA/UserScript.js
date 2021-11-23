
var localURL =  "https://localhost:44307/user"

async function requestUsersALL() {   //traigo todos los usr y los relleno en la tabla
    let response = await fetch(localURL, {
        method: 'GET',
        headers: { 'token': localStorage.getItem("token"),'Content-Type':'application / json'}
    })
    return response.json()
}

function retieveALL(type) {
        requestUsersALL().then(returnedData => { 

        $("#rowContent tr").remove();
        var x = document.getElementById("rowContent")

        for (obj in returnedData) {
            let i = 0
            var row = document.createElement("tr")
            console.log(returnedData[obj])
            for (let key in returnedData[obj]) {
                var cell = document.createElement("td")
                console.log(key)
                cell.innerHTML = returnedData[obj][key]
                row.append(cell)
            }
            if (type) {
                var cell = document.createElement("td")
                var botonM = document.createElement("input")
                var botonD = document.createElement("input")
                var div = document.createElement("div")
                botonM.classList.add("btn", "btn-outline-warning","modifyUserBtn")
                botonD.classList.add("btn", "btn-outline-danger","deleteUsersBtn")
                botonM.type = "button"
                botonD.type = "button"
                botonM.value = "M"
                botonD.value = "D"
                botonM.disabled = true
                botonD.disabled = true
                botonM.addEventListener("click", function (e) { location.replace('manageUsersForm.aspx?user=' + document.getElementById('userTitle').innerHTML + '&userID=' + getID(e)) })
                botonD.addEventListener("click", function (e) { confirmameDelete(type, getID(e)) })
                div.class = "btn-group"
                div.role = "group"
                div.append(botonM)
                div.append(botonD)
                cell.append(div)
                row.append(cell)
            }
            x.append(row)

        }
    })
}

async function requestGetOne(userID) {
    let response = await fetch(localURL+"/"+userID,{
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' }
    })
    return response.json()
}



function retrieveOne(userID) {
 
    requestGetOne(userID).then(returnedData => { 
        for (obj in returnedData) {


            switch (obj) {
                case "Id": document.getElementById("MainContent_id").value = returnedData[obj]
                    break;
                case "Username": document.getElementById("Username").value = returnedData[obj]
                    break;
                case "Salt": document.getElementById("Salt").value = returnedData[obj]
                    break;
                case "Privileges": for (atr of returnedData[obj]) { chequeamelos(atr["Id"] )}
                    break;
            }

        }


    })
}

function chequeamelos(ID) {
    document.getElementById(ID).checked = true
}


                                            


async function createUser(userdata) {       
    let response = await fetch(localURL,{
        method: 'POST', //aclaro para que quede ordenado
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' },
        body: userdata 
    })
    return response.json()
}

function CreameEste(priv) {

    if (checkPSW()) {
        let x = '{"Username":"' + document.getElementById("Username").value + '","Salt":"' + document.getElementById("Salt").value + '","' + priv + ',"Password":"' + document.getElementById("password").value + '"}'

        console.log(x)
        createUser(x).then(response => {
            if (response == 0) {
                Swal.fire(
                    'Hubo un error'
                )
            }
            else if (response == 1) {
                Swal.fire(
                    'Usuario creado'
                ).then((result) => {
                    if (result.isConfirmed) {
                        location.replace('seeUsersFormAdmin.aspx?user=' + document.getElementById('userTitle').innerHTML)
                    }
                })
            }
        })
    }
  
}

async function modifyUser(userID, userdata) {
    let response = await fetch(localURL + "/" +userID,{
        method: 'PUT', //aclaro para que quede ordenado
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' },
        body: userdata 
    })
    return response.json();

}

//genero jason y llamo segun un valor
function ModficameEste(priv) {
    if (checkPSW()) {
        let x = '{"Username":"' + document.getElementById("Username").value + '","Salt":"' + document.getElementById("Salt").value + '","' + priv + ',"Password":"' + document.getElementById("password").value + '"}'

        modifyUser(document.getElementById("MainContent_id").value, x).then(response => {
                if (response == 0) {
                    Swal.fire(
                        'Hubo un error'
                    )
                }
                else if (response == 1) {
                    Swal.fire(
                        'Usuario modificado'
                    ).then((result) => {
                        if (result.isConfirmed) {
                            location.replace('seeUsersFormAdmin.aspx?user=' + document.getElementById('userTitle').innerHTML)
                        }
                    })
                }
            
        })

    }
}

function checkPSW() {
    if (document.getElementById('password').value === "" || document.getElementById('password2').value === "") {
        Swal.fire(
            'Rellena las Claves!',
        ).then(result => {
            if (result.isConfirmed) {
                return false
            }
        })


    }
    else {
        if (document.getElementById('password').value == document.getElementById('password2').value) {
            return true
        }
        else {
            Swal.fire(
                'Las claves difieren!',
            ).then(result => {
                if (result.isConfirmed) {
                    return false
                }
            })
        }
    }
    return false
}

async function requestDelUser(userID) {
    let response = await fetch(localURL + "/" +userID,{
        method: 'DELETE', //aclaro para que quede ordenado
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' },
    })
    return response.json();
}



//confirmacion de Delete
function confirmameDelete(type, user) {
    Swal.fire({
        title: 'Estas seguro que quiere eliminar al usuario?',
        text: "Esto no es revertible!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#38d39f',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Borrar!'
    }).then((result) => {
        if (result.isConfirmed) {
            console.log(user)
            requestDelUser(user).then(response => {
            
                if (response == 1) {
                    Swal.fire(
                        'Borrado!',
                    ).then((result) => {
                        if (result.isConfirmed) {
                            console.log(type)
                            retieveALL(true)
                        }
                    })
                }
                else if (response == 0){
                    Swal.fire(
                        'Hubo un error!',
                    ).then((result) => {
                        if (result.isConfirmed) {
                            retieveALL(true)
                        }
                    })
                }
                

            })

        }
    })
}


//Habilita los botones

function enableAccess() {
    var i = localStorage.getItem('privileges')
    let priviliges = i.split(",")
    for (x in priviliges) {
        switch (priviliges[x]) {
            case "1": //Leer
                document.getElementById('User').hidden = false
                break;
            case "2": //admin
                document.getElementById('Admin').hidden = false
                document.getElementById('User').hidden = true
                break;
            case "5"://super admin
                document.getElementById('User').hidden = true
                document.getElementById('Admin').hidden = false
                document.getElementById('SuperAdmin').hidden = false
                break;
        }
    }

    
}

// recivo un id de privilegio y evalua si lo tiene
function evalAccessUser() {

    var i = localStorage.getItem('privileges')
    let priviliges = i.split(",")
    for (x in priviliges) {
        
       switch (priviliges[x]) {
           case "2": //create usr
               document.querySelector('.createUserBtn').removeAttribute('disabled')
               break;

       }
        
    }
}

function evalAccessPriv() {

    var i = localStorage.getItem('privileges')
    let priviliges = i.split(",")

    

    
    for (x in priviliges) {

        switch (priviliges[x]) {
            case "6": //create priv
                document.querySelector('.createPrivBtn').removeAttribute('disabled')
                break;
            case "7": //modify priv
                buscarBoton('.modifyPrivBtn')
                break;
            case "8"://delete priv
                buscarBoton('.deletePrivBtn')
                break;
        }

    }
}

function buscarBoton(str) {
    content = document.getElementById('rowContent')
    
    var tds = document.querySelectorAll('#permTable tbody tr')
    for (i = 0; i < tds.length; ++i) {
        content.rows[i].cells[2].querySelector(str).removeAttribute('disabled')
    }
}


function evalToken() {
    if (localStorage.getItem('token') != null) {
 
        var x = Date.parse(localStorage.getItem('timeStamp'))
        
        if (x > Date.parse(Date())) {

        }
        else {
            
            logout()
            
        }
    }
    else {
        
        logout()
    }
}

//test sin backend de retrieveall
function TestTables(type) {
    $("#rowContent tr").remove();
    var x = document.getElementById("rowContent")


    let dData = '{"usuario1":{ "id": "1", "nombre": "juAan", "apellido": "flori" , "PRIVILEGIOS":"1234"}, "usuario2": { "id": "2", "nombre": "pedro", "apellido": "flori", "PRIVILEGIOS":"1234"}}'

    let a = JSON.parse(dData)

    //console.log(a)


    for (obj in a) {



        let i = 0
        var row = document.createElement("tr")
        console.log(a[obj])
        //var row = x.insertRow(i-1)
        for (let key in a[obj]) {
            var cell = document.createElement("td")
            console.log(key)
            //var cell = row.insertCell(i)
            cell.innerHTML = a[obj][key]
            //i++
            row.append(cell)
        }
        if (type == true) {
            var cell = document.createElement("td")
            var botonM = document.createElement("input")
            var botonD = document.createElement("input")
            var div = document.createElement("div")
            botonM.classList.add("btn", "btn-outline-warning")
            botonD.classList.add("btn", "btn-outline-danger")
            botonM.type = "button"
            botonD.type = "button"
            botonM.value = "M"
            botonD.value = "D"
            botonM.addEventListener("click", function (e) { location.replace('manageUsersForm.aspx?user=' + document.getElementById('userTitle').innerHTML + '&userID=' + getID(e)) })
            botonD.addEventListener("click", function () { confirmameDelete(type) })
            div.class = "btn-group"
            div.role = "group"
            div.append(botonM)
            div.append(botonD)
            cell.append(div)
            row.append(cell)
        }
        x.append(row)
    }

}

//me trae el id de la primera row
function getID(e) {
    console.log(e.target)
    var row1 = e.target.closest("tr")

    return row1.cells[0].innerHTML
}








