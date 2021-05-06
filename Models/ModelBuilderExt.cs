using Microsoft.EntityFrameworkCore;

namespace UserManagement.Models
{
    public static class ModelBuilderExt
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Name = "Romdon", Email = "romdon@gmail.com", Department = Dep.Engineer, PhotoPath = "" }
            );
        }
    }
}