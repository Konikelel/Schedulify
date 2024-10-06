namespace Schedulify.Contracts.Responses;

public abstract class AbstractBaseCategoryResponse
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
}

public class GetCategoryResponse: AbstractBaseCategoryResponse;

public class GetByUserCategoryResponse
{
    public required IEnumerable<GetCategoryResponse> Categories { get; init; }
}

public class CreateCategoryResponse : AbstractBaseCategoryResponse;

public class UpdateCategoryResponse: AbstractBaseCategoryResponse;