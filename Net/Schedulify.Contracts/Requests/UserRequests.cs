namespace Schedulify.Contracts.Requests;

public class CreateUserRequest
{
    public required string Username { get; init; }
    
    public required string Email { get; init; }
    
    public string? ImageUrl { get; init; }
}

public class UpdateUserRequest
{
    public required Guid Id { get; init; }
    
    public required string Username { get; init; }
    
    public required string Email { get; init; }
    
    public string? ImageUrl { get; init; }
}