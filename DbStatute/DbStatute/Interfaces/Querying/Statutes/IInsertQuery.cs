using System;

namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface IInsertQuery : IStatuteQuery
    {
    }

    public interface IInsertQuery<TId, TModel> : IInsertQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        TModel InserterModel { get; }
    }
}