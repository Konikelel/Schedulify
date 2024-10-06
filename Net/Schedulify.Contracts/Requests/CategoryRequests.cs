namespace Schedulify.Contracts.Requests;

public class AbstractCategoryBaseRequest
{
    public required string Name { get; init; }
}

public class CreateCategoryRequest: AbstractCategoryBaseRequest;

public class UpdateCategoryRequest: AbstractCategoryBaseRequest
{
    public required Guid Id { get; init; }
}