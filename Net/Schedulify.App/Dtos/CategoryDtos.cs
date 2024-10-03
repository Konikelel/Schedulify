namespace Schedulify.App.Dtos;

public class CreateCategoryDto
{
    public required Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required Guid OwnerId { get; set; }
}

public class UpdateCategoryDto: CreateCategoryDto;