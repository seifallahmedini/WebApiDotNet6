
using WebApi.Models;
using WebApi.Models.Requests.Queries;
using WebApi.Models.Responses;

namespace WebApi.Services.V1
{
    public interface IUserService
    {
        PagedResponse<UserResponse> GetAll(PaginationQuery paginationQuery);
        UserResponse GetById(string id);
    }
}
