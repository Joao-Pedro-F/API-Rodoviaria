//GERAL
let placa = document.getElementById("inputPlaca");
let capTotal = document.getElementById("inputCapacidade");
let enderecoInicio = document.getElementById("inputEnderecoI");
let enderecoFIm = document.getElementById("inputEnderecoF");
let dataSaida = document.getElementById("inputDataS");
let dataChegada = document.getElementById("inputDataC");
let motorista = document.getElementById("selectMotorista");
const btnCadastrar = document.getElementById("btnCadOnibus");

const urlPadrao = 'https://localhost:7171/api';

//FUNÇÕES

async function cadastrarOnibus() {
    const dataChegadaISO = new Date(dataChegada.value).toISOString();
    const dataSaidaISO = new Date(dataSaida.value).toISOString();


    let payload = {
        placa : placa.value,
        capacidadeTotal : capTotal.value,
        fkMotorista : motorista.value,
        enderecoInicio : enderecoInicio.value,
        enderecoFim : enderecoFIm.value,
        dataSaida: dataSaidaISO,
        dataChegada: dataChegadaISO 
    }

    const res = await fetch(`${urlPadrao}/Onibus`, {
       method: 'POST',
       credentials: "include",
       headers: { 'Content-Type': 'application/json' },
       body: JSON.stringify(payload),
    })

    var response = await res.json();

}


async function renderMotoristas() {

    const res = await fetch(`${urlPadrao}/Motoristas`, {
        method: 'GET',
        credentials: "include",
        headers: { 'Content-Type': 'application/json' },
    })

    var response = await res.json();

    var selectLista;

    response.itens.forEach((item) => {
        selectLista += `
            <option value="${item.id}">${item.nome}</option>
        `
    });

    motorista.innerHTML = selectLista;

}

//CHAMADO (FUNÇÕES)

renderMotoristas();

btnCadastrar.addEventListener('click', async () => {
    await cadastrarOnibus();
});
