using Front.Atributos;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{

    [ValidarSessao]
    public class CadastrosController : Controller
    {
        public IActionResult cadMotoristasView()
        {
            return View();
        }
        public IActionResult cadOnibusView()
        {
            return View();
        }
        public IActionResult cadRotasView()
        {
            return View();
        }
        public IActionResult cadViagensView()
        {
            return View();
        }
    }
}
