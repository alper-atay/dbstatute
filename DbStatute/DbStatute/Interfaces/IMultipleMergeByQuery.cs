using DbStatute.Interfaces.Querying.Statutes;
using System;

namespace DbStatute.Interfaces
{
    public interface IMultipleMergeByQuery<TId, TModel, TMergeQuery> : IMerge
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TMergeQuery : IMergerQuery<TId, TModel>
    {
    }
}