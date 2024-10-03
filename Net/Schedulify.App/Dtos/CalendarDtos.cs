namespace Schedulify.App.Dtos;

public class CreateCalendarDto
{
    public required Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required Guid OwnerId { get; set; }
}

public class UpdateCalendarDto: CreateCalendarDto;