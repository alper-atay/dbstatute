using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Proxies
{
    public class MergeProxy : MergeProxyBase, IMergeProxy
    {
    }

    public class MergeProxy<TModel> : MergeProxyBase<TModel>, IMergeProxy<TModel>
        where TModel : class, IModel, new()
    {
    }
}