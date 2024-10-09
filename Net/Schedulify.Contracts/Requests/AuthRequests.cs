namespace Schedulify.Contracts.Requests;

public class LoginRequest
{
    public required string UsernameOrEmail { get; init; }
    
    public required string Password { get; init; }
}

public class RegisterRequest
{
    public required string Username { get; init; }
    
    public required string Email { get; init; }
    
    public string? ImageUrl { get; init; }
    
    public required string Password { get; init; }
}