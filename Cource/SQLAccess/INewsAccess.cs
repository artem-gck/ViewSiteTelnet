using Cource.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cource.SQLAccess
{
    public interface INewsAccess
    {
        public Task<List<News>> GetAllNews();
        public Task<News> GetNews(int id);
        public Task<int> UpdateNews(News news);
        public Task<int> DeleteNews(int id);
        public Task<int> CreateNews(News news);
    }
}
