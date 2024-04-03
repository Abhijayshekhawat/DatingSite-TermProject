using Microsoft.AspNetCore.Mvc;

namespace DatingSite_TermProject.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
