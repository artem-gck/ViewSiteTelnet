using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cource.Controllers
{
    public class HistoryController : Controller
    {
        public async Task<IActionResult> Index()
            => View();
    }
}
