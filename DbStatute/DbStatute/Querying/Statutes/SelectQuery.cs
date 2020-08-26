using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Statutes;
using System;

namespace DbStatute.Querying.Statutes
{
    public class SelectQuery : StatuteQuery, ISelectQuery
    {
        public SelectQuery()
        {
            OperationalQueryQualifier = new OperationalQueryQualifier(default);
        }

        public SelectQuery(IOperationalQueryQualifier operationalQueryQualifier, IOrderFieldQualifier orderFieldQualifier)
        {
            OperationalQueryQualifier = operationalQueryQualifier ?? throw new ArgumentNullException(nameof(operationalQueryQualifier));
            OrderFieldQualifier = orderFieldQualifier ?? throw new ArgumentNullException(nameof(orderFieldQualifier));
        }

        public IOperationalQueryQualifier OperationalQueryQualifier { get; }
        public IOrderFieldQualifier OrderFieldQualifier { get; }
    }

    public class SelectQuery<TModel> : StatuteQuery<TModel>, ISelectQuery<TModel>
        where TModel : class, IModel, new()
    {
        public SelectQuery()
        {
            OperationalQueryQualifier = new OperationalQueryQualifier<TModel>();
            OrderFieldQualifier = new OrderFieldQualifier<TModel>();
        }

        public SelectQuery(IOperationalQueryQualifier<TModel> operationalQueryQualifier, IOrderFieldQualifier<TModel> orderFieldQualifier)
        {
            OperationalQueryQualifier = operationalQueryQualifier ?? throw new ArgumentNullException(nameof(operationalQueryQualifier));
            OrderFieldQualifier = orderFieldQualifier ?? throw new ArgumentNullException(nameof(orderFieldQualifier));
        }

        public IOperationalQueryQualifier<TModel> OperationalQueryQualifier { get; }
        IOperationalQueryQualifier ISelectQuery.OperationalQueryQualifier => OperationalQueryQualifier;
        public IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
        IOrderFieldQualifier ISelectQuery.OrderFieldQualifier => OrderFieldQualifier;
    }
}