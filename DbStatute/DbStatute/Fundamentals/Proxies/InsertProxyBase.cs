using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class InsertProxyBase : ProxyBase, IInsertProxyBase
    {
        public IFieldQualifier InsertedFieldQualifier { get; }
    }

    public abstract class InsertProxyBase<TModel> : ProxyBase<TModel>, IInsertProxyBase<TModel>
        where TModel : class, IModel, new()
    {
        public IFieldQualifier<TModel> InsertedFieldQualifier { get; }

        IFieldQualifier IInsertProxyBase.InsertedFieldQualifier => InsertedFieldQualifier;
    }
}