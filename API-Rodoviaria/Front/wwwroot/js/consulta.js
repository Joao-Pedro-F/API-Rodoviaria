//CONTINUAR A FAZER O TRATAMENTO DE ERROS JUNTO A SOLUÇÃO PARA OCUPAR MENOS ESPAÇO QUE O GEMINI DEU








//GERAL
const btnConsultar = document.getElementById("btnConsultar");
const qtdPag = document.getElementById("qtdPag");
const selectTipo = document.getElementById("selectTipo");
const renderTabela = document.getElementById("renderTabela");
const confirmar = document.querySelector(".confirmar");
const conteudoViagens = document.querySelector(".conteudoViagens");

const urlPadrao = 'https://localhost:7171/api';
let id;
let tipo;

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

        if(!res.ok) return

        tipo = selectTipo.value;
        var response = await res.json();
    
        let itensLista = ``;

        switch (tipo) {
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

    try {

        const res = await fetch(`${urlPadrao}/${tipo}/${id}`, {
            method: 'DELETE',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
        });

        if (!res.ok) {
            switch (tipo) {
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
            return
        }

    } catch (error) {
        throw error
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
                        <input type="text" class="form-control shadow-none mb-2" id="inputNome" />
                        <label for="inputNome" class="form-label text-start px-3">Nome</label>
                    </div>
                    <div class="form-floating">
                        <input type="text" maxlength="15" class="form-control shadow-none mb-2" id="inputCPF" />
                        <label for="inputCPF" class="form-label text-start px-3">CPF</label>
                    </div>
                    <div class="form-floating">
                        <input type="text" maxlength="15" class="form-control shadow-none mb-3" id="inputCNH" />
                        <label for="inputCNH" class="form-label text-start px-3">CNH</label>
                    </div>
                </div>
                <button id="btnAtualizar" type="button" class="w-100 btn btn-secondary shadow-none">Atualizar</button>
            `;
            conteudoViagens.innerHTML = render;
            break; 
        case "Onibus":
            render = `
                <div class="mb-3  d-flex flex-column gap-2">
                    <div class="form-floating">
                        <input type="text" maxlength="7" class="form-control shadow-none" id="inputPlaca" />
                        <label for="inputPlaca">Placa do Veículo:</label>
                    </div>
                    <div class="form-floating">
                        <input type="number" class="form-control shadow-none" id="inputCapacidade" />
                        <label for="inputCapacidade">Capacidade do Veículo:</label>
                    </div>
                    <div class="form-floating">
                        <select id="selectMotoristas" class="form-select" aria-label="Default select example">
                        </select>
                        <label for="selectMotoristas">Selecione um Motorista:</label>
                    </div>
                    <div class="form-floating">
                        <input type="text" class="form-control shadow-none" id="inputEnderecoI" />
                        <label for="inputEnderecoI">Endereço Inicial:</label>
                    </div>
                    <div class="form-floating">
                        <input type="text" class="form-control shadow-none" id="inputEnderecoF" />
                        <label for="inputEnderecoF">Endereço Final:</label>
                    </div>
                    <div class="form-floating">
                        <input type="datetime-local" class="form-control shadow-none" id="inputDataS" />
                        <label for="inputDataS">Data e hora Saída:</label>
                    </div>
                    <div class="form-floating">
                        <input type="datetime-local" class="form-control shadow-none" id="inputDataC" />
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
                <div class="mb-3  d-flex flex-column gap-2">
                    <div class="form-floating">
                        <input type="text" class="form-control shadow-none" id="inputEnderecoI" />
                        <label for="inputEnderecoI">Endereço Inicial:</label>
                    </div>
                
                    <div class="form-floating">
                        <input type="text" class="form-control shadow-none" id="inputEnderecoF" />
                        <label for="inputEnderecoF">Endereço Final:</label>
                    </div>
                </div>
                <button type="button" id="btnAtualizar" class="btn btn-secondary shadow-none w-100">Atualizar</button>
            `;
            conteudoViagens.innerHTML = render;
            break; 
        case "Viagens":
            render = `
                <div class="mb-3  d-flex flex-column gap-2">
                    <div class="form-floating">
                        <input type="datetime-local" class="form-control shadow-none" id="inputDataS" />
                        <label for="inputDataS">Data Saida:</label>
                    </div>
                    <div class="form-floating">
                        <input type="datetime-local" class="form-control shadow-none" id="inputDataC" />
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

    switch(tipo) {
    
        case "Motoristas":
            let iNome = document.getElementById("inputNome").value;
            let iCpf = document.getElementById("inputCPF").value;
            let iCnh = document.getElementById("inputCNH").value;

            payload = {
                nome: iNome,
                cpf: iCpf,
                cnh: iCnh
            }

            const res = await fetch(`${urlPadrao}/${tipo}/${id}`, {
                method: 'PUT',
                credentials: "include",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload),
            });
            
            break;

        case "Onibus":

            let placa = document.getElementById("inputPlaca");
            let capTotal = document.getElementById("inputCapacidade");
            let enderecoInicio = document.getElementById("inputEnderecoI");
            let enderecoFIm = document.getElementById("inputEnderecoF");
            let dataSaida = document.getElementById("inputDataS");
            let dataChegada = document.getElementById("inputDataC");
            let motorista = document.getElementById("selectMotorista");

            const dataChegadaISO = new Date(dataChegada.value).toISOString();
            const dataSaidaISO = new Date(dataSaida.value).toISOString();

            payload = {
                placa : placa.value,
                capacidadeTotal : capTotal.value,
                fkMotorista : motorista.value,
                enderecoInicio : enderecoInicio.value,
                enderecoFim : enderecoFIm.value,
                dataSaida: dataSaidaISO,
                dataChegada: dataChegadaISO 
            }

            res0 = await fetch(`${urlPadrao}/${tipo}/${id}`, {
                method: 'PUT',
                credentials: "include",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload),
            });

            break;

        case "Rota":
            let enderecoInicial = document.getElementById("inputEnderecoI");
            let enderecoFinal = document.getElementById("inputEnderecoF");

            payload = {
                enderecoInicial : enderecoInicial.value.trim(),
                enderecoFinal : enderecoFinal.value.trim(),
            }

            res1 = await fetch(`${urlPadrao}/${tipo}/${id}`, {
                method: 'PUT',
                credentials: "include",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload),
            });

            break;

        case "Viagens":
            let dataSaidaV = document.getElementById("inputDataS");
            let dataChegadaV = document.getElementById("inputDataC");
            let motoristaV = document.getElementById("selectMotoristas");

            const dataChegadaISOV = new Date(dataChegadaV.value).toISOString();
            const dataSaidaISOV = new Date(dataSaidaV.value).toISOString();

            payload = {
                fkMotorista: motoristaV.value,
                dataSaida: dataSaidaISOV,
                dataChegada: dataChegadaISOV
            }

            res2 = await fetch(`${urlPadrao}/${tipo}/${id}`, {
                method: 'PUT',
                credentials: "include",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload),
            });

            break; 
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

        const motorista = document.getElementById("selectMotoristas");
        motorista.innerHTML = selectLista;
    } catch (error) {
        throw error
    }
}

function mostrarToast(msg) {
    const toastElemento = document.getElementById('toast');
    toastElemento.querySelector('.toast-body').textContent = msg;
    const toast = new bootstrap.Toast(toastElemento, { delay: 4000 });
    toast.show();
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
    const btnE = event.target.closest(".editar");
    if (btnE) {
        id = btnE.dataset.id;
        renderEdit();   
    }
});

confirmar.addEventListener('click', async () => {
    deletar();
});

conteudoViagens.addEventListener('click', async (event) => {
    if (event.target.id === 'btnAtualizar') {
        await editar();
    }
});