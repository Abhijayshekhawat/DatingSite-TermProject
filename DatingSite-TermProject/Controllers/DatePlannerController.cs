using Microsoft.AspNetCore.Mvc;

namespace DatingSite_TermProject.Controllers
{
    public class DatePlannerController : Controller
    {
        public IActionResult UpsertDatePlan()
        {
            return View("~/Views/Main/Dates/DatePlanner.cshtml");
        }
    }
}
