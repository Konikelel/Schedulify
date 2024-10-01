namespace Schedulify.Contracts.Responses;

public class UpdateScheduleResponses
{
    public Guid Id { get; init; }
    
    public required Guid CalendarId { get; init; }
    
    public required Guid CategoryId { get; init; }
    
    public required DateTimeOffset TimeStart { get; init; }
    
    public required DateTimeOffset TimeEnd { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
    
    public string? Link { get; init; }
    
    public required DateTimeOffset CreatedAt { get; set; }
    
    public required DateTimeOffset UpdatedAt { get; set; }
    
    public required Guid AuthorId { get; set; }
}