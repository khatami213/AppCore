using Microsoft.AspNetCore.Mvc;

namespace WebPanel.Controllers
{
    public class PassengerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
