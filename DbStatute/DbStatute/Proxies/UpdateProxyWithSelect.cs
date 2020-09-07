using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using System;

namespace DbStatute.Proxies
{
    public class UpdateProxyWithSelect<TModel> : UpdateProxy<TModel>, IUpdateProxyWithSelect<TModel>
        where TModel : class, IModel, new()
    {
        public UpdateProxyWithSelect()
        {
            SelectProxy = new SelectProxy<TModel>();
        }

        public UpdateProxyWithSelect(ISelectProxy<TModel> selectProxy)
        {
            SelectProxy = selectProxy ?? throw new ArgumentNullException(nameof(selectProxy));
        }

        public UpdateProxyWithSelect(ISelectProxy<TModel> selectProxy, IFieldQualifier<TModel> updatedFieldQualifier, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier) : base(updatedFieldQualifier, valueFieldQualifier, predicateFieldQualifier)
        {
            SelectProxy = selectProxy ?? throw new ArgumentNullException(nameof(selectProxy));
        }

        public ISelectProxy<TModel> SelectProxy { get; }

        ISelectProxy IUpdateProxyWithSelect.SelectProxy => SelectProxy;
    }
}