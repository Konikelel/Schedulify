namespace Schedulify.Contracts.Requests;

public abstract class CalendarBaseRequest
{
    public required string Name { get; init; }
}

public class CreateCalendarRequest: CalendarBaseRequest;

public class UpdateCalendarRequest: CalendarBaseRequest
{
    public required Guid Id { get; init; }
}