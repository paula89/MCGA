
var localURL = "https://localhost:44307/user"; // TO DO

async function requestGraphicsALL() {   //traigo todos los bultos y los relleno en la tabla
    let response = await fetch(localURL, {
        method: 'GET',
        headers: { 'token': localStorage.getItem("token"),'Content-Type':'application / json'}
    })
    return response.json()
}

function retieveAllGraphics(type) {
        requestUsersALL().then(returnedData => { 

        $("#rowContent tr").remove();
        var x = document.getElementById("rowContent")

        for (obj in returnedData) {
            let i = 0
            var row = document.createElement("tr")
           // console.log(returnedData[obj])
            for (let key in returnedData[obj]) {
                var cell = document.createElement("td")
               // console.log(key)
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








