
var localURL = "https://localhost:44307";

//TurnOffArm TurnOnBand TurnOnPress POST

//orden  = cinta brazo prensa
async function requestPanelALL(path) {   //traigo todos los estados de los sensores y los relleno en la tabla
    let response = JSON.parse('{ "cinta": false, "brazo": false, "prensa":false}')

    await fetch(localURL + "/band/GetBandStatus", {
        method: 'GET',
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' }
    }).then((res) =>
        res.json()
    ).then(j => {
        j === 1 ? response['cinta'] = true : response['cinta'] = false
    })


    await fetch(localURL + "/arm/GetArmStatus", {
        method: 'GET',
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' }
    }).then((res) =>
        res.json()
    ).then(j => {
        j === 1 ? response['brazo'] = true : response['brazo'] = false
            })

    await fetch(localURL + "/press/PressSensorStatus", {
        method: 'GET',
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' }
    }).then((res) =>
        res.json()
    ).then(j => {
        j === true ? response['prensa'] = true : response['prensa'] = false
    })

    return response
}

// REVIEW
async function requestPanel(sensor, estado) {
    var path = ''
    switch (sensor) {
        case 'prensa':
            estado === 'Encender' ? path = '/TurnOnPress' : path = '/TurnOffPress'
            break;
        case 'brazo':
            estado === 'Encender' ? path = '/TurnOnArm' : path = '/TurnOffArm'
            break;
        case 'cinta':
            estado === 'Encender' ? path = '/TurnOnBand' : path = '/TurnOffBand'
            break;
        
    }

    let response = await fetch(localURL + path, { // enviar el sensor para actualizar
        method: 'POST', 
        headers: { 'token': localStorage.getItem("token"), 'Content-Type': 'application / json' },
    })
    return response.json();
}


function retrieveAllPanel() {
    requestPanelALL().then(returnedData => {
        console.log('returnedData ::: ', returnedData)
        $("#rowContent tr").remove();
        var rowContent = document.getElementById("rowContent")
        let div = document.createElement("div")


        for (obj in returnedData) {
            var row = document.createElement("tr")


                var cell = document.createElement("td")
                var cellInner = document.createElement("td")

                cellInner.innerHTML = returnedData[obj]
                var botonHabilitar = null
            console.log('obj', obj)   
            switch (obj) {
                case 'cinta':
                    console.log('entre cinta')
                    let divCinta = document.createElement("div")
                        var botonHabilitarCinta = document.createElement("input")
                        botonHabilitarCinta.id = "BotonHabilitarCinta"

                        cellInner.innerHTML !== "true"
                            ? botonHabilitarCinta.classList.add("btn", "btn-primary") : botonHabilitarCinta.classList.add("btn", "btn-danger")
                        botonHabilitarCinta.type = "button"
                        cellInner.innerHTML === "true" ? botonHabilitarCinta.value = "Apagar" : botonHabilitarCinta.value = "Encender"
                        botonHabilitarCinta.disabled = false
                        botonHabilitarCinta.addEventListener("click", function (e) { confirmPopUp(botonHabilitarCinta.value, "cinta") })

                    //div = divCinta

                    botonHabilitar = botonHabilitarCinta
                        break;
                case 'brazo':
                    console.log('entre brazo')
                    let divBrazo = document.createElement("div")

                        var botonHabilitarBrazo = document.createElement("input")
                        botonHabilitarBrazo.id = "BotonHabilitarBrazo"

                        cellInner.innerHTML !== "true"
                            ? botonHabilitarBrazo.classList.add("btn", "btn-primary") : botonHabilitarBrazo.classList.add("btn", "btn-danger")
                        botonHabilitarBrazo.type = "button"
                        cellInner.innerHTML === "true" ? botonHabilitarBrazo.value = "Apagar" : botonHabilitarBrazo.value = "Encender"
                        botonHabilitarBrazo.disabled = false
                        botonHabilitarBrazo.addEventListener("click", function (e) { confirmPopUp(botonHabilitarBrazo.value, "brazo") })

                    //div = divBrazo
                    botonHabilitar = botonHabilitarBrazo
                        break;
                case 'prensa':
                    console.log('entre prensa')
                    let divPrensa = document.createElement("div")

                        var botonHabilitarPrensa = document.createElement("input")
                        botonHabilitarPrensa.id = "BotonHabilitarPrensa"

                        cellInner.innerHTML !== "true"
                            ? botonHabilitarPrensa.classList.add("btn", "btn-primary") : botonHabilitarPrensa.classList.add("btn", "btn-danger")
                        botonHabilitarPrensa.type = "button"
                        cellInner.innerHTML === "true" ? botonHabilitarPrensa.value = "Apagar" : botonHabilitarPrensa.value = "Encender"
                        botonHabilitarPrensa.disabled = false
                        botonHabilitarPrensa.addEventListener("click", function (e) { confirmPopUp(botonHabilitarPrensa.value, "prensa") })

                   // div = divPrensa
                    botonHabilitar = botonHabilitarPrensa
                        break;

            }

            cell.className = "btn-center"
            div.role = "group"
            div.append(botonHabilitar)
            cell.append(div)
            row.append(cell)
               

            }

        rowContent.append(row)

         } )
}


//confirmacion de apagar o prender los sensores
function confirmPopUp(value, sensor) {
    console.log(value)
    Swal.fire({
        title: 'Confirme ' + value + ' el sensor ' + sensor,
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
            console.log('new value sensor ::: ', newValue);
                //REVIEW
              /*  requestPanel(sensor, newValue).then(response => {

                    if (response == 1) {
                        Swal.fire(
                            'Borrado!',
                        ).then((result) => {
                            if (result.isConfirmed) {
                                console.log(type)
                                retieveAllPanel()
                            }
                        })
                    }
                    else if (response == 0) {
                        Swal.fire(
                            'Hubo un error!',
                        ).then((result) => {
                            if (result.isConfirmed) {
                                retieveAllPanel()
                            }
                        })
                    }


                })*/

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








