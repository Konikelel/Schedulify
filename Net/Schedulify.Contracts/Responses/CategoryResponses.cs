namespace Schedulify.Contracts.Responses;

public class CreateCategoryResponse
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public required Guid AuthorId { get; init; }
}

public class UpdateCategoryResponse: CreateCategoryResponse { }

public class GetCategoryResponse: CreateCalendarResponse { }