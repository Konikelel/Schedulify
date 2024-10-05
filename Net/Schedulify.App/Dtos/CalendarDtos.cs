namespace Schedulify.App.Dtos;

public abstract class CalendarBaseDto
{
    public required Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required Guid OwnerId { get; set; }
    
    public required DateTimeOffset UpdatedAt { get; set; }
}

public class CreateCalendarDto : CalendarBaseDto
{
    public required DateTimeOffset CreatedAt { get; set; }
}

public class UpdateCalendarDto : CalendarBaseDto;