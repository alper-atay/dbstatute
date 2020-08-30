using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Querying.Builders;
using DbStatute.Querying.Qualifiers.Fields;
using System;

namespace DbStatute.Querying
{
    public class SelectQuery : StatuteQueryBase, ISelectQuery
    {
        public SelectQuery()
        {
            SelectQueryGroupBuilder = new SelectQueryGroupBuilder();
            OrderFieldQualifier = new OrderFieldQualifier();
        }

        public SelectQuery(ISelectQueryGroupBuilder selectQueryGroupBuilder, IOrderFieldQualifier orderFieldQualifier)
        {
            SelectQueryGroupBuilder = selectQueryGroupBuilder ?? throw new ArgumentNullException(nameof(selectQueryGroupBuilder));
            OrderFieldQualifier = orderFieldQualifier ?? throw new ArgumentNullException(nameof(orderFieldQualifier));
        }

        public IOrderFieldQualifier OrderFieldQualifier { get; }
        public ISelectQueryGroupBuilder SelectQueryGroupBuilder { get; }
    }

    public class SelectQuery<TModel> : StatuteQueryBase<TModel>, ISelectQuery<TModel>
        where TModel : class, IModel, new()
    {
        public SelectQuery()
        {
            SelectQueryGroupBuilder = new SelectQueryGroupBuilder<TModel>();
            OrderFieldQualifier = new OrderFieldQualifier<TModel>();
        }

        public SelectQuery(ISelectQueryGroupBuilder<TModel> selectQueryGroupBuilder, IOrderFieldQualifier<TModel> orderFieldQualifier)
        {
            SelectQueryGroupBuilder = selectQueryGroupBuilder ?? throw new ArgumentNullException(nameof(selectQueryGroupBuilder));
            OrderFieldQualifier = orderFieldQualifier ?? throw new ArgumentNullException(nameof(orderFieldQualifier));
        }

        public IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }

        IOrderFieldQualifier ISelectQuery.OrderFieldQualifier => OrderFieldQualifier;
        public ISelectQueryGroupBuilder<TModel> SelectQueryGroupBuilder { get; }
        ISelectQueryGroupBuilder ISelectQuery.SelectQueryGroupBuilder => SelectQueryGroupBuilder;
    }
}