using DbStatute.Interfaces.Querying.Statutes;

namespace DbStatute.Interfaces
{
    public interface ISingleMergeByQuery<TMergeQuery> : ISingleMerge
        where TMergeQuery : IMergeQuery
    {
        TMergeQuery MergeQuery { get; }
    }

    public interface ISingleMergeByQuery<TModel, TMergeQuery> : ISingleMerge<TModel>, ISingleMerge
        where TModel : class, IModel, new()
        where TMergeQuery : IMergerQuery<TModel>
    {
        new TMergeQuery MergeQuery { get; }
    }
}