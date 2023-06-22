using MediatR.Pipeline;

namespace GamingApi.SharedKernel.Validation;

public abstract class RequestValidator<TRequest> : AbstractValidator<TRequest>, IRequestPreProcessor<TRequest>
    where TRequest : IBaseRequest
{
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var validator = this as IValidator<TRequest>;

        try
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);
        }
        catch (ValidationException ex)
        {
            foreach (var error in ex.Errors)
            {
                var state = error.CustomState;
                if (state is HttpStatusCode)
                {
                    ex.Data[nameof(HttpStatusCode)] = state;
                }
            }
            throw;
        }
    }
}


