using Microsoft.AspNetCore.Mvc;

namespace KinoVerwaltungAPI.Controllers
{
    public class KinoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
