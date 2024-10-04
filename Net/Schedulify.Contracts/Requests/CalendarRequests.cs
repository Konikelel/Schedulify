namespace Schedulify.Contracts.Requests;

public class CreateCalendarRequest
{
    public required string Name { get; init; }
}

public class UpdateCalendarRequest: CreateCalendarRequest
{
    public required Guid Id { get; init; }
}