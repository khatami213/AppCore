using Microsoft.AspNetCore.Mvc;

namespace WebPanel.Controllers
{
    public class adminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
