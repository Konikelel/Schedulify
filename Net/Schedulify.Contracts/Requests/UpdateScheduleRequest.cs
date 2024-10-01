namespace Schedulify.Contracts.Requests;

public class UpdateScheduleRequest
{
    public required Guid CalendarId { get; init; }
    
    public required Guid CategoryId { get; init; }
    
    public required DateTimeOffset TimeStart { get; init; }
    
    public required DateTimeOffset TimeEnd { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
    
    public string? Link { get; init; }
}