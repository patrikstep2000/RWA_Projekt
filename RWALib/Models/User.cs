using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWALib.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }
        public string CreatedAt { get; set; }
        public string DeletedAt { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}
