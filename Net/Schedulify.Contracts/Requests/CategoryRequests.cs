namespace Schedulify.Contracts.Requests;

public class CreateCategoryRequest
{
    public required string Name { get; init; }
}

public class UpdateCategoryRequest: CreateCategoryRequest
{
    public required Guid Id { get; init; }
}