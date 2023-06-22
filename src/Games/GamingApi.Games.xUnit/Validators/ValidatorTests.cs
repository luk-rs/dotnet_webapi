using FluentValidation;
using GamingApi.Games.CQ;
using GamingApi.Games.Validators;

namespace GamingApi.Games.xUnit.Validators;
public sealed class ValidatorTests
{

    [Theory]
    [InlineAutoNSubstituteData(0, 2, false)]
    [InlineAutoNSubstituteData(2, 0, false)]
    [InlineAutoNSubstituteData(0, 9, false)]
    [InlineAutoNSubstituteData(0, -2, true)]
    [InlineAutoNSubstituteData(-1, 2, true)]
    [InlineAutoNSubstituteData(-1, -2, true)]
    [InlineAutoNSubstituteData(0, 11, true)]
    public async Task ValidationWorksForGetGamesQuery(int offset, int limit, bool expectedThrow, GetGamesQueryValidator sut)
    {
        var query = new GetGamesQuery(limit, offset);

        var validation = async () => await sut.ValidateAndThrowAsync(query);

        if (expectedThrow)
            await validation.Should().ThrowExactlyAsync<ValidationException>();
        else
            await validation.Should().NotThrowAsync();
    }
}
