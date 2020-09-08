using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Qualifiers;
using System;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class UpdateProxyBase : ProxyBase, IUpdateProxyBase
    {
        protected UpdateProxyBase()
        {
            UpdatedFieldQualifier = new FieldQualifier();
        }

        protected UpdateProxyBase(IFieldQualifier updatedFieldQualifier)
        {
            UpdatedFieldQualifier = updatedFieldQualifier ?? throw new ArgumentNullException(nameof(updatedFieldQualifier));
        }

        public IFieldQualifier UpdatedFieldQualifier { get; }
    }

    public abstract class UpdateProxyBase<TModel> : ProxyBase<TModel>, IUpdateProxyBase<TModel>
        where TModel : class, IModel, new()
    {
        public UpdateProxyBase()
        {
            UpdatedFieldQualifier = new FieldQualifier<TModel>();
        }

        public UpdateProxyBase(IFieldQualifier<TModel> updatedFieldQualifier)
        {
            UpdatedFieldQualifier = updatedFieldQualifier ?? throw new ArgumentNullException(nameof(updatedFieldQualifier));
        }

        public IFieldQualifier<TModel> UpdatedFieldQualifier { get; }

        IFieldQualifier IUpdateProxyBase.UpdatedFieldQualifier => UpdatedFieldQualifier;
    }
}