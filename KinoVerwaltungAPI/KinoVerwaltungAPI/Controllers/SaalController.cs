using Microsoft.AspNetCore.Mvc;

namespace KinoVerwaltungAPI.Controllers
{
    public class SaalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
