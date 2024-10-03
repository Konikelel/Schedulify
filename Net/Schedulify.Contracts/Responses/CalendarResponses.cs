using Schedulify.App.Models;

namespace Schedulify.Contracts.Responses;

public class CreateCalendarResponse
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
}

public class UpdateCalendarResponse: CreateCalendarResponse;

public class GetCalendarResponse: CreateCalendarResponse
{
    public required IEnumerable<CategoryModel> Categories { get; init; }
    
    public required IEnumerable<ScheduleModel> Schedules { get; init; }
}