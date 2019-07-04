using System.ComponentModel.DataAnnotations;

namespace Supermarket.API.Resources
{
    public class CategoryResponse
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}