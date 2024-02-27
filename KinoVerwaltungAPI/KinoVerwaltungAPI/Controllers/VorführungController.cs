using Microsoft.AspNetCore.Mvc;

namespace KinoVerwaltungAPI.Controllers
{
    public class VorführungController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
