using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class UserController : Controller
    {
        public IActionResult userView()
        {
            return View();
        }
    }
}
