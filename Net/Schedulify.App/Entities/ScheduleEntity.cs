using Schedulify.App.Enums;

namespace Schedulify.App.Entities;

public class ScheduleEntity
{
    public required Guid Id { get; init; }
    
    public required Guid CalendarId { get; init; }
    
    public required Guid CategoryId { get; init; }
    
    public required DateTimeOffset TimeStart { get; init; }
    
    public required DateTimeOffset TimeEnd { get; init; }
    
    public required FrequencyEnum Frequency { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }

    public string? Link { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
    
    public required DateTimeOffset UpdatedAt { get; init; }
    
    public required Guid OwnerId { get; init; }
}