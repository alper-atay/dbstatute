using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Proxies
{
    public class DeleteProxy : StatuteProxyBase, IDeleteProxy
    {
        public DeleteProxy(ISelectProxy selectProxy)
        {
            SelectProxy = selectProxy;
        }

        public ISelectProxy SelectProxy { get; }
    }

    public class DeleteProxy<TModel> : StatuteProxyBase<TModel>, IDeleteProxy<TModel>
        where TModel : class, IModel, new()
    {
        public DeleteProxy()
        {
            SelectProxy = new SelectProxy<TModel>();
        }

        public DeleteProxy(ISelectProxy<TModel> selectProxy)
        {
            SelectProxy = selectProxy;
        }

        public ISelectProxy<TModel> SelectProxy { get; }

        ISelectProxy IDeleteProxy.SelectProxy => SelectProxy;
    }
}