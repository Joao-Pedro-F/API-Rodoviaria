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

    try {
        btnCadastrar.textContent = "Carregando..."
        btnCadastrar.setAttribute('disabled', '');

        let payload = {
            placa: placa.value,
            capacidadeTotal: capTotal.value,
            fkMotorista: motorista.value,
            enderecoInicio: enderecoInicio.value,
            enderecoFim: enderecoFIm.value,
            dataSaida: dataSaidaISO,
            dataChegada: dataChegadaISO
        }

        const res = await fetch(`${urlPadrao}/Onibus`, {
            method: 'POST',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload),
        })

        if (!res.ok) return

        var response = await res.json();

        placa.value = "";
        capTotal.value = "";
        enderecoInicio.value = "";
        enderecoFIm.value = "";
        dataSaida.value = "";
        dataChegada.value = "";
        motorista.value = "";

        mostrarToast("Ônibus cadastrado com sucesso!");

    } catch (error) {
        throw error
    } finally {
        btnCadastrar.textContent = "Cadastrar";
        btnCadastrar.removeAttribute('disabled');
    }

}


async function renderMotoristas() {

    try {

        const res = await fetch(`${urlPadrao}/Motoristas?Pagina=1&TamanhoPagina=100`, {
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
    } catch (error) {
        throw error
    }
}

//CHAMADO (FUNÇÕES)

renderMotoristas();

btnCadastrar.addEventListener('click', async () => {
    await cadastrarOnibus();
});

function mostrarToast(msg) {
    const toastElemento = document.getElementById('toast');
    toastElemento.querySelector('.toast-body').textContent = msg;
    const toast = new bootstrap.Toast(toastElemento, { delay: 4000 });
    toast.show();
} 