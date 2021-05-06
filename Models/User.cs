using UserManagement.Models;

namespace UserManagement.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Dep Department { get; set; }
        public string PhotoPath { get; set; }
    }
}