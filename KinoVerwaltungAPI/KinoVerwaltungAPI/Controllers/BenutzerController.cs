using Microsoft.AspNetCore.Mvc;

namespace KinoVerwaltungAPI.Controllers
{
    public class BenutzerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
