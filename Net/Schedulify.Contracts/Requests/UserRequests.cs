namespace Schedulify.Contracts.Requests;

public class LoginUserRequest
{
    public required string UsernameOrEmail { get; init; }
    
    public required string Password { get; init; }
}

public class RegisterUserRequest
{
    public required string Username { get; init; }
    
    public required string Email { get; init; }
    
    public string? ImageUrl { get; init; }
    
    public required string Password { get; init; }
}