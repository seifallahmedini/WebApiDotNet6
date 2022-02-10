namespace WebApi.Services;

using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Entities;
using WebApi.Entities.V1;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Models.Requests.Queries;
using WebApi.Models.Responses;
using WebApi.Repositories.V1;
using WebApi.Services.V1;
using WebApi.Services.V1.Helpers;

public class UserService : IUserService
{
    private static string BASE_URL = "api/1/users";
    private readonly AppSettings _appSettings;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUriService _uriService;

    public UserService(
        IMapper mapper, 
        IOptions<AppSettings> appSettings, 
        IUserRepository userRepository, 
        IUriService uriService)
    {
        _appSettings = appSettings.Value;
        _mapper = mapper;
        _userRepository = userRepository;
        _uriService = uriService;
    }

    public PagedResponse<UserResponse> GetAll(PaginationQuery paginationQuery)
    {
        var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);

        var users = _userRepository.FindAll();

        // if the paginationFilter is null then return the first page + 10 elements
        if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
        {
            var defaultUsers = _mapper.Map<IList<UserResponse>>(users.ToList().OrderBy(x => x.CreatedAt));
            return (PagedResponse<UserResponse>)PaginationHelpers.CreatePaginatedResponse(BASE_URL, _uriService, paginationQuery, defaultUsers, users.Count());
        }

        // if the paginationFilter is not null
        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
        var paginatedUsers = users.Skip(skip).Take(paginationFilter.PageSize).OrderBy(x => x.CreatedAt);

        var filteredUsers = _mapper.Map<IList<UserResponse>>(paginatedUsers);
        return (PagedResponse<UserResponse>)PaginationHelpers.CreatePaginatedResponse(BASE_URL, _uriService, paginationQuery, filteredUsers, users.Count());
    }

    public UserResponse GetById(string id)
    {
        var user = _userRepository.FindOne(x => x.Id.Equals(id));
        return _mapper.Map<UserResponse>(user);
    }

    // helper methods

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}