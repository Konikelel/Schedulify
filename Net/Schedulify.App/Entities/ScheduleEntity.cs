using Schedulify.App.Enums;

namespace Schedulify.App.Entities;

public class ScheduleEntity
{
    public required Guid Id { get; init; }
    
    public required Guid CalendarId { get; set; }
    
    public required Guid CategoryId { get; set; }
    
    public required DateTimeOffset TimeStart { get; set; }
    
    public required DateTimeOffset TimeEnd { get; set; }
    
    public required FrequencyEnum Frequency { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }

    public string? Link { get; set; }

    public DateTimeOffset? CreatedAt { get; init; }
    
    public DateTimeOffset? UpdatedAt { get; init; }
    
    public required Guid AuthorId { get; set; }
}