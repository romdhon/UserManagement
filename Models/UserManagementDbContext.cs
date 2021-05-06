using Microsoft.EntityFrameworkCore;

namespace UserManagement.Models
{
    public class UserManagementDbContext : DbContext
    {
        public UserManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
            // modelBuilder.Entity<User>().HasData(
            //     new User { UserID = 1, Name = "Romdon", Email = "romdon@gmail.com", Department = Dep.Engineer, PhotoPath = "" }
            //     // new User { UserID = 2, Name = "Hasnan", Email = "hasnan@gmail.com", Department = Dep.Engineer, PhotoPath = "" },
            //     // new User { UserID = 3, Name = "Nan", Email = "Nan@gmail.com", Department = Dep.Engineer, PhotoPath = "" }
            // );
        }
    }
}