using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Qualifiers;
using System;

namespace DbStatute.Queries
{
    public class OrderFieldQuery : IOrderFieldQuery
    {
        public OrderFieldQuery()
        {
            OrderFields = new OrderFieldQualifier();
        }

        public OrderFieldQuery(IOrderFieldQualifier orderFields)
        {
            OrderFields = orderFields ?? throw new ArgumentNullException(nameof(orderFields));
        }

        public IOrderFieldQualifier OrderFields { get; }
    }

    public class OrderFieldQuery<TModel> : IOrderFieldQuery<TModel>
        where TModel : class, IModel, new()
    {
        public OrderFieldQuery()
        {
            OrderFields = new OrderFieldQualifier<TModel>();
        }

        public OrderFieldQuery(IOrderFieldQualifier<TModel> orderFields)
        {
            OrderFields = orderFields ?? throw new ArgumentNullException(nameof(orderFields));
        }

        public IOrderFieldQualifier<TModel> OrderFields { get; }

        IOrderFieldQualifier IOrderFieldQuery.OrderFields => OrderFields;
    }
}