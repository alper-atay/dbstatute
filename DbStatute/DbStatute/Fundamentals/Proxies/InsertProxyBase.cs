using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class InsertProxyBase : ProxyBase, IInsertProxyBase
    {
    }

    public abstract class InsertProxyBase<TModel> : ProxyBase<TModel>, IInsertProxyBase<TModel>
        where TModel : class, IModel, new()
    {
    }
}