//GERAL
const btnConsultar = document.getElementById("btnConsultar");
const qtdPag = document.getElementById("qtdPag");
const selectTipo = document.getElementById("selectTipo");
const tabelaConsulta = document.getElementById("tabelaConsulta");
const renderTabela = document.getElementById("renderTabela");

const urlPadrao = 'https://localhost:7171/api';

var tableMotorista = `
    <table>
        <thead>
            <tr>
                <th>Nome</th>
                <th>CPF</th>
                <th>CNH</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
`
var tableOnibus = `
    <table>
        <thead>
            <tr>
                <th>Placa</th>
                <th>Capacidade</th>
                <th>Data Saída</th>
                <th>Data Chegada</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
`
var tableRota = `
    <table>
        <thead>
            <tr>
                <th>Endereço Inicial</th>
                <th>Endereço Final</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
`
var tableViagens = `
    <table>
        <thead>
            <tr>
                <th>Motorista</th>
                <th>Placa do Veículo</th>
                <th>Data Saída</th>
                <th>Data Chegada</th>
                <th>Endereço Inicial</th>
                <th>Endereço Final</th>
            </tr>
        </thead>
        <tbody id="tabelaConsulta">
        </tbody>
    </table>
`

//FUNÇÕES

async function consultarTabela() {
    console.log(selectTipo.value)
    const res = await fetch(`${urlPadrao}/${selectTipo.value}`, {
        method: 'GET',
        credentials: "include",
        headers: { 'Content-Type': 'application/json' },
    });

    var response = res.json();

    if(!response.ok) return

    let itensLista = ``;

    switch (selectTipo.value) {
        case "Motorista":
            itensLista = ``;
            renderTabela.innerHTML = tableMotorista;
            response.itens.forEach((item) => {
                itensLista += `
                    <tr>
                        <td>${item.nome}</td>
                        <td>${item.cpf}</td>
                        <td>${item.cnh}</td>
                    </tr>
                `;
            });
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
                    </tr>
                `;
            });
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
                    </tr>
                `;
            });
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
                    </tr>
                `;
            });
            tabelaConsulta.innerHTML = itensLista;
            break;
    }

}



//CHAMADA (FUNÇÕES)

btnConsultar.addEventListener('click', async () => {

    await consultarTabela();

});