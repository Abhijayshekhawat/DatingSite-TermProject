using Microsoft.AspNetCore.Mvc;

namespace DatingSite_TermProject.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
