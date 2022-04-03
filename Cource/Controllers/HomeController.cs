using Cource.Models;
using Cource.SQLAccess;
using Cource.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cource.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsAccess _newsAccess;

        public HomeController(INewsAccess newsAccess)
            => _newsAccess = newsAccess;

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var news = await _newsAccess.GetAllNews();

            var newsView = news.Select(n => new NewsModel()
            {
                Id = n.Id,
                Title = n.Title,
                Text = n.Text,
            });

            return View(newsView);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
            => View();

        [HttpPost]
        public async Task<IActionResult> Create(NewsModel newsView)
        {
            var news = new News()
            {
                Id = newsView.Id,
                Title = newsView.Title,
                Text = newsView.Text,
            };

            await _newsAccess.CreateNews(news);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _newsAccess.GetNews(id);

            var newsView = new NewsModel()
            {
                Id = news.Id,
                Title = news.Title,
                Text = news.Text,
            };

            return View(newsView);
        }

        public async Task<IActionResult> Delete(NewsModel newsView)
        {
            await _newsAccess.DeleteNews(newsView.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var news = await _newsAccess.GetNews(id);

            var newsView = new NewsModel()
            {
                Id = news.Id,
                Title = news.Title,
                Text = news.Text,
            };

            return View(newsView);
        }

        [HttpPost]
        public async Task<IActionResult> Update(NewsModel newsView)
        {
            var news = new News()
            {
                Id = newsView.Id,
                Title = newsView.Title,
                Text = newsView.Text,
            };

            await _newsAccess.UpdateNews(news);

            return RedirectToAction("Index");
        }
    }
}
