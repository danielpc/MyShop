using System.ComponentModel.DataAnnotations;

namespace Supermarket.API.Resources.Token
{
    public class RevokeTokenResource
    {
        [Required]
        public string Token { get; set; }
    }
}