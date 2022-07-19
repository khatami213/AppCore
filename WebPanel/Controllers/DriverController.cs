using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebPanel.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
