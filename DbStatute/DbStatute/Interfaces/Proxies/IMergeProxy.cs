using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Querying.Builders;

namespace DbStatute.Interfaces.Proxies
{
    public interface IMergeProxy : IStatuteProxyBase
    {
    }

    public interface IMergeProxy<TModel> : IStatuteProxyBase<TModel>, IMergeProxy
        where TModel : class, IModel, new()
    {
    }
}