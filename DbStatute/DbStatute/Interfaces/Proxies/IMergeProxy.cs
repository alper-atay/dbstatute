using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Interfaces.Proxies
{
    public interface IMergeProxy : IMergeProxyBase
    {
    }

    public interface IMergeProxy<TModel> : IMergeProxyBase<TModel>, IMergeProxy
        where TModel : class, IModel, new()
    {
    }
}