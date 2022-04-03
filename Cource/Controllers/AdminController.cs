using Cource.Models;
using Cource.SQLAccess;
using Cource.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Cource.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAccauntAccess _accauntAccess;

        public AdminController(IAccauntAccess accauntAccess)
            => _accauntAccess = accauntAccess;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _accauntAccess.GetUsers();

            var usersView = users.Select(user => new LoginModel()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                NewsMaker = user.NewsMaker,
            });

            return View(usersView);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _accauntAccess.GetRoles();

            ViewBag.roles = new SelectList(roles, "Role", "Role");

            var makes = await _accauntAccess.GetMakes();

            ViewBag.making = new SelectList(makes, "Make", "Make");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoginModel userView)
        {
            var user = new User() {
                Id = userView.Id,
                Email = userView.Email,
                Password = userView.Password,
                Role = userView.Role,
                NewsMaker = userView.NewsMaker,
            };

            await _accauntAccess.AddUser(user);

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _accauntAccess.GetUserById(id);

            var userView = new LoginModel()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                NewsMaker = user.NewsMaker,
            };

            return View(userView);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LoginModel userView)
        {
            var user = new User()
            {
                Id = userView.Id,
                Email = userView.Email,
                Password = userView.Password,
                Role = userView.Role,
                NewsMaker = userView.NewsMaker,
            };

            await _accauntAccess.DeleteUser(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var roles = await _accauntAccess.GetRoles();

            ViewBag.roles = new SelectList(roles, "Role", "Role");

            var makes = await _accauntAccess.GetMakes();

            ViewBag.making = new SelectList(makes, "Make", "Make");

            var user = await _accauntAccess.GetUserById(id);

            var userView = new LoginModel()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                NewsMaker = user.NewsMaker,
            };

            return View(userView);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LoginModel userView)
        {
            var user = new User()
            {
                Id = userView.Id,
                Email = userView.Email,
                Password = userView.Password,
                Role = userView.Role,
                NewsMaker = userView.NewsMaker,
            };

            await _accauntAccess.UpdateUser(user);

            return RedirectToAction("Index");
        }
    }
}