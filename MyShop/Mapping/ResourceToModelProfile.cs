using AutoMapper;
using Supermarket.API.Domain.Models;
using Supermarket.API.Resources;
using Supermarket.API.Resources.User;

namespace Supermarket.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<CategoryResponse, Category>();
            CreateMap<UserPost, User>();
            CreateMap<UserCredentialsResource, User>();
            CreateMap<UserLoginResource, User>();
        }
    }
}