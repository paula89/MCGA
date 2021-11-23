
let localURLP = 'https://localhost:44307/privilege'

async function requestGetallPrivileges() {
    let response = await fetch(localURLP, {
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' }

    })

    return response.json();

     
}
//genero un json con la lista de privilegios para evaluar/cargar en users
function jsonForCreateModif(tipo) {
    requestGetallPrivileges().then(returnedData => {

        var privJson = 'Privileges":['

        for (obj in returnedData) {

            var check = document.getElementById(returnedData[obj]["Id"])

            if (check.checked) {
                privJson +=   '{"id":' + returnedData[obj]["Id"] + ',"Description":"' + returnedData[obj]["Description"]+'"},'
            }

        }


        privJson = privJson.slice(0, privJson.length - 1);

        privJson += ']'

        if (tipo == "alta") {
            CreameEste(privJson)
        }
        else if (tipo == "modif") {
            ModficameEste(privJson)
        }
        

    })
}

function retrieveAllPrivileges() {
    requestGetallPrivileges().then(returnedData => {

        for (obj in returnedData) {
            rellenamelos(returnedData[obj]["Description"], returnedData[obj]["Id"]) 
        }

    })
}
//cargo checkboxes
function rellenamelos(Descripcion, ID) {
    var label = document.createElement("label")
    var checkbox = document.createElement("input")
    label.htmlFor = ID
    label.fo
    checkbox.id = ID
    checkbox.type = "checkbox"
    label.innerHTML = Descripcion
    label.append(checkbox)
    var x = document.getElementById("checkboxes")
    x.append(label)


}

//old carga
function rellenameLista(returnedData) {

    $("#rowContent tr").remove();
    var x = document.getElementById("rowContent")

    for (obj in returnedData) {
        var row = document.createElement("tr")
        console.log(returnedData[obj])
        for (let key in returnedData[obj]) {
            var cell = document.createElement("td")
            var input = document.createElement("input")
            input.classList.add("form-control")
            input.type = "text"
            input.disabled = true
            console.log(key)
            input.value = a[obj][key]
            cell.append(input)
            row.append(cell)
        }

        x.append(actionButtons(1, row))
    }

    x.append(actionButtons(1, row))
}

async function createPrivilege(description) {
    let response = await fetch(localURLP, {
        method: 'POST', //aclaro para que quede ordenado
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' },
        body: description // a definir
    })
    return response;
}

async function modifyPrivilege(privilegeID, description) {
    let response = await fetch(localURLP + "/" + privilegeID, {
        method: 'PUT', //aclaro para que quede ordenado
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' },
        body: description
    })
    return response.json();
}

async function deletePrivilege(privilegeID) {
    let response = await fetch(localURLP + "/" + privilegeID, {
        method: 'DELETE', //aclaro para que quede ordenado
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' },
    })
    return response.json();
}

//creo row
function addRow() {
    var x = document.getElementById("rowContent")
    var row = document.createElement("tr")
    for (var i = 0; i < 2; i++) {
        var cell = document.createElement("td")
        var input = document.createElement("input")
        input.classList.add("form-control")
        input.type = "text"
        cell.append(input)
        row.append(cell)
    }
    row.cells[0].querySelector('input').readOnly = true
    
    x.append(actionButtons(2, row))

    document.getElementById('newRowBtn').disabled = true
}

//cargo info en tabla
function GenerateTablePriv() {
    $("#rowContent tr").remove();
    var x = document.getElementById("rowContent")

    requestGetallPrivileges().then(returnedData => {

        for (obj in returnedData) {
            var row = document.createElement("tr")

            for (let key in returnedData[obj]) {
                var cell = document.createElement("td")
                var input = document.createElement("input")
                input.classList.add("form-control")
                input.type = "text"
                input.disabled = true
                console.log(key)
                input.value = returnedData[obj][key]
                cell.append(input)
                row.append(cell)
            }

            x.append(actionButtons(1, row))
        }
        evalAccessPriv()

    })

    

    
}

//genero los botones de m y d
function actionButtons(type, row1) {
    var cell = document.createElement("td")
    var botonC = document.createElement("input")
    var botonM = document.createElement("input")
    var botonD = document.createElement("input")
    var div = document.createElement("div")
    botonC.classList.add("btn", "btn-outline-success")
    botonM.classList.add("btn", "btn-outline-warning")
    botonD.classList.add("btn", "btn-outline-danger")
    botonC.type = "button"
    botonM.type = "button"
    botonD.type = "button"
    botonC.value = "Confirm"
    botonM.value = "M"
    botonD.value = "D"
    botonM.hidden = true
    if (type == 1) {
        botonC.hidden = true
        botonM.hidden = false
        botonM.disabled = true
        botonD.disabled = true
        botonM.classList.add("modifyPrivBtn")
        botonD.classList.add("deletePrivBtn")
        botonM.addEventListener("click", function (e) { modificar(e) })
        botonD.addEventListener("click", function (e) { borrar(e) })
    }
    else if (type == 2) {
        botonD.addEventListener("click", function (e) { cancelarCreacion(e) })
        botonC.addEventListener("click", function (e) { confirmarCreacion(e) })
    }
    else {
        botonD.addEventListener("click", function (e) { cancelarModify(e) })
        botonC.addEventListener("click", function (e) { confirmarModify(e) })
    }
    div.class = "btn-group"
    div.role = "group"
    div.append(botonC)
    div.append(botonM)
    div.append(botonD)
    cell.append(div)
    row1.append(cell)

    return row1
}


function cancelarCreacion(e) {
    
    var Index = e.target.closest("tr").rowIndex

    document.getElementById("permTable").deleteRow(Index)

    document.getElementById('newRowBtn').disabled = false
}

function confirmarCreacion(e) {
    var row = e.target.closest("tr")

    var ID = row.cells[0].getElementsByTagName('input')[0].value
    var DESC = row.cells[1].getElementsByTagName('input')[0].value

    DESC = '{"Description":"' + DESC+'"}'

    createPrivilege(DESC).then(response => { 
        if (response["status"] == 200) {
            row.deleteCell(2)
            row.append(actionButtons(1, row).cells[2])
            row.cells[0].getElementsByTagName('input')[0].disabled = true
            row.cells[1].getElementsByTagName('input')[0].disabled = true
            document.getElementById('newRowBtn').disabled = false
            Swal.fire(
                'Privilegio creado'
            ).then((result) => {
                if (result.isConfirmed) {
                    GenerateTablePriv()
                }
            })
        }
        else  {
            Swal.fire(
                'Hubo un error'
            )
        }
    })
}

function modificar(e) {

    var row = e.target.closest("tr")
    row.cells[1].getElementsByTagName('input')[0].disabled = false

    document.getElementById('newRowBtn').disabled = true

    row.deleteCell(2)

    row.append(actionButtons(3, row).cells[2])

}

//cancelar modificar
function cancelarModify(e) {

    GenerateTablePriv()

    document.getElementById('newRowBtn').disabled = false
}

//confirmar modificar
function confirmarModify(e) {
    var row = e.target.closest("tr")

    var ID = row.cells[0].getElementsByTagName('input')[0].value
    var DESC = row.cells[1].getElementsByTagName('input')[0].value

    DESC = 'Privileges:[{"Id":"'+ID+'","Description":"' + DESC + '"}]'

    row.deleteCell(2)

    row.append(actionButtons(1, row).cells[2])

    modifyPrivilege(ID,DESC).then(response => { 
    if (response == 1) {
        row.deleteCell(2)

        row.append(actionButtons(1, row).cells[2])
        row.cells[0].getElementsByTagName('input')[0].disabled = true
        row.cells[1].getElementsByTagName('input')[0].disabled = true
        document.getElementById('newRowBtn').disabled = false
        Swal.fire(
            'Privilegio creado'
        ).then((result) => {
            if (result.isConfirmed) {
                GenerateTablePriv()
            }
        })
    }
    else if (response == 0) {
        Swal.fire(
            'Hubo un error'
        )
    }
})
}

function borrar(e) {
    var row = e.target.closest("tr")

    var ID = row.cells[0].getElementsByTagName('input')[0].value

    confirmame(ID)

}


function confirmame(ID) {
    Swal.fire({
        title: 'Estas seguro que quiere eliminar el permiso?',
        text: "Esto no es revertible!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#38d39f',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Borrar!'
    }).then((result) => {
        if (result.isConfirmed) {

            deletePrivilege(ID).then(response => {
                if (response == 1) {
                    Swal.fire(
                        'Privilegio Borrado!',

                    ).then((result) => {
                        if (result.isConfirmed) {
                            GenerateTablePriv()
                        }
                    })
                }
                else if (response == 0){
                    Swal.fire(
                        'Hubo un error al borrar el Privilegio!',

                    ).then((result) => {
                        if (result.isConfirmed) {
                            GenerateTablePriv()
                        }
                    })
                }
            })
            

        }
    })
}