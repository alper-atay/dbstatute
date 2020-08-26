using System;

namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface IUpdateQuery : IStatuteQuery
    {
        IFieldQualifier FieldQualifier { get; }
    }

    public interface IUpdateQuery<TId, TModel> : IStatuteQuery<TId, TModel>, IUpdateQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        new IFieldQualifier<TId, TModel> FieldQualifier { get; }
    }
}