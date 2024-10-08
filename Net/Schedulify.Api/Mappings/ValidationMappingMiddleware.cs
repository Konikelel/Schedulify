using FluentValidation;
using Schedulify.Contracts.Responses;

namespace Schedulify.Api.Mappings;

public class ValidationMappingMiddleware
{
    private readonly RequestDelegate _next;
    
    public ValidationMappingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var validationFailureResponse = new ValidationFailureResponse
            {
                Errors = ex.Errors.Select(x => new ValidationResponse
                {
                    PropertyName = x.PropertyName,
                    Message = x.ErrorMessage
                }).ToList()
            };
            
            await context.Response.WriteAsJsonAsync(validationFailureResponse);
        }
    }
}