using Schedulify.App.Models;

namespace Schedulify.Contracts.Responses;

public abstract class AbstractBaseCalendarResponse
{
    public required Guid Id { get; init; }
        
    public required string Name { get; init; }
}

public class CreateCalendarResponse : AbstractBaseCalendarResponse;

public class UpdateCalendarResponse: AbstractBaseCalendarResponse;

public class GetCalendarResponse: AbstractBaseCalendarResponse;