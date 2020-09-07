using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using System;

namespace DbStatute.Proxies
{
    public class UpdateProxyWithSelect : UpdateProxyBase, IUpdateProxyWithSelect
    {
        public UpdateProxyWithSelect()
        {
            SelectProxy = new SelectProxy();
        }

        public UpdateProxyWithSelect(ISelectProxy selectProxy)
        {
            SelectProxy = selectProxy ?? throw new ArgumentNullException(nameof(selectProxy));
        }

        public ISelectProxy SelectProxy { get; }
    }

    public class UpdateProxyWithSelect<TModel> : UpdateProxyBase<TModel>, IUpdateProxyWithSelect<TModel>
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

        public ISelectProxy<TModel> SelectProxy { get; }

        ISelectProxy IUpdateProxyWithSelect.SelectProxy => SelectProxy;
    }
}