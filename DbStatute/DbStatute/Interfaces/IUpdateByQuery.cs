using DbStatute.Interfaces.Querying;
using System;

namespace DbStatute.Interfaces
{
    public interface IUpdateByQuery<TId, TModel, TUpdateQuery> : IUpdate
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : IUpdateQuery<TId, TModel>
    {
        TUpdateQuery UpdateQuery { get; }
    }
}