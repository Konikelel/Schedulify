namespace Schedulify.Contracts.Responses;

public abstract class AbstractCategoryBaseResponse
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
}

public class CreateCategoryResponse : AbstractCategoryBaseResponse;

public class UpdateCategoryResponse: AbstractCategoryBaseResponse;

public class GetCategoryResponse: AbstractCategoryBaseResponse;