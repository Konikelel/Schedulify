namespace Schedulify.Contracts.Requests;

public class CreateCalendarRequest
{
    public required string Name { get; init; }
}

public class UpdateCalendarRequest
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
}