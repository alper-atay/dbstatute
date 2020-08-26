using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Statutes;
using System;

namespace DbStatute.Querying.Statutes
{
    public class SelectQuery : 



    public class SelectQuery<TId, TModel> : StatuteQuery, ISelectQuery<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        public SelectQuery()
        {
            OperationalQueryQualifier = new OperationalQueryQualifier<TId, TModel>();
            OrderFieldQualifier = new OrderFieldQualifier<TId, TModel>();
        }

        public SelectQuery(IOperationalQueryQualifier<TId, TModel> operationalQueryQualifier, IOrderFieldQualifier<TId, TModel> orderFieldQualifier)
        {
            OperationalQueryQualifier = operationalQueryQualifier ?? throw new ArgumentNullException(nameof(operationalQueryQualifier));
            OrderFieldQualifier = orderFieldQualifier ?? throw new ArgumentNullException(nameof(orderFieldQualifier));
        }

        public IOperationalQueryQualifier<TId, TModel> OperationalQueryQualifier { get; }
        IOperationalQueryQualifier ISelectQuery.OperationalQueryQualifier => OperationalQueryQualifier;
        public IOrderFieldQualifier<TId, TModel> OrderFieldQualifier { get; }
        IOrderFieldQualifier ISelectQuery.OrderFieldQualifier => OrderFieldQualifier;
    }
}