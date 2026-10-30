//GERAL
const btnConsultar = document.getElementById("btnConsultar");
const qtdPag = document.getElementById("qtdPag");
const selectTipo = document.getElementById("selectTipo");
const renderTabela = document.getElementById("renderTabela");
const cancelar = document.querySelector(".cancelar");
const cancelar1 = document.querySelector(".cancelar1");
const confirmar = document.querySelector(".confirmar");

const urlPadrao = 'https://localhost:7171/api';
let id;

var tableMotorista = `
    <table class="table table-striped">
        <thead>
            <tr>
                <th>Nome</th>
                <th>Alterações</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
`
var tableOnibus = `
    <table class="table table-striped">
        <thead>
            <tr>
                <th>Placa</th>
                <th>Capacidade</th>
                <th>Data Saída</th>
                <th>Data Chegada</th>
                <th>Alterações</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
`
var tableRota = `
    <table class="table table-striped">
        <thead>
            <tr>
                <th>Endereço Inicial</th>
                <th>Endereço Final</th>
                <th>Alterações</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
`
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
                <th>Alterações</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
`

//FUNÇÕES

async function consultarTabela() {
    try {
        const res = await fetch(`${urlPadrao}/${selectTipo.value}?Pagina=1&TamanhoPagina=${qtdPag.value}`, {
            method: 'GET',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
        });

        var response = await res.json();
    
        let itensLista = ``;

        switch (selectTipo.value.trim()) {
            case "Motoristas":
                itensLista = ``;
                renderTabela.innerHTML = tableMotorista;
                response.itens.forEach((item) => {
                    itensLista += `
                        <tr>
                            <td>${item.nome}</td>
                            <td>
                                <button type="button" class="btn btn-sm btn-outline-secondary editar" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalEditar">Editar</button>
                                <button type="button" class="btn btn-sm btn-outline-danger excluir" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalExcluir">Excluir</button>
                            </td>
                        </tr>
                    `;
                });
                var tabelaConsulta = document.getElementById("tabelaConsulta");
            
                tabelaConsulta.innerHTML = itensLista;
                break;

            case "Onibus":
                itensLista = ``;
                renderTabela.innerHTML = tableOnibus;
                response.itens.forEach((item) => {
                    itensLista += `
                        <tr>
                            <td>${item.placa}</td>
                            <td>${item.capacidadeTotal}</td>
                            <td>${item.datasaida}</td>
                            <td>${item.dataChegada}</td>
                            <td>
                                <button type="button" class="btn btn-sm btn-outline-secondary editar" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalEditar">Editar</button>
                                <button type="button" class="btn btn-sm btn-outline-danger excluir" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalExcluir">Excluir</button>
                            </td>
                        </tr>
                    `;
                });
                var tabelaConsulta = document.getElementById("tabelaConsulta");

                tabelaConsulta.innerHTML = itensLista;
                break;

            case "Rota":
                itensLista = ``;
                renderTabela.innerHTML = tableRota;
                response.itens.forEach((item) => {
                    itensLista += `
                        <tr>
                            <td>${item.enderecoInicio}</td>
                            <td>${item.enderecoFim}</td>
                            <td>
                                <button type="button" class="btn btn-sm btn-outline-secondary editar" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalEditar">Editar</button>
                                <button type="button" class="btn btn-sm btn-outline-danger excluir" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalExcluir">Excluir</button>
                            </td>
                        </tr>
                    `;
                });
                var tabelaConsulta = document.getElementById("tabelaConsulta");

                tabelaConsulta.innerHTML = itensLista;
                break;

            case "Viagens":
                itensLista = ``;
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
                            <td>
                                <button type="button" class="btn btn-sm btn-outline-secondary editar" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalEditar">Editar</button>
                                <button type="button" class="btn btn-sm btn-outline-danger excluir" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#modalExcluir">Excluir</button>
                            </td>
                        </tr>
                    `;
                });
                var tabelaConsulta = document.getElementById("tabelaConsulta");

                tabelaConsulta.innerHTML = itensLista;
                break;
        }

    } catch (error) { 
        throw error
    }   

}

async function deletar() {

    const res = await fetch(`${urlPadrao}/${selectTipo.value}/${id}`, {
        method: 'DELETE',
        credentials: "include",
        headers: { 'Content-Type': 'application/json' },
    });

    if (!res.ok) {
        switch (selectTipo.value) {
            case "Motoristas":
                console.log("Não foi possível apagar pelo motoristar estar vinculado à uma viagem!")
                break;
            case "Onibus":
                console.log("Não foi possível apagar por onibus estar vinculado à uma viagem!")
                break;
            case "Rota":
                console.log("Não foi possível apagar pois a rota esta vinculada à uma viagem!")
                break;
        }
    }

}

async function rendeEdit() {

}
async function editar() {

}

//CHAMADA (FUNÇÕES)

btnConsultar.addEventListener('click', async () => {
    await consultarTabela();
});

renderTabela.addEventListener('click', async (event) => {
    const btn = event.target.closest(".excluir");
    if (btn) {
        id = btn.dataset.id;
    }
});

cancelar.addEventListener('click', async () => {
    id = "";
});
cancelar1.addEventListener('click', async () => {
    id = "";
});
confirmar.addEventListener('click', async () => {
    deletar()
});

