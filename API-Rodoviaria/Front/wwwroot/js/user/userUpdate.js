//GERAL
const btnVoltar = document.getElementById("btnVoltar");
const btnSalvar = document.getElementById("btnSalvar");
const btnDelete = document.getElementById("btnDeletar");
let usuario = document.getElementById("inputText1");
let perfil = document.getElementById("inputText2");
let email = document.getElementById("inputEmail");
let endereco = document.getElementById("inputEndereco");

//FUNÇÕES




//CHAMADA
btnVoltar.addEventListener('click', () => {
    const perfil = localStorage.getItem("Usuario") 
    if (perfil == "Cliente") {
        window.location.href = "/Home/viagensViewUser"
        return
    }
    window.location.href = "/Home/viagensView"
});