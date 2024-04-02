using Microsoft.AspNetCore.Mvc;

namespace DatingSite_TermProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
