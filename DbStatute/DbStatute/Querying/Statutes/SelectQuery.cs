using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Statutes;
using System;

namespace DbStatute.Querying.Statutes
{
    public class SelectQuery<TId, TModel> : StatuteQuery, ISelectQuery<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        public IOperationalQueryQualifier OperationalQueryQualifier { get; }
        public IOrderFieldQualifier OrderFieldQualifier { get; }
    }
}