using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class SelectProxyBase : ProxyBase, ISelectProxyBase
    {
    }

    public abstract class SelectProxyBase<TModel> : ProxyBase<TModel>, ISelectProxyBase<TModel>
        where TModel : class, IModel, new()
    {
    }
}