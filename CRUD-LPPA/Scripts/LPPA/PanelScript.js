
var localURL = "https://localhost:44307/user"; // TO DO

async function requestPanelALL() {   //traigo todos los bultos y los relleno en la tabla
    let response = await fetch(localURL, {
        method: 'GET',
        headers: { 'token': localStorage.getItem("token"),'Content-Type':'application / json'}
    })
    return response.json()
}

function retieveAllPanel(type) {
        requestUsersALL().then(returnedData => { 

        $("#rowContent tr").remove();
        var x = document.getElementById("rowContent")

            for (obj in returnedData) {

                var row = document.createElement("tr")

                for (let key in returnedData[obj]) {
                    var cell = document.createElement("td")
                    var cellInner = document.createElement("td")

                    cellInner.innerHTML = returnedData[obj][key]

                    var botonHabilitar = document.createElement("input")
                    var div = document.createElement("div")

                    cellInner.innerHTML !== "true"
                        ? botonHabilitar.classList.add("btn", "btn-primary") : botonHabilitar.classList.add("btn", "btn-danger")
                    botonHabilitar.type = "button"
                    cellInner.innerHTML === "true" ? botonHabilitar.value = "Apagar" : botonHabilitar.value = "Encender"
                    botonHabilitar.disabled = false
                    botonHabilitar.addEventListener("click", function (e) { location.replace('manageUsersForm.aspx?user=' + document.getElementById('userTitle').innerHTML + '&userID=' + getID(e)) })
                    cell.className = "btn-center"
                    div.role = "group"
                    div.append(botonHabilitar)
                    cell.append(div)
                    row.append(cell)
                }
                x.append(row)
            }
    })
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
function TestTablesPanel() {
    $("#rowContent tr").remove();
    var x = document.getElementById("rowContent")


    let dData = '{"Sensores":{ "cinta": true, "brazo": false,"pensa":true}}'

    let a = JSON.parse(dData)

    console.log(a);


    for (obj in a) {

        var row = document.createElement("tr")
 
        for (let key in a[obj]) {
            var cell = document.createElement("td")
            var cellInner = document.createElement("td")
        
            cellInner.innerHTML = a[obj][key]

            var botonHabilitar = document.createElement("input")
            var div = document.createElement("div")
            
            cellInner.innerHTML !== "true"
                ? botonHabilitar.classList.add("btn", "btn-primary") : botonHabilitar.classList.add("btn", "btn-danger")
            botonHabilitar.type = "button"
            cellInner.innerHTML === "true" ? botonHabilitar.value = "Apagar" : botonHabilitar.value = "Encender"
            botonHabilitar.disabled = false
            botonHabilitar.addEventListener("click", function (e) { location.replace('manageUsersForm.aspx?user=' + document.getElementById('userTitle').innerHTML + '&userID=' + getID(e)) })
            cell.className = "btn-center"
            div.role = "group"
            div.append(botonHabilitar)
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








