using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.API.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}