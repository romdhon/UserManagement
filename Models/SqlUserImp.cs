using System.Collections.Generic;

namespace UserManagement.Models
{
    public class SqlUserImp : IUserRepository
    {
        private UserManagementDbContext dbContext;
        public SqlUserImp(UserManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User AddUser(User user)
        {
            dbContext.Add(user);
            dbContext.SaveChanges();
            return user;
        }

        public IEnumerable<User> GetAllUser()
        {
            return dbContext.Users;
        }

        public User GetUser(int id)
        {
            return dbContext.Users.Find(id);
        }

        public User RemoveUser(int id)
        {
            User user = dbContext.Users.Find(id);
            dbContext.Remove(user);
            dbContext.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            var updateUser = dbContext.Users.Attach(user);
            updateUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();
            return user;
        }
    }
}