using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleMergeByProxy<TMergeProxy> : IMultipleMergeBase
        where TMergeProxy : IMergeProxy
    {
        TMergeProxy MergeProxy { get; }
    }

    public interface IMultipleMergeByProxy<TModel, TMergeProxy> : IMultipleMergeBase<TModel>, IMultipleMergeByProxy<TMergeProxy>
        where TModel : class, IModel, new()
        where TMergeProxy : IMergeProxy<TModel>
    {
        new TMergeProxy MergeProxy { get; }
    }
}