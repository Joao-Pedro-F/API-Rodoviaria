//GERAL
let iNome = document.getElementById("inputNome");
let iCpf = document.getElementById("inputCPF");
let iCnh = document.getElementById("inputCNH");
let btnCadastrar = document.getElementById("btnCadastrar");

const urlPadrao = "https://localhost:7171/api";

//FUNÇÕES

async function cadastrarMotorista() {

    try {
    const payload = {
        nome: iNome.value,
        cpf: iCpf.value,
        cnh: iCnh.value
    }

    const res = await fetch(`https://localhost:7171/api/Motoristas`, {
        method: 'POST',
        credentials: "include",
        headers: {'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
    })     
    } catch (error) {
        throw error
    }

}

//CHAMADO (FUNÇÕES)
btnCadastrar.addEventListener('click', async () => {
    await cadastrarMotorista();

    iNome.value = "";
    iCpf.value = "";
    iCnh.value = "";

    mostrarToast("Motorista Cadastrado com sucesso!");
});

function mostrarToast(msg) {
    const toastElemento = document.getElementById('toast');
    toastElemento.querySelector('.toast-body').textContent = msg;
    const toast = new bootstrap.Toast(toastElemento, { delay: 4000 });
    toast.show();
}  