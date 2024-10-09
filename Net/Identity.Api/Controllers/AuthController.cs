using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost(ApiEndpoints.Register)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost(ApiEndpoints.Login)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        throw new NotImplementedException();
    }
}