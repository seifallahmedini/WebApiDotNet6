using AutoMapper;
using WebApi.Entities.V1;
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

            // From request to domain
            CreateMap<CreateDummyRequest, Dummy>();

            // From event messge to domain
        }
    }
}
