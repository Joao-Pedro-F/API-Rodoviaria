//GERAL
const btnConsultar = document.getElementById("btnConsultar");
const qtdPag = document.getElementById("qtdPag");
const selectTipo = document.getElementById("selectTipo");
const renderTabela = document.getElementById("renderTabela");

const urlPadrao = 'https://localhost:7171/api';

var tableMotorista = `
    <table class="table table-striped">
        <thead>
            <tr>
                <th>Nome</th>
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
        console.log(response)
    
        let itensLista = ``;

        switch (selectTipo.value.trim()) {
            case "Motoristas":
                itensLista = ``;
                renderTabela.innerHTML = tableMotorista;
                response.itens.forEach((item) => {
                    itensLista += `
                        <tr>
                            <td>${item.nome}</td>
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



//CHAMADA (FUNÇÕES)

btnConsultar.addEventListener('click', async () => {
    await consultarTabela();
});