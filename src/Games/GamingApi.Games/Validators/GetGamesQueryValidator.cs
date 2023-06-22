using GamingApi.Games.CQ;
using GamingApi.SharedKernel.Validation;

namespace GamingApi.Games.Validators;

public sealed class GetGamesQueryValidator : RequestValidator<GetGamesQuery>
{
    public GetGamesQueryValidator()
    {
        RuleFor(query => query.Limit).GreaterThanOrEqualTo(0);
        RuleFor(query => query.Limit).LessThanOrEqualTo(10);
        RuleFor(query => query.Offset).GreaterThanOrEqualTo(0);
    }
}

