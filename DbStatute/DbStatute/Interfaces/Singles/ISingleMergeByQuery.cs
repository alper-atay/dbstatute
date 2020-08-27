using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMergeByQuery<TMergeQuery> : ISingleMergeBase
        where TMergeQuery : IMergeQuery
    {
        TMergeQuery MergeQuery { get; }
    }

    public interface ISingleMergeByQuery<TModel, TMergeQuery> : ISingleMergeBase<TModel>, ISingleMergeByQuery<TMergeQuery>
        where TModel : class, IModel, new()
        where TMergeQuery : IMergeQuery<TModel>
    {
        new TMergeQuery MergeQuery { get; }
    }
}