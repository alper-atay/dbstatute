using System;

namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface ISelectQuery : IStatuteQuery
    {
        public IOperationalQueryQualifier OperationalQueryQualifier { get; }
        public IOrderFieldQualifier OrderFieldQualifier { get; }
    }

    public interface ISelectQuery<TId, TModel> : IStatuteQuery<TId, TModel>, ISelectQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        new IOperationalQueryQualifier<TId, TModel> OperationalQueryQualifier { get; }
        new IOrderFieldQualifier<TId, TModel> OrderFieldQualifier { get; }
    }
}