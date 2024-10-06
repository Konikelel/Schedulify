namespace Schedulify.Contracts.Responses;

public abstract class AbstractBaseCategoryResponse
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
}

public class CreateCategoryResponse : AbstractBaseCategoryResponse;

public class UpdateCategoryResponse: AbstractBaseCategoryResponse;

public class GetCategoryResponse: AbstractBaseCategoryResponse;