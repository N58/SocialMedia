using FluentResults;
using FluentValidation;
using MediatR;

namespace SocialMedia.Application.Validation;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest> validator)
    : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ResultBase, new()
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(request, cancellationToken);

        if (result.IsValid) return await next();

        var response = new TResponse();

        foreach (var reason in result.Errors)
            response.Reasons.Add(new Error(reason.ErrorMessage));

        return response;
    }
}