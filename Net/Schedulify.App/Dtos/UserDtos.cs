namespace Schedulify.App.Dtos;

public abstract class AbstractBaseUserDtos
{
    public required Guid Id { get; init; }
    
    public required string Username { get; set; }
    
    public required string Email { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public required byte[] PasswordHash { get; set; }
    
    public required byte[] PasswordSalt { get; set; }
    
    public required DateTimeOffset UpdatedAt { get; set; }
}

public class CreateUserDto : AbstractBaseUserDtos
{
    public required DateTimeOffset CreatedAt { get; init; }
}

public class UpdateUserDto: AbstractBaseUserDtos;