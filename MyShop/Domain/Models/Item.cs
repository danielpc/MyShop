using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.API.Domain.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}