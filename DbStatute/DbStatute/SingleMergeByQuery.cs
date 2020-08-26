using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Statutes;
using System;

namespace DbStatute
{
    public abstract class SingleMergeByQuery<TId, TModel, TMergeQuery> : SingleMerge<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TMergeQuery : IMergeQuery
    {
        protected SingleMergeByQuery(TModel rawModel) : base(rawModel)
        {
        }
    }
}