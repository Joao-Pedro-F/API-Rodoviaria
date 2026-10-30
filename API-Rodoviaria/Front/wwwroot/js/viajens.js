//GERAL
const listarViajens = document.getElementById("listarViajens");
const conteudoViagens = document.querySelector(".conteudoViagens");

const urlPadrao = 'https://localhost:7171/api';
let idViajem;

//FUNÇÕES

async function renderViajens() {

    try {
        const res = await fetch(`${urlPadrao}/Viagens?Pagina=1&TamanhoPagina=30`, {
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
                    <td><button class="btn btn-outline-secondary btnVisualizar" data-id="${item.id}">Visualizar</button></td>
                </tr>
            `

        });

        listarViajens.innerHTML = itensViajens;

    }catch (error) {
        throw error;
    }
}

async function renderViajem() {

    const res = await fetch(`${urlPadrao}/Viagens/${idViajem}`, {
        method: 'GET',
        credentials: "include",
        headers: { 'Content-Type': 'application/json' },
    });

    var response = await res.json();

    const infoViajem = `
        <div class="d-flex flex-column gap-3">
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

}

//CHAMADA (FUNÇÕES)

renderViajens();

listarViajens.addEventListener('click', async (event) => {
    const btn = event.target.closest(".btnVisualizar");
    if (btn) {
        idViajem = btn.dataset.id;
        const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById('teste'));
        modal.show();
        await renderViajem();
    }
});