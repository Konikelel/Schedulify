using Schedulify.App.Enums;

namespace Schedulify.Contracts.Requests;

public abstract class AbstractScheduleBaseRequest
{
    public required Guid CalendarId { get; init; }
    
    public required Guid CategoryId { get; init; }
    
    public required DateTimeOffset TimeStart { get; init; }
    
    public required DateTimeOffset TimeEnd { get; init; }
    
    public required FrequencyEnum Frequency { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
    
    public string? Link { get; init; }
}

public class CreateScheduleRequest: AbstractScheduleBaseRequest;

public class UpdateScheduleRequest: AbstractScheduleBaseRequest
{
    public required Guid Id { get; init; }
}