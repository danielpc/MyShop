using System;
using System.Security.Claims;

namespace Supermarket.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return Convert.ToInt32(principal.Identity.Name);
        }
    }
}