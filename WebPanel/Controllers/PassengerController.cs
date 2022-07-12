using CoreService;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace WebPanel.Controllers
{
    [Authorize]
    public class PassengerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PassengerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.FirstOrDefault(r => r.Type == "UserId").Value.ToLong();
            var user = await _unitOfWork._user.GetByID(userId);
            ViewBag.Wallet = user.Wallet;
            ViewBag.Username = user.Username;
            return View();
        }

        [HttpPost]
        public IActionResult CalculateFee(double distance)
        {
            double fee = 0;

            if (distance < 1)
            {
                fee = 10000;
            }
            else if (distance >= 1 && distance < 10)
            {
                fee = 20000;
            }
            else if (distance >= 10 && distance < 50)
            {
                fee = 50000;
            }
            else
                fee = 100000;

            return Content(fee.ToString());

        }
    }
}
