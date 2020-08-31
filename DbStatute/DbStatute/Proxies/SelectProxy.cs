using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Querying
{
    public class SelectProxy : StatuteProxyBase, ISelectProxy
    {
        public IFieldQualifier SelectedFieldQualifier { get; }
        public bool SelectedFieldQualifierEnabled { get; set; }
        public IOrderFieldQualifier OrderFieldQualifier { get; }
        public bool OrderFieldQualifierEnabled { get; set; }
    }

    public class SelectProxy<TModel> : StatuteProxyBase<TModel>, ISelectProxy<TModel>
        where TModel : class, IModel, new()
    {
        public IFieldQualifier<TModel> SelectedFieldQualifier { get; }
        IFieldQualifier ISelectProxy.SelectedFieldQualifier => SelectedFieldQualifier;
        public bool SelectedFieldQualifierEnabled { get; set; }
        public IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
        IOrderFieldQualifier ISelectProxy.OrderFieldQualifier => OrderFieldQualifier;
        public bool OrderFieldQualifierEnabled { get; set; }
    }
}