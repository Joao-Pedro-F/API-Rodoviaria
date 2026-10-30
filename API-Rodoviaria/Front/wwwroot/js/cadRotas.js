//GERAL
let enderecoInicial = document.getElementById("inputEnderecoI");
let enderecoFinal = document.getElementById("inputEnderecoF");
let btnCadastrar = document.getElementById("btnCadastrar");

const urlPadrao = 'https://localhost:7171/api';

//FUNÇÕES

async function cadastrarRotas() {

    try {
        btnCadastrar.textContent = "Carregando..."
        btnCadastrar.setAttribute('disabled', '');

        const payload = {
            enderecoInicio: enderecoInicial.value,
            enderecoFim: enderecoFinal.value,
        }

        const res = await fetch(`${urlPadrao}/Rota`, {
            method: 'POST',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload),
        });

        if(!res.ok) return

        var response = await res.json();

        enderecoFinal.value = "";
        enderecoInicial.value = "";

        mostrarToast("Rota cadastrada com sucesso")

    } catch (error) {
        throw error
    } finally {
        btnCadastrar.textContent = "Cadastrar";
        btnCadastrar.removeAttribute('disabled');
    }
}



//CHAMADA (FUNÇÕES)

btnCadastrar.addEventListener('click', async () => {
    await cadastrarRotas();
})

function mostrarToast(msg) {
    const toastElemento = document.getElementById('toast');
    toastElemento.querySelector('.toast-body').textContent = msg;
    const toast = new bootstrap.Toast(toastElemento, { delay: 4000 });
    toast.show();
}  