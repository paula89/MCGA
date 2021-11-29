
var localURL = "https://localhost:44307/Package/GetAllPackages"; 

async function requestGraphicsALL() {   //traigo todos los bultos y los relleno en la tabla
    let response = await fetch(localURL, {
        method: 'GET',
        headers: { 'token': localStorage.getItem("token"),'Content-Type':'application / json'}
    })
    return response.json()
}

function retieveAllGraphics(type) {
    requestGraphicsALL().then(returnedData => {
        $("#rowContent tr").remove();
        var x = document.getElementById("rowContent")
        let cantidades = [0,0,0]

        for (obj in returnedData) {
            var row = document.createElement("tr")

            // 6 apilado   0ingresado 5prensado
            switch (returnedData[obj]['Status']) {
                case 0:
                    cantidades[0] += 1
                    break;
                case 5:
                    cantidades[1] += 1;
                    break;
                case 6:
                    cantidades[2] += 1;
                    break;
            }
        }
            for (let i = 0; i < 3; i++) {
                    var cell = document.createElement("td")
                    cell.innerHTML = cantidades[i]
                    row.append(cell)   
            }
                var cell = document.createElement("td")    
                var div = document.createElement("div")
                
                cell.append(div)
                row.append(cell)
            
            x.append(row)        
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
function TestTablesGraphic(type) {
    $("#rowContent tr").remove();
    var x = document.getElementById("rowContent")


    let dData = '{"Bulto1":{ "ingresados": "1", "enProceso": "2","apilados":"4"}}'

    let a = JSON.parse(dData)
    console.log(a);

    for (obj in a) {
        var row = document.createElement("tr")
        for (let key in a[obj]) {
            var cell = document.createElement("td")
            cell.innerHTML = a[obj][key]
            row.append(cell)
        }
        if (type == true) {
            var cell = document.createElement("td")
            var div = document.createElement("div")
            div.class = "btn-group"
            div.role = "group"
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








