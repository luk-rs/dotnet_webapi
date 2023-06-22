namespace GamingApi.SharedKernel.Mapping;


public interface IMapper
{
    bool Maps<TExpectedSource, TExpectedTarget>();
}

public interface IMapper<TSource, TTarget> : IMapper
    where TSource : class
    where TTarget : class
{
    new bool Maps<TExpectedSource, TExpectedTarget>() => typeof(TExpectedSource) == typeof(TSource) && typeof(TExpectedTarget) == typeof(TTarget);
}


public abstract class Mapper<TSource, TTarget> : IMapper<TSource, TTarget>
    where TSource : class
    where TTarget : class
{
    public bool Maps<TExpectedSource, TExpectedTarget>()
    {
        return (this as IMapper<TSource, TTarget>)?.Maps<TExpectedSource, TExpectedTarget>() ?? false;
    }
}
