namespace GamingApi.Tests.SharedKernel.Attributes;

public sealed class InlineAutoNSubstituteDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoNSubstituteDataAttribute(params object[] values)
        : base(new AutoNSubstituteDataAttribute(), values)
    {

    }
}
