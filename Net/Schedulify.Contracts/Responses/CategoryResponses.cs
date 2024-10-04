namespace Schedulify.Contracts.Responses;

public abstract class CategoryBaseResponse
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
}

public class CreateCategoryResponse : CategoryBaseResponse;

public class UpdateCategoryResponse: CategoryBaseResponse;

public class GetCategoryResponse: CategoryBaseResponse;