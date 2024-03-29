using System.Linq;
using AutoMapper;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Security.Tokens;
using Supermarket.API.Extensions;
using Supermarket.API.Resources;
using Supermarket.API.Resources.Token;
using Supermarket.API.Resources.User;

namespace Supermarket.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();

            CreateMap<Product, ProductResource>()
                .ForMember(src => src.UnitOfMeasurement,
                    opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));

            CreateMap<User, UserResource>()
                .ForMember(u => u.Roles, opt => opt.MapFrom((u => u.UserRoles.Select(ur => ur.Role.Name))));
            
            CreateMap<User, UserWithoutOrdersResource>();

            CreateMap<User, UserWithOrdersResource>();
            
            CreateMap<AccessToken, AccessTokenResource>()
                .ForMember(a => a.AccessToken, opt => opt.MapFrom(a => a.Token))
                .ForMember(a => a.RefreshToken, opt => opt.MapFrom(a => a.RefreshToken.Token))
                .ForMember(a => a.Expiration, opt => opt.MapFrom(a => a.Expiration));

        }
    }
}