using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Fundamentals.Proxies
{
    public  abstract class UpdateProxyBase : ProxyBase, IUpdateProxyBase
    {
    }

    public abstract class UpdateProxyBase<TModel> : ProxyBase<TModel>, IUpdateProxyBase<TModel>
        where TModel : class, IModel, new()
    {
    }
}