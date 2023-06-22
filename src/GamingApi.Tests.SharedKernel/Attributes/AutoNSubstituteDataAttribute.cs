using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace GamingApi.Tests.SharedKernel.Attributes;
public class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute()
        : base(() =>
        {
            var customization = new AutoNSubstituteCustomization();
            return new Fixture().Customize(customization);
        })
    {
    }
}
