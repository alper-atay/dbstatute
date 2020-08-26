using System;

namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface IStatuteQuery
    {
    }

    public interface IStatuteQuery<TId, TModel> : IStatuteQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
    }
}