let pokemons = [];
let connection = null;

let pokemonsInKantoRegion = [];
let pokemonsWhereTrainerWinIs10 = [];
let PokemonsWhereTrainerLevelUnder10 = [];

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

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

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

//nonCRUDsGetdata();

//function nonCRUDsGetdata() {
    pokemonsInKantoRegionGetdata();
    pokemonsWhereTrainerWinIs10Getdata();
    pokemonsWhereTrainerLevelUnder10Getdata();
//}




async function pokemonsInKantoRegionGetdata() {
    await fetch('http://localhost:35206/stat/pokemonsInKantoRegion')
        .then(x => x.json())
        .then(y => {
            pokemonsInKantoRegion = y;
            pokemonsInKantoRegionDisplay();
        });
}

function pokemonsInKantoRegionDisplay() {
    document.getElementById('PokemonsInKantoRegionTablediv').style.display = 'flex';

    document.getElementById('pokemonsInKantoRegionResultarea').innerHTML = "";

    pokemonsInKantoRegion.forEach(t => {
        document.getElementById('pokemonsInKantoRegionResultarea').innerHTML += "<tr><td>"
        + t.pokemonID + "</td><td>"
        + t.name + "</td><td>"
        + t.trainerID + "</td><td>"
        + t.type
        + "</td></tr>";
    });
}

async function pokemonsWhereTrainerWinIs10Getdata() {
    await fetch('http://localhost:35206/stat/pokemonsWhereTrainerWinIs10')
        .then(x => x.json())
        .then(y => {
            pokemonsWhereTrainerWinIs10 = y;
            pokemonsWhereTrainerWinIs10Display();
        });
}

function pokemonsWhereTrainerWinIs10Display() {
    document.getElementById('pokemonsWhereTrainerWinIs10Tablediv').style.display = 'flex';

    document.getElementById('pokemonsWhereTrainerWinIs10Resultarea').innerHTML = "";

    pokemonsWhereTrainerWinIs10.forEach(t => {
        document.getElementById('pokemonsWhereTrainerWinIs10Resultarea').innerHTML += "<tr><td>"
            + t.pokemonID + "</td><td>"
            + t.name + "</td><td>"
            + t.trainerID + "</td><td>"
            + t.type
            + "</td></tr>";
    });
}

async function pokemonsWhereTrainerLevelUnder10Getdata() {
    await fetch('http://localhost:35206/stat/pokemonsWhereTrainerLevelUnder10')
        .then(x => x.json())
        .then(y => {
            pokemonsWhereTrainerLevelUnder10 = y;
            pokemonsWhereTrainerLevelUnder10Display();
        });
}

function pokemonsWhereTrainerLevelUnder10Display() {
    document.getElementById('PokemonsWhereTrainerLevelUnder10Tablediv').style.display = 'flex';

    document.getElementById('pokemonsWhereTrainerLevelUnder10Resultarea').innerHTML = "";

    pokemonsWhereTrainerLevelUnder10.forEach(t => {
        document.getElementById('pokemonsWhereTrainerLevelUnder10Resultarea').innerHTML += "<tr><td>"
            + t.pokemonID + "</td><td>"
            + t.name + "</td><td>"
            + t.trainerID + "</td><td>"
            + t.type
            + "</td></tr>";
    });
}
