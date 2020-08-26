using System;

namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface ISelectQuery : IStatuteQuery
    {
        public IOrderFieldQualifier OrderFieldQualifier { get; }
        public IOperationalQueryQualifier OperationalQueryQualifier { get; }
    }

    public interface ISelectQuery<TId, TModel> : ISelectQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
    }
}