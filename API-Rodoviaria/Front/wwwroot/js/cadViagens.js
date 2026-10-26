//GERAL
let dataSaida = document.getElementById("inputDataS");
let dataChegada = document.getElementById("inputDataC");
let onibus = document.getElementById("selectOnibus");
let motorista = document.getElementById("selectMotoristas");
let rota = document.getElementById("selectRotas");
const btnCadastrar = document.getElementById("btnCadastrar");

const urlPadrao = 'https://localhost:7171/api';

//FUNÇÕES

async function renderOnibus() {
    const res = await fetch(`${urlPadrao}/Onibus`, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' },
        credentials: "include",
    })

    var response = await res.json();

    var selectLista;

    response.itens.forEach((item) => {
        selectLista += `
            <option value="${item.id}">${item.placa}</option>
        `
    });

    onibus.innerHTML = selectLista;
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

async function renderRotas() {
    const res = await fetch(`${urlPadrao}/Rota`, {
        method: 'GET',
        credentials: "include",
        headers: { 'Content-Type': 'application/json' },
    })

    var response = await res.json();

    var selectLista;

    response.itens.forEach((item) => {
        selectLista += `
            <option value="${item.id}">${item.enderecoInicio} -> ${item.enderecoFim}</option>
        `
    });

    rota.innerHTML = selectLista;
}

async function cadastrarViajem() {
    const dataChegadaISO = new Date(dataChegada.value).toISOString();
    const dataSaidaISO = new Date(dataSaida.value).toISOString();


    let payload = {
        dataSaida : dataSaidaISO,
        dataChegada : dataChegadaISO,
        onibusId : onibus.value,
        motoristaId : motorista.value,
        rotaId : rota.value,
    }

    const res = await fetch(`${urlPadrao}/Viagens`, {
        method: 'POST',
        credentials: "include",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
    })

    var response = await res.json();
}

//CHAMADA (FUNÇÕES)

renderMotoristas();
renderOnibus();
renderRotas();

btnCadastrar.addEventListener('click', () => {
    cadastrarViajem();
});