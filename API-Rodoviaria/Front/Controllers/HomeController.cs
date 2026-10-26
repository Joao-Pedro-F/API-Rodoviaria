using Front.Atributos;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Front.Controllers
{

    [ValidarSessao]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult cadViagensView()
        {
            return View();
        }
        public IActionResult cadRotasView()
        {
            return View();
        }
        public IActionResult cadMotoristasView()
        {
            return View();
        }
        public IActionResult cadOnibusView()
        {
            return View();
        }
        public IActionResult consultaView()
        {
            return View();
        }
        public IActionResult viagensView()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
