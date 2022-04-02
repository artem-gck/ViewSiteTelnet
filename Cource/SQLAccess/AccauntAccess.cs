using Cource.Models;
using Cource.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace Cource.SQLAccess
{
    public class AccauntAccess : IAccauntAccess
    {
        private readonly string connectionString;
        private const string sqlCheckUser = "sp_CheckUser";
        private const string sqlGetUser = "sp_GetUser";
        private const string sqlAddUser = "sp_InsertUser";

        public AccauntAccess(IConfiguration configuration)
            => connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task<User> CheckUser(LoginModel model)
        {
            User user = null;

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlCheckUser, connection);
            command.CommandType = CommandType.StoredProcedure;

            var nameParam = new SqlParameter("@email", model.Email);
            var passwordParam = new SqlParameter("@password", model.Password);
            command.Parameters.Add(nameParam);
            command.Parameters.Add(passwordParam);

            using var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    var id = reader.GetValue(0);
                    var email = reader.GetValue(1);
                    var password = reader.GetValue(2);
                    var role = reader.GetValue(3);

                    user = new User
                    {
                        Id = (int)id,
                        Email = (string)email,
                        Password = (string)password,
                        Role = (string)role,
                    };
                }
            }

            return user;
        }

        public async Task<User> GetUser(RegisterModel model)
        {
            User user = null;

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlGetUser, connection);
            command.CommandType = CommandType.StoredProcedure;

            var nameParam = new SqlParameter("@email", model.Email);
            command.Parameters.Add(nameParam);

            using var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    var id = reader.GetValue(0);
                    var email = reader.GetValue(1);
                    var password = reader.GetValue(2);
                    var role = reader.GetValue(3);

                    user = new User
                    {
                        Id = (int)id,
                        Email = (string)email,
                        Password = (string)password,
                        Role = (string)role
                    };
                }
            }

            return user;
        }

        public async Task<int> AddUser(User user)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlAddUser, connection);
            command.CommandType = CommandType.StoredProcedure;

            var nameParam = new SqlParameter("@email", user.Email);
            var passwordParam = new SqlParameter("@password", user.Password);
            var roleParam = new SqlParameter("@role", user.Role);
            command.Parameters.Add(nameParam);
            command.Parameters.Add(passwordParam);
            command.Parameters.Add(roleParam);

            var id = await command.ExecuteNonQueryAsync();

            return id;
        }
    }
}
