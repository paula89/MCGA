
var localURL = "https://localhost:44307";

//orden  = cinta brazo prensa
async function requestPanelALL() {   //traigo todos los estados de los sensores y los relleno en la tabla
    let response = JSON.parse('{ "cinta": false, "brazo": false, "prensa":false}')

    await fetch(localURL + "/band/GetBandStatus", {
        method: 'GET',
        headers: { 'Content-Type': 'application / json' }
    }).then((res) =>
        res.json()
    ).then(j => {
        j === 1 ? response['cinta'] = true : response['cinta'] = false
    })


    await fetch(localURL + "/arm/GetArmStatus", {
        method: 'GET',
        headers: { 'Content-Type': 'application / json' }
    }).then((res) =>
        res.json()
    ).then(j => {
        j === 1 ? response['brazo'] = true : response['brazo'] = false
            })

    await fetch(localURL + "/press/GetPressStatus", {
        method: 'GET',
        headers: { 'Content-Type': 'application / json' }
    }).then((res) =>
        res.json()
    ).then(j => {
        j === 1 ? response['prensa'] = true : response['prensa'] = false
    })

    return response
}

async function requestPanel(sensor, estado) {
    var path = ''
    switch (sensor) {
        case 'prensa':
            estado === 'Encender' ? path = '/press/TurnOnPress' : path = '/press/TurnOffPress'
            break;
        case 'brazo':
            estado === 'Encender' ? path = '/arm/TurnOnArm' : path = '/arm/TurnOffArm'
            break;
        case 'cinta':
            estado === 'Encender' ? path = '/band/TurnOnBand' : path = '/band/TurnOffBand'
            break;
        
    }

    let response = await fetch(localURL + path, { // enviar el sensor para actualizar
        method: 'POST', 
        headers: { 'Content-Type': 'application / json' },
    })
    return response.json();
}

function retrieveAllPanel() {
    requestPanelALL().then(returnedData => {
        $("#rowContent tr").remove();
        var rowContent = document.getElementById("rowContent")
        var row = document.createElement("tr")

        for (obj in returnedData) {
            var cell = document.createElement("td")
            var cellInner = document.createElement("td")

            cellInner.innerHTML = returnedData[obj]
            var botonHabilitar = null

            switch (obj) {
                case 'cinta':
                    var botonHabilitarCinta = document.createElement("input")
                    botonHabilitarCinta.id = "BotonHabilitarCinta"
                    cellInner.innerHTML !== "true"
                    ? botonHabilitarCinta.classList.add("btn", "btn-primary") : botonHabilitarCinta.classList.add("btn", "btn-danger")
                    botonHabilitarCinta.type = "button"
                    cellInner.innerHTML === "true" ? botonHabilitarCinta.value = "Apagar" : botonHabilitarCinta.value = "Encender"
                    botonHabilitarCinta.disabled = false
                    botonHabilitarCinta.addEventListener("click", function (e) { confirmPopUp(botonHabilitarCinta.value, "cinta") })

                    botonHabilitar = botonHabilitarCinta
                    break;
                case 'brazo':
                    var botonHabilitarBrazo = document.createElement("input")
                    botonHabilitarBrazo.id = "BotonHabilitarBrazo"
                    cellInner.innerHTML !== "true"
                    ? botonHabilitarBrazo.classList.add("btn", "btn-primary") : botonHabilitarBrazo.classList.add("btn", "btn-danger")
                    botonHabilitarBrazo.type = "button"
                    cellInner.innerHTML === "true" ? botonHabilitarBrazo.value = "Apagar" : botonHabilitarBrazo.value = "Encender"
                    botonHabilitarBrazo.disabled = false
                    botonHabilitarBrazo.addEventListener("click", function (e) { confirmPopUp(botonHabilitarBrazo.value, "brazo") })

                    botonHabilitar = botonHabilitarBrazo
                    break;
                case 'prensa':
                    var botonHabilitarPrensa = document.createElement("input")

                    //sensor prendido o apagado 
                    botonHabilitarPrensa.id = "BotonHabilitarPrensa"
                    cellInner.innerHTML !== "true"
                    ? botonHabilitarPrensa.classList.add("btn", "btn-primary") : botonHabilitarPrensa.classList.add("btn", "btn-danger")
                    botonHabilitarPrensa.type = "button"
                    cellInner.innerHTML === "true" ? botonHabilitarPrensa.value = "Apagar" : botonHabilitarPrensa.value = "Encender"
                    botonHabilitarPrensa.disabled = false
                    botonHabilitarPrensa.addEventListener("click", function (e) { confirmPopUp(botonHabilitarPrensa.value, "prensa") })

                    botonHabilitar = botonHabilitarPrensa
                    break;
            }

            let div = document.createElement("div")
            cell.className = "btn-center"
            div.role = "group"
            div.append(botonHabilitar)
            cell.append(div)
            row.append(cell)               

         }
        rowContent.append(row)
        sensorPressStatus().then(data =>{

            let label = document.createElement("Label")
            let div = document.createElement("div")
            let cell = document.createElement("td")

            label.innerHTML = status ? "Libre" : "Arriba"
            cell.className = "btn-center"
            div.role = "group"
            div.append(label)
            cell.append(div)
            row.append(cell)
            rowContent.append(row)
        })

        sensorPressOnOff().then(data => {
            let label = document.createElement("Label")
            let div = document.createElement("div")
            let cell = document.createElement("td")

            label.innerHTML = status ? "Libre" : "Ocupado"
            cell.className = "btn-center"
            div.role = "group"
            div.append(label)
            cell.append(div)
            row.append(cell)
            rowContent.append(row)
        })

    })

  
}


//confirmacion de apagar o prender los sensores
function confirmPopUp(value, sensor) {
    console.log(value)
    Swal.fire({
        title: 'Confirme ' + value + ' el/la ' + sensor,
           // text: "",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#38d39f',
            cancelButtonColor: '#d33',
            confirmButtonText: 'OK'
    }).then((result) => {
        if (result.isConfirmed) {
            // false apagado  true encendido
            let newValue = null
            value === 'Apagar' ? newValue = false : newValue = true;

            requestPanel(sensor, value).then(response => {

                    if (response === 1) {
                        Swal.fire(
                            'Estado actualizado',
                        ).then((result) => {
                            if (result.isConfirmed) {
                                retrieveAllPanel()
                            }
                        })
                    }
                    else if (response === 0) {
                        Swal.fire(
                            'Hubo un error, intente nuevamente',
                        ).then((result) => {
                            if (result.isConfirmed) {
                                retrieveAllPanel()
                            }
                        })
                    }
             })
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
    var rowContent = document.getElementById("rowContent")


    let dData = '{"Sensores":{ "cinta": true, "brazo": false,"prensa":false}}'

    let a = JSON.parse(dData)

    console.log(a);


    for (obj in a) {

        var row = document.createElement("tr")

        for (let key in a[obj]) {
          
            var cell = document.createElement("td")
            var cellInner = document.createElement("td")
        
            cellInner.innerHTML = a[obj][key]
            var botonHabilitar = null

            switch (key) {
                case 'cinta':
                    var botonHabilitarCinta = document.createElement("input")
                    botonHabilitarCinta.id = "BotonHabilitarCinta"

                    cellInner.innerHTML !== "true"
                        ? botonHabilitarCinta.classList.add("btn", "btn-primary") : botonHabilitarCinta.classList.add("btn", "btn-danger")
                    botonHabilitarCinta.type = "button"
                    cellInner.innerHTML === "true" ? botonHabilitarCinta.value = "Apagar" : botonHabilitarCinta.value = "Encender"
                    botonHabilitarCinta.disabled = false
                    botonHabilitarCinta.addEventListener("click", function (e) { confirmPopUp(botonHabilitarCinta.value, "cinta") })
                    botonHabilitar = botonHabilitarCinta
                    break;
                case 'brazo':
                    var botonHabilitarBrazo = document.createElement("input")
                    botonHabilitarBrazo.id = "BotonHabilitarBrazo"

                    cellInner.innerHTML !== "true"
                        ? botonHabilitarBrazo.classList.add("btn", "btn-primary") : botonHabilitarBrazo.classList.add("btn", "btn-danger")
                    botonHabilitarBrazo.type = "button"
                    cellInner.innerHTML === "true" ? botonHabilitarBrazo.value = "Apagar" : botonHabilitarBrazo.value = "Encender"
                    botonHabilitarBrazo.disabled = false
                    botonHabilitarBrazo.addEventListener("click", function (e) { confirmPopUp(botonHabilitarBrazo.value, "brazo") })
                    botonHabilitar = botonHabilitarBrazo
                    break;
                case 'prensa':
                    var botonHabilitarPrensa = document.createElement("input")
                    botonHabilitarPrensa.id = "BotonHabilitarPrensa"

                    cellInner.innerHTML !== "true"
                        ? botonHabilitarPrensa.classList.add("btn", "btn-primary") : botonHabilitarPrensa.classList.add("btn", "btn-danger")
                    botonHabilitarPrensa.type = "button"
                    cellInner.innerHTML === "true" ? botonHabilitarPrensa.value = "Apagar" : botonHabilitarPrensa.value = "Encender"
                    botonHabilitarPrensa.disabled = false
                    botonHabilitarPrensa.addEventListener("click", function (e) { confirmPopUp(botonHabilitarPrensa.value, "prensa") })
                    botonHabilitar = botonHabilitarPrensa
                    break;

            }   

            var div = document.createElement("div")
            
            
            cell.className = "btn-center"
            div.role = "group"
            div.append(botonHabilitar)
            cell.append(div)
            row.append(cell)
        }
        rowContent.append(row)
    }

}

//me trae el id de la primera row
function getID(e) {
    console.log(e.target)
    var row1 = e.target.closest("tr")

    return row1.cells[0].innerHTML
}



async function sensorPressStatus() {
    let status = 0;

    var response = await fetch(localURL + "/press/PressSensorStatus", {
        method: 'GET',
        headers: { 'Content-Type': 'application / json' }
    }).then((res) =>
        res.json()
    ).then(j => {
        status = j
    })

    console.log(status)

    return status    
}

async function sensorPressOnOff() {
    let status = 0;

    var response = await fetch(localURL + "/press/PressIsIdle", {
        method: 'GET',
        headers: { 'Content-Type': 'application / json' }
    }).then((res) =>
        res.json()
    ).then(j => {
        status = j
    })

    console.log(status)

    return status
}


