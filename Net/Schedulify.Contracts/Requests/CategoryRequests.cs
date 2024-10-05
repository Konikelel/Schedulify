namespace Schedulify.Contracts.Requests;

public class CategoryBaseRequest
{
    public required string Name { get; init; }
}

public class CreateCategoryRequest: CategoryBaseRequest;

public class UpdateCategoryRequest: CategoryBaseRequest
{
    public required Guid Id { get; init; }
}