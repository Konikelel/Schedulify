namespace Schedulify.App.Dtos;

public abstract class UserBaseDtos
{
    public required Guid Id { get; init; }
    
    public required string Username { get; set; }
    
    public required string Email { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public required string Password { get; set; }
}

public class CreateUserDto: UserBaseDtos;

public class UpdateUserDto: UserBaseDtos
{
    public required DateTimeOffset UpdatedAt { get; set; }
}