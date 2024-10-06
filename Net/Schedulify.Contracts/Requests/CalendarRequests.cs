namespace Schedulify.Contracts.Requests;

public abstract class AbstractBaseCalendarRequest
{
    public required string Name { get; init; }
}

public class CreateCalendarRequest : AbstractBaseCalendarRequest;

public class UpdateCalendarRequest : AbstractBaseCalendarRequest
{
    public required Guid Id { get; init; }
}