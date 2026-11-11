using Front.Atributos;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Web;

public class LoginController : Controller
{
    public IActionResult cadastroView()
    {
        return View();
    }
    public IActionResult loginView()
    {
        return View();
    }
    public class LoginRequest
    {
        public string? Perfil { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> Logar([FromBody] LoginRequest request)
    {
        HttpContext.Session.SetString("logado", "true");
        HttpContext.Session.SetString("Usuario", request.Perfil ?? "");
        return Ok();
    }

    [HttpPost]
    public IActionResult Deslogar()
    {
        HttpContext.Session.Clear();
        return Ok();
    }
}