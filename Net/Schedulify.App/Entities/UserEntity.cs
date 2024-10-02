namespace Schedulify.App.Entities;

public class UserEntity
{
    public required Guid Id { get; init; }
    
    public required string Username { get; set; }
    
    public required string Email { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public required byte[] PasswordHash { get; set; }
    
    public required byte[] PasswordSalt { get; set; }

    public DateTimeOffset? CreatedAt { get; init; }
    
    public DateTimeOffset? UpdatedAt { get; init; }
}