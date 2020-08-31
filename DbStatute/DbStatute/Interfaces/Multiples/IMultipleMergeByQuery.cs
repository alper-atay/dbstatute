using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleMergeByQuery<TMergeQuery> : IMultipleMergeBase
        where TMergeQuery : IMergeProxy
    {
        TMergeQuery MergeQuery { get; }
    }

    public interface IMultipleMergeByQuery<TModel, TMergeQuery> : IMultipleMergeBase<TModel>, IMultipleMergeByQuery<TMergeQuery>
        where TModel : class, IModel, new()
        where TMergeQuery : IMergeProxy<TModel>
    {
        new TMergeQuery MergeQuery { get; }
    }
}