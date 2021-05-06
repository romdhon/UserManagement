using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Models
{
    public interface IUserRepository
    {
         User GetUser(int id);
         IEnumerable<User> GetAllUser();
         User AddUser(User user);
         User RemoveUser(int id);
         User UpdateUser(User user);
    }
}