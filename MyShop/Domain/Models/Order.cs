using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.API.Domain.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public  ICollection<Item> Items { get; set; }
    }
}