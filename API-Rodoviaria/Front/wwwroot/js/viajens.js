//GERAL
const listarViajens = document.getElementById("listarViajens");

const urlPadrao = 'https://localhost:7171/api';

//FUNÇÕES

async function renderViajens() {

    try {
        const res = await fetch(`${urlPadrao}/Viagens`, {
            method: 'GET',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
        });

        var response = await res.json();


        let itensViajens = ``;
        response.itens.forEach((item) => {
            itensViajens += `
                <tr>
                    <td>${item.enderecoOrigem}</td>
                    <td>${item.enderecoFim}</td>
                    <td>${item.dataSaida}</td>
                    <td>${item.dataChegada}</td>
                    <td>${item.nomeMotorista}</td>
                    <td><button class="btn btn-outline-secondary" data-id="${item.id}" data-bs-toggle="modal" data-bs-target="#teste">Visualizar</button></td>
                </tr>
            `

        });

        listarViajens.innerHTML = itensViajens;

    }catch (error) {
        throw error;
    }
}



//CHAMADA (FUNÇÕES)

renderViajens();