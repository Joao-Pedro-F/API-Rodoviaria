using Front.Atributos;
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

    [HttpPost]
    public async Task<IActionResult> Logar()
    {
        HttpContext.Session.SetString("logado", "true");
        return Ok();
    }

    [HttpPost]
    public IActionResult Deslogar()
    {
        HttpContext.Session.Clear();
        return Ok();
    }
}