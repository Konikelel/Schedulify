namespace Schedulify.Contracts.Requests;

public abstract class AbstractBaseCategoryRequest
{
    public required string Name { get; init; }
}

public class CreateCategoryRequest : AbstractBaseCategoryRequest;

public class UpdateCategoryRequest : AbstractBaseCategoryRequest
{
    public required Guid Id { get; init; }
}