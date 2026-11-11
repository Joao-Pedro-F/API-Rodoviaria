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

        if (!res.ok) {
            return
            }

        inputNome.value = ""
        inputSenha.value = ""

        mostrarToast("Usuário logado com sucesso!");

        var response = await res.json();

        localStorage.setItem("Expira", response.expiraEm);
        localStorage.setItem("Usuario", response.perfil);

        const resLogar = await fetch(`https://localhost:7217/Login/Logar`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            credentials: 'include',
            body: JSON.stringify({perfil: response.perfil}),
        })

        localStorage.setItem('toastSucesso', 'Login realizado com sucesso!');
        if (localStorage.getItem("Usuario") == "Admin") {
            window.location.href = "/Home/consultaView";
            return
        }
        window.location.href = "/Home/viagensViewUser";

    } catch (error) {
        mostrarToast(`Não foi possível fazer login. ${response.erro}`);
        throw error
    }
}

btnlogar.addEventListener('click', async () => {
    await fazerLogin()
})

function mostrarToast(msg) {
    const toastElemento = document.getElementById('toast');
    toastElemento.querySelector('.toast-body').textContent = msg;
    const toast = new bootstrap.Toast(toastElemento, { delay: 4000 });
    toast.show();
} 