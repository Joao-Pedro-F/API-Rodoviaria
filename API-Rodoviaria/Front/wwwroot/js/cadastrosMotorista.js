//GERAL
let iNome = document.getElementById("inputNome");
let iCpf = document.getElementById("inputCPF");
let iCnh = document.getElementById("inputCNH");
let btnCadastrar = document.getElementById("btnCadastrar");

const urlPadrao = "https://localhost:7171/api";

//FUNÇÕES

async function cadastrarMotorista() {

    try {
        btnCadastrar.textContent = "Carregando..."
        btnCadastrar.setAttribute('disabled', '');

        const payload = {
            nome: iNome.value,
            cpf: iCpf.value,
            cnh: iCnh.value
        }

        const res = await fetch(`https://localhost:7171/api/Motoristas`, {
            method: 'POST',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload),
        })

        if (!res.ok) return

        iNome.value = "";
        iCpf.value = "";
        iCnh.value = "";

        mostrarToast("Motorista Cadastrado com sucesso!");

    } catch (error) {
        throw error
    } finally {

        btnCadastrar.textContent = "Cadastrar";
        btnCadastrar.removeAttribute('disabled');

    }

}

//CHAMADO (FUNÇÕES)
btnCadastrar.addEventListener('click', async () => {
    await cadastrarMotorista();
});

function mostrarToast(msg) {
    const toastElemento = document.getElementById('toast');
    toastElemento.querySelector('.toast-body').textContent = msg;
    const toast = new bootstrap.Toast(toastElemento, { delay: 4000 });
    toast.show();
}  