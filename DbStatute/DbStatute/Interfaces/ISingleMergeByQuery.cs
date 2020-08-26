using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces
{
    public interface ISingleMergeByQuery<TMergeQuery> : ISingleMerge
        where TMergeQuery : IMergeQuery
    {
        TMergeQuery MergeQuery { get; }
    }

    public interface ISingleMergeByQuery<TModel, TMergeQuery> : ISingleMerge<TModel>, ISingleMergeByQuery<TMergeQuery>
        where TModel : class, IModel, new()
        where TMergeQuery : IMergerQuery<TModel>
    {
        new TMergeQuery MergeQuery { get; }
    }
}