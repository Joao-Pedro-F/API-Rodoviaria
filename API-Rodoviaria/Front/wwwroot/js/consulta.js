// GERAL
const btnConsultar = document.getElementById("btnConsultar");
const qtdPag = document.getElementById("qtdPag");
const selectTipo = document.getElementById("selectTipo");
const renderTabela = document.getElementById("renderTabela");
const confirmar = document.querySelector(".confirmar");
const conteudoViagens = document.querySelector(".conteudoViagens");

const urlPadrao = 'https://localhost:7171/api';

let id;
let nome;
let cpf;
let cnh;
let placa;
let capacidade;
let dSaida;
let dChegada;
let enderecoI;
let enderecoF;

let tipo = "Motoristas";
let paginaAtualGlobal = 1;
let totalPaginasGlobal = 1;

var tableMotorista = `
    <table class="table table-striped">
        <thead>
            <tr>
                <th>Nome</th>
                <th class="text-end pe-5">Alterações</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
    <nav aria-label="Page navigation example">
        <ul class="pagination justify-content-between mt-5 align-items-center">
            <li class="page-item" id="btnVoltar"><a class="page-link" href="#">Voltar</a></li>
            <li class="page-item"><input placeholder="Ir para a página..." id="inputpage" type="number" class="form-control" /></li>
            <li class="page-item text-secondary fw-bold" id="paginaInfo"></li>
            <li class="page-item" id="btnAvancar"><a class="page-link" href="#">Avançar</a></li>
        </ul>
    </nav>
`;
var tableOnibus = `
    <table class="table table-striped">
        <thead>
            <tr>
                <th>Placa</th>
                <th>Motorista</th>
                <th>Capacidade</th>
                <th>Data Saída</th>
                <th>Data Chegada</th>
                <th class="text-end pe-5">Alterações</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
    <nav aria-label="Page navigation example">
        <ul class="pagination justify-content-between mt-5 align-items-center">
            <li class="page-item" id="btnVoltar"><a class="page-link" href="#">Voltar</a></li>
            <li class="page-item"><input placeholder="Ir para a página..." id="inputpage" type="number" class="form-control" /></li>
            <li class="page-item text-secondary fw-bold" id="paginaInfo"></li>
            <li class="page-item" id="btnAvancar"><a class="page-link" href="#">Avançar</a></li>
        </ul>
    </nav>
`;
var tableRota = `
    <table class="table table-striped">
        <thead>
            <tr>
                <th>Endereço Inicial</th>
                <th>Endereço Final</th>
                <th class="text-end pe-5">Alterações</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
    <nav aria-label="Page navigation example">
        <ul class="pagination justify-content-between mt-5 align-items-center">
            <li class="page-item" id="btnVoltar"><a class="page-link" href="#">Voltar</a></li>
            <li class="page-item"><input placeholder="Ir para a página..." id="inputpage" type="number" class="form-control" /></li>
            <li class="page-item text-secondary fw-bold" id="paginaInfo"></li>
            <li class="page-item" id="btnAvancar"><a class="page-link" href="#">Avançar</a></li>
        </ul>
    </nav>
`;
var tableViagens = `
    <table class="table table-striped">
        <thead>
            <tr>
                <th>Motorista</th>
                <th>Placa do Veículo</th>
                <th>Data Saída</th>
                <th>Data Chegada</th>
                <th>Endereço Inicial</th>
                <th>Endereço Final</th>
                <th class="text-end pe-5">Alterações</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
    <nav aria-label="Page navigation example">
        <ul class="pagination justify-content-between mt-5 align-items-center">
            <li class="page-item" id="btnVoltar"><a class="page-link" href="#">Voltar</a></li>
            <li class="page-item"><input placeholder="Ir para a página..." id="inputpage" type="number" class="form-control" /></li>
            <li class="page-item text-secondary fw-bold" id="paginaInfo"></li>
            <li class="page-item" id="btnAvancar"><a class="page-link" href="#">Avançar</a></li>
        </ul>
    </nav>
`;

// FUNÇÕES

async function consultarTabela(pagina = 1) {
    try {
        tipo = selectTipo.value;
        const res = await fetch(`${urlPadrao}/${tipo}?Pagina=${pagina}&TamanhoPagina=${qtdPag.value}`, {
            method: 'GET',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
        });

        if (!res.ok) {
            mostrarToast("Não foi possível consultar");
            return;
        }

        var response = await res.json();

        paginaAtualGlobal = response.paginaAtual;
        totalPaginasGlobal = response.totalPaginas;

        let itensLista = ``;


        switch (tipo) {
            case "Motoristas":
                renderTabela.innerHTML = tableMotorista;
                response.itens.forEach((item) => {
                    itensLista += `
                        <tr>
                            <td>${item.nome}</td>
                            <td class="text-end pe-4">
                                <button type="button" class="btn btn-sm btn-outline-secondary editar" data-id="${item.id}" data-nome="${item.nome}" data-cpf="${item.cpf}" data-cnh="${item.cnh}" data-bs-toggle="modal" data-bs-target="#modalEditar">Editar</button>
                                <button type="button" class="btn btn-sm btn-outline-danger excluir" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalExcluir">Excluir</button>
                            </td>
                        </tr>
                    `;
                });
                break;

            case "Onibus":
                renderTabela.innerHTML = tableOnibus;
                response.itens.forEach((item) => {
                    itensLista += `
                        <tr>
                            <td>${item.placa}</td>
                            <td>${item.motorista}</td>
                            <td>${item.capacidadeTotal}</td>
                            <td>${item.datasaida}</td>
                            <td>${item.dataChegada}</td>
                            <td class="text-end pe-4">
                                <button type="button" class="btn btn-sm btn-outline-secondary editar" data-id="${item.id}" data-placa="${item.placa}" data-capacidade="${item.capacidadeTotal}" data-enderecoI="${item.enderecoInicio}" data-dsaida="${item.datasaida}" data-dchegada="${item.dataChegada}" data-enderecoF="${item.enderecoFim}" data-bs-toggle="modal" data-bs-target="#modalEditar">Editar</button>
                                <button type="button" class="btn btn-sm btn-outline-danger excluir" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalExcluir">Excluir</button>
                            </td>
                        </tr>
                    `;
                });
                break;

            case "Rota":
                renderTabela.innerHTML = tableRota;
                response.itens.forEach((item) => {
                    itensLista += `
                        <tr>
                            <td>${item.enderecoInicio}</td>
                            <td>${item.enderecoFim}</td>
                            <td class="text-end pe-4">
                                <button type="button" class="btn btn-sm btn-outline-secondary editar" data-id="${item.id}" data-EnderecoI="${item.enderecoInicio}" data-EnderecoF="${item.enderecoFim}" data-bs-toggle="modal" data-bs-target="#modalEditar">Editar</button>
                                <button type="button" class="btn btn-sm btn-outline-danger excluir" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalExcluir">Excluir</button>
                            </td>
                        </tr>
                    `;
                });
                break;

            case "Viagens":
                renderTabela.innerHTML = tableViagens;
                response.itens.forEach((item) => {
                    itensLista += `
                        <tr>
                            <td>${item.nomeMotorista}</td>
                            <td>${item.placa}</td>
                            <td>${item.dataSaida}</td>
                            <td>${item.dataChegada}</td>
                            <td>${item.enderecoOrigem}</td>
                            <td>${item.enderecoFim}</td>
                            <td class="text-end pe-4">
                                <button type="button" class="btn btn-sm btn-outline-secondary editar" data-id="${item.id}" data-dsaida="${item.dataSaida}" data-dchegada="${item.dataChegada}" data-bs-toggle="modal" data-bs-target="#modalEditar">Editar</button>
                                <button type="button" class="btn btn-sm btn-outline-danger excluir" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalExcluir">Excluir</button>
                            </td>
                        </tr>
                    `;
                });
                break;
        }


        document.getElementById("tabelaConsulta").innerHTML = itensLista;

        const paginaInfo = document.getElementById("paginaInfo");
        if (paginaInfo) {
            paginaInfo.textContent = `Página ${paginaAtualGlobal} de ${totalPaginasGlobal}`;
        }

        const btnVoltar = document.getElementById("btnVoltar");
        const btnAvancar = document.getElementById("btnAvancar");
        if (paginaAtualGlobal <= 1) btnVoltar.classList.add("disabled");
        if (paginaAtualGlobal >= totalPaginasGlobal) btnAvancar.classList.add("disabled");

    } catch (error) {
        mostrarToast("Não foi possível consultar");
        throw error;
    }
}

async function deletar() {
    try {
        const res = await fetch(`${urlPadrao}/${tipo}/${id}`, {
            method: 'DELETE',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
        });

        if (!res.ok) {
            console.log("Não foi possível apagar o registro por estar vinculado.");
            return;
        }
        const modal = bootstrap.Modal.getInstance(document.getElementById('modalExcluir'));
        modal.hide();
        mostrarToast("Excluído com sucesso!");
        consultarTabela(1);
    } catch (error) {
        mostrarToast("Não foi possível excluir.");
        throw error;
    }
}

function renderEdit() {
    let render = ``;
    conteudoViagens.innerHTML = render;
    switch (tipo) {
        case "Motoristas":
            render = `
                <div class="mb-3">
                    <div class="form-floating">
                        <input type="text" class="form-control shadow-none mb-2" id="inputNome" value="${nome}"/>
                        <label for="inputNome" class="form-label text-start px-3">Nome</label>
                    </div>
                    <div class="form-floating">
                        <input type="text" maxlength="15" class="form-control shadow-none mb-2" id="inputCPF" value="${cpf}"/>
                        <label for="inputCPF" class="form-label text-start px-3">CPF</label>
                    </div>
                    <div class="form-floating">
                        <input type="text" maxlength="15" class="form-control shadow-none mb-3" id="inputCNH" value="${cnh}"/>
                        <label for="inputCNH" class="form-label text-start px-3">CNH</label>
                    </div>
                </div>
                <button id="btnAtualizar" type="button" class="w-100 btn btn-secondary shadow-none">Atualizar</button>
            `;
            conteudoViagens.innerHTML = render;
            break;
        case "Onibus":
            render = `
                <div class="mb-3 d-flex flex-column gap-2">
                    <div class="form-floating">
                        <input type="text" maxlength="7" class="form-control shadow-none" id="inputPlaca" value="${placa}"/>
                        <label for="inputPlaca">Placa do Veículo:</label>
                    </div>
                    <div class="form-floating">
                        <input type="number" class="form-control shadow-none" id="inputCapacidade" value="${capacidade}"/>
                        <label for="inputCapacidade">Capacidade do Veículo:</label>
                    </div>
                    <div class="form-floating">
                        <select id="selectMotoristas" class="form-select" aria-label="Default select example">
                        </select>
                        <label for="selectMotoristas">Selecione um Motorista:</label>
                    </div>
                    <div class="form-floating">
                        <input type="text" class="form-control shadow-none" id="inputEnderecoI" value="${enderecoI}"/>
                        <label for="inputEnderecoI">Endereço Inicial:</label>
                    </div>
                    <div class="form-floating">
                        <input type="text" class="form-control shadow-none" id="inputEnderecoF" value="${enderecoF}"/>
                        <label for="inputEnderecoF">Endereço Final:</label>
                    </div>
                    <div class="form-floating">
                        <input type="datetime-local" class="form-control shadow-none" id="inputDataS" value="${dSaida}"/>
                        <label for="inputDataS">Data e hora Saída:</label>
                    </div>
                    <div class="form-floating">
                        <input type="datetime-local" class="form-control shadow-none" id="inputDataC" value="${dChegada}"/>
                        <label for="inputDataC">Data e Hora Chegada:</label>
                    </div>   
                </div>
                <button id="btnAtualizar" type="button" class="btn btn-secondary shadow-none w-100">Atualizar</button>
            `;
            conteudoViagens.innerHTML = render;
            renderMotoristas();
            break;

        case "Rota":
            render = `
                <div class="mb-3 d-flex flex-column gap-2">
                    <div class="form-floating">
                        <input type="text" class="form-control shadow-none" id="inputEnderecoI" value="${enderecoI}"/>
                        <label for="inputEnderecoI">Endereço Inicial:</label>
                    </div>
                    <div class="form-floating">
                        <input type="text" class="form-control shadow-none" id="inputEnderecoF" value="${enderecoF}"/>
                        <label for="inputEnderecoF">Endereço Final:</label>
                    </div>
                </div>
                <button type="button" id="btnAtualizar" class="btn btn-secondary shadow-none w-100">Atualizar</button>
            `;
            conteudoViagens.innerHTML = render;
            break;
        case "Viagens":
            render = `
                <div class="mb-3 d-flex flex-column gap-2">
                    <div class="form-floating">
                        <input type="datetime-local" class="form-control shadow-none" id="inputDataS" value="${dSaida}"/>
                        <label for="inputDataS">Data Saida:</label>
                    </div>
                    <div class="form-floating">
                        <input type="datetime-local" class="form-control shadow-none" id="inputDataC" value="${dChegada}"/>
                        <label for="inputDataC">Data Chegada:</label>
                    </div>
                    <div class="form-floating">
                        <select type="text" class="form-control shadow-none form-select" aria-label="Default select example" id="selectMotoristas"></select>
                        <label for="selectMotoristas">Selecionar Motorista:</label>
                    </div>
                </div>
            <button type="button" id="btnAtualizar" class="btn btn-secondary shadow-none w-100 btn-geral">Atualizar</button>
            `;
            conteudoViagens.innerHTML = render;
            renderMotoristas();
            break;
    }
}

async function editar() {
    try {
        let payload = {};
        switch (tipo) {
            case "Motoristas":
                payload = {
                    nome: document.getElementById("inputNome").value,
                    cpf: document.getElementById("inputCPF").value,
                    cnh: document.getElementById("inputCNH").value
                };
                break;
            case "Onibus":
                payload = {
                    placa: document.getElementById("inputPlaca").value,
                    capacidadeTotal: document.getElementById("inputCapacidade").value,
                    fkMotorista: document.getElementById("selectMotoristas").value,
                    enderecoInicio: document.getElementById("inputEnderecoI").value,
                    enderecoFim: document.getElementById("inputEnderecoF").value,
                    dataSaida: new Date(document.getElementById("inputDataS").value).toISOString(),
                    dataChegada: new Date(document.getElementById("inputDataC").value).toISOString()
                };
                break;
            case "Rota":
                payload = {
                    enderecoInicial: document.getElementById("inputEnderecoI").value.trim(),
                    enderecoFinal: document.getElementById("inputEnderecoF").value.trim(),
                };
                break;
            case "Viagens":
                payload = {
                    fkMotorista: document.getElementById("selectMotoristas").value,
                    dataSaida: new Date(document.getElementById("inputDataS").value).toISOString(),
                    dataChegada: new Date(document.getElementById("inputDataC").value).toISOString()
                };
                break;
        }

        const res = await fetch(`${urlPadrao}/${tipo}/${id}`, {
            method: 'PUT',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload),
        });

        if (!res.ok) return;

        mostrarToast("Atualizado com sucesso!");
        consultarTabela(paginaAtualGlobal);
    } catch (error) {
        mostrarToast("Não foi possível editar.");
        throw error;
    }
}

async function renderMotoristas() {
    try {
        const res = await fetch(`${urlPadrao}/Motoristas?Pagina=1&TamanhoPagina=100`, {
            method: 'GET',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
        });
        var response = await res.json();
        let selectLista = ``;
        response.itens.forEach((item) => {
            selectLista += `<option value="${item.id}">${item.nome}</option>`;
        });
        const motorista = document.getElementById("selectMotoristas");
        motorista.innerHTML = selectLista;
    } catch (error) {
        throw error;
    }
}

// CHAMADA (FUNÇÕES)
document.addEventListener("DOMContentLoaded", () => {
    consultarTabela(1)
});



btnConsultar.addEventListener('click', async () => {
    await consultarTabela(1);
    mostrarToast("Consulta realizada com sucesso!");
});

renderTabela.addEventListener('click', async (event) => {
    const btnExcluir = event.target.closest(".excluir");
    if (btnExcluir) {
        id = btnExcluir.dataset.id;
    }
    const btnEditar = event.target.closest(".editar");
    if (btnEditar) {
        const rawSaida = btnEditar.dataset.dsaida;
        const rawChegada = btnEditar.dataset.dchegada;

        id = btnEditar.dataset.id;
        nome = btnEditar.dataset.nome;
        cpf = btnEditar.dataset.cpf;
        cnh = btnEditar.dataset.cnh;
        placa = btnEditar.dataset.placa;
        capacidade = btnEditar.dataset.capacidade;
        enderecoI = btnEditar.dataset.enderecoi;
        enderecoF = btnEditar.dataset.enderecof;
        dSaida = rawSaida ? rawSaida.substring(0, 16) : "";
        dChegada = rawChegada ? rawChegada.substring(0, 16) : "";

        renderEdit();

        document.getElementById("inputCPF").addEventListener('input', () => {
            let valor = document.getElementById("inputCPF").value.replace(/\D/g, '').slice(0, 11);

            valor = valor.replace(/^(\d{3})(\d)/, '$1.$2');
            valor = valor.replace(/^(\d{3})\.(\d{3})(\d)/, '$1.$2.$3');
            valor = valor.replace(/\.(\d{3})(\d)/, '.$1-$2');

            document.getElementById("inputCPF").value = valor;
        });
    }
    if (event.target.closest("#btnVoltar")) {
        event.preventDefault();
        if (paginaAtualGlobal > 1) {
            await consultarTabela(paginaAtualGlobal - 1);
        }
    }
    if (event.target.closest("#btnAvancar")) {
        event.preventDefault();
        if (paginaAtualGlobal < totalPaginasGlobal) {
            await consultarTabela(paginaAtualGlobal + 1);
        }
    }
});

renderTabela.addEventListener('change', async (event) => {
    if (event.target.id === 'inputpage') {
        const paginaPesquisa = parseInt(event.target.value);
        if (paginaPesquisa >= 1 && paginaPesquisa <= totalPaginasGlobal) {
            await consultarTabela(paginaPesquisa);
        } else {
            mostrarToast("Página inválida!");
        }
    }
});

confirmar.addEventListener('click', async () => {
    await deletar();
});

conteudoViagens.addEventListener('click', async (event) => {
    if (event.target.id === 'btnAtualizar') {
        await editar();
    }
});

function mostrarToast(msg) {
    const toastElemento = document.getElementById('toast');
    if (!toastElemento) return;
    toastElemento.querySelector('.toast-body').textContent = msg;
    const toast = new bootstrap.Toast(toastElemento, { delay: 4000 });
    toast.show();
}

document.addEventListener("DOMContentLoaded", () => {
    const mensagemSucesso = localStorage.getItem('toastSucesso');

    if (mensagemSucesso) {
        mostrarToast(mensagemSucesso);
        localStorage.removeItem('toastSucesso');
    }
});

