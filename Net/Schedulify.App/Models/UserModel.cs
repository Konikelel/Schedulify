namespace Schedulify.App.Models;

public class UserModel
{
    public required Guid Id { get; init; }
    
    public required string Username { get; set; }
    
    public required string Email { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public required byte[] PasswordHash { get; set; }
    
    public required byte[] PasswordSalt { get; set; }
}