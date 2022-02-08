using AutoMapper;
using WebApi.Entities;
using WebApi.Entities.V1;
using WebApi.Models;
using WebApi.Models.Requests;
using WebApi.Models.Responses;

namespace WebApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            // From domain to reponse   
            CreateMap<Dummy, DummyResponse>();
            CreateMap<User, AuthenticateResponse>();
            CreateMap<User, UserResponse>();

            // From request to domain
            CreateMap<CreateDummyRequest, Dummy>();
            CreateMap<RegisterRequest, User>();

            // From event messge to domain
        }
    }
}
