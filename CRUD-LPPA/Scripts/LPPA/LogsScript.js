
var localURL = "https://localhost:44307/user"; // REVIEW

async function requestLogssALL() {   //traigo todos los logs y los relleno en la tabla
    let response = await fetch(localURL, {
        method: 'GET',
        headers: { 'token': localStorage.getItem("token"),'Content-Type':'application / json'}
    })
    return response.json()
}

function retieveAllLogs(type) {
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
    
                var div = document.createElement("div")
                
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
function TestTablesLogs(type) {
    $("#rowContent tr").remove();
    var x = document.getElementById("rowContent")


    let dData = '{"usuario1":{ "FechaHora": "1", "Usuario": "admin", "Accion": "test accion 1" }, "usuario2": { "FechaHora": "2", "Usuario": "pedro", "Accion": "test accion 2"}}'

    let a = JSON.parse(dData)

    console.log(a);


    for (obj in a) {



        let i = 0
        var row = document.createElement("tr")
        console.log(a[obj])
        //var row = x.insertRow(i-1)
        for (let key in a[obj]) {
            var cell = document.createElement("td")
            //console.log(key)
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








