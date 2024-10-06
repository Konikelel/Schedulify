using Schedulify.App.Models;

namespace Schedulify.Contracts.Responses;

public abstract class AbstractCalendarBaseResponse
{
    public required Guid Id { get; init; }
        
    public required string Name { get; init; }
}

public class CreateCalendarResponse : AbstractCalendarBaseResponse;

public class UpdateCalendarResponse: AbstractCalendarBaseResponse;

public class GetCalendarResponse: AbstractCalendarBaseResponse
{
    public required IEnumerable<CategoryModel> Categories { get; init; }
    
    public required IEnumerable<ScheduleModel> Schedules { get; init; }
}