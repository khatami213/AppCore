using Microsoft.AspNetCore.Mvc;

namespace WebPanel.Controllers
{
    public class DriverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
