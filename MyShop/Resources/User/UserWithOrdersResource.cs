using System.Collections.Generic;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Resources.User
{
    public class UserWithOrdersResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Domain.Models.Order> Orders { get; set; } = new List<Domain.Models.Order>();
    }
}