using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using UserManagement.Models;

namespace UserManagement.ViewModel
{
    public class UserCreateViewModel
    {   
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="Office Email")]
        public string Email { get; set; }

        [Required]
        public Dep Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}