using Schedulify.App.Enums;

namespace Schedulify.Contracts.Responses;

public abstract class AbstractBaseScheduleResponses
{
    public required Guid Id { get; init; }
    
    public required Guid CalendarId { get; init; }
    
    public Guid? CategoryId { get; init; }
    
    public required DateTimeOffset TimeStart { get; init; }
    
    public required DateTimeOffset TimeEnd { get; init; }
    
    public required FrequencyEnum Frequency { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
    
    public string? Link { get; init; }
}

public class CreateScheduleResponses : AbstractBaseScheduleResponses;

public class UpdateScheduleResponses : AbstractBaseScheduleResponses;

public class GetScheduleResponse : AbstractBaseScheduleResponses;