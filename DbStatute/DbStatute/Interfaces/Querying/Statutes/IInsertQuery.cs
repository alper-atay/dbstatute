using System;

namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface IInsertQuery : IStatuteQuery
    {
        IFieldQualifier FieldQualifier { get; }
    }

    public interface IInsertQuery<TId, TModel> : IStatuteQuery<TId, TModel>, IInsertQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        new IFieldQualifier<TId, TModel> FieldQualifier { get; }
    }
}