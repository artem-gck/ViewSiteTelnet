using Cource.Models;
using Cource.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cource.SQLAccess
{
    public interface IAccauntAccess
    {
        public Task<User> CheckUser(LoginModel model);
        public Task<User> GetUser(RegisterModel model);
        public Task<User> GetUserById(int id);
        public Task<int> AddUser(User user);
        public Task<List<User>> GetUsers();
        public Task<int> DeleteUser(User user);
        public Task<int> UpdateUser(User user);
        public Task<List<RoleModel>> GetRoles();
        public Task<List<MakeModel>> GetMakes();
    }
}
