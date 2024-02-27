using Microsoft.AspNetCore.Mvc;

namespace KinoVerwaltungAPI.Controllers
{
    public class VorfuehrungController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
