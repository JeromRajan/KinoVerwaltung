using Microsoft.AspNetCore.Mvc;

namespace KinoVerwaltungAPI.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
