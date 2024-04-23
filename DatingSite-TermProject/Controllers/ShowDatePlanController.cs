using Microsoft.AspNetCore.Mvc;

namespace DatingSite_TermProject.Controllers
{
    public class ShowDatePlanController : Controller
    {
        public IActionResult ShowPlan()
        {
            return View("~/Views/Dates/ShowDatePlan.cshtml");
        }
    }
}
