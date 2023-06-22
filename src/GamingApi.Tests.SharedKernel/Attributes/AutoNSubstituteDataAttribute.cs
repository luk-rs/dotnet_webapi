namespace GamingApi.Tests.SharedKernel.Attributes;
public sealed class AutoNSubstituteDataAttribute : AutoDataAttribute
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
