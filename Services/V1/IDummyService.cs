using WebApi.Models.Requests;
using WebApi.Models.Responses;

namespace WebApi.Services.V1
{
    public interface IDummyService
    {
        DummyResponse CreateDummy(CreateDummyRequest dummyRequest);
        IEnumerable<DummyResponse> FindAllDummies();
    }
}
