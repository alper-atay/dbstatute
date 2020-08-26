using System;

namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface IMergeQuery : IStatuteQuery
    {
    }

    public interface IMergerQuery<TId, TModel> : IStatuteQuery<TId, TModel>, IMergeQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
    }
}