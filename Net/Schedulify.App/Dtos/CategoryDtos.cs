namespace Schedulify.App.Dtos;

public abstract class CategoryBaseDto
{
    public required Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required Guid OwnerId { get; set; }
    
    public required DateTimeOffset UpdatedAt { get; set; }
}

public class CreateCategoryDto : CategoryBaseDto
{
    public required DateTimeOffset CreatedAt { get; init; }
}

public class UpdateCategoryDto: CategoryBaseDto;