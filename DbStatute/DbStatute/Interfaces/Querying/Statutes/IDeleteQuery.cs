using System;

namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface IDeleteQuery : IStatuteQuery
    {
        ISelectQuery SelectQuery { get; }
    }

    public interface IDeleteQuery<TId, TModel> : IStatuteQuery<TId, TModel>, IDeleteQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        new ISelectQuery<TId, TModel> SelectQuery { get; }
    }
}