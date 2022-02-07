
using WebApi.Models;
using WebApi.Models.Responses;

namespace WebApi.Services.V1
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<UserResponse> GetAll();
        UserResponse GetById(string id);
    }
}
