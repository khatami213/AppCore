using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            long s = 1;
            var student = await _unitOfWork._student.GetByID(s);
            var course = await _unitOfWork._course.GetByID(s);


            student.Name = student.Name + "    " + course.Title;
            return View(student);
        }
    }
}
