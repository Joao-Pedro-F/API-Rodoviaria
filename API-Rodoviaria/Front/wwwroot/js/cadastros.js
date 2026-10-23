//GERAL
let iNome = document.getElementById("inputNome");
let iCpf = document.getElementById("inputCPF");
let iCnh = document.getElementById("inputCNH");
let btnCadastrar = document.getElementById("btnCadastrar");

const urlPadrao = "https://localhost:7171/api";

//FUNÇÕES

async function cadastrarMotorista() {
    let payload = {
        nome: iNome.value,
        cpf: iCpf.value,
        cnh: iCnh.value
    }

    console.log("tentativa");

    const res = await fetch(`https://localhost:7171/api/Motoristas`, {
        method: 'POST',
        headers: {'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
    })

}


//CHAMADO (FUNÇÕES)

btnCadastrar.addEventListener('click', async () => {
    await cadastrarMotorista();
});