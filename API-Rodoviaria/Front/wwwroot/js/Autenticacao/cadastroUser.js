//GERAL
let nome = document.getElementById("inputNome");
let cpf = document.getElementById("inputCpf");
let email = document.getElementById("inputEmail");
let senha1 = document.getElementById("inputSenha");
let senha2 = document.getElementById("inputSenha2");
let endereco = document.getElementById("inputEndereco");
const btnCadastrar = document.getElementById("btnCadastro");

//FUNÇÕES
async function cadastrarUser() {

    try {

    const payload = {
        username: nome.value,
        email: email.value,
        password: senha1.value,
        cpf: cpf.value,
        endereco: endereco.value
    }
        
    const sendRequest = await fetch("https://localhost:7171/api/Usuarios", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
        credentials: "include",
    });

        if (!sendRequest.ok) {
            mostrarToast("Não foi possível cadastrar usuário.")
            return
        }

    inputNome.value = "";
    inputEmail.value = "";
    inputConfirmSenha.value = "";
    inputSenha.value = "";

    window.location.href = "/Login/loginView";

    } catch (error) {
        throw error
    }

}

//CHAMADA (FUNÇÕES)

btnCadastrar.addEventListener('click', async () => {
    await cadastrarUser()
})

function mostrarToast(msg) {
    const toastElemento = document.getElementById('toast');
    toastElemento.querySelector('.toast-body').textContent = msg;
    const toast = new bootstrap.Toast(toastElemento, { delay: 4000 });
    toast.show();
} 