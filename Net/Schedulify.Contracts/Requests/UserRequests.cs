namespace Schedulify.Contracts.Requests;

public abstract class AbstractBaseUserRequest
{
    public required string Username { get; init; }
    
    public required string Email { get; init; }
    
    public string? ImageUrl { get; init; }
}

public class CreateUserRequest : AbstractBaseUserRequest;

public class UpdateUserRequest : AbstractBaseUserRequest
{
    public required Guid Id { get; init; }
}