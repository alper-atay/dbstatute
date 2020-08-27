using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleMergeByQuery<TMergeQuery> : IMultipleMergeBase
        where TMergeQuery : IMergeQuery
    {
        TMergeQuery MergeQuery { get; }
    }

    public interface IMultipleMergeByQuery<TModel, TMergeQuery> : IMultipleMergeBase<TModel>, IMultipleMergeByQuery<TMergeQuery>
        where TModel : class, IModel, new()
        where TMergeQuery : IMergeQuery<TModel>
    {
        new TMergeQuery MergeQuery { get; }
    }
}