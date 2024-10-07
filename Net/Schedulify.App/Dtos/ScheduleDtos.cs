using Schedulify.App.Enums;

namespace Schedulify.App.Dtos;

public abstract class AbstractBaseScheduleDto
{
    public required Guid Id { get; init; }
    
    public required Guid CalendarId { get; set; }
    
    public Guid? CategoryId { get; set; }
    
    public required DateTimeOffset TimeStart { get; set; }
    
    public required DateTimeOffset TimeEnd { get; set; }    
    
    public required FrequencyEnum Frequency { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
    public string? Link { get; set; }
    
    public required Guid OwnerId { get; set; }
    
    public required DateTimeOffset UpdatedAt { get; set; }
}

public class CreateScheduleDto: AbstractBaseScheduleDto
{
    public required DateTimeOffset CreatedAt { get; init; }
}

public class UpdateScheduleDto: AbstractBaseScheduleDto;