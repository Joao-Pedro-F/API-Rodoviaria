//GERAL
const listarViajens = document.getElementById("listarViajens");
const conteudoViagens = document.querySelector(".conteudoViagens");
const containerTabela = document.getElementById("containerTabela");
const btnReservar = document.getElementById("btnReservar");

const urlPadrao = 'https://localhost:7171/api';

let idViajem;
let dataStatus;
let paginaAtualGlobal = 1;
let totalPaginasGlobal = 1;
let tamanhoPagina = 10;

var tableViagensHtml = `
    <div class="table-responsive">
        <table class="table table-striped mb-0">
            <thead>
                <tr>
                    <th>Endereço Inicial</th>
                    <th>Endereço Final</th>
                    <th>Motorista</th>
                    <th>Status</th>
                    <th>Mais Informações</th>
                </tr>
            </thead>
            <tbody id="listarViajens">
            </tbody>
        </table>
    </div>
    <nav aria-label="Page navigation" class="p-3">
        <ul class="pagination justify-content-between align-items-center mb-0">
            <li class="page-item" id="btnVoltar"><a class="page-link" href="#">Voltar</a></li>
            <li class="page-item"><input placeholder="Ir para a página..." id="inputpage" type="number" class="form-control" /></li>
            <li class="page-item text-secondary fw-bold" id="paginaInfo"></li>
            <li class="page-item" id="btnAvancar"><a class="page-link" href="#">Avançar</a></li>
        </ul>
    </nav>
`;

//FUNÇÕES
async function renderViajens(pagina) {
    try {
        const res = await fetch(`${urlPadrao}/Viagens?Pagina=${pagina}&TamanhoPagina=${tamanhoPagina}`, {
            method: 'GET',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
        });

        if (!res.ok) {
            mostrarToast("Não foi possível carregar as viagens.");
            return;
        }

        var response = await res.json();

        paginaAtualGlobal = response.paginaAtual;
        totalPaginasGlobal = response.totalPaginas;

        let itensViajens = ``;
        response.itens.forEach((item) => {

            const dataSaidaFormatada = new Date(item.dataSaida);
            const hoje = new Date();

            const status = Math.round((dataSaidaFormatada - hoje) / 86400000);

            if (status >= 1) {
                itensViajens += `
                    <tr>
                        <td>${item.enderecoOrigem}</td>
                        <td>${item.enderecoFim}</td>
                        <td>${item.nomeMotorista}</td>
                        <td class="text-success fw-bold">Disponível para Reserva</td>
                        <td><button class="btn btn-outline-secondary btnVisualizar" data-status="true" data-id="${item.id}" data-fkonibus="${item.fkOnibus}">Visualizar</button></td>
                    </tr>
                `;
                return
            }

            itensViajens += `
                <tr>
                    <td>${item.enderecoOrigem}</td>
                    <td>${item.enderecoFim}</td>
                    <td>${item.nomeMotorista}</td>
                    <td class="text-danger fw-bold">Indisponível para Reserva</td>
                    <td><button class="btn btn-outline-secondary btnVisualizar" data-status="false" data-id="${item.id}" data-fkonibus="${item.fkOnibus}">Visualizar</button></td>
                </tr>
            `;
        });

        const tbody = document.getElementById("listarViajens");
        if (tbody) {
            tbody.innerHTML = itensViajens;
        }

        const paginaInfo = document.getElementById("paginaInfo");
        if (paginaInfo) {
            paginaInfo.textContent = `Página ${paginaAtualGlobal} de ${totalPaginasGlobal}`;
        }

        const btnVoltar = document.getElementById("btnVoltar");
        const btnAvancar = document.getElementById("btnAvancar");
        if (btnVoltar && btnAvancar) {
            btnVoltar.classList.toggle("disabled", paginaAtualGlobal <= 1);
            btnAvancar.classList.toggle("disabled", paginaAtualGlobal >= totalPaginasGlobal);
        }

    } catch (error) {
        mostrarToast("Erro ao conectar com o servidor.");
        throw error;
    }
}

async function renderViajem() {
    try {
        const res = await fetch(`${urlPadrao}/Viagens/${idViajem}`, {
            method: 'GET',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
        });

        if (!res.ok) {
            mostrarToast("Não foi possível renderizar viagens")
            return
        };

        var response = await res.json();

        const infoViajem = `
            <div class="d-flex flex-column">
                <div class="d-flex flex-column gap-3 w-100 mb-2">
                    <div class="d-flex gap-3">
                        <p class="mb-0 w-50"><strong>Motorista:</strong> ${response.nomeMotorista}</p>
                        <p class="mb-0 w-50"><strong>Placa:</strong> ${response.placa}</p>
                    </div>
                    <div class="d-flex gap-3">
                        <p class="mb-0 w-50"><strong>Origem:</strong> ${response.enderecoOrigem}</p>
                        <p class="mb-0 w-50"><strong>Destino:</strong> ${response.enderecoFim}</p>
                    </div>
                    <div class="d-flex gap-3">
                        <p class="mb-0"><strong>Saída:</strong> ${response.dataSaida}</p>
                        <p class="mb-0"><strong>Chegada:</strong> ${response.dataChegada}</p>
                    </div>
                </div>
         
                <div class="card-body p-4">
                    <hr>
                    <form>
                        <h1 class="text-center mb-3 fs-4">Reservar Cadeiras</h1>

                        <div class="card mb-4 shadow-sm">
                            <div class="card-body d-flex justify-content-around text-center py-2 small">
                                <div><span class="badge bg-outline-secondary border text-dark fs-6">Livre</span></div>
                                <div><span class="badge bg-secondary fs-6">Selecionado</span></div>
                                <div><span class="badge bg-danger fs-6">Reservado</span></div>
                            </div>
                        </div>

                        <div class="d-flex flex-wrap justify-content-between gap-2" id="containerCadeiras">
                        </div>
                    </form>
                </div>
            </div>
        `;

        conteudoViagens.innerHTML = infoViajem;
    } catch (error) {
        throw error
    }
}

async function renderCadeiras() {

    try {

    const res = await fetch(`${urlPadrao}/Cadeira/${idViajem}`, {
        method: 'GET',
        credentials: "include",
        headers: { 'Content-Type': 'application/json' },
    });

        if (!res.ok) {
            mostrarToast("Erro ao renderizar cadeiras.")
            return
        };

    var response = await res.json();

    let cadeiras = ``;
    response.forEach((item) => {
        if (item.disponivel) {
            cadeiras += `
                <div>
                    <input type="checkbox" class="btn-check cadeiraSelecionada" data-id="${item.id}" id="${item.id}" value="${item.numero}">
                    <label class="btn btn-outline-secondary btn-check-success d-flex align-items-center justify-content-center fw-bold assento-btn" for="${item.id}">${item.numero}</label>
                </div>
            `;
        } else {
            cadeiras += `
                <div>
                    <input type="checkbox" class="btn-check" id="${item.id}" value="${item.numero} disabled">
                    <label class="btn btn-danger d-flex align-items-center justify-content-center fw-bold assento-btn" for="${item.id}">${item.numero}</label>
                </div>
            `;
        }
    })

    const containerCadeiras = document.getElementById("containerCadeiras");
    containerCadeiras.innerHTML = cadeiras;
    } catch (error){
        throw error
    }

}

async function reservarCadeiras() {
    const selecionarCadeiras = document.querySelectorAll('.cadeiraSelecionada:checked');
    console.log(selecionarCadeiras);

    let listaCadeiras = [];
    selecionarCadeiras.forEach((item) => {
        console.log(item.id)
        listaCadeiras.push(item.id);
    });

    console.log(listaCadeiras)

    const payload = {
        fkViagem: idViajem,
        idsCadeiras: listaCadeiras,
    }

    try {
    const res = await fetch(`${urlPadrao}/Reservas`, {
        method: 'POST',
        credentials: "include",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
    });

        if (!res.ok) {
            mostrarToast("Não foi possível reservar esta(s) cadeira(s).")
            return
        }

        mostrarToast("Cadeira(s) reservada(s) com sucesso!")
    } catch (error) {
        throw error
    }
}

function mostrarToast(msg) {
    const toastElemento = document.getElementById('toast');
    if (!toastElemento) return;
    toastElemento.querySelector('.toast-body').textContent = msg;
    const toast = new bootstrap.Toast(toastElemento, { delay: 4000 });
    toast.show();
}

//CHAMADA (FUNÇÕES)
// document.addEventListener("DOMContentLoaded", () => {
//     containerTabela.innerHTML = tableViagensHtml;
//     renderViajens(1);
// });

btnReservar.addEventListener('click', async () => {
    await reservarCadeiras()
    const modal = bootstrap.Modal.getInstance(document.getElementById('teste'));
    renderViajens(1);
    modal.hide();
});

document.addEventListener('click', async (event) => {
    const btn = event.target.closest(".btnVisualizar");
    if (btn) {
        idViajem = btn.dataset.id;
        dataStatus = btn.dataset.status;
        const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById('teste'));
        modal.show();
        await renderViajem();
        await renderCadeiras();
        if (dataStatus == "false") {
            btnReservar.setAttribute('disabled', '');
            return
        }
        btnReservar.removeAttribute('disabled');
    }

    if (event.target.closest("#btnVoltar")) {
        event.preventDefault();
        if (paginaAtualGlobal > 1) {
            await renderViajens(paginaAtualGlobal - 1);
        }
    }

    if (event.target.closest("#btnAvancar")) {
        event.preventDefault();
        if (paginaAtualGlobal < totalPaginasGlobal) {
            await renderViajens(paginaAtualGlobal + 1);
        }
    }
});

document.addEventListener('change', async (event) => {
    if (event.target.id === 'inputpage') {
        const paginaPesquisa = parseInt(event.target.value);
        if (paginaPesquisa >= 1 && paginaPesquisa <= totalPaginasGlobal) {
            await renderViajens(paginaPesquisa);
        } else {
            mostrarToast("Página inválida!");
            event.target.value = "";
        }
    }
});

document.addEventListener("DOMContentLoaded", () => {
    const mensagemSucesso = localStorage.getItem('toastSucesso');

    if (mensagemSucesso) {
        mostrarToast(mensagemSucesso);
        localStorage.removeItem('toastSucesso');
    }
    containerTabela.innerHTML = tableViagensHtml;
    renderViajens(1);
});
