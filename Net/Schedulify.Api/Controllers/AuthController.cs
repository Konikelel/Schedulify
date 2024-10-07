using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedulify.Contracts.Requests;

namespace Schedulify.Api.Controllers;

[ApiController]
public class AuthController: ControllerBase
{
    private readonly IMapper _mapper;

    public AuthController(
        IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost(ApiEndpoints.Users.Register)]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost(ApiEndpoints.Users.Login)]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        throw new NotImplementedException();
    }
}