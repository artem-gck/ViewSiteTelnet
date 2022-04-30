using Microsoft.AspNetCore.Mvc;

namespace Cource.Controllers
{
    public class TheoryController : Controller
    {
        public IActionResult Index()
            => View();
    }
}
