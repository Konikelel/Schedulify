using Schedulify.App.Models;

namespace Schedulify.Contracts.Responses;

public abstract class CalendarBaseResponse
{
    public required Guid Id { get; init; }
        
    public required string Name { get; init; }
}

public class CreateCalendarResponse : CalendarBaseResponse;

public class UpdateCalendarResponse: CalendarBaseResponse;

public class GetCalendarResponse: CalendarBaseResponse
{
    public required IEnumerable<CategoryModel> Categories { get; init; }
    
    public required IEnumerable<ScheduleModel> Schedules { get; init; }
}