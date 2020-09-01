using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Querying.Builders;
using DbStatute.Querying.Qualifiers.Fields;

namespace DbStatute.Querying
{
    public class SelectProxy : StatuteProxyBase, ISelectProxy
    {
        public SelectProxy(ISelectQueryGroupBuilder selectQueryGroupBuilder, IOrderFieldQualifier orderFieldQualifier, IFieldQualifier selectedFieldQualifier)
        {
            SelectQueryGroupBuilder = selectQueryGroupBuilder ?? throw new System.ArgumentNullException(nameof(selectQueryGroupBuilder));
            OrderFieldQualifier = orderFieldQualifier ?? throw new System.ArgumentNullException(nameof(orderFieldQualifier));
            SelectedFieldQualifier = selectedFieldQualifier ?? throw new System.ArgumentNullException(nameof(selectedFieldQualifier));
        }

        public IOrderFieldQualifier OrderFieldQualifier { get; }
        public IFieldQualifier SelectedFieldQualifier { get; }
        public ISelectQueryGroupBuilder SelectQueryGroupBuilder { get; }
    }

    public class SelectProxy<TModel> : StatuteProxyBase<TModel>, ISelectProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SelectProxy()
        {
            SelectQueryGroupBuilder = new SelectQueryGroupBuilder<TModel>();
            OrderFieldQualifier = new OrderFieldQualifier<TModel>();
            SelectedFieldQualifier = new FieldQualifier<TModel>();
        }

        public SelectProxy(ISelectQueryGroupBuilder<TModel> selectQueryGroupBuilder, IOrderFieldQualifier<TModel> orderFieldQualifier, IFieldQualifier<TModel> selectedFieldQualifier)
        {
            SelectQueryGroupBuilder = selectQueryGroupBuilder ?? throw new System.ArgumentNullException(nameof(selectQueryGroupBuilder));
            OrderFieldQualifier = orderFieldQualifier ?? throw new System.ArgumentNullException(nameof(orderFieldQualifier));
            SelectedFieldQualifier = selectedFieldQualifier ?? throw new System.ArgumentNullException(nameof(selectedFieldQualifier));
        }

        public IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
        IOrderFieldQualifier ISelectProxy.OrderFieldQualifier => OrderFieldQualifier;
        public IFieldQualifier<TModel> SelectedFieldQualifier { get; }
        IFieldQualifier ISelectProxy.SelectedFieldQualifier => SelectedFieldQualifier;
        public ISelectQueryGroupBuilder<TModel> SelectQueryGroupBuilder { get; }
        ISelectQueryGroupBuilder ISelectProxy.SelectQueryGroupBuilder => SelectQueryGroupBuilder;
    }
}