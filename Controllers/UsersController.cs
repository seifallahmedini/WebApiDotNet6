namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Models.Requests.Queries;
using WebApi.Services;
using WebApi.Services.V1;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] PaginationQuery paginationQuery)
    {
        var users = _userService.GetAll(paginationQuery);
        return Ok(users);
    }

    [HttpGet("{id:Guid}")]
    public IActionResult GetById(Guid id)
    {
        var users = _userService.GetById(id.ToString());
        return Ok(users);
    }
}
