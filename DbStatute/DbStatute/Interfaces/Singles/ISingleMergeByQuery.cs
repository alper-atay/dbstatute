using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMergeByQuery<TMergeQuery> : ISingleMergeBase
        where TMergeQuery : IMergeProxy
    {
        TMergeQuery MergeQuery { get; }
    }

    public interface ISingleMergeByQuery<TModel, TMergeQuery> : ISingleMergeBase<TModel>, ISingleMergeByQuery<TMergeQuery>
        where TModel : class, IModel, new()
        where TMergeQuery : IMergeProxy<TModel>
    {
        new TMergeQuery MergeQuery { get; }
    }
}