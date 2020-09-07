using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class DeleteProxyBase : ProxyBase, IDeleteProxyBase
    {
    }

    public abstract class DeleteProxyBase<TModel> : ProxyBase<TModel>, IDeleteProxyBase<TModel>
        where TModel : class, IModel, new()
    {
    }
}