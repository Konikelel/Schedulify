using Schedulify.App.Enums;

namespace Schedulify.App.Models;

public class ScheduleModel
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
    
    public required DateTimeOffset CreatedAt { get; set; }
    
    public required DateTimeOffset UpdatedAt { get; set; }
    
    public required Guid AuthorId { get; set; }
}