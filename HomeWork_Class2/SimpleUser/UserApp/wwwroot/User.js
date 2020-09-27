getAllUsers = document.getElementById(`btn1`);
getAllUsersNames = document.getElementById(`btn2`);
getUserById = document.getElementById(`btn3`);
getUserIdInput = document.getElementById(`input1`);
addUserFirstName = document.getElementById(`input2`);
addUserLastName = document.getElementById(`input3`);
addUser = document.getElementById(`btn4`);
deleteUserIdInput = document.getElementById(`input4`);
deleteUser = document.getElementById(`btn5`);

getAllUser.addEventListener(`click`, getAllUsers);
getAllUsersNames.addEventListener(`click`, getAllUsersNames);
getUserById.addEventListener(`click`, getUserById);
getUserById.addEventListener(`click`, deleteUser);
getUserById.addEventListener(`click`, addUser);

let port = "51123";

let getAllUsers = async () => {
    let url = `http://localhost:${port}/api/User`;
    let data = await fetch(url);
    let users = await data.json();

    users.forEach(x => console.log(`User: ${x.FirstName} ${x.LastName}`));    
}

let getUserById = async () => {
    let url = `http://localhost:${port}/api/User${getUserIdInput.value}`;
    let data = await fetch(url);
    let user = await data.json();

    console.log(`User: ${user.FirstName} ${user.LastName}`);
}

let getAllUsersNames = async () => {
    let url = `http://localhost:${port}/api/User/names`;
    let data = await fetch(url);
    let names = await data.text();
    console.log(names);
}

let addUser = async () => {
    let url = `http://localhost:${port}/api/User`;
    let user = { FirstName: addUserFirstName.value, LastName: addUserLastName.value }
    var response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    });
    console.log(response);
}

let deleteUser = async () => {
    let url = `http://localhost:${port}/api/User`;
    let userId = { FirstName: deleteUserIdInput.value }
    var response = await fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userId)
    });
    console.log(response);
}
