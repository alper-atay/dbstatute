using DbStatute.Interfaces.Querying;
using System;

namespace DbStatute.Interfaces
{
    public interface IMergeByQuery<TId, TModel, TUpdateQuery> : IMerge
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : IUpdateQuery<TId, TModel>
    {
    }
}