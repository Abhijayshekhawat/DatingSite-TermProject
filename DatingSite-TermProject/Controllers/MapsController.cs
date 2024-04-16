using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using DatingSite_TermProject.Models;



namespace DatingSite_TermProject.Controllers
{
    public class MapsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
