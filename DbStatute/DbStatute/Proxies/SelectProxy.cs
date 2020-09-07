using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Qualifiers;
using DbStatute.Queries;
using System;

namespace DbStatute.Proxies
{
    public class SelectProxy : SelectProxyBase, ISelectProxy
    {
        public SelectProxy()
        {
            WhereQuery = new WhereQuery();
            OrderFieldQualifier = new OrderFieldQualifier();
            SelectedFieldQualifier = new FieldQualifier();
        }

        public SelectProxy(IWhereQuery whereQuery, IOrderFieldQualifier orderFieldQualifier, IFieldQualifier selectedFieldQualifier)
        {
            WhereQuery = whereQuery ?? throw new ArgumentNullException(nameof(whereQuery));
            OrderFieldQualifier = orderFieldQualifier ?? throw new ArgumentNullException(nameof(orderFieldQualifier));
            SelectedFieldQualifier = selectedFieldQualifier ?? throw new ArgumentNullException(nameof(selectedFieldQualifier));
        }

        public IOrderFieldQualifier OrderFieldQualifier { get; }

        public IFieldQualifier SelectedFieldQualifier { get; }

        public IWhereQuery WhereQuery { get; }
    }

    public class SelectProxy<TModel> : SelectProxyBase<TModel>, ISelectProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SelectProxy()
        {
            WhereQuery = new WhereQuery<TModel>();
            OrderFieldQualifier = new OrderFieldQualifier<TModel>();
            SelectedFieldQualifier = new FieldQualifier<TModel>();
        }

        public SelectProxy(IWhereQuery<TModel> whereQuery, IOrderFieldQualifier<TModel> orderFieldQualifier, IFieldQualifier<TModel> selectedFieldQualifier)
        {
            WhereQuery = whereQuery ?? throw new ArgumentNullException(nameof(whereQuery));
            OrderFieldQualifier = orderFieldQualifier ?? throw new ArgumentNullException(nameof(orderFieldQualifier));
            SelectedFieldQualifier = selectedFieldQualifier ?? throw new ArgumentNullException(nameof(selectedFieldQualifier));
        }

        public IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }

        IOrderFieldQualifier ISelectProxy.OrderFieldQualifier => OrderFieldQualifier;

        public IFieldQualifier<TModel> SelectedFieldQualifier { get; }

        IFieldQualifier ISelectProxy.SelectedFieldQualifier => SelectedFieldQualifier;

        public IWhereQuery<TModel> WhereQuery { get; }

        IWhereQuery ISelectProxy.WhereQuery => WhereQuery;
    }
}