using System;

namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface IDeleteQuery : IStatuteQuery
    {
    }

    public interface IDeleteQuery<TId, TModel> : IDeleteQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
    }
}