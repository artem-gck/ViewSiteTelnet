using Cource.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Cource.SQLAccess
{
    public class NewsAccess : INewsAccess
    {
        private readonly string connectionString;
        private const string sqlGetNews = "sp_GetNews";
        private const string sqlGetNewss = "sp_GetNewss";
        private const string sqlAddNews = "sp_InsertNews";
        private const string sqlUpdateNews = "sp_UpdateNews";
        private const string sqlDeleteNews = "sp_DeleteNews";

        public NewsAccess(IConfiguration configuration)
            => connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task<List<News>> GetAllNews()
        {
            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlGetNewss, connection);
            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();
            var listOfNews = new List<News>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    var id = reader.GetValue(0);
                    var title = reader.GetValue(1);
                    var text = reader.GetValue(2);

                    var news = new News
                    {
                        Id = (int)id,
                        Title = (string)title,
                        Text = (string)text,
                    };

                    listOfNews.Add(news);
                }
            }

            return listOfNews;
        }

        public async Task<News> GetNews(int id)
        {
            News news = null;

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlGetNews, connection);
            command.CommandType = CommandType.StoredProcedure;

            var nameParam = new SqlParameter("@id", id);
            command.Parameters.Add(nameParam);

            using var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    var title = reader.GetValue(1);
                    var text = reader.GetValue(2);

                    news = new News
                    {
                        Id = (int)id,
                        Title = (string)title,
                        Text = (string)text,
                    };
                }
            }

            return news;
        }

        public async Task<int> UpdateNews(News news)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlUpdateNews, connection);
            command.CommandType = CommandType.StoredProcedure;

            var id = new SqlParameter("@id", news.Id);
            var title = new SqlParameter("@title", news.Title);
            var text = new SqlParameter("@text", news.Text);
            command.Parameters.Add(id);
            command.Parameters.Add(title);
            command.Parameters.Add(text);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteNews(int id)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlDeleteNews, connection);
            command.CommandType = CommandType.StoredProcedure;

            var idParam = new SqlParameter("@id", id);
            command.Parameters.Add(idParam);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> CreateNews(News news)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlAddNews, connection);
            command.CommandType = CommandType.StoredProcedure;

            var title = new SqlParameter("@title", news.Title);
            var text = new SqlParameter("@text", news.Text);
            command.Parameters.Add(title);
            command.Parameters.Add(text);

            var id = await command.ExecuteNonQueryAsync();

            return id;
        }
    }
}
