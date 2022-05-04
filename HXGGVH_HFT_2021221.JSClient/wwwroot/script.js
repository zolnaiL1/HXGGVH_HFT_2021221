let pokemons = [];
let connection = null;

let pokemonIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:35206/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PokemonCreated", (user, message) => {
        getdata();
    });

    connection.on("PokemonDeleted", (user, message) => {
        getdata();
    });

    connection.on("PokemonUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function getdata() {
    await fetch('http://localhost:35206/pokemon')
        .then(x => x.json())
        .then(y => {
            pokemons = y;
            /*console.log(pokemons);*/
            display();
        });
}

function display()
{
    document.getElementById('resultarea').innerHTML = "";

    pokemons.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
        "<tr><td>" + t.pokemonID + "</td><td>"
        + t.name + "</td><td>"
        + t.trainerID + "</td><td>"
        + t.type + "</td><td>"
        + `<button type="button" onclick="remove(${t.pokemonID})">Delete</button>`
        + `<button type="button" onclick="showupdate(${t.pokemonID})">Update</button>`
        + "</td></tr>";
    });
}

function create() {
    let pokemonName = document.getElementById('Name').value;
    let pokemonTrainerID = document.getElementById('TrainerID').value;
    let pokemonType = document.getElementById('Type').value;
    fetch('http://localhost:35206/pokemon', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: pokemonName,
                trainerID : pokemonTrainerID,
                type : pokemonType
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
    });

}

function remove(id) {
    fetch('http://localhost:35206/pokemon/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showupdate(pokemonID) {
    document.getElementById('NameToUpdate').value = pokemons.find(t => t['pokemonID'] == pokemonID)['name'];
    document.getElementById('TrainerIDToUpdate').value = pokemons.find(t => t['pokemonID'] == pokemonID)['trainerID'];
    document.getElementById('TypeToUpdate').value = pokemons.find(t => t['pokemonID'] == pokemonID)['type'];
    document.getElementById('updateformdiv').style.display = 'flex';
    pokemonIdToUpdate = pokemonID;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let pokemonName = document.getElementById('NameToUpdate').value;
    let pokemonTrainerID = document.getElementById('TrainerIDToUpdate').value;
    let pokemonType = document.getElementById('TypeToUpdate').value;
    fetch('http://localhost:35206/pokemon', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                pokemonID: pokemonIdToUpdate,
                name: pokemonName,
                trainerID: pokemonTrainerID,
                type: pokemonType                
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}

