namespace Schedulify.App.Entities;

public class CalendarEntity
{
    public required Guid Id { get; init; }
    
    public required string Name { get; set; }

    public required DateTimeOffset CreatedAt { get; init; }

    public required DateTimeOffset UpdatedAt { get; set; }
    
    public required Guid OwnerId { get; set; }
}