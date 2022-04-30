using Cource.Models;
using Cource.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Cource.SQLAccess
{
    public class AccauntAccess : IAccauntAccess
    {
        private readonly string connectionString;
        private const string sqlCheckUser = "sp_CheckUser";
        private const string sqlGetUser = "sp_GetUser";
        private const string sqlGetUserById = "sp_GetUserById";
        private const string sqlGetUsers = "sp_GetUsers";
        private const string sqlAddUser = "sp_InsertUser";
        private const string sqlUpdateUser = "sp_UpdateUser";
        private const string sqlDeleteUser = "sp_DeleteUser";
        private const string sqlGetRoles = "sp_GetRoles";
        private const string sqlGetMakes = "sp_GetMakes";
        private const string sqlGetRole = "sp_GetRole";

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
                    var newsMaker = reader.GetValue(4);

                    user = new User
                    {
                        Id = (int)id,
                        Email = (string)email,
                        Password = (string)password,
                        Role = (string)role,
                        NewsMaker = (bool)newsMaker,
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
                    var newsMaker = reader.GetValue(4);

                    user = new User
                    {
                        Id = (int)id,
                        Email = (string)email,
                        Password = (string)password,
                        Role = (string)role,
                        NewsMaker = (bool)newsMaker,
                    };
                }
            }

            return user;
        }

        public async Task<int> AddUser(User user)
        {
            int roleId;

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlGetRole, connection);
            command.CommandType = CommandType.StoredProcedure;

            var roleParam1 = new SqlParameter("@role", user.Role);
            command.Parameters.Add(roleParam1);

            var reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();
            roleId = reader.GetInt32(0);

            await reader.CloseAsync();

            command = new SqlCommand(sqlAddUser, connection);
            command.CommandType = CommandType.StoredProcedure;

            var nameParam = new SqlParameter("@email", user.Email);
            var passwordParam = new SqlParameter("@password", user.Password);
            var roleParam = new SqlParameter("@role", roleId);
            var newsMakerParam = new SqlParameter("@newsMaker", user.NewsMaker);
            command.Parameters.Add(nameParam);
            command.Parameters.Add(passwordParam);
            command.Parameters.Add(roleParam);
            command.Parameters.Add(newsMakerParam);

            var id = await command.ExecuteNonQueryAsync();

            return id;
        }

        public async Task<int> UpdateUser(User user)
        {
            int roleId;

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlGetRole, connection);
            command.CommandType = CommandType.StoredProcedure;

            var roleParam1 = new SqlParameter("@role", user.Role);
            command.Parameters.Add(roleParam1);

            var reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();
            roleId = reader.GetInt32(0);

            await reader.CloseAsync();

            command = new SqlCommand(sqlUpdateUser, connection);
            command.CommandType = CommandType.StoredProcedure;

            var oldNameParam = new SqlParameter("@id", user.Id);
            var newNameParam = new SqlParameter("@email", user.Email);
            var newPasswordParam = new SqlParameter("@password", user.Password);
            var newRoleParam = new SqlParameter("@role", roleId);
            var newsMakerParam = new SqlParameter("@newsMaker", user.NewsMaker);
            command.Parameters.Add(oldNameParam);
            command.Parameters.Add(newNameParam);
            command.Parameters.Add(newPasswordParam);
            command.Parameters.Add(newRoleParam);
            command.Parameters.Add(newsMakerParam);

            var id = await command.ExecuteNonQueryAsync();

            return id;
        }

        public async Task<int> DeleteUser(User user)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlDeleteUser, connection);
            command.CommandType = CommandType.StoredProcedure;

            var nameParam = new SqlParameter("@id", user.Id);
            command.Parameters.Add(nameParam);

            var id = await command.ExecuteNonQueryAsync();

            return id;
        }

        public async Task<List<User>> GetUsers()
        {
            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlGetUsers, connection);
            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();
            var listOfUsers = new List<User>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    var id = reader.GetValue(0);
                    var email = reader.GetValue(1);
                    var password = reader.GetValue(2);
                    var role = reader.GetValue(3);
                    var newsMaker = reader.GetValue(4);

                    var user = new User
                    {
                        Id = (int)id,
                        Email = (string)email,
                        Password = (string)password,
                        Role = (string)role,
                        NewsMaker = (bool)newsMaker,
                    };

                    listOfUsers.Add(user);
                }
            }

            return listOfUsers;
        }

        public async Task<User> GetUserById(int id)
        {
            User user = null;

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlGetUserById, connection);
            command.CommandType = CommandType.StoredProcedure;

            var nameParam = new SqlParameter("@id", id);
            command.Parameters.Add(nameParam);

            using var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    var email = reader.GetValue(1);
                    var password = reader.GetValue(2);
                    var role = reader.GetValue(3);
                    var newsMaker = reader.GetValue(4);

                    user = new User
                    {
                        Id = (int)id,
                        Email = (string)email,
                        Password = (string)password,
                        Role = (string)role,
                        NewsMaker = (bool)newsMaker,
                    };
                }
            }

            return user;
        }

        public async Task<List<RoleModel>> GetRoles()
        {
            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlGetRoles, connection);
            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();
            var listOfRoles = new List<RoleModel>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    var role = reader.GetValue(0);

                    listOfRoles.Add(new RoleModel() { Role = (string)role });
                }
            }

            return listOfRoles;
        }

        public async Task<List<MakeModel>> GetMakes()
        {
            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            var command = new SqlCommand(sqlGetMakes, connection);
            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();
            var listOfMakes = new List<MakeModel>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    var make = reader.GetValue(0);

                    listOfMakes.Add(new MakeModel() { Make = (bool)make });
                }
            }

            return listOfMakes;
        }
    }
}
