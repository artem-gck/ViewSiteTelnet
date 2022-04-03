using Cource.SQLAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;

namespace Cource.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var a = User.Claims.FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultRoleClaimType);
            var role = a is null ? null : a.Value;
            //return Content(role);
            return View();
        }
    }
}
