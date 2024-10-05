namespace Schedulify.Contracts.Requests;

public abstract class UserBaseRequest
{
    public required string Username { get; init; }
    
    public required string Email { get; init; }
    
    public string? ImageUrl { get; set; }
}

public class CreateUserRequest: UserBaseRequest;

public class UpdateUserRequest: UserBaseRequest
{
    public required Guid Id { get; init; }
}