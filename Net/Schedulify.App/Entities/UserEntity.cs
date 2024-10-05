namespace Schedulify.App.Entities;

public class UserEntity
{
    public required Guid Id { get; init; }
    
    public required string Username { get; init; }
    
    public required string Email { get; init; }
    
    public string? ImageUrl { get; init; }
    
    public required byte[] PasswordHash { get; init; }
    
    public required byte[] PasswordSalt { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
    
    public DateTimeOffset UpdatedAt { get; init; }
}