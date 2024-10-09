namespace Schedulify.Contracts.Requests;

public class LoginAuthRequest
{
    public required string UsernameOrEmail { get; init; }
    
    public required string Password { get; init; }
}

public class RegisterAuthRequest
{
    public required string Username { get; init; }
    
    public required string Email { get; init; }
    
    public string? ImageUrl { get; init; }
    
    public required string Password { get; init; }
}