using AutoMapper;
using CleanArchitecture.Core.Domain;
using CleanArchitecture.Web.ApiModels;

namespace CleanArchitecture.Web
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Product, ProductDTO>();
        }
    }
}
