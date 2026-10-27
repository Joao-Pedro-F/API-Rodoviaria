//GERAL
let enderecoInicial = document.getElementById("inputEnderecoI");
let enderecoFinal = document.getElementById("inputEnderecoF");
let btnCadastrar = document.getElementById("btnCadastrar");

const urlPadrao = 'https://localhost:7171/api';

//FUNÇÕES

async function cadastrarRotas() {

    try {

    const payload = {
        enderecoInicio : enderecoInicial.value,
        enderecoFim : enderecoFinal.value,
    }

    const res = await fetch(`${urlPadrao}/Rota`, {
        method: 'POST',
        credentials: "include",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
    });

    var response = await res.json();
    } catch (error){
        throw error
    }
}



//CHAMADA (FUNÇÕES)

btnCadastrar.addEventListener('click', async () => {
    await cadastrarRotas();
})