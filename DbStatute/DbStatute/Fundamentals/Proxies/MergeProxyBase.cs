using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class MergeProxyBase : ProxyBase, IMergeProxyBase
    {
    }

    public abstract class MergeProxyBase<TModel> : ProxyBase<TModel>, IMergeProxyBase<TModel>
        where TModel : class, IModel, new()
    {
    }
}