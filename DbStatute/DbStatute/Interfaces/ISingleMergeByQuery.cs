using DbStatute.Interfaces.Querying;
using System;

namespace DbStatute.Interfaces
{
    public interface ISingleMergeByQuery<TId, TModel, TUpdateQuery> : ISingleMerge<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : IUpdateQuery
    {
        TUpdateQuery UpdateQuery { get; }
    }
}