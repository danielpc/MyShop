using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.API.Domain.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Product> Products { get; set; } = new List<Product>();
    }
}