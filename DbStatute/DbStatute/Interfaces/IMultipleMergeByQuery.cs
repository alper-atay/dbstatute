using DbStatute.Interfaces.Querying.Statutes;

namespace DbStatute.Interfaces
{
    public interface IMultipleMergeByQuery<TModel, TMergeQuery> : IMerge
        where TModel : class, IModel, new()
        where TMergeQuery : IMergerQuery<TModel>
    {
    }
}