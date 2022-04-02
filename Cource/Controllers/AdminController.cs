using Cource.SQLAccess;
using Microsoft.AspNetCore.Mvc;

namespace Cource.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAccauntAccess _accauntAccess;

        public AdminController(IAccauntAccess accauntAccess)
            => _accauntAccess = accauntAccess;
    }
}
