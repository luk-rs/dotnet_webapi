using System.Net;
using FluentValidation;
using GamingApi.Games.CQ;
using GamingApi.SharedKernel.Validation;

namespace GamingApi.Games.Validation;

internal class GetGamesQueryValidator : RequestValidator<GetGamesQuery>
{
    public GetGamesQueryValidator()
    {
        RuleFor(query => query.Limit).GreaterThanOrEqualTo(0);
        RuleFor(query => query.Limit).LessThanOrEqualTo(10).WithState(_ => HttpStatusCode.BadRequest);
        RuleFor(query => query.Offset).GreaterThanOrEqualTo(0);
    }
}

