//GERAL
let nome = document.getElementById("inputNome");
let senha = document.getElementById("inputSenha");
const btnlogar = document.getElementById("btnLogin");

async function fazerLogin() {

    try {

        const payload = {

            username: nome.value,
            password: senha.value
        }

        const res = await fetch("https://localhost:7171/api/Auth/login", {
            method: 'POST',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload),
        });

        if (!res.ok) return

        inputNome.value = ""
        inputSenha.value = ""

        var response = await res.json();

        localStorage.setItem("Expira", response.expiraEm);
        localStorage.setItem("Usuario", response.perfil);

        const resLogar = await fetch(`https://localhost:7217/Login/Logar`, {
            method: 'POST',
        })

    window.location.href = "/Home/Index";

    } catch (error) {
        throw error
    }
}

btnlogar.addEventListener('click', async () => {
    await fazerLogin()
})