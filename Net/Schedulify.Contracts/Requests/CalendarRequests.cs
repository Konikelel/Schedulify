namespace Schedulify.Contracts.Requests;

public abstract class AbstractCalendarBaseRequest
{
    public required string Name { get; init; }
}

public class CreateCalendarRequest: AbstractCalendarBaseRequest;

public class UpdateCalendarRequest: AbstractCalendarBaseRequest
{
    public required Guid Id { get; init; }
}