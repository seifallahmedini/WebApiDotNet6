
using WebApi.Models;
using WebApi.Models.Responses;

namespace WebApi.Services.V1
{
    public interface IUserService
    {
        IEnumerable<UserResponse> GetAll();
        UserResponse GetById(string id);
    }
}
