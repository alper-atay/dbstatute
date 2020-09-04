using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;
using DbStatute.Qualifiers;
using DbStatute.Qualifiers.Groups;
using System;

namespace DbStatute.Proxies
{
    public class SelectProxy : ProxyBase, ISelectProxy
    {
        public SelectProxy()
        {
            SelectQualifierGroup = new SelectQualifierGroup();
            OrderFieldQualifier = new OrderFieldQualifier();
            SelectedFieldQualifier = new FieldQualifier();
        }

        public SelectProxy(ISelectQualifierGroup selectQualifierGroup, IOrderFieldQualifier orderFieldQualifier, IFieldQualifier selectedFieldQualifier)
        {
            SelectQualifierGroup = selectQualifierGroup ?? throw new ArgumentNullException(nameof(selectQualifierGroup));
            OrderFieldQualifier = orderFieldQualifier ?? throw new ArgumentNullException(nameof(orderFieldQualifier));
            SelectedFieldQualifier = selectedFieldQualifier ?? throw new ArgumentNullException(nameof(selectedFieldQualifier));
        }

        public IOrderFieldQualifier OrderFieldQualifier { get; }

        public IFieldQualifier SelectedFieldQualifier { get; }

        public ISelectQualifierGroup SelectQualifierGroup { get; }
    }

    public class SelectProxy<TModel> : ProxyBase<TModel>, ISelectProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SelectProxy()
        {
            SelectQualifierGroup = new SelectQualifierGroup<TModel>();
            OrderFieldQualifier = new OrderFieldQualifier<TModel>();
            SelectedFieldQualifier = new FieldQualifier<TModel>();
        }

        public SelectProxy(ISelectQualifierGroup<TModel> selectQualifierGroup, IOrderFieldQualifier<TModel> orderFieldQualifier, IFieldQualifier<TModel> selectedFieldQualifier)
        {
            SelectQualifierGroup = selectQualifierGroup ?? throw new ArgumentNullException(nameof(selectQualifierGroup));
            OrderFieldQualifier = orderFieldQualifier ?? throw new ArgumentNullException(nameof(orderFieldQualifier));
            SelectedFieldQualifier = selectedFieldQualifier ?? throw new ArgumentNullException(nameof(selectedFieldQualifier));
        }

        public IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }

        IOrderFieldQualifier ISelectProxy.OrderFieldQualifier => OrderFieldQualifier;

        public IFieldQualifier<TModel> SelectedFieldQualifier { get; }

        IFieldQualifier ISelectProxy.SelectedFieldQualifier => SelectedFieldQualifier;

        public ISelectQualifierGroup<TModel> SelectQualifierGroup { get; }

        ISelectQualifierGroup ISelectProxy.SelectQualifierGroup => SelectQualifierGroup;
    }
}