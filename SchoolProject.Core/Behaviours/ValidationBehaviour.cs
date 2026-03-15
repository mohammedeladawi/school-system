using FluentValidation;
using MediatR;


namespace SchoolProject.Core.Behaviours;

/// <summary>
/// MediatR Pipeline Behavior that intercepts all requests and validates them before executing the handler.
/// This behavior automatically runs all registered validators for the request type and throws an exception if validation fails.
/// </summary>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    // Collection of all registered validators for the request type
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    // Constructor: receives validators through dependency injection
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Check if there are any validators registered for this request type
        if (_validators.Any())
        {
            // Create a validation context with the incoming request object to pass to validators
            var context = new ValidationContext<TRequest>(request);

            // Execute all registered validators against the request object and collect validation failures
            var failures = _validators
                // Run validation on each registered validator
                .Select(v => v.Validate(context))
                // Flatten all errors from all validators into a single collection
                .SelectMany(result => result.Errors)
                // Filter out any null errors
                .Where(f => f != null)
                // Convert to list for easy counting
                .ToList();

            // If any validation errors were found, throw a ValidationException with all collected errors
            if (failures.Count != 0)
            {
                string errorMessage = failures.Select(f => f.ErrorMessage).FirstOrDefault();
                throw new ValidationException(errorMessage);
            }
        }

        // If validation passes or no validators exist, proceed to the next handler in the pipeline
        return await next();
    }
}
