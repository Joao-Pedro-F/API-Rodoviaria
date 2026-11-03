//GERAL
const listarViajens = document.getElementById("listarViajens");
const conteudoViagens = document.querySelector(".conteudoViagens");
const containerTabela = document.getElementById("containerTabela");

const urlPadrao = 'https://localhost:7171/api';

let idViajem;
let paginaAtualGlobal = 1;
let totalPaginasGlobal = 1;
const tamanhoPagina = 10;

var tableViagensHtml = `
    <div class="table-responsive">
        <table class="table table-striped mb-0">
            <thead>
                <tr>
                    <th>Endereço Inicial</th>
                    <th>Endereço Final</th>
                    <th>Data Saida</th>
                    <th>Data Chegada</th>
                    <th>Motorista</th>
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
            itensViajens += `
                <tr>
                    <td>${item.enderecoOrigem}</td>
                    <td>${item.enderecoFim}</td>
                    <td>${item.dataSaida}</td>
                    <td>${item.dataChegada}</td>
                    <td>${item.nomeMotorista}</td>
                    <td><button class="btn btn-outline-secondary btnVisualizar" data-id="${item.id}">Visualizar</button></td>
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

        if (!res.ok) return;

        var response = await res.json();

        const infoViajem = `
            <div class="d-flex flex-column gap-3 w-100">
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
        `;

        conteudoViagens.innerHTML = infoViajem;
    } catch (error) {
        mostrarToast("Erro ao carregar detalhes da viagem.");
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
document.addEventListener("DOMContentLoaded", () => {
    containerTabela.innerHTML = tableViagensHtml;
    renderViajens(1);
});

document.addEventListener('click', async (event) => {
    const btn = event.target.closest(".btnVisualizar");
    if (btn) {
        idViajem = btn.dataset.id;
        const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById('teste'));
        modal.show();
        await renderViajem();
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