using System.ComponentModel.DataAnnotations;

namespace Supermarket.API.Resources.User
{
    public class UserPost
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
    }
}