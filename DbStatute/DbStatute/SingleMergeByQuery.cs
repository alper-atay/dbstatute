using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Statutes;

namespace DbStatute
{
    public abstract class SingleMergeByQuery<TModel, TMergeQuery> : SingleMerge<TModel>

        where TModel : class, IModel, new()
        where TMergeQuery : IMergeQuery
    {
        protected SingleMergeByQuery(TModel rawModel) : base(rawModel)
        {
        }
    }
}