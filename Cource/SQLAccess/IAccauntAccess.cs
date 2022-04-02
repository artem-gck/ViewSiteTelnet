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
        public Task<int> AddUser(User user);
        public Task<List<User>> GetUsers();
        public Task<int> DeleteUser(LoginModel model);
        public Task<int> UpdateUser(LoginModel model);
    }
}
