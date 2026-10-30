//GERAL
let dataSaida = document.getElementById("inputDataS");
let dataChegada = document.getElementById("inputDataC");
let onibus = document.getElementById("selectOnibus");
let rota = document.getElementById("selectRotas");
let motorista = document.getElementById("selectMotoristas");
const btnCadastrar = document.getElementById("btnCadastrar");

const urlPadrao = 'https://localhost:7171/api';

//FUNÇÕES

async function renderOnibus() {

    try {
        const res = await fetch(`${urlPadrao}/Onibus?Pagina=1&TamanhoPagina=100`, {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            credentials: "include",
        })

        if(!res.ok) return

        var response = await res.json();

        var selectLista;

        response.itens.forEach((item) => {
            selectLista += `
                <option value="${item.id}">${item.placa}</option>
            `
        });

        onibus.innerHTML = selectLista;

    } catch (error) {
        throw error
    }
}

async function renderMotoristas() {
    try {
        const res = await fetch(`${urlPadrao}/Motoristas?Pagina=1&TamanhoPagina=100`, {
            method: 'GET',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
        })

        if (!res.ok) return

        var response = await res.json();

        var selectLista;

        response.itens.forEach((item) => {
            selectLista += `
            <option value="${item.id}">${item.nome}</option>
        `
        });

        motorista.innerHTML = selectLista;

    } catch (error){
        throw error
    }
}

async function renderRotas() {
    try {
        const res = await fetch(`${urlPadrao}/Rota?Pagina=1&TamanhoPagina=100`, {
            method: 'GET',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
        })

        if (!res.ok) return

        var response = await res.json();

        var selectLista;

        response.itens.forEach((item) => {
            selectLista += `
                <option value="${item.id}">${item.enderecoInicio} -> ${item.enderecoFim}</option>
            `
        });

        rota.innerHTML = selectLista;

    } catch (error){
        throw error
    }
}

async function cadastrarViajem() {
    const dataChegadaISO = new Date(dataChegada.value).toISOString();
    const dataSaidaISO = new Date(dataSaida.value).toISOString();

    try {

        btnCadastrar.textContent = "Carregando..."
        btnCadastrar.setAttribute('disabled', '');

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

        if(!res.ok) return

        var response = await res.json();

        dataSaida.value = "";
        dataChegada.value = "";

        mostrarToast('Viagem cadsatrada com sucesso!')

    } catch (error) {
        throw error
    } finally {
        btnCadastrar.textContent = "Cadastrar"
        btnCadastrar.removeAttribute('disabled');
    }
}

//CHAMADA (FUNÇÕES)

renderMotoristas();
renderOnibus();
renderRotas();

btnCadastrar.addEventListener('click', () => {
    cadastrarViajem();
});
function mostrarToast(msg) {
    const toastElemento = document.getElementById('toast');
    toastElemento.querySelector('.toast-body').textContent = msg;
    const toast = new bootstrap.Toast(toastElemento, { delay: 4000 });
    toast.show();
}